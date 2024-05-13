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

using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.Views;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.FocusArea
{
    public class IndexGridSpec : GridSpec<Models.FocusArea>
    {
        public IndexGridSpec(Person person)
        {
            
            Add("Delete", x => AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteFocusAreaUrl(), new FocusAreaManageFeature().HasPermissionByPerson(person), x.CanFocusAreaBeDeleted(), true), 30, AgGridColumnFilterType.None);
            Add(Models.FieldDefinition.FocusArea.GetFieldDefinitionLabel(), a => UrlTemplate.MakeHrefString(a.GetDetailUrl(), a.FocusAreaName), 195, AgGridColumnFilterType.Html);
            Add("Status", a => a.FocusAreaStatus.FocusAreaStatusDisplayName, 75, AgGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Region", a => a.DNRUplandRegion.DNRUplandRegionName, 75, AgGridColumnFilterType.SelectFilterHtmlStrict);
            Add($"# of {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}", a => a.GetAssociatedProjects(person).Count, 65);
        }
    }
}
