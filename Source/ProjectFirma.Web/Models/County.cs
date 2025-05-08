/*-----------------------------------------------------------------------
<copyright file="County.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common.GeoJson;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class County : IAuditableEntity
    {
        public string DisplayName => CountyName;

        public List<Project> GetAssociatedProjects(Person currentPerson)
        {
            return ProjectCounties.Select(ptc => ptc.Project).ToList().GetActiveProjectsVisibleToUser(currentPerson);
        }

        public string AuditDescriptionString => CountyName;

        public Feature MakeFeatureWithRelevantProperties()
        {
            var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(CountyFeature);
            feature.Properties.Add("County", this.GetCountyDisplayNameAsUrl().ToString());
            return feature;
        }


        public string GetDetailUrl()
        {
            var urlTemplateString = SitkaRoute<CountyController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int));
            return urlTemplateString.Replace(UrlTemplate.Parameter1Int.ToString(), this.CountyID.ToString());
        }

        public static LayerGeoJson GetCountyWmsLayerGeoJson(string layerColor, decimal layerOpacity, LayerInitialVisibility layerInitialVisibility)
        {
            return new LayerGeoJson("All Washington Counties", FirmaWebConfiguration.WebMapServiceUrl,
                FirmaWebConfiguration.GetCountyWmsLayerName(), layerColor, layerOpacity,
                layerInitialVisibility, "/Content/leaflet/images/washington_county.png");
        }

        public static List<LayerGeoJson> GetCountyAndAssociatedProjectLayers(County county, List<Project> projects)
        {
            var projectLayerGeoJson = new LayerGeoJson($"{FieldDefinition.ProjectLocation.GetFieldDefinitionLabel()} - Simple",
                Project.MappedPointsToGeoJsonFeatureCollection(projects, true, false),
                "#ffff00", 1, LayerInitialVisibility.Show);
            var countyLayerGeoJson = new LayerGeoJson(county.DisplayName,
                new List<County> { county }.ToGeoJsonFeatureCollection(), "#2dc3a1", 1,
                LayerInitialVisibility.Show);

            var layerGeoJsons = new List<LayerGeoJson>
            {
                projectLayerGeoJson,
                countyLayerGeoJson,
                GetCountyWmsLayerGeoJson("#59ACFF", 0.6m,
                    LayerInitialVisibility.Show)
            };

            return layerGeoJsons;
        }


    }
}
