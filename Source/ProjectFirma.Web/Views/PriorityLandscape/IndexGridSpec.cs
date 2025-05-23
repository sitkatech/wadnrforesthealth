﻿/*-----------------------------------------------------------------------
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

namespace ProjectFirma.Web.Views.PriorityLandscape
{
    public class IndexGridSpec : GridSpec<Models.PriorityLandscape>
    {
        public IndexGridSpec(Person person)
        {
            Add("Priority Landscape", a => new HtmlLinkObject(a.PriorityLandscapeName, a.GetDetailUrl()).ToJsonObjectForAgGrid(), 150, AgGridColumnFilterType.HtmlLinkJson);
            Add("Priority Landscape Category", a => a.PriorityLandscapeCategory.PriorityLandscapeCategoryDisplayName, 250, AgGridColumnFilterType.SelectFilterStrict);
            Add($"# of {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}", a => a.GetAssociatedProjectsVisibleToUser(person).Count, 65);
        }
    }
}
