/*-----------------------------------------------------------------------
<copyright file="EditTreatmentViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Microsoft.Ajax.Utilities;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Treatment
{
    public class EditTreatmentViewModel : FormViewModel, IValidatableObject
    {

        public int TreatmentID { get; set; }

        public int ProjectID { get; set; }


        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.TreatmentDetailedActivityType)]
        public int TreatmentDetailedActivityTypeID { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.TreatmentType)]
        public int TreatmentTypeID { get; set; }

        [Required]
        [DisplayName("Project Location - Treatment Area")]
        public int ProjectLocationID { get; set; }





        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareStartDate)]
        [Required]
        public DateTime StartDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareEndDate)]
        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(Models.Treatment.FieldLengths.TreatmentNotes)]
        public string Notes { get; set; }




        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareFootprintAcres)]
        public decimal FootprintAcres { get; set; }

        [DisplayName("Treated Acres")]
        public decimal TreatedAcres { get; set; }




        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditTreatmentViewModel()
        {
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
        }

        public EditTreatmentViewModel(Models.Treatment treatment)
        {
            StartDate = treatment?.TreatmentStartDate ?? DateTime.Today;
            EndDate = treatment?.TreatmentEndDate ?? DateTime.Today;
            FootprintAcres = (treatment?.TreatmentFootprintAcres ?? 0).ToDecimalFormatted();
            TreatedAcres = (treatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();
            if (treatment.ProjectLocationID.HasValue)
            {
                ProjectLocationID = treatment.ProjectLocationID.Value;
            }

            TreatmentDetailedActivityTypeID = treatment.TreatmentDetailedActivityTypeID;
            TreatmentTypeID = treatment.TreatmentTypeID;
            Notes = treatment.TreatmentNotes;
            TreatmentID = treatment.TreatmentID;
            ProjectID = treatment.ProjectID;

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate != DateTime.MinValue && EndDate < StartDate)
            {
                yield return new ValidationResult("End Date cannot be before Start Date");
            }



        }



        public void UpdateModel(Models.Treatment treatment)
        {
            treatment.TreatmentFootprintAcres = FootprintAcres;
            treatment.TreatmentTreatedAcres = TreatedAcres;
            treatment.TreatmentStartDate = StartDate;
            treatment.TreatmentEndDate = EndDate;
            treatment.TreatmentNotes = Notes;

            treatment.ProjectLocationID = ProjectLocationID;
            treatment.TreatmentTypeID = TreatmentTypeID;
            treatment.TreatmentDetailedActivityTypeID = TreatmentDetailedActivityTypeID;
            treatment.ProjectID = ProjectID;


        }




    }

}