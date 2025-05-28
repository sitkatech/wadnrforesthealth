/*-----------------------------------------------------------------------
<copyright file="DNRUplandRegion.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Data.Entity;
using System.Linq;
using System.Web;
using LtInfo.Common.GeoJson;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class DNRUplandRegion : IAuditableEntity
    {
        public string DisplayName => DNRUplandRegionName;

        public List<Project> GetAssociatedProjects(Person currentPerson)
        {
            var projects = HttpRequestStorage.DatabaseEntities.Projects
                .Where(x => x.ProjectRegions.Any(y => y.DNRUplandRegionID == this.DNRUplandRegionID))
                .Include(x => x.ProjectOrganizations)
                .Include(x => x.ProjectTags)
                .Include(x => x.ProjectGrantAllocationRequests)
                .Include(x => x.ProjectType)
                .ToList();

            return projects.GetActiveProjectsVisibleToUser(currentPerson);
        }

        public List<GrantAllocation> GetAssociatedGrantAllocations(Person currentPerson)
        {
            var grantAllocations = HttpRequestStorage.DatabaseEntities.GrantAllocations
                .Where(x => x.DNRUplandRegionID == this.DNRUplandRegionID)
                //.Include(x => x.GrantAllocationLikelyPeople)
                .ToList();

            return grantAllocations;
        }

        public string AuditDescriptionString => DNRUplandRegionName;

        public Feature MakeFeatureWithRelevantProperties()
        {
            var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(DNRUplandRegionLocation);
            feature.Properties.Add("Region", this.GetRegionDisplayNameAsUrl().ToString());
            return feature;
        }

        public HtmlString GetDisplayNameAsUrl()
        {
            return UrlTemplate.MakeHrefString(GetDetailUrl(), DisplayName);
        }

        public string GetDetailUrl()
        {
            var urlTemplateString = SitkaRoute<DNRUplandRegionController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int));
            return urlTemplateString.Replace(UrlTemplate.Parameter1Int.ToString(), this.DNRUplandRegionID.ToString());
        }

        public static LayerGeoJson GetRegionWmsLayerGeoJson(string layerColor, decimal layerOpacity, LayerInitialVisibility layerInitialVisibility)
        {
            return new LayerGeoJson("All DNR Upland Regions", FirmaWebConfiguration.WebMapServiceUrl,
                FirmaWebConfiguration.GetDNRUplandRegionWmsLayerName(), layerColor, layerOpacity,
                layerInitialVisibility, "/Content/leaflet/images/washington_upland_region.png");
        }

        public static List<LayerGeoJson> GetRegionAndAssociatedProjectLayers(DNRUplandRegion dnrUplandRegion, List<Project> projects)
        {
            var projectLayerGeoJson = new LayerGeoJson($"{FieldDefinition.ProjectLocation.GetFieldDefinitionLabel()} - Simple",
                Project.MappedPointsToGeoJsonFeatureCollection(projects, true, false),
                "#ffff00", 1, LayerInitialVisibility.Show);
            var regionLayerGeoJson = new LayerGeoJson(dnrUplandRegion.DisplayName,
                new List<DNRUplandRegion> { dnrUplandRegion }.ToGeoJsonFeatureCollection(), "#2dc3a1", 1,
                LayerInitialVisibility.Show);

            var layerGeoJsons = new List<LayerGeoJson>
            {
                projectLayerGeoJson,
                regionLayerGeoJson,
                GetRegionWmsLayerGeoJson("#59ACFF", 0.6m,
                    LayerInitialVisibility.Show)
            };

            return layerGeoJsons;
        }

    }
}
