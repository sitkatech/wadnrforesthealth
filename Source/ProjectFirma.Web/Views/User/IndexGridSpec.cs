/*-----------------------------------------------------------------------
<copyright file="IndexGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Web;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.Views;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.User
{
    public class IndexGridSpec : GridSpec<Person>
    {
        public IndexGridSpec(Person currentPerson)
        {
            var hasDeletePermission = new PersonDeleteFeature().HasPermissionByPerson(currentPerson);
            if (hasDeletePermission)
            {
                Add(string.Empty,
                    x => string.IsNullOrWhiteSpace(x.PersonUniqueIdentifier) ? DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), !x.HasDependentObjects(), !x.HasDependentObjects()) : new HtmlString(""),
                    30, DhtmlxGridColumnFilterType.None);
            }
            Add("Last Name", a => UrlTemplate.MakeHrefString(a.GetDetailUrl(), a.LastName), 100, DhtmlxGridColumnFilterType.Html);
            Add("First Name", a => UrlTemplate.MakeHrefString(a.GetDetailUrl(), a.FirstName), 100, DhtmlxGridColumnFilterType.Html);
            Add("Email", a => a.Email, 200);
            Add($"{Models.FieldDefinition.Organization.GetFieldDefinitionLabelPluralized()}", a => a.Organization.GetShortNameAsUrl(), 200);
            Add("Phone", a => a.Phone.ToPhoneNumberString(), 100);
            Add("Username", a => a.IsFullUser() ? a.LoginName?.ToString() : "N/A", 200);
            Add("Last Activity", a => a.IsFullUser() ? a.LastActivityDate : null, 120);
            Add("Role", a => a.IsFullUser() ? a.Role.GetDisplayNameAsUrl() : new HtmlString("N/A"), 100, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Active?", a => a.IsFullUser() ? a.IsActive.ToYesNo() : "N/A", 75, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Receives Support Emails?", a => a.IsFullUser() ? a.ReceiveSupportEmails.ToYesNo(): "N/A", 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add($"{Models.FieldDefinition.OrganizationPrimaryContact.GetFieldDefinitionLabel()} for Organizations", a => a.PrimaryContactOrganizations.Count, 120);
            Add("Added On", x => x.CreateDate, 130, DhtmlxGridColumnFormatType.Date);
            Add("Added By", x => x.AddedByPerson == null ? new HtmlString("") : x.AddedByPerson.GetFullNameFirstLastAsUrl(), 200, DhtmlxGridColumnFilterType.Html);
        }
    }
}
