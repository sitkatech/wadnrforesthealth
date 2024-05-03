/*-----------------------------------------------------------------------
<copyright file="ProjectLocationDetailViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using GeoJSON.Net.Feature;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views;

namespace ProjectFirma.Web.Views.Shared.ProjectLocationControls
{
    public class ProjectLocationDetailViewData : FirmaUserControlViewData
    {
        public int ProjectId { get; }
        public bool HasProjectLocationPoint { get; }
        public MapInitJson InitJson { get; }
        public string UploadGisFileUrl { get; }
        public string MapFormId { get; }
        public string SaveFeatureCollectionUrl { get; }
        public int AnnotationMaxLength { get; }
        public string SimplePointMarkerImg { get; }
        public ProjectLocationDetailViewDataForAngular ViewDataForAngular { get; }
        
        
        public ProjectLocationDetailViewData(int projectID, MapInitJson mapInitJson, LayerGeoJson editableLayerGeoJson, LayerGeoJson arcGisLayerGeoJson, string uploadGisFileUrl, string mapFormID, string saveFeatureCollectionUrl, int annotationMaxLength, bool hasProjectLocationPoint)
        {
            ProjectId = projectID;
            InitJson = mapInitJson;
            UploadGisFileUrl = uploadGisFileUrl;
            MapFormId = mapFormID;
            SaveFeatureCollectionUrl = saveFeatureCollectionUrl;
            AnnotationMaxLength = annotationMaxLength;
            HasProjectLocationPoint = hasProjectLocationPoint;
            ViewDataForAngular = new ProjectLocationDetailViewDataForAngular(mapInitJson, editableLayerGeoJson, arcGisLayerGeoJson, annotationMaxLength);
            SimplePointMarkerImg = $"https://api.tiles.mapbox.com/v3/marker/pin-s-marker+838383.png?access_token={FirmaWebConfiguration.MapBoxApiKey}";
        }
    }

    public class ProjectLocationDetailViewDataForAngular
    {
        public MapInitJson MapInitJson { get; }
        public LayerGeoJson EditableLayerGeoJson { get; }
        public LayerGeoJson ArcGisLayerGeoJson { get; }
        public List<string> GeospatialAreaMapServiceLayerNames { get; }
        public int AnnotationMaxLength { get; }
        public List<ProjectLocationTypeJson> ProjectLocationTypeJsons { get; }

        public ProjectLocationDetailViewDataForAngular(MapInitJson mapInitJson, LayerGeoJson editableLayerGeoJson, LayerGeoJson arcGisLayerGeoJson, int annotationMaxLength)
        {
            MapInitJson = mapInitJson;
            EditableLayerGeoJson = editableLayerGeoJson;
            ArcGisLayerGeoJson = arcGisLayerGeoJson;
            AnnotationMaxLength = annotationMaxLength;
            GeospatialAreaMapServiceLayerNames = FirmaWebConfiguration.GetWmsLayerNames();
            ProjectLocationTypeJsons = ProjectLocationType.GetAllProjectLocationTypeJsons();
        }
    }

    

}
