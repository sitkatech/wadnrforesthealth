using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using ApprovalUtilities.Utilities;
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

            var principalIdentity = principal.Identity;
            if (principalIdentity == null)
            {
                throw new ClaimsIdentityException($"Should have {nameof(IIdentity)} on principal but got null.");
            }

            if (!(principalIdentity is ClaimsIdentity claimsIdentity))
            {
                throw new ClaimsIdentityException($"The {nameof(IIdentity)} is not expected type {nameof(ClaimsIdentity)} but rather type {principalIdentity.GetType()}. {nameof(IIdentity.Name)}: {principalIdentity.Name}");
            }

            var personIDClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimUsedToStorePersonID);
            if (personIDClaim == null)
            {
                throw new ClaimsIdentityException($"Can't find required claim \"{ClaimUsedToStorePersonID}\" in Identity Provider claims.");
            }

            var personID = Int32.Parse(personIDClaim.Value);
            var person = HttpRequestStorage.DatabaseEntities.People.GetPerson(personID);

            return person;
        }
    }
}