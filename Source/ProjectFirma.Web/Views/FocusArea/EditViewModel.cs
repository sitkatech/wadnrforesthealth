/*-----------------------------------------------------------------------
<copyright file="EditViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using LtInfo.Common.Models;
using ProjectFirma.Web.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Linq;
using LtInfo.Common;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views.FocusArea
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {

        public int FocusAreaID { get; set; }

        [Required]
        [StringLength(Models.FocusArea.FieldLengths.FocusAreaName)]
        [DisplayName("Name")]
        public string FocusAreaName { get; set; }

        [Required]
        [DisplayName("Status")]
        public int FocusAreaStatusID { get; set; }

        [Required]
        [DisplayName("Region")]
        public int RegionID { get; set; }

        [DisplayName("Planned Footprint Acres")]
        public decimal? PlannedFootprintAcres { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(Models.FocusArea focusArea)
        {
            FocusAreaID = focusArea.FocusAreaID;
            FocusAreaName = focusArea.FocusAreaName;
            FocusAreaStatusID = focusArea.FocusAreaStatusID;
            RegionID = focusArea.RegionID;
            PlannedFootprintAcres = focusArea.PlannedFootprintAcres;
        }

        public void UpdateModel(Models.FocusArea focusArea)
        {
            focusArea.FocusAreaName = FocusAreaName;
            focusArea.FocusAreaStatusID = FocusAreaStatusID;
            focusArea.RegionID = RegionID;
            focusArea.FocusAreaID = FocusAreaID;
            focusArea.PlannedFootprintAcres = PlannedFootprintAcres;
        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (!string.IsNullOrEmpty(FocusAreaName))
            {
                var results = HttpRequestStorage.DatabaseEntities.FocusAreas.Where(x => x.FocusAreaName == FocusAreaName && x.FocusAreaID != FocusAreaID);
                if(results.Any())
                    validationResults.Add(new SitkaValidationResult<EditViewModel, string>("Focus Area Name must be unique. A Focus Area already exists with the name provided.", x => x.FocusAreaName));
            }

            return validationResults;
        }
    }
}
