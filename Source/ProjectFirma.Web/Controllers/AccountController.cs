/*-----------------------------------------------------------------------
<copyright file="AccountController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
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
using LtInfo.Common.DesignByContract;
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
            SawSamlResponse sawSamlResponse = null;
            try
            {
                // SAML providers usually POST the data into this var
                var base64SawSamlResponse = Request.Form["SAMLResponse"];
                Logger.Debug($"SAML Debugging - SAWPost Received Base64 Encoded Form Variable SAMLResponse: {base64SawSamlResponse}");

                sawSamlResponse = SawSamlResponse.CreateFromBase64String(base64SawSamlResponse);

                Logger.Debug($"SAML Debugging - SAWPost Received Pretty Printed From Variable SAMLResponse: {sawSamlResponse.GetSamlAsPrettyPrintXml()}");

                if (sawSamlResponse.IsValid(out var userDisplayableValidationErrorMessage))
                {
                    var firstName = sawSamlResponse.GetFirstName();
                    var lastName = sawSamlResponse.GetLastName();
                    var email = sawSamlResponse.GetEmail();
                    var roleGroups = sawSamlResponse.GetRoleGroups();
                    var sawAuthenticator = sawSamlResponse.GetWhichSawAuthenticator();
                    ProcessLogin(firstName, lastName, email, roleGroups, sawAuthenticator);
                }
                else
                {
                    SitkaHttpApplication.Logger.Error($"Processing SAW login, SAML Response invalid. Validation error: {userDisplayableValidationErrorMessage}, SAW SAML XML:\r\n{sawSamlResponse.GetSamlAsPrettyPrintXml()}");
                    SetErrorForDisplay($"There was a SAW validation problem while trying to log into your account. {userDisplayableValidationErrorMessage} Please try again in a few minutes. If this issue keeps happening, please contact support: <a href=\"mailto:{FirmaWebConfiguration.SitkaSupportEmail}\">{FirmaWebConfiguration.SitkaSupportEmail}</a>");
                }
                return new RedirectResult(HomeUrl);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Exception during SAW Login, see inner exception for details. SAW SAML XML:\r\n{sawSamlResponse?.GetSamlAsPrettyPrintXml()}", ex);
            }
        }

        [AnonymousUnclassifiedFeature]
        [HttpPost]
        // ReSharper disable once InconsistentNaming
        public ActionResult ADFSPost(string returnUrl)
        {
            var adfsSamlResponse = new ADFSSamlResponse();
            try
            {
                // SAML providers usually POST the data into this var
                var base64AdfsSamlResponse = Request.Form["SAMLResponse"];
                Logger.Debug($"SAML Debugging - ADFSPost Received Base64 Encoded Form Variable SAMLResponse: {base64AdfsSamlResponse}");

                adfsSamlResponse.LoadXmlFromBase64(base64AdfsSamlResponse);
                adfsSamlResponse.Decrypt();

                Logger.Debug($"SAML Debugging - ADFSPost Received Pretty Printed: {adfsSamlResponse.GetSamlAsPrettyPrintXml()}");

                var firstName = adfsSamlResponse.GetFirstName();
                var lastName = adfsSamlResponse.GetLastName();
                var email = adfsSamlResponse.GetEmail();
                var roleGroups = adfsSamlResponse.GetRoleGroups();

                ProcessLogin(firstName, lastName, email, roleGroups, Authenticator.ADFS);
                return new RedirectResult(HomeUrl);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Exception during ADFS Login, see inner exception for details. ADFS SAML XML:\n\r{adfsSamlResponse.GetSamlAsPrettyPrintXml()}", ex);
            }
        }

        private void ProcessLogin(string firstName, string lastName, string email, List<string> roleGroups, Authenticator authenticator)
        {
            var userDetailsStringForLogging = $"FirstName: {firstName} LastName: {lastName} Email: {email} Groups: {string.Join(",", roleGroups)}";
            SitkaHttpApplication.Logger.Info($"In {nameof(ProcessLogin)} - Authenticator: {authenticator.AuthenticatorName} [{userDetailsStringForLogging}]");
            Check.RequireNotNullNotEmptyNotWhitespace(email, $"Cannot complete sign in with a blank or missing email address. Be sure that there is an email address for user in identity provider {authenticator.AuthenticatorFullName}.");

            var authenticatorsToAllow = AuthenticatorHelper.GetAuthenticatorsForEmailAddress(email);
            Check.RequireThrowUserDisplayable(authenticatorsToAllow.Select(x => x.AuthenticatorID).Contains(authenticator.AuthenticatorID), $"This email address {email} is not allowed to login with {authenticator.AuthenticatorFullName}. Please try logging in again with: {string.Join(",", authenticatorsToAllow.Select(x => x.AuthenticatorFullName))}.");

            var shouldSendNewUserCreatedMessage = false;

            var person = HttpRequestStorage.DatabaseEntities.People.GetPersonByEmail(email);
            SitkaHttpApplication.Logger.Info($"In {nameof(ProcessLogin)} - {(person != null ? "Found" : "Did NOT find")} by email address. [{userDetailsStringForLogging}]");

            // If there's no Person already that corresponds to the Person who is logging in, we create Person
            if (person == null)
            {
                // new user - initially provision with limited Role.Unassigned
                SitkaHttpApplication.Logger.Info($"In {nameof(ProcessLogin)} - Creating new Person. [{userDetailsStringForLogging}]");
                
                person = new Person(firstName, DateTime.Now, true, false)
                {
                    LastName = lastName,
                    Email = email,
                    OrganizationID = HttpRequestStorage.DatabaseEntities.Organizations.GetUnknownOrganization().OrganizationID
                };


                if (authenticator.ToEnum == AuthenticatorEnum.ADFS)
                {
                    SitkaHttpApplication.Logger.InfoFormat($"In {nameof(ProcessLogin)} - Setup of WA DNR account and mapping ADFS role groups. [{userDetailsStringForLogging}]");
                    //4/23/24 TK - We no longer want to set the Persons Role based on their AD role group. Default all WADNR employees to Normal on initial login. and allow admins to manage roles in FHT from there
                    person.PersonRoles.Add(new PersonRole(person, Role.Normal));
                    person.OrganizationID = OrganizationModelExtensions.WadnrID;
                }
                else
                {
                    person.PersonRoles.Add(new PersonRole(person, Role.Unassigned));
                }
                
                HttpRequestStorage.DatabaseEntities.People.Add(person);

                shouldSendNewUserCreatedMessage = true;
            }
            else
            {
                // existing user - sync values
                SitkaHttpApplication.Logger.InfoFormat($"In {nameof(ProcessLogin)} - user record already exists -- syncing local profile. [{userDetailsStringForLogging}]");
                Check.RequireThrowUserDisplayable(person.IsActive, $"User account for {email} is not active and cannot login at this time. Contact support for more information.");
            }

            //if person needs authenticators setup, lets add them
            if (!person.PersonAllowedAuthenticators.Any())
            {
                var personAllowedAuthenticators = authenticatorsToAllow.Select(x => new PersonAllowedAuthenticator(person, x));
                HttpRequestStorage.DatabaseEntities.PersonAllowedAuthenticators.AddRange(personAllowedAuthenticators);
            }

            person.FirstName = firstName;
            person.LastName = lastName;
            person.Email = email;
            person.UpdateDate = DateTime.Now;

            

            HttpRequestStorage.DatabaseEntities.SaveChanges(person);

            if (shouldSendNewUserCreatedMessage)
            {
                SitkaHttpApplication.Logger.InfoFormat($"In {nameof(ProcessLogin)} - Sending new user created message. [{userDetailsStringForLogging}]");
                SendNewUserCreatedMessage(person);
            }

            ClaimsIdentityHelper.IdentitySignIn(HttpContext.GetOwinContext().Authentication, person);
        }

        [AnonymousUnclassifiedFeature]
        public ActionResult LogOff()
        {
            SitkaHttpApplication.Logger.Info($"Logoff - {CurrentPerson.FullNameFirstLast} ({CurrentPerson.Email})");
            ClaimsIdentityHelper.IdentitySignOut(HttpContext.GetOwinContext().Authentication);
            var returnUrl = !string.IsNullOrWhiteSpace(Request["returnUrl"]) ? Request["returnUrl"] : HomeUrl;
            return Redirect(returnUrl);
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

        private static void SendNewUserCreatedMessage(Person person)
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
                    SitkaRoute<UserController>.BuildAbsoluteUrlHttpsFromExpression(x => x.Detail(person.PersonID))
                }"">assign this user roles</a> to allow them to work with specific areas of the site. Or you can leave the user with an unassigned role if they don't need special privileges.
    </p>
    <br />
    <br />
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
