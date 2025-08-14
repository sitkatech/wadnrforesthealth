/*-----------------------------------------------------------------------
<copyright file="PersonModelExtensions.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Web;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using LtInfo.Common;

namespace ProjectFirma.Web.Models
{
    /// <summary>
    /// These have been implemented as extension methods on <see cref="Person"/> so we can handle the anonymous user as a null person object
    /// </summary>
    public static class PersonModelExtensions
    {
        public static HtmlString GetFullNameFirstLastAsUrl(this Person person)
        {
            return person != null ? UrlTemplate.MakeHrefString(person.GetDetailUrl(), person.FullNameFirstLast) : new HtmlString(null);
        }

        public static HtmlString GetFullNameLastFirstAsUrl(this Person person)
        {
            return person != null ? UrlTemplate.MakeHrefString(person.GetDetailUrl(), person.FullNameLastFirst) : new HtmlString(null);
        }

        public static HtmlString GetFullNameFirstLastAndOrgAsUrl(this Person person)
        {
            if (person == null)
            {
                return new HtmlString(string.Empty);
            }
            var userUrl = person.GetFullNameFirstLastAsUrl();
            var orgUrl = person.Organization?.GetDisplayNameAsUrl();
            return new HtmlString($"{userUrl} - {orgUrl}");
        }

        public static HtmlString GetFullNameFirstLastAndOrgShortNameAsUrl(this Person person)
        {
            if (person == null)
            {
                return new HtmlString(string.Empty);
            }
            var userUrl = person.GetFullNameFirstLastAsUrl();
            var orgUrl = person.Organization?.GetShortNameAsUrl();
            return new HtmlString($"{userUrl} ({orgUrl})");
        }

        public static HtmlString GetFullNameFirstLastAsStringAndOrgAsUrl(this Person person)
        {
            if (person == null)
            {
                return new HtmlString(string.Empty);
            }
            var userString = person.FullNameFirstLast;
            var orgUrl = person.Organization?.GetShortNameAsUrl();
            return new HtmlString($"{userString} - {orgUrl}");
        }

        public static HtmlString GetFirstNameAsUrl(this Person person)
        {
            return person != null ? UrlTemplate.MakeHrefString(person.GetDetailUrl(), person.FirstName) : new HtmlString(null);
        }

        public static HtmlString GetLastNameAsUrl(this Person person)
        {
            return person != null ? UrlTemplate.MakeHrefString(person.GetDetailUrl(), person.LastName) : new HtmlString(null);
        }

        public static string GetEditUrl(this Person person)
        {
            return SitkaRoute<UserController>.BuildUrlFromExpression(t => t.EditRoles(person));
        }

        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<UserController>.BuildUrlFromExpression(t => t.Delete(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this Person person)
        {
            return DeleteUrlTemplate.ParameterReplace(person.PersonID);
        }

        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<UserController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this Person person)
        {
            return DetailUrlTemplate.ParameterReplace(person.PersonID);
        }

        public static bool IsSitkaAdministrator(this Person person)
        {
            return person != null && person.HasRole(Role.EsaAdmin);

        }

        public static bool IsAdministrator(this Person person)
        {
            return person != null && (person.HasRole(Role.Admin) || IsSitkaAdministrator(person));
        }

        public static bool IsApprover(this Person person)
        {
            return person != null && (person.IsAdministrator() || person.IsSitkaAdministrator());
        }
        
        public static bool ShouldReceiveNotifications(this Person person)
        {
            return person.ReceiveSupportEmails;
        }

        public static bool HasRole(this Person person, Role role)
        {
            return person.PersonRoles.Select(x => x.Role).Contains(role);
        }

        public static bool HasRoleID(this Person person, int roleID)
        {
            return person.PersonRoles.Select(x => x.Role.RoleID).Contains(roleID);
        }

        public static bool HasAnyOfTheseRoles(this Person person, IEnumerable<IRole> iroles)
        {
            var roleIDs = iroles.Select(x => x.RoleID);
            return person.PersonRoles.Select(x => x.Role.RoleID).Intersect(roleIDs).Any();
        }

        /// <summary>
        /// Should only be used for full users
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public static Role GetUsersBaseRole(this Person person)
        {
            if (!person.IsFullUser())
            {
                throw new Exception("Person is not a Full User and should not have a base role");
            }

            var thisPersonsBaseRoles = person.PersonRoles.Where(x => x.Role.IsBaseRole).Select(x => x.Role).ToList();
            return thisPersonsBaseRoles.Single();//expecting each user to have only one base role
        }

        /// <summary>
        /// Should only be used for full users
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public static List<Role> GetUsersSupplementalRoles(this Person person)
        {
            if (!person.IsFullUser())
            {
                throw new Exception("Person is not a Full User and should not have a base role");
            }
            return person.PersonRoles.Where(x => !x.Role.IsBaseRole).Select(x => x.Role).ToList();
        }
    }
}
