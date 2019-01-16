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

using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.Views;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.FocusArea
{
    public class IndexGridSpec : GridSpec<Models.FocusArea>
    {
        public IndexGridSpec(Person person)
        {
            
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteFocusAreaUrl(), new FirmaAdminFeature().HasPermissionByPerson(person), x.CanFocusAreaBeDeleted()), 30, DhtmlxGridColumnFilterType.None);
            Add("Focus Area", a => UrlTemplate.MakeHrefString(a.GetDetailUrl(), a.FocusAreaName), 270, DhtmlxGridColumnFilterType.Html);
            Add("Status", a => a.FocusAreaStatus.FocusAreaStatusDisplayName, 75, DhtmlxGridColumnFilterType.Text);
            Add($"# of {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}", a => a.GetAssociatedProjects(person).Count, 65);
        }
    }
}
