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

        [DisplayName("Location")]
        public DbGeometry FocusAreaLocation { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(Models.FocusArea focusArea)
        {
            FocusAreaName = focusArea.FocusAreaName;
            FocusAreaStatusID = focusArea.FocusAreaStatusID;
        }

        public void UpdateModel(Models.FocusArea focusArea)
        {
            focusArea.FocusAreaName = FocusAreaName;
            focusArea.FocusAreaStatusID = FocusAreaStatusID;
            focusArea.FocusAreaLocation = FocusAreaLocation;

        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

                return validationResults;
        }
    }
}
