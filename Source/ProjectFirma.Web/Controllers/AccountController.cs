/*-----------------------------------------------------------------------
<copyright file="AccountController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using LtInfo.Common;
using LtInfo.Common.Email;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.Account;
using Saml;

namespace ProjectFirma.Web.Controllers
{
    public class AccountController : FirmaBaseController
    {
        protected override bool IsCurrentUserAnonymous()
        {
            return HttpRequestStorage.Person.IsAnonymousUser;
        }

        protected override string LoginUrl
        {
            get
            {
                return SitkaRoute<AccountController>.BuildAbsoluteUrlHttpsFromExpression(c => c.Login(null));
            }
        }

        protected override ISitkaDbContext SitkaDbContext => HttpRequestStorage.DatabaseEntities;

        protected string HomeUrl
        {
            get { return SitkaRoute<HomeController>.BuildUrlFromExpression(c => c.Index()); }
        }


        [AnonymousUnclassifiedFeature]
        public ContentResult NotAuthorized()
        {
            return Content("Not Authorized");
        }

        [AnonymousUnclassifiedFeature]
        public ActionResult Login(string returnUrl)
        {
            var sawLoginUrl = FirmaWebConfiguration.SAWEndPoint;
            var adfsLoginUrl = FirmaWebConfiguration.ADFSEndPoint;
            var viewData = new LoginViewData(CurrentPerson, sawLoginUrl, adfsLoginUrl);
            return RazorView<Login, LoginViewData>(viewData);
        }

        [AnonymousUnclassifiedFeature]
        [HttpPost]
        public ActionResult SAWPost(string returnUrl)
        {
            var samlResponse = new Response(CertificateHelpers.GetX509Certificate2FromStore(FirmaWebConfiguration.Saml2IDPCertificateThumbPrint));
            samlResponse.LoadXmlFromBase64(Request.Form["SAMLResponse"]); //SAML providers usually POST the data into this var

            if (samlResponse.IsValid())
            {
                var username = samlResponse.GetNameID();
                var fullName = samlResponse.GetName();
                var email = samlResponse.GetEmail();
                var userName = samlResponse.GetUserName();

                IdentitySignin(username, fullName, email, userName, null, AuthenticationMethod.SAW);
            }
            return new RedirectResult(HomeUrl);
        }

        [AnonymousUnclassifiedFeature]
        [HttpPost]
        public ActionResult ADFSPost(string returnUrl)
        {
            var samlResponse = new ADFSSamlResponse();
            samlResponse.LoadXmlFromBase64(Request.Form["SAMLResponse"]); //SAML providers usually POST the data into this var

            samlResponse.Decrypt();
            var firstName = samlResponse.GetFirstName();
            var lastName = samlResponse.GetLastName();
            var email = samlResponse.GetEmail();
            var upn = samlResponse.GetUPN();
            var groups = samlResponse.GetRoleGroups();
            IdentitySignin(upn, firstName + " " + lastName, email, upn, groups, AuthenticationMethod.ADFS);
            return new RedirectResult(HomeUrl);
        }

        private void IdentitySignin(string userId, string name, string email, string userName, string groups,
            AuthenticationMethod authenticationMethod, string providerKey = null, bool isPersistent = false)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.WindowsAccountName, userName)
            };
            if (!string.IsNullOrWhiteSpace(email))
            {
                claims.Add(new Claim(ClaimTypes.Email, email));
            }

            if (!string.IsNullOrWhiteSpace(groups))
            {
                claims.Add(new Claim(ClaimTypes.Role, groups));
            }

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            // add to user here!
            HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = isPersistent,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);

            SyncLocalAccountStore(identity, authenticationMethod);
        }

        public void IdentitySignout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalCookie);
        }

        [AnonymousUnclassifiedFeature]
        public ActionResult LogOff()
        {
            IdentitySignout();
            var returnUrl = !string.IsNullOrWhiteSpace(Request["returnUrl"]) ? Request["returnUrl"] : HomeUrl;
            return Redirect(returnUrl);
        }

        private static Person SyncLocalAccountStore(IIdentity userIdentity, AuthenticationMethod authenticationMethod)
        {
            SitkaHttpApplication.Logger.DebugFormat("In SyncLocalAccountStore - User '{0}', Authenticated = '{1}'", userIdentity.Name, userIdentity.IsAuthenticated);
            var saml2UserClaims = Saml2ClaimsHelpers.ParseOpenIDClaims(userIdentity);
            var personUniqueIdentifier = saml2UserClaims.UniqueIdentifier;
            var names = saml2UserClaims.DisplayName.Split(' ');
            var firstName = "";
            var lastName = "";
            if (names.Length == 2)
            {
                firstName = names[0];
                lastName = names[1];
            }
            else
            {
                firstName = saml2UserClaims.DisplayName;
            }
            var email = saml2UserClaims.Email;
            var username = saml2UserClaims.Username;

            var sendNewUserNotification = false;
            var person = FirmaWebConfiguration.SAWOverrideLookupUsingEmail ? HttpRequestStorage.DatabaseEntities.People.GetPersonByEmail(email, false) : HttpRequestStorage.DatabaseEntities.People.GetPersonByPersonUniqueIdentifier(personUniqueIdentifier);

            if (person == null)
            {
                // new user - provision with limited role
                SitkaHttpApplication.Logger.DebugFormat("In SyncLocalAccountStore - creating local profile for User '{0}'", personUniqueIdentifier);
                var unknownOrganization = HttpRequestStorage.DatabaseEntities.Organizations.GetUnknownOrganization();
                person = new Person(firstName, lastName, Role.Unassigned.RoleID, DateTime.Now, true, false)
                {
                    Email = email,
                    LoginName = username,
                    OrganizationID = unknownOrganization.OrganizationID
                };
                HttpRequestStorage.DatabaseEntities.People.Add(person);

                // It should be relatively safe to create credentials like this, regardless of environment, since all users start out with minimal roles.
                var personEnvironmentCredential = new PersonEnvironmentCredential(person, Saml2ClaimsHelpers.GetDeploymentEnvironment(), Saml2ClaimsHelpers.GetAuthenticator(personUniqueIdentifier), personUniqueIdentifier);
                HttpRequestStorage.DatabaseEntities.PersonEnvironmentCredentials.Add(personEnvironmentCredential);

                sendNewUserNotification = true;
            }
            else
            {
                // existing user - sync values
                SitkaHttpApplication.Logger.DebugFormat("In SyncLocalAccountStore - syncing local profile for User Email '{1}' '{0}'", personUniqueIdentifier, email);
            }

            person.FirstName = firstName;
            person.LastName = lastName;
            person.Email = email;
            person.LoginName = username;
            person.UpdateDate = DateTime.Now;

            if (authenticationMethod == AuthenticationMethod.ADFS)
            {
                var roleGroups = saml2UserClaims.RoleGroups;
                if (roleGroups.Any())
                {
                    person.RoleID = MapRoleFromClaims(roleGroups).RoleID;
                }

                person.OrganizationID = OrganizationModelExtensions.WadnrID;
            }
            HttpRequestStorage.Person = person;
            HttpRequestStorage.DatabaseEntities.SaveChanges(person);

            if (sendNewUserNotification)
            {
                SendNewUserCreatedMessage(person, username);
            }
            return HttpRequestStorage.Person;
        }

        private static Role MapRoleFromClaims(List<string> roleGroups)
        {
            if (roleGroups.Any(x => x.Contains(@"G-S-DNR_WF_ForestHealth_Admin")))
            {
                return Role.Admin;
            }

            if (roleGroups.Any(x => x.Contains(@"G-S-DNR_WF_ForestHealth_Review")))
            {
                return Role.ProjectSteward;
            }

            if (roleGroups.Any(x => x.Contains(@"G-S-DNR_WF_ForestHealth_Users")))
            {
                return Role.Normal;
            }

            return Role.Unassigned;
        }


        private static void SendNewUserCreatedMessage(Person person, string loginName)
        {
            var subject = $"User added: {person.FullNameFirstLast}";
            var message = $@"
<div style='font-size: 12px; font-family: Arial'>
    <strong>WA DNR Forest Health Tracker User added:</strong> {person.FullNameFirstLast}<br />
    <strong>Added on:</strong> {DateTime.Now}<br />
    <strong>Email:</strong> {person.Email}<br />
    <strong>Phone:</strong> {person.Phone.ToPhoneNumberString()}<br />
    <br />
    <p>
        You may want to <a href=""{
                    SitkaRoute<UserController>.BuildAbsoluteUrlFromExpression(x => x.Detail(person.PersonID))
                }"">assign this user roles</a> to allow them to work with specific areas of the site. Or you can leave the user with an unassigned role if they don't need special privileges.
    </p>
    <br />
    <br />
    <div style='font-size: 10px; color: gray'>
    OTHER DETAILS:<br />
    LOGIN: {loginName}<br />
    <br />
    </div>
    <div>You received this email because you are set up as a point of contact for support - if that's not correct, let us know: {
                    FirmaWebConfiguration.SitkaSupportEmail
                }.</div>
</div>
";

            var mailMessage = new MailMessage { From = new MailAddress(FirmaWebConfiguration.DoNotReplyEmail), Subject = subject, Body = message, IsBodyHtml = true };
            mailMessage.To.Add(FirmaWebConfiguration.SitkaSupportEmail);

            // Reply-To Header
            mailMessage.ReplyToList.Add(person.Email ?? FirmaWebConfiguration.SitkaSupportEmail);

            // TO field
            var supportPersons = HttpRequestStorage.DatabaseEntities.People.GetPeopleWhoReceiveSupportEmails();
            foreach (var supportPerson in supportPersons)
            {
                mailMessage.To.Add(supportPerson.Email);
            }

            SitkaSmtpClient.Send(mailMessage);
        }

        private enum AuthenticationMethod
        {
            ADFS,
            SAW
        }
    }
}
