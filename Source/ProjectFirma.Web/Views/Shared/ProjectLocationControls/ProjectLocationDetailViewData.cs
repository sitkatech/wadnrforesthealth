/*-----------------------------------------------------------------------
<copyright file="ProjectLocationDetailViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views;

namespace ProjectFirma.Web.Views.Shared.ProjectLocationControls
{
    public class ProjectLocationDetailViewData : FirmaUserControlViewData
    {
        public int ProjectID { get; }
        public bool HasProjectLocationPoint { get; }
        public MapInitJson MapInitJson { get; }
        public LayerGeoJson EditableLayerGeoJson { get; }
        public string UploadGisFileUrl { get; }
        public string MapFormID { get; }
        public string SaveFeatureCollectionUrl { get; }
        public int AnnotationMaxLength { get; }
        public string SimplePointMarkerImg { get; }

        //new parameters from java/kevin
        public AngularViewDataForProjectLocationTypes AngularViewData { get; }
        public string PostUrl { get; }
        public string UploadGisUrl { get; }
        public string BasicsPageUrl { get; }
        private ProjectLocationDetailGridSpec ProjectLocationDetailGridSpec { get; }
        private string ProjectLocationDetailGridDataUrl { get; }



        public ProjectLocationDetailViewData(int projectID, MapInitJson mapInitJson, LayerGeoJson editableLayerGeoJson, string uploadGisFileUrl, string mapFormID, string saveFeatureCollectionUrl, int annotationMaxLength, bool hasProjectLocationPoint)
        {
            ProjectID = projectID;
            MapInitJson = mapInitJson;
            EditableLayerGeoJson = editableLayerGeoJson;
            UploadGisFileUrl = uploadGisFileUrl;
            MapFormID = mapFormID;
            SaveFeatureCollectionUrl = saveFeatureCollectionUrl;
            AnnotationMaxLength = annotationMaxLength;
            HasProjectLocationPoint = hasProjectLocationPoint;

            SimplePointMarkerImg = "https://api.tiles.mapbox.com/v3/marker/pin-s-marker+838383.png";
        }
    }

    public class AngularViewDataForProjectLocationTypes
    {
        public MapInitJson ProjectLocationMapInitJson { get; set; }
        public List<ProjectLocationType> ProjectLocationTypeJsons { get; set; }

        public int ProjectID { get; set; }
        public LayerGeoJson ProjectLocationLayerGeoJson { get; set; }
        public bool IsInCompletedReview { get; set; }

        public AngularViewDataForProjectLocationTypes(MapInitJson projectLocationMapInitJson,
            List<ProjectLocationType> projectLocationTypeJsons, Models.Project project, LayerGeoJson projectLocationLayerGeoJson)
        {
            ProjectLocationMapInitJson = projectLocationMapInitJson;
            ProjectLocationTypeJsons = projectLocationTypeJsons;

            ProjectID = project.ProjectID;
            ProjectLocationLayerGeoJson = projectLocationLayerGeoJson;
            IsInCompletedReview = ProjectExtension.isCompletedReviewProject(project);
        }
    }
}
