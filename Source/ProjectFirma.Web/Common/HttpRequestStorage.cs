/*-----------------------------------------------------------------------
<copyright file="HttpRequestStorage.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using LtInfo.Common;
using ProjectFirma.Web.Models;
using Person = ProjectFirma.Web.Models.Person;

namespace ProjectFirma.Web.Common
{
    public class HttpRequestStorage : SitkaHttpRequestStorage
    {
        static HttpRequestStorage()
        {
            LtInfoEntityTypeLoaderFactoryFunction = () => MakeNewContext(false);
        }

        protected override List<string> BackingStoreKeys
        {
            get { return new List<string>(); }
        }

        public static IPrincipal GetHttpContextUserThroughOwin()
        {
            return HttpContext.Current.GetOwinContext().Authentication.User;
        }

        public static Person Person
        {
            get
            {
                return GetValueOrDefault(PersonKey,
                    () => Saml2ClaimsHelpers.GetOpenIDUserFromPrincipal(GetHttpContextUserThroughOwin(),
                        Person.GetAnonymousSitkaUser(), DatabaseEntities.People.GetPersonByPersonUniqueIdentifier));
            }
            set { SetValue(PersonKey, value); }
        }

        public static DatabaseEntities DatabaseEntities
        {
            get { return (DatabaseEntities) LtInfoEntityTypeLoader; }
        }

        private static DatabaseEntities MakeNewContext(bool autoDetectChangesEnabled)
        {
            var databaseEntities = new DatabaseEntities();
            databaseEntities.Configuration.AutoDetectChangesEnabled = autoDetectChangesEnabled;
            return databaseEntities;
        }

        public static void StartContextForTest()
        {
            var context = MakeNewContext(true);
            SetValue(DatabaseContextKey, context);
        }

        public static void EndContextForTest()
        {
            if (!BackingStore.Contains(DatabaseContextKey))
            {
                return;
            }
            var databaseEntities = BackingStore[DatabaseContextKey] as DatabaseEntities;
            if (databaseEntities != null)
            {
                databaseEntities.Dispose();
                BackingStore[DatabaseContextKey] = null;
            }
            BackingStore.Remove(DatabaseContextKey);

            if (!BackingStore.Contains(PersonKey))
            {
                return;
            }
            var person = BackingStore[PersonKey] as Person;
            if (person != null)
            {
                BackingStore[PersonKey] = null;
            }
            BackingStore.Remove(PersonKey);
        }
    }

    public class Saml2ClaimNotFoundException : Exception
    {
        public Saml2ClaimNotFoundException(string message) : base(message) { }
        public Saml2ClaimNotFoundException() { }
        public Saml2ClaimNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        protected Saml2ClaimNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    public static class Saml2ClaimsHelpers
    {
        public static Saml2UserClaims ParseOpenIDClaims(IIdentity userIdentity)
        {
            if (userIdentity == null)
            {
                throw new NullReferenceException($"Should have {nameof(IIdentity)}");
            }

            var saml2UserClaims = new Saml2UserClaims();
            if (userIdentity is ClaimsIdentity claimsIdentity)
            {
                var claimsDictionary = claimsIdentity.Claims.ToList().GroupBy(x => x.Type, StringComparer.InvariantCultureIgnoreCase).ToDictionary(x => x.Key, x => x.First().Value);
                // core claims always supplied
                saml2UserClaims.UniqueIdentifier = GetStringClaimValue(claimsDictionary, ClaimTypes.NameIdentifier);
                saml2UserClaims.DisplayName = GetStringClaimValue(claimsDictionary, ClaimTypes.Name);
                saml2UserClaims.Email = GetStringOptionalClaimValue(claimsDictionary, ClaimTypes.Email);
                saml2UserClaims.Username = GetStringClaimValue(claimsDictionary, ClaimTypes.WindowsAccountName);
                var roleGroups = GetStringOptionalClaimValue(claimsDictionary, ClaimTypes.Role);
                if (!string.IsNullOrWhiteSpace(roleGroups))
                {
                    saml2UserClaims.RoleGroups = roleGroups.Split(',').ToList();
                }
                else
                {
                    saml2UserClaims.RoleGroups = new List<string>();
                }
            }
            else
            {
                throw new Saml2ClaimNotFoundException($"The {nameof(IIdentity)} is not expected type {nameof(ClaimsIdentity)}. {nameof(IIdentity.Name)}: {userIdentity.Name}");
            }

            return saml2UserClaims;
        }

        private static void EnsureClaimTypeExists(IReadOnlyDictionary<string, string> claimsDictionary, string claimType)
        {
            if (!claimsDictionary.ContainsKey(claimType))
            {
                throw new Saml2ClaimNotFoundException(claimType);
            }
        }

        private static Guid GetGuidClaimValue(IReadOnlyDictionary<string, string> claimsDictionary, string claimType)
        {
            EnsureClaimTypeExists(claimsDictionary, claimType);
            Guid.TryParse(claimsDictionary[claimType], out var claimValue);
            return claimValue;
        }

        private static Guid? GetGuidOptionalClaimValue(IReadOnlyDictionary<string, string> claimsDictionary, string claimType)
        {
            if (claimsDictionary.ContainsKey(claimType) && Guid.TryParse(claimsDictionary[claimType], out var claimValue))
            {
                return claimValue;
            }
            return null;
        }

        private static string GetStringClaimValue(IReadOnlyDictionary<string, string> claimsDictionary, string claimType)
        {
            EnsureClaimTypeExists(claimsDictionary, claimType);
            return claimsDictionary[claimType];
        }

        private static string GetStringOptionalClaimValue(IReadOnlyDictionary<string, string> claimsDictionary, string claimType)
        {
            return claimsDictionary.ContainsKey(claimType) ? claimsDictionary[claimType] : null;
        }

        private static TimeZoneInfo GetTimeZoneInfoClaimValue(IReadOnlyDictionary<string, string> claimsDictionary, string claimType)
        {
            var claimValue = TimeZoneInfo.Local;
            if (claimsDictionary.ContainsKey(claimType))
            {
                try
                {
                    return TimeZoneInfo.FromSerializedString(claimsDictionary[claimType]);
                }
                catch
                {
                    return claimValue;
                }
            }

            return claimValue;
        }

        public static Person GetOpenIDUserFromPrincipal(IPrincipal principal, Person anonymousSitkaUser, Func<string, Person> getUserByGuid)
        {
            if (principal?.Identity == null || !principal.Identity.IsAuthenticated)
            {
                return anonymousSitkaUser;
            }

            // calls to the account provisioning service from keystone are authenticated calls, but not by forms auth tickets.  they come in with the user identity of the
            // application pool that keystone runs under and have an authentication type of "Kerberos". these particular invocations need to be treated the same way as the
            // unauthenticated calls over basic bindings - that is they do not map to a MM user and should be considered "anonymous".

            //These are OpenID AuthenticationTypes, WIF ones include "Keberos" and "Federation"
            if (principal.Identity.AuthenticationType == "ApplicationCookie")
            {
                // otherwise remap claims from principal
                var saml2UserClaims = ParseOpenIDClaims(principal.Identity);
                var user = getUserByGuid(saml2UserClaims.UniqueIdentifier);
                if (user == null)
                {
                    throw new Saml2ClaimNotFoundException($"User not found for GUID {saml2UserClaims.UniqueIdentifier} ({saml2UserClaims.DisplayName})");
                }
                var names = saml2UserClaims.DisplayName.Split(' ');
                if (names.Length == 2)
                {
                    user.FirstName = names[0];
                    user.LastName = names[1];
                }
                else
                {
                    user.FirstName = saml2UserClaims.DisplayName;
                }
                user.Email = saml2UserClaims.Email;
                
                return user;
            }
            return anonymousSitkaUser;
        }
    }
}
