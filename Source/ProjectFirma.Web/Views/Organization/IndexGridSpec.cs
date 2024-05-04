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
using System.Web;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;

namespace ProjectFirma.Web.Views.Organization
{
    public class IndexGridSpec : GridSpec<Models.Organization>
    {
        public IndexGridSpec(Person currentPerson, bool hasDeletePermissions)
        {
            var userViewFeature = new UserViewFeature();
            if (hasDeletePermissions)
            {
                Add(string.Empty, x => AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true, true), 30, AgGridColumnFilterType.None);
            }
            Add(Models.FieldDefinition.Organization.ToGridHeaderString(), a => UrlTemplate.MakeHrefString(a.GetDetailUrl(), a.OrganizationName), 400, AgGridColumnFilterType.Html);
            Add("Short Name", a => a.OrganizationShortName, 100);
            Add(Models.FieldDefinition.OrganizationType.ToGridHeaderString(), a => a.OrganizationType?.OrganizationTypeName, 100, AgGridColumnFilterType.SelectFilterStrict);
            Add($"# of {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} associated with this {Models.FieldDefinition.Organization.GetFieldDefinitionLabel()}", a => a.GetAllActiveProjects(currentPerson).Count, 90);
            Add($"# of {Models.FieldDefinition.Grant.GetFieldDefinitionLabelPluralized()} in this system associated with this {Models.FieldDefinition.Organization.GetFieldDefinitionLabel()}", a => a.Grants.Count, 90);
            Add($"# of Forest Health Tracker User Accounts associated with this {Models.FieldDefinition.Organization.GetFieldDefinitionLabel()}", a => a.People.Count, 90);
            Add("Is Active", a => a.IsActive.ToYesNo(), 80, AgGridColumnFilterType.SelectFilterStrict);
        }
    }
}
