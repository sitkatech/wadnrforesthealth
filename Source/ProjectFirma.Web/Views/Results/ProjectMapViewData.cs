/*-----------------------------------------------------------------------
<copyright file="ProjectMapViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Web.Mvc;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Map;
using ProjectFirma.Web.Views.Shared.ProjectLocationControls;

namespace ProjectFirma.Web.Views.Results
{
    public class ProjectMapViewData : FirmaViewData
    {
        public ProjectLocationsMapInitJson ProjectLocationsMapInitJson { get; }
        public ProjectLocationsMapViewData ProjectLocationsMapViewData { get; }
        public Dictionary<ProjectLocationFilterTypeSimple, IEnumerable<SelectListItem>> ProjectLocationFilterTypesAndValues { get; }
        public string ProjectLocationsUrl { get; }
        public string FilteredProjectsWithLocationAreasUrl { get; }
        public ProjectMapViewDataForAngular ProjectMapViewDataForAngular { get; }

        public string GeocodeAddressUrl { get; }

        public ProjectMapViewData(Person currentPerson, Models.FirmaPage firmaPage, ProjectLocationsMapInitJson projectLocationsMapInitJson, ProjectLocationsMapViewData projectLocationsMapViewData, Dictionary<ProjectLocationFilterTypeSimple, IEnumerable<SelectListItem>> projectLocationFilterTypesAndValues, string projectLocationsUrl, string filteredProjectsWithLocationAreasUrl, List<ProjectMapLocationJson> projectMapLocationJsons) : base(currentPerson, firmaPage)
        {
            PageTitle = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Map";
            ProjectLocationsMapInitJson = projectLocationsMapInitJson;
            ProjectLocationFilterTypesAndValues = projectLocationFilterTypesAndValues;
            ProjectLocationsMapViewData = projectLocationsMapViewData;
            ProjectLocationsUrl = projectLocationsUrl;
            FilteredProjectsWithLocationAreasUrl = filteredProjectsWithLocationAreasUrl;
            ProjectMapViewDataForAngular = new ProjectMapViewDataForAngular(projectLocationsMapInitJson, projectLocationsMapInitJson.ProjectLocationsLayerGeoJson, projectMapLocationJsons);
            GeocodeAddressUrl = SitkaRoute<ResultsController>.BuildUrlFromExpression(t => t.GeocodeAddress(null));
        }
    }
    public class ProjectMapViewDataForAngular
    {
        public MapInitJson MapInitJson { get; }
        public LayerGeoJson LayerGeoJson { get; }
        public List<ProjectMapLocationJson> ProjectMapLocationJsons { get; set; }

        public ProjectMapViewDataForAngular(MapInitJson mapInitJson, LayerGeoJson layerGeoJson, List<ProjectMapLocationJson> projectMapLocationJsons)
        {
            MapInitJson = mapInitJson;
            LayerGeoJson = layerGeoJson;
            ProjectMapLocationJsons = projectMapLocationJsons;

        }
    }

    public class ProjectMapLocationJson
    {
        public ProjectMapLocationJson()
        {
        }

        public ProjectMapLocationJson(Models.Project x)
        {
            ProjectMapLocationName = x.ProjectName;
            ProjectMapLocationNotes = x.ProjectLocationNotes;
            ProjectMapLocationFeatureType = x.ProjectLocationPoint.SpatialTypeName;
            ProjectMapLocationGeometryWellKnownText = x.ProjectLocationPoint.AsText();
            ProjectMapProjectID = x.ProjectID;
            ProjectMapStageName = x.ProjectStage?.ProjectStageName;
        }

        public string ProjectMapLocationGeometryWellKnownText { get; set; }
        public string ProjectMapLocationFeatureType { get; set; }
        public string ProjectMapLocationName { get; set; }
        public string ProjectMapLocationNotes { get; set; }
        public int ProjectMapProjectID { get; set; }
        public string ProjectMapStageName { get; set; }
    }

}
