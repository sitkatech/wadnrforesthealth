/*-----------------------------------------------------------------------
<copyright file="EditTreatmentUpdateViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

namespace ProjectFirma.Web.Views.TreatmentUpdate
{
    public class EditTreatmentUpdateViewModel : FormViewModel, IValidatableObject
    {

        public int TreatmentUpdateID { get; set; }

        public int ProjectUpdateBatchID { get; set; }

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
        public int ProjectLocationUpdateID { get; set; }

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
        public EditTreatmentUpdateViewModel()
        {
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
        }

        public EditTreatmentUpdateViewModel(Models.TreatmentUpdate treatmentUpdate)
        {
            StartDate = treatmentUpdate?.TreatmentStartDate ?? DateTime.Today;
            EndDate = treatmentUpdate?.TreatmentEndDate ?? DateTime.Today;
            FootprintAcres = (treatmentUpdate?.TreatmentFootprintAcres ?? 0).ToDecimalFormatted();
            TreatedAcres = (treatmentUpdate?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();
            if (treatmentUpdate.ProjectLocationUpdateID.HasValue)
            {
                ProjectLocationUpdateID = treatmentUpdate.ProjectLocationUpdateID.Value;
            }

            CostPerAcre = treatmentUpdate?.CostPerAcre;
            TreatmentDetailedActivityTypeID = treatmentUpdate.TreatmentDetailedActivityTypeID;
            TreatmentTypeID = treatmentUpdate.TreatmentTypeID;
            TreatmentCodeID = treatmentUpdate.TreatmentCodeID;
            Notes = treatmentUpdate.TreatmentNotes;
            TreatmentUpdateID = treatmentUpdate.TreatmentUpdateID;
            ProjectUpdateBatchID = treatmentUpdate.ProjectUpdateBatchID;

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate != DateTime.MinValue && EndDate < StartDate)
            {
                yield return new ValidationResult("End Date cannot be before Start Date");
            }
        }

        public void UpdateModel(Models.TreatmentUpdate treatmentUpdate)
        {
            treatmentUpdate.TreatmentFootprintAcres = FootprintAcres;
            treatmentUpdate.TreatmentTreatedAcres = TreatedAcres;
            treatmentUpdate.CostPerAcre = CostPerAcre;
            treatmentUpdate.TreatmentStartDate = StartDate;
            treatmentUpdate.TreatmentEndDate = EndDate;
            treatmentUpdate.TreatmentNotes = Notes;

            treatmentUpdate.ProjectLocationUpdateID = ProjectLocationUpdateID;
            treatmentUpdate.TreatmentTypeID = TreatmentTypeID;
            treatmentUpdate.TreatmentCodeID = TreatmentCodeID;
            treatmentUpdate.TreatmentDetailedActivityTypeID = TreatmentDetailedActivityTypeID;
            treatmentUpdate.ProjectUpdateBatchID = ProjectUpdateBatchID;
        }

    }

}