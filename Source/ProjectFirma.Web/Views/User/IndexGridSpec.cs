/*-----------------------------------------------------------------------
<copyright file="IndexGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System.Linq;
using System.Web;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.Views;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.User
{
    public class IndexGridSpec : GridSpec<Person>
    {
        public IndexGridSpec(Person currentPerson)
        {
            //var hasDeletePermission = new PersonDeleteFeature().HasPermissionByPerson(currentPerson);
            //if (hasDeletePermission)
            //{
            //    Add(string.Empty,
            //        x => x.IsFullUser() ? AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), hasDeletePermission, !x.HasDependentObjects(), true) : new HtmlString(""),
            //        30, AgGridColumnFilterType.None);
            //}
            Add("Last Name", a => UrlTemplate.MakeHrefString(a.GetDetailUrl(), a.LastName), 100, AgGridColumnFilterType.Html);
            Add("First Name", a => UrlTemplate.MakeHrefString(a.GetDetailUrl(), a.FirstName), 100, AgGridColumnFilterType.Html);
            Add("Email", a => a.Email, 200);
            Add($"{Models.FieldDefinition.Organization.GetFieldDefinitionLabelPluralized()}", a => GetOrganizationShortNameUrl(a), 200);
            Add("Phone", a => a.Phone.ToPhoneNumberString(), 100);
            Add("Last Activity", a => a.LastActivityDate, 120);
            Add("Base Role", a => a.IsFullUser() ? a.GetUsersBaseRole().RoleDisplayName : "N/A", 100, AgGridColumnFilterType.SelectFilterStrict);
            Add("Supplemental Role(s)", a => a.IsFullUser() ? string.Join(", ", a.GetUsersSupplementalRoles().Select(x => x.RoleDisplayName)) : "N/A", 100, AgGridColumnFilterType.Text);
            Add("Active?", a => a.IsActive.ToYesNo(), 75, AgGridColumnFilterType.SelectFilterStrict);
            //Add("Receives Support Emails?", a => a.IsFullUser() ? a.ReceiveSupportEmails.ToYesNo(): "N/A", 100, AgGridColumnFilterType.SelectFilterStrict);
            Add($"{Models.FieldDefinition.OrganizationPrimaryContact.GetFieldDefinitionLabel()} for Organizations", a => a.PrimaryContactOrganizations.Count, 120);
            Add("Added On", x => x.CreateDate, 130, AgGridColumnFormatType.Date);
            Add("Added By", x => x.AddedByPerson == null ? new HtmlString("") : x.AddedByPerson.GetFullNameFirstLastAsUrl(), 200, AgGridColumnFilterType.Html);
            Add("Authentication Methods", x => string.Join(", ", x.PersonAllowedAuthenticators.OrderBy(paa => paa.Authenticator.AuthenticatorName).Select(paa => paa.Authenticator.AuthenticatorName)), 75, AgGridColumnFilterType.SelectFilterStrict);
        }

        private static HtmlString GetOrganizationShortNameUrl(Person person)
        {
            if (person == null)
            {
                return new HtmlString(string.Empty);
            }

            if (person.Organization == null)
            {
                return new HtmlString(string.Empty);
            }

            return person.Organization.GetShortNameAsUrl();
        }

        public enum UsersStatusFilterTypeEnum
        {
            AllActiveUsers,
            AllContacts,
            AllActiveUsersAndContacts,
            AllUsersAndContacts
        }
    }
}
