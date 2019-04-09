/*-----------------------------------------------------------------------
<copyright file="ProjectLocationsMapViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Shared.ProjectLocationControls
{

    public enum ProjectMapGridDisplayType
    {
        NoGrid,
        ShowGrid
    }

    public class ProjectLocationsMapViewData : FirmaUserControlViewData
    {
        public ProjectMapGridDisplayType ProjectMapGridDisplayType { get; }
        public string MapDivID { get; }
        public string LegendTitle { get; }
        public Dictionary<string, List<ProjectMapLegendElement>> LegendFormats { get; }
        public MapInitJson MapInitJson { get; }
        public LayerGeoJson LayerGeoJson { get; }
        public List<ProjectLocationJson> ProjectLocationJsons { get; }
        //public List<string> GeospatialAreaMapServiceLayerNames { get; }
        //public List<ProjectLocationTypeJson> ProjectLocationTypeJsons { get; }

        public ProjectLocationsMapViewData(string mapDivID, string legendTitle, List<ITaxonomyTier> topLevelTaxonomyTiers, bool showProposals, MapInitJson mapInitJson, LayerGeoJson layerGeoJson, List<ProjectLocationJson> projectLocationJsons , ProjectMapGridDisplayType projectMapGridDisplayType)
        {
            MapDivID = mapDivID;
            LegendTitle = legendTitle;
            LegendFormats = ProjectMapLegendElement.BuildLegendFormatDictionary(topLevelTaxonomyTiers, showProposals);
            MapInitJson = mapInitJson;
            LayerGeoJson = layerGeoJson;
            ProjectMapGridDisplayType = projectMapGridDisplayType;
            ProjectLocationJsons = projectLocationJsons;
        }

    }
}
