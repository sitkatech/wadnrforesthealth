using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using LtInfo.Common;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static class ClaimsIdentityHelper
    {
        public const string AuthenticationApplicationCookieName = "WashingtonDNRForestHealthTrackerIdentity";

        public static void IdentitySignIn(IAuthenticationManager authenticationManager, Person person)
        {
            authenticationManager.SignIn(new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = false,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, ClaimsIdentityFromPerson(person));
        }

        public static void IdentitySignOut(IAuthenticationManager authenticationManager)
        {
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalCookie);
            HttpContext.Current.Request.Cookies.Remove(AuthenticationApplicationCookieName);
            HttpRequestStorage.Person = Person.GetAnonymousSitkaUser();
        }

        private static ClaimsIdentity ClaimsIdentityFromPerson(Person person)
        {
            var claims = new List<Claim>
            {
                // Using ClaimTypes.Name to get data into field Principal.Identity.Name for parse out later
                new Claim(ClaimTypes.Name, person.PersonID.ToString()),
            };
            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            return identity;
        }

        public static Person PersonFromClaimsIdentity(IAuthenticationManager authenticationManager)
        {
            var claimsPrincipal = authenticationManager.User;
            if (claimsPrincipal?.Identity == null || !claimsPrincipal.Identity.IsAuthenticated  || claimsPrincipal.Identity.AuthenticationType != DefaultAuthenticationTypes.ApplicationCookie)
            {
                return Person.GetAnonymousSitkaUser();
            }

            try
            {
                // This parsing out of depends on the write of data into ClaimTypes.Name
                var personID = int.Parse(claimsPrincipal.Identity.Name);
                var person = HttpRequestStorage.DatabaseEntities.People.GetPerson(personID);
                return person;
            }
            catch (Exception ex)
            {
                IdentitySignOut(authenticationManager);
                throw new SitkaDisplayErrorException("Something went wrong with your session or credentials. Please try logging in again. If this does not resolve the issue, please contact support.", ex);
            }
        }
    }
}