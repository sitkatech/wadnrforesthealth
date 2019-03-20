/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Collections.Generic;
using System.Linq;
using ProjectFirma.Web.Views.Project;
using ProjectFirma.Web.Views.Results;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Views.PerformanceMeasure;
using ProjectFirma.Web.Views.Shared;

namespace ProjectFirma.Web.Views.Region
{
    public class DetailViewData : FirmaViewData
    {
        public readonly Models.Region Region;
        public readonly bool UserHasRegionManagePermissions;
        public readonly string IndexUrl;
        public readonly BasicProjectInfoGridSpec BasicProjectInfoGridSpec;
        public readonly string BasicProjectInfoGridName;
        public readonly string BasicProjectInfoGridDataUrl;
        public readonly MapInitJson MapInitJson;
        public readonly ViewGoogleChartViewData ViewGoogleChartViewData;
        public readonly List<PerformanceMeasureChartViewData> PerformanceMeasureChartViewDatas;

        public DetailViewData(Person currentPerson, Models.Region region, MapInitJson mapInitJson, ViewGoogleChartViewData viewGoogleChartViewData, List<Models.PerformanceMeasure> performanceMeasures) : base(currentPerson)
        {
            Region = region;
            MapInitJson = mapInitJson;
            ViewGoogleChartViewData = viewGoogleChartViewData;
            PageTitle = region.RegionName;
            EntityName = "Region";
            UserHasRegionManagePermissions = new RegionManageFeature().HasPermissionByPerson(currentPerson);
            IndexUrl = SitkaRoute<RegionController>.BuildUrlFromExpression(x => x.Index());

            BasicProjectInfoGridName = "regionProjectListGrid";
            BasicProjectInfoGridSpec = new BasicProjectInfoGridSpec(CurrentPerson, false)
            {
                ObjectNameSingular = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} in this Region",
                ObjectNamePlural = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} in this Region",
                SaveFiltersInCookie = true
            };
          
            BasicProjectInfoGridDataUrl = SitkaRoute<RegionController>.BuildUrlFromExpression(tc => tc.ProjectsGridJsonData(region));

            PerformanceMeasureChartViewDatas = performanceMeasures.Select(x=>region.GetPerformanceMeasureChartViewData(x, CurrentPerson)).ToList();
        }

        
    }
}
