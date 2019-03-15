using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using log4net;
using LtInfo.Common;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static class ClaimsIdentityHelper
    {
        public const string AuthenticationApplicationCookieName = "WashingtonDNRForestHealthTrackerIdentity";
        private const string ClaimUsedToStorePersonID = ClaimTypes.Name;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ClaimsIdentityHelper));

        public static void IdentitySignIn(ClaimsIdentity claimsIdentity, IAuthenticationManager authenticationManager)
        {
            authenticationManager.SignIn(new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = false,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, claimsIdentity);
        }

        public static void IdentitySignOut(IAuthenticationManager authenticationManager)
        {
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalCookie);
            HttpContext.Current.Request.Cookies.Remove(AuthenticationApplicationCookieName);
            HttpRequestStorage.Person = Person.GetAnonymousSitkaUser();
        }

        public static ClaimsIdentity MakeClaimsIdentityFromPerson(Person person)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimUsedToStorePersonID, person.PersonID.ToString()),
            };
            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            return identity;
        }

        public static Person GetPersonFromClaimsIdentity(IAuthenticationManager authenticationManager)
        {
            var principal = authenticationManager.User;
            var anonymousSitkaPerson = Person.GetAnonymousSitkaUser();
            if (principal?.Identity == null || !principal.Identity.IsAuthenticated)
            {
                return anonymousSitkaPerson;
            }

            if (principal.Identity.AuthenticationType != "ApplicationCookie")
            {
                return anonymousSitkaPerson;
            }
            
            var displayException = new SitkaDisplayErrorException("Something went wrong with your session or credentials. Please try logging in again. If this does not resolve the issue, please contact support.");
            var principalIdentity = principal.Identity;
            if (principalIdentity == null)
            {
                IdentitySignOut(authenticationManager);
                Logger.Warn($"Should have {nameof(IIdentity)} on principal but got null.");
                throw displayException;
            }

            if (!(principalIdentity is ClaimsIdentity claimsIdentity))
            {
                IdentitySignOut(authenticationManager);
                Logger.Warn(
                    $"The {nameof(IIdentity)} is not expected type {nameof(ClaimsIdentity)} but rather type {principalIdentity.GetType()}. {nameof(IIdentity.Name)}: {principalIdentity.Name}");
                throw displayException;
            }

            var personIDClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimUsedToStorePersonID);
            if (personIDClaim == null)
            {
                IdentitySignOut(authenticationManager);
                Logger.Warn($"Can't find required claim \"{ClaimUsedToStorePersonID}\" in Identity Provider claims.");
                throw displayException;
            }

            var personID = Int32.Parse(personIDClaim.Value);
            var person = HttpRequestStorage.DatabaseEntities.People.GetPerson(personID);

            return person;
        }

        
    }
}