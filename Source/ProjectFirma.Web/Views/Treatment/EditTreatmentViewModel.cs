/*-----------------------------------------------------------------------
<copyright file="EditTreatmentViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using LtInfo.Common;
using LtInfo.Common.Models;
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

        [FieldDefinitionDisplay(FieldDefinitionEnum.TreatmentCode)]
        public int? TreatmentCodeID { get; set; }

        [Required]
        [DisplayName("Project Location - Treatment Area")]
        public int ProjectLocationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.TreatmentStartDate)]
        [Required]
        public DateTime StartDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.TreatmentEndDate)]
        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(Models.Treatment.FieldLengths.TreatmentNotes)]
        public string Notes { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FootprintAcres)]
        public decimal FootprintAcres { get; set; }

        [DisplayName("Treated Acres")]
        public decimal TreatedAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.TreatmentCostPerAcre)]
        public Money? CostPerAcre { get; set; }


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

            CostPerAcre = treatment?.CostPerAcre;
            TreatmentDetailedActivityTypeID = treatment.TreatmentDetailedActivityTypeID;
            TreatmentTypeID = treatment.TreatmentTypeID;
            TreatmentCodeID = treatment.TreatmentCodeID;
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
            treatment.CostPerAcre = CostPerAcre;
            treatment.TreatmentStartDate = StartDate;
            treatment.TreatmentEndDate = EndDate;
            treatment.TreatmentNotes = Notes;

            treatment.ProjectLocationID = ProjectLocationID;
            treatment.TreatmentTypeID = TreatmentTypeID;
            treatment.TreatmentCodeID = TreatmentCodeID;
            treatment.TreatmentDetailedActivityTypeID = TreatmentDetailedActivityTypeID;
            treatment.ProjectID = ProjectID;
        }

    }

}