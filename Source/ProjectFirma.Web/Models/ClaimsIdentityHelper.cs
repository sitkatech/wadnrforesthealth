using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static class ClaimsIdentityHelper
    {
        private static string ClaimUsedToStorePersonID = ClaimTypes.Sid;

        public static ClaimsIdentity MakeClaimsIdentityFromPerson(Person person)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimUsedToStorePersonID, person.PersonID.ToString()),
            };
            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            return identity;
        }

        public static Person GetPersonFromClaimsIdentity(IPrincipal principal)
        {
            var anonymousSitkaPerson = Person.GetAnonymousSitkaUser();
            if (principal?.Identity == null || !principal.Identity.IsAuthenticated)
            {
                return anonymousSitkaPerson;
            }

            if (principal.Identity.AuthenticationType != "ApplicationCookie")
            {
                return anonymousSitkaPerson;
            }

            var userIdentity = principal.Identity;
            if (userIdentity == null)
            {
                throw new NullReferenceException($"Should have {nameof(IIdentity)}");
            }

            if (!(userIdentity is ClaimsIdentity claimsIdentity))
            {
                throw new Saml2ClaimException($"The {nameof(IIdentity)} is not expected type {nameof(ClaimsIdentity)}. {nameof(IIdentity.Name)}: {userIdentity.Name}");
            }

            var personIDClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimUsedToStorePersonID);
            if (personIDClaim == null)
            {
                throw new Saml2ClaimException($"Can't find required claim \"{ClaimUsedToStorePersonID}\" in Identity Provider claims.");
            }

            var personID = Int32.Parse(personIDClaim.Value);
            var person = HttpRequestStorage.DatabaseEntities.People.GetPerson(personID);

            return person;
        }
    }
}