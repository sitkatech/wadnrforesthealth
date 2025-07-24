﻿/*-----------------------------------------------------------------------
<copyright file="LocationSimpleViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using LtInfo.Common.DbSpatial;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Shared.ProjectLocationControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;

namespace ProjectFirma.Web.Views.ProjectUpdate
{
    public class LocationSimpleViewModel : ProjectLocationSimpleViewModel
    {
        [DisplayName("Review Comments")]
        [StringLength(ProjectUpdateBatch.FieldLengths.LocationSimpleComment)]
        public string Comments { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public LocationSimpleViewModel()
        {
        }

        public LocationSimpleViewModel(DbGeometry projectLocationPoint, ProjectLocationSimpleTypeEnum projectLocationSimpleType, string projectLocationNotes, string comments)
            : base(projectLocationPoint, projectLocationSimpleType, projectLocationNotes)
        {
            Comments = comments;
        }

        public void UpdateModelBatch(ProjectUpdateBatch projectUpdateBatch)
        {
            var project = projectUpdateBatch.ProjectUpdate;
            project.ProjectLocationSimpleTypeID = Models.ProjectLocationSimpleType.ToType(ProjectLocationSimpleType).ProjectLocationSimpleTypeID;
            switch (ProjectLocationSimpleType)
            {
                case ProjectLocationSimpleTypeEnum.PointOnMap:

                    project.ProjectLocationPoint = ProjectLocationPointX.HasValue && ProjectLocationPointY.HasValue
                        ? DbSpatialHelper.MakeDbGeometryFromCoordinates(ProjectLocationPointX.Value, ProjectLocationPointY.Value, MapInitJson.CoordinateSystemId)
                        : null;
                    break;
                case ProjectLocationSimpleTypeEnum.None:
                    project.ProjectLocationPoint = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            project.ProjectLocationNotes = ProjectLocationNotes;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
