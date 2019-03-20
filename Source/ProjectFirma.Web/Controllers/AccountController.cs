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
using System.Web;
using System.Web.Mvc;
using LtInfo.Common;
using LtInfo.Common.Email;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.Account;

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
        // ReSharper disable once InconsistentNaming
        public ActionResult SAWPost(string returnUrl)
        {
            var samlResponse = new SawSamlResponse(CertificateHelpers.GetX509Certificate2FromStore(FirmaWebConfiguration.Saml2IDPCertificateThumbPrint));
            samlResponse.LoadXmlFromBase64(Request.Form["SAMLResponse"]); //SAML providers usually POST the data into this var
            if (samlResponse.IsValid())
            {
                var sawAuthenticator = DetermineWhichSawAuthenticator(samlResponse);
                var userName = samlResponse.GetUserName();
                var fullName = samlResponse.GetName();
                var email = samlResponse.GetEmail();
                IdentitySignIn(userName, fullName, email, null, sawAuthenticator);
            }
            return new RedirectResult(HomeUrl);
        }

        private static Authenticator DetermineWhichSawAuthenticator(SawSamlResponse samlResponse)
        {
            // ReSharper disable once StringLiteralTypo
            if (samlResponse.GetIssuer()
                .Contains("test-secureaccess.wa.gov", StringComparison.InvariantCultureIgnoreCase))
            {
                return Authenticator.SAWTEST;
            }
            return Authenticator.SAWPROD;
        }

        [AnonymousUnclassifiedFeature]
        [HttpPost]
        // ReSharper disable once InconsistentNaming
        public ActionResult ADFSPost(string returnUrl)
        {
            var adfsSamlResponse = new ADFSSamlResponse();
            adfsSamlResponse.LoadXmlFromBase64(Request.Form["SAMLResponse"]); //SAML providers usually POST the data into this var
            adfsSamlResponse.Decrypt();

            var username = adfsSamlResponse.GetUpn();
            var firstName = adfsSamlResponse.GetFirstName();
            var lastName = adfsSamlResponse.GetLastName();
            var email = adfsSamlResponse.GetEmail();
            var groups = adfsSamlResponse.GetRoleGroups();

            IdentitySignIn(username, firstName + " " + lastName, email, groups, Authenticator.ADFS);
            return new RedirectResult(HomeUrl);
        }

        private void IdentitySignIn(string userName, string name, string email, string groups, Authenticator authenticator)
        {
            SitkaHttpApplication.Logger.Info($"Logon (IdentitySignIn) - AuthMethod {authenticator.AuthenticatorName} userName: {userName} name: {name} email: {email} providerKey: {(string) null} isPersistent: {false}");

            var names = name.Split(' ');
            string firstName;
            var lastName = "";
            if (names.Length == 2)
            {
                firstName = names[0];
                lastName = names[1];
            }
            else
            {
                firstName = name;
            }

            var roleGroups = !string.IsNullOrWhiteSpace(groups) ? groups.Split(',').ToList() : new List<string>();

            var person = LookupExistingPersonOrProvisionNewPerson(authenticator, userName, firstName, lastName, email, roleGroups);

            // add to user here!
            ClaimsIdentityHelper.IdentitySignIn(HttpContext.GetOwinContext().Authentication, person);
        }

        [AnonymousUnclassifiedFeature]
        public ActionResult LogOff()
        {
            SitkaHttpApplication.Logger.Debug($"Logoff - {CurrentPerson.FullNameFirstLast} ({CurrentPerson.Email})");
            ClaimsIdentityHelper.IdentitySignOut(HttpContext.GetOwinContext().Authentication);
            var returnUrl = !string.IsNullOrWhiteSpace(Request["returnUrl"]) ? Request["returnUrl"] : HomeUrl;
            return Redirect(returnUrl);
        }

        private static Person LookupExistingPersonOrProvisionNewPerson(Authenticator authenticator, string username,
            string firstName, string lastName, string email, List<string> groups)
        {
            var sendNewUserNotification = false;

            string userDetailsString =
                $"Username: {username} FirstName: {firstName} LastName: {lastName} Email: {email}";

            bool attemptingSawAuthentication = authenticator.ToEnum == AuthenticatorEnum.SAWTEST ||
                                               authenticator.ToEnum == AuthenticatorEnum.SAWPROD;

            // Always try to validate first using unique identifier, as it is arguably more secure
            var person = HttpRequestStorage.DatabaseEntities.People.GetPersonByPersonUniqueIdentifier(authenticator, username);

            string personLookupSuccess = person != null ? "Found" : "Did NOT find";
            SitkaHttpApplication.Logger.Debug(
                $"In {nameof(LookupExistingPersonOrProvisionNewPerson)} - {personLookupSuccess} by PersonUniqueIdentifier. [{userDetailsString}] ");

            // For SAW only, we allow ourselves to fall back to email.
            if (person == null && attemptingSawAuthentication)
            {
                SitkaHttpApplication.Logger.Debug(
                    $"In {nameof(LookupExistingPersonOrProvisionNewPerson)} - Falling back to SAW email authentication --  {userDetailsString}");
                person = HttpRequestStorage.DatabaseEntities.People.GetPersonByEmail(email);
            }

            // If there's no Person already that corresponds to the Person who is logging in,
            // we create Person and  PersonEnvironmentCredential records for them.
            if (person == null)
            {
                // new user - provision with limited role
                SitkaHttpApplication.Logger.Debug(
                    $"In {nameof(LookupExistingPersonOrProvisionNewPerson)} - Person not found using any available authentication method -- {userDetailsString}");
                SitkaHttpApplication.Logger.Debug($"In {nameof(LookupExistingPersonOrProvisionNewPerson)} - Creating new Person for -- {userDetailsString}");
                var unknownOrganization = HttpRequestStorage.DatabaseEntities.Organizations.GetUnknownOrganization();
                person = new Person(firstName, lastName, Role.Unassigned.RoleID, DateTime.Now, true, false)
                {
                    Email = email,
                    LoginName = username,
                    OrganizationID = unknownOrganization.OrganizationID
                };
                HttpRequestStorage.DatabaseEntities.People.Add(person);

                var authenticatorsToAllow = AuthenticatorHelper.GetAuthenticators(username);
                var personAllowedAuthenticators = authenticatorsToAllow.Select(x => new PersonAllowedAuthenticator(person, x));
                HttpRequestStorage.DatabaseEntities.PersonAllowedAuthenticators.AddRange(personAllowedAuthenticators);

                // It should be relatively safe to create credentials like this, regardless of environment, since all users start out with minimal roles.
                var personEnvironmentCredentialForCurrentEnvironment = new PersonEnvironmentCredential(person, authenticator, username);
                HttpRequestStorage.DatabaseEntities.PersonEnvironmentCredentials.Add(personEnvironmentCredentialForCurrentEnvironment);
                sendNewUserNotification = true;
            }
            else
            {
                // existing user - sync values
                SitkaHttpApplication.Logger.DebugFormat($"In {nameof(LookupExistingPersonOrProvisionNewPerson)} - user record already exists -- syncing local profile for {userDetailsString}");
            }

            person.FirstName = firstName;
            person.LastName = lastName;
            person.Email = email;
            person.LoginName = username;
            person.UpdateDate = DateTime.Now;

            if (authenticator.ToEnum == AuthenticatorEnum.ADFS)
            {
                SitkaHttpApplication.Logger.DebugFormat(
                    $"In {nameof(LookupExistingPersonOrProvisionNewPerson)} - Mapping ADFS role groups for {userDetailsString}");
                if (groups.Any())
                {
                    person.RoleID = MapRoleFromClaims(groups).RoleID;
                }

                person.OrganizationID = OrganizationModelExtensions.WadnrID;
            }

            HttpRequestStorage.DatabaseEntities.SaveChanges(person);
            HttpRequestStorage.Person = person;

            if (sendNewUserNotification)
            {
                SitkaHttpApplication.Logger.DebugFormat(
                    $"In {nameof(LookupExistingPersonOrProvisionNewPerson)} - Sending new user created message for {userDetailsString}");
                SendNewUserCreatedMessage(person, username);
            }

            return person;
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
    }
}
