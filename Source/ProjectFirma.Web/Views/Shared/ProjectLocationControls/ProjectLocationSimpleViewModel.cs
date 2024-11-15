﻿/*-----------------------------------------------------------------------
<copyright file="EditProjectLocationSimpleViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ProjectFirma.Web.Views.Shared.ProjectLocationControls
{
    public class ProjectLocationSimpleViewModel : FormViewModel, IValidatableObject
    {
        public double? ProjectLocationPointX { get; set; }
        public double? ProjectLocationPointY { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectLocationSimpleTypeEnum ProjectLocationSimpleType { get; set; }

        [DisplayName("Notes")]
        [StringLength(Models.Project.FieldLengths.ProjectLocationNotes)]
        public string ProjectLocationNotes { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public ProjectLocationSimpleViewModel()
        {
        }

        public ProjectLocationSimpleViewModel(DbGeometry projectLocationPoint, ProjectLocationSimpleTypeEnum projectLocationSimpleType, string projectLocationNotes)
        {
            ProjectLocationSimpleType = projectLocationSimpleType;
            if (projectLocationPoint != null)
            {
                ProjectLocationPointX = projectLocationPoint.XCoordinate;
                ProjectLocationPointY = projectLocationPoint.YCoordinate;
            }
            else
            {
                ProjectLocationPointX = null;
                ProjectLocationPointY = null;
            }
            ProjectLocationNotes = projectLocationNotes;
        }

        public virtual void UpdateModel(IProject project)
        {
            project.ProjectLocationSimpleTypeID = Models.ProjectLocationSimpleType.ToType(ProjectLocationSimpleType).ProjectLocationSimpleTypeID;
            switch (ProjectLocationSimpleType)
            {                
                case ProjectLocationSimpleTypeEnum.PointOnMap:
                    project.ProjectLocationPoint = DbSpatialHelper.MakeDbGeometryFromCoordinates(ProjectLocationPointX.Value, ProjectLocationPointY.Value, MapInitJson.CoordinateSystemId);
                    break;
                case ProjectLocationSimpleTypeEnum.None:
                    project.ProjectLocationPoint = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            project.ProjectLocationNotes = ProjectLocationNotes;
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return GetValidationResults();
        }

        public IEnumerable<ValidationResult> GetValidationResults()
        {
            var errors = new List<ValidationResult>();

            if (ProjectLocationSimpleType == ProjectLocationSimpleTypeEnum.PointOnMap && (!ProjectLocationPointX.HasValue || !ProjectLocationPointY.HasValue))
            {
                errors.Add(new SitkaValidationResult<ProjectLocationSimpleViewModel, double?>("Please specify a point on the map", x => x.ProjectLocationPointX));
            }

            if (ProjectLocationSimpleType == ProjectLocationSimpleTypeEnum.None && string.IsNullOrWhiteSpace(ProjectLocationNotes))
            {
                errors.Add(
                    new SitkaValidationResult<ProjectLocationSimpleViewModel, string>(
                        $"If a location point or general {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} area is not available, explanatory information in the Notes section is required.",
                        x => x.ProjectLocationNotes));
            }

            return errors;
        }
    }
}
