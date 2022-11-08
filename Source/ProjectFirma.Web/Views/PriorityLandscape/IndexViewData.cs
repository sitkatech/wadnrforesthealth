/*-----------------------------------------------------------------------
<copyright file="IndexViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views.PriorityLandscape
{
    public class IndexViewData : FirmaViewData
    {
        public readonly MapInitJson MapInitJson;
        public readonly IndexGridSpec GridSpec;
        public readonly string GridName;
        public readonly string GridDataUrl;

        public IndexViewData(Person currentPerson, MapInitJson mapInitJson, Models.FirmaPage firmaPage) : base(currentPerson, firmaPage)
        {
            PageTitle = $"{Models.FieldDefinition.PriorityLandscape.GetFieldDefinitionLabelPluralized()}";
            MapInitJson = mapInitJson;
            GridSpec = new IndexGridSpec(currentPerson) {ObjectNameSingular = $"{Models.FieldDefinition.PriorityLandscape.GetFieldDefinitionLabel()}", ObjectNamePlural = $"{Models.FieldDefinition.PriorityLandscape.GetFieldDefinitionLabelPluralized()}", SaveFiltersInCookie = true};
            GridName = "priorityLandscapesGrid";
            GridDataUrl = SitkaRoute<PriorityLandscapeController>.BuildUrlFromExpression(tc => tc.IndexGridJsonData());
        }

    }
}
