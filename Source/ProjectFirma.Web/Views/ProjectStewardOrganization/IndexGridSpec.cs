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
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.ProjectStewardOrganization
{
    public class IndexGridSpec : GridSpec<Models.Organization>
    {
        public IndexGridSpec(Person currentPerson)
        {
            var userViewFeature = new UserViewFeature();
            
            Add(Models.FieldDefinition.Organization.ToGridHeaderString(), a => new HtmlLinkObject(a.OrganizationName, a.GetDetailUrl()).ToJsonObjectForAgGrid(), 400, AgGridColumnFilterType.HtmlLinkJson);
            Add("Short Name", a => a.OrganizationShortName, 100);
            Add(Models.FieldDefinition.OrganizationPrimaryContact.ToGridHeaderString(), a => userViewFeature.HasPermission(currentPerson, a.PrimaryContactPerson).HasPermission ? a.PrimaryContactPersonAsUrl : new HtmlString(a.PrimaryContactPersonAsString), 120);
            Add($"# of {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}", a => a.GetAllActiveProjectsAndProposals(currentPerson).Count, 90);
            Add($"# of {Models.FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabelPluralized()}", a => a.FundSourceAllocations.Count, 150);
            Add("# of Users", a => a.People.Count, 90);
            
        }
    }
}
