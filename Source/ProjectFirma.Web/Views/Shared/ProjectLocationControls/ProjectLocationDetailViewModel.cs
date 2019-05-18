/*-----------------------------------------------------------------------
<copyright file="ProjectLocationDetailViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Shared.ProjectLocationControls
{
    public class ProjectLocationDetailViewModel : FormViewModel, IValidatableObject
    {

        public List<ProjectLocationJson> ProjectLocationJsons { get; set; }
        public List<ProjectLocationJson> ArcGisProjectLocationJsons { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public ProjectLocationDetailViewModel()
        {
            ProjectLocationJsons = new List<ProjectLocationJson>();
            ArcGisProjectLocationJsons = new List<ProjectLocationJson>();
        }

        public ProjectLocationDetailViewModel(ICollection<Models.ProjectLocation> projectLocations)
        {
            ProjectLocationJsons = projectLocations.Where(x => !x.ArcGisObjectID.HasValue).Select(x => new ProjectLocationJson(x)).ToList();
            ArcGisProjectLocationJsons = projectLocations.Where(x => x.ArcGisObjectID.HasValue).Select(x => new ProjectLocationJson(x)).ToList();
        }

        public ProjectLocationDetailViewModel(ICollection<Models.ProjectLocationUpdate> projectLocationUpdates)
        {
            ProjectLocationJsons = projectLocationUpdates.Where(x => !x.ArcGisObjectID.HasValue).Select(x => new ProjectLocationJson(x)).ToList();
            ArcGisProjectLocationJsons = projectLocationUpdates.Where(x => x.ArcGisObjectID.HasValue).Select(x => new ProjectLocationJson(x)).ToList();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            foreach (var plj in ProjectLocationJsons)
            {
                if (LtInfo.Common.GeneralUtility.IsNullOrEmptyOrOnlyWhitespace(plj.ProjectLocationName))
                {
                    results.Add(new SitkaValidationResult<ProjectLocationJson, string>("Project Location Name must not be blank.", x => x.ProjectLocationName));
                }

                if (plj.ProjectLocationTypeID == -1)
                {
                    results.Add(new SitkaValidationResult<ProjectLocationJson, int>("Project Location Type must be selected.", x => x.ProjectLocationTypeID));
                }
            }

            

            return results;
        }
    }

    public class ProjectLocationJson
    {
        public ProjectLocationJson()
        {
        }

        public ProjectLocationJson(Models.ProjectLocation x)
        {
            ProjectLocationName = x.ProjectLocationName;
            ProjectLocationNotes = x.ProjectLocationNotes;
            ProjectLocationTypeID = x.ProjectLocationTypeID;
            ProjectLocationTypeName = x.ProjectLocationType.ProjectLocationTypeDisplayName;
            ProjectLocationFeatureType = x.ProjectLocationGeometry.SpatialTypeName.Replace("LineString", "Line");
            ProjectLocationID = x.ProjectLocationID;
            ProjectLocationGeometryWellKnownText = x.ProjectLocationGeometry.AsText();
            IsGeometryFromArcGis = x.ArcGisObjectID.HasValue;
            ArcGisObjectID = x.ArcGisObjectID;
            ArcGisGlobalID = x.ArcGisGlobalID;
        }

        public ProjectLocationJson(Models.ProjectLocationUpdate x)
        {
            ProjectLocationName = x.ProjectLocationUpdateName;
            ProjectLocationNotes = x.ProjectLocationUpdateNotes;
            ProjectLocationTypeID = x.ProjectLocationTypeID;
            ProjectLocationTypeName = x.ProjectLocationType.ProjectLocationTypeDisplayName;
            ProjectLocationFeatureType = x.ProjectLocationUpdateGeometry.SpatialTypeName.Replace("LineString", "Line");
            ProjectLocationID = x.ProjectLocationUpdateID;
            ProjectLocationGeometryWellKnownText = x.ProjectLocationUpdateGeometry.AsText();
            IsGeometryFromArcGis = x.ArcGisObjectID.HasValue;
            ArcGisObjectID = x.ArcGisObjectID;
            ArcGisGlobalID = x.ArcGisGlobalID;
        }

        public string ProjectLocationGeometryWellKnownText { get; set; }
        public int ProjectLocationID { get; set; }
        public string ProjectLocationFeatureType { get; set; }
        public int ProjectLocationTypeID { get; set; }
        public string ProjectLocationName { get; set; }
        public string ProjectLocationTypeName { get; set; }
        public string ProjectLocationNotes { get; set; }
        public int? ArcGisObjectID { get; set; }
        public string ArcGisGlobalID { get; set; }
        public bool IsGeometryFromArcGis { get; set; }
    }
}
