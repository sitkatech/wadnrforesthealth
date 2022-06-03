/*-----------------------------------------------------------------------
<copyright file="MapInitJson.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GeoJson;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public class MapInitJson
    {
        public const int CoordinateSystemId = 4326;

        public string MapDivID;
        public BoundingBox BoundingBox;
        public int ZoomLevel;
        public List<LayerGeoJson> Layers;
        public readonly bool TurnOnFeatureIdentify;
        public bool AllowFullScreen = true;
        public bool DisablePopups = false;

        public MapInitJson(string mapDivID, int zoomLevel, List<LayerGeoJson> layers, BoundingBox boundingBox, bool turnOnFeatureIdentify)
        {
            MapDivID = mapDivID;
            ZoomLevel = zoomLevel;
            Layers = layers;
            BoundingBox = boundingBox;
            TurnOnFeatureIdentify = turnOnFeatureIdentify;
        }

        /// <summary>
        /// Summary maps with no editing should use this constructor
        /// </summary>
        public MapInitJson(string mapDivID, int zoomLevel, List<LayerGeoJson> layers, BoundingBox boundingBox) : this(mapDivID, zoomLevel, layers, boundingBox, true)
        {
        }

        public static List<LayerGeoJson> GetAllGeospatialAreaMapLayers(LayerInitialVisibility layerInitialVisibility)
        {
            var layerGeoJsons = new List<LayerGeoJson>
            {
                GetAllSimpleProjectLocations(),
                GetAllDetailedProjectLocations(),
                GetAllProjectTreatments(),
                PriorityLandscape.GetPriorityLandscapeWmsLayerGeoJson("#59ACFF", 0.2m, layerInitialVisibility),
                PriorityLandscape.GetPriorityLandscapeWmsLayerGeoJson("#59ACFF", 0.2m, layerInitialVisibility),
                DNRUplandRegion.GetRegionWmsLayerGeoJson("#59ACFF", 0.2m, layerInitialVisibility)
                
            };
            return layerGeoJsons;
        }

        public static List<LayerGeoJson> GetRegionMapLayers( LayerInitialVisibility layerInitialVisibility)
        {
            var layerGeoJsons = new List<LayerGeoJson>
            {
                DNRUplandRegion.GetRegionWmsLayerGeoJson("#59ACFF", 0.2m, layerInitialVisibility)
            };

            return layerGeoJsons;
        }

        public static List<LayerGeoJson> GetPriorityLandscapeMapLayers( LayerInitialVisibility layerInitialVisibility)
        {
            var layerGeoJsons = new List<LayerGeoJson>
            {
                PriorityLandscape.GetPriorityLandscapeWmsLayerGeoJson("#59ACFF", 0.2m, layerInitialVisibility),
                PriorityLandscape.GetPriorityLandscapeWmsLayerGeoJson("#59ACFF", 0.2m, layerInitialVisibility)
            };

            return layerGeoJsons;
        }

        public static LayerGeoJson GetAllSimpleProjectLocations()
        {
            return new LayerGeoJson($"All {FieldDefinition.ProjectLocation.GetFieldDefinitionLabelPluralized()} - Simple", FirmaWebConfiguration.WebMapServiceUrl,
                FirmaWebConfiguration.GetAllProjectLocationsSimpleWmsLayerName(), "orange", .2m,
                LayerInitialVisibility.Hide);
        }

        public static LayerGeoJson GetAllDetailedProjectLocations()
        {
            return new LayerGeoJson($"All {FieldDefinition.ProjectLocation.GetFieldDefinitionLabelPluralized()} - Detail", FirmaWebConfiguration.WebMapServiceUrl,
                FirmaWebConfiguration.GetAllProjectLocationsDetailedWmsLayerName(), "orange", .2m,
                LayerInitialVisibility.Hide, "/Content/leaflet/images/washington_location_detailed.png");
        }

        public static LayerGeoJson GetAllProjectTreatments()
        {
            return new LayerGeoJson($"All Project Treatment Areas", FirmaWebConfiguration.WebMapServiceUrl,
                FirmaWebConfiguration.GetAllProjectTreatmentAreasWmsLayerName(), "orange", .2m,
                LayerInitialVisibility.Hide, "/Content/leaflet/images/washington_project_treatment.png");
        }

        public static LayerGeoJson GetWashingtonCountyLayer()
        {
            return new LayerGeoJson($"Washington Counties", FirmaWebConfiguration.WebMapServiceUrl,
                FirmaWebConfiguration.GetWashingtonCountyWmsLayerName(), "orange", .2m,
                LayerInitialVisibility.Hide, "/Content/leaflet/images/washington_county.png");
        }

        public static LayerGeoJson GetWashingtonLegislativeDistrictLayer()
        {
            return new LayerGeoJson($"Washington Legislative Districts", FirmaWebConfiguration.WebMapServiceUrl,
                FirmaWebConfiguration.GetWashingtonLegislativeDistrictWmsLayerName(), "orange", .2m,
                LayerInitialVisibility.Hide, "/Content/leaflet/images/washington_legislative_district.png");
        }

        public static List<LayerGeoJson> GetProjectLocationSimpleMapLayer(IProject project)
        {
            var layerGeoJsons = new List<LayerGeoJson>();
            if (project.ProjectLocationPoint != null)
            {
                layerGeoJsons.Add(new LayerGeoJson($"{FieldDefinition.ProjectLocation.GetFieldDefinitionLabel()} - Simple",
                    new FeatureCollection(new List<Feature>
                    {
                        DbGeometryToGeoJsonHelper.FromDbGeometry(project.ProjectLocationPoint)
                    }),
                    "#838383", 1, LayerInitialVisibility.Show));
            }
            return layerGeoJsons;
        }

        public static List<LayerGeoJson> GetProjectLocationSimpleAndDetailedMapLayers(IProject project)
        {
            var layerGeoJsons = new List<LayerGeoJson>();
            if (project.ProjectLocationPoint != null)
            {
                layerGeoJsons.AddRange(GetProjectLocationSimpleMapLayer(project));                
            }
            var detailedLocationGeoJsonFeatureCollection = project.AllDetailedLocationsToGeoJsonFeatureCollection();
            if (detailedLocationGeoJsonFeatureCollection.Features.Any())
            {
                layerGeoJsons.Add(new LayerGeoJson($"{FieldDefinition.ProjectLocation.GetFieldDefinitionLabel()} - Detail", detailedLocationGeoJsonFeatureCollection, "#838383", 1, LayerInitialVisibility.Show));
            }
            return layerGeoJsons;
        }
    }
}
