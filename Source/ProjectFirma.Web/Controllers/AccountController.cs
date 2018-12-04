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
using System.IO;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using LtInfo.Common;
using LtInfo.Common.Email;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security.Shared;
using Saml;

namespace ProjectFirma.Web.Controllers
{
    public class AccountController : SitkaController
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
            // Request a redirect to the external login provider
            return new ChallengeResult("wadnr", Url.Action("LogOn", "Account", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LogOn(string returnUrl)
        {
            var samlResponse = new Response(new FileInfo(HostingEnvironment.MapPath("~/saw_test_key.txt")));
            samlResponse.LoadXmlFromBase64(Request.Form["SAMLResponse"]); //SAML providers usually POST the data into this var

            if (samlResponse.IsValid())
            {
                var username = samlResponse.GetNameID();
                var fullName = samlResponse.GetName();
                var email = samlResponse.GetEmail();
                var userName = samlResponse.GetUserName();

                IdentitySignin(username, fullName, email, userName);
            }
            return new RedirectResult(HomeUrl);
        }

        public void IdentitySignin(string userId, string name, string email, string userName, string providerKey = null, bool isPersistent = false)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.WindowsAccountName, userName)
            };

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            // add to user here!
            HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = isPersistent,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);

            SyncLocalAccountStore(identity);
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

        public static Person SyncLocalAccountStore(IIdentity userIdentity)
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
            var person = HttpRequestStorage.DatabaseEntities.People.GetPersonByPersonUniqueIdentifier(personUniqueIdentifier);

            if (person == null)
            {
                // we need to do one more check; check if the email exists
                person = HttpRequestStorage.DatabaseEntities.People.GetPersonByEmail(email, false);

                if (person == null)
                {
                    // new user - provision with limited role
                    SitkaHttpApplication.Logger.DebugFormat(
                        "In SyncLocalAccountStore - creating local profile for User '{0}'",
                        personUniqueIdentifier);
                    var unknownOrganization =
                        HttpRequestStorage.DatabaseEntities.Organizations.GetUnknownOrganization();
                    person = new Person(personUniqueIdentifier,
                        firstName,
                        lastName,
                        email,
                        Role.Unassigned.RoleID,
                        DateTime.Now,
                        true,
                        unknownOrganization.OrganizationID,
                        false,
                        username);
                    HttpRequestStorage.DatabaseEntities.AllPeople.Add(person);
                    sendNewUserNotification = true;
                }
                else
                {
                    person.PersonUniqueIdentifier = personUniqueIdentifier;
                    // existing user - sync values
                    SitkaHttpApplication.Logger.DebugFormat(
                        "In SyncLocalAccountStore - syncing local profile for User '{0}'", personUniqueIdentifier);
                }
            }
            else
            {
                // existing user - sync values
                SitkaHttpApplication.Logger.DebugFormat(
                    "In SyncLocalAccountStore - syncing local profile for User '{0}'", personUniqueIdentifier);
            }

            person.FirstName = firstName;
            person.LastName = lastName;
            person.Email = email;
            person.LoginName = username;
            person.UpdateDate = DateTime.Now;
            HttpRequestStorage.Person = person;
            HttpRequestStorage.DatabaseEntities.SaveChanges(person);

            if (sendNewUserNotification)
            {
                SendNewUserCreatedMessage(person, username);
            }
            return HttpRequestStorage.Person;
        }


        private static void SendNewUserCreatedMessage(Person person, string loginName)
        {
            var subject = $"User added: {person.FullNameFirstLast}";
            var message = $@"
<div style='font-size: 12px; font-family: Arial'>
    <strong>Project Firma User added:</strong> {person.FullNameFirstLast}<br />
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
            mailMessage.ReplyToList.Add(person.Email);

            // TO field
            var supportPersons = HttpRequestStorage.DatabaseEntities.People.GetPeopleWhoReceiveSupportEmails();
            foreach (var supportPerson in supportPersons)
            {
                mailMessage.To.Add(supportPerson.Email);
            }

            SitkaSmtpClient.Send(mailMessage);
        }
    }

    public class ChallengeResult : HttpUnauthorizedResult
    {
        public ChallengeResult(string provider, string redirectUri)
        {
            LoginProvider = provider;
            RedirectUri = redirectUri;
        }

        public string LoginProvider { get; set; }
        public string RedirectUri { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        }
    }

}
