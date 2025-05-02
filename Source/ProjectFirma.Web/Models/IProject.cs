/*-----------------------------------------------------------------------
<copyright file="IProject.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;

namespace ProjectFirma.Web.Models
{
    public interface IProject
    {
        int EntityID { get; }
        int ProjectTypeID { get; }
        DbGeometry ProjectLocationPoint { get; set; }
        string DisplayName { get; }
        ProjectLocationSimpleType ProjectLocationSimpleType { get; }
        int ProjectLocationSimpleTypeID { get; set; }
        string ProjectLocationNotes { get; set; }
        DateTime? PlannedDate { get; }
        int? GetImplementationStartYear();
        int? GetCompletionYear();
        ProjectStage ProjectStage { get; }
        decimal? EstimatedTotalCost { get; }
        IEnumerable<IProjectLocation> GetProjectLocationDetails();
        DbGeometry GetDefaultBoundingBox();
        IEnumerable<DNRUplandRegion> GetProjectRegions();
        IEnumerable<PriorityLandscape> GetProjectPriorityLandscapes();
        IEnumerable<County> GetProjectCounties();
        GeoJSON.Net.Feature.FeatureCollection AllDetailedLocationsToGeoJsonFeatureCollection();
        GeoJSON.Net.Feature.FeatureCollection DetailedLocationsByTypeToGeoJsonFeatureCollection(ProjectLocationType projectLocationType);
        GeoJSON.Net.Feature.FeatureCollection SimpleLocationToGeoJsonFeatureCollection(bool addProjectProperties);        
        
    }
}
