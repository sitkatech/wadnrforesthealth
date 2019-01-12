/*-----------------------------------------------------------------------
<copyright file="EditTreatmentActivitysViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ApprovalUtilities.Utilities;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Project
{
    public class EditTreatmentActivitiesViewModel : FormViewModel, IValidatableObject
    {
 
        public int ProjectID { get; set; }
        public int TreatmentActivityID { get; set; }
        public int? TreatmentActivityContactID { get; set; }
        public DateTime TreatmentActivityStartDate { get; set; }
        public DateTime? TreatmentActivityEndDate { get; set; }
        public string TreatmentActivityProgramIndex { get; set; }
        public string TreatmentActivityProjectCode { get; set; }
        public int TreatmentActivityStatusID { get; set; }
        public string TreatmentActivityNotes { get; set; }
        public decimal TreatmentActivityFootprintAcres { get; set; }
        public decimal TreatmentActivityChippingAcres { get; set; }
        public decimal TreatmentActivityPruningAcres { get; set; }
        public decimal TreatmentActivityThinningAcres { get; set; }
        public decimal TreatmentActivityMasticationAcres { get; set; }
        public decimal TreatmentActivityGrazingAcres { get; set; }
        public decimal TreatmentActivityLopAndScatterAcres { get; set; }
        public decimal TreatmentActivityBiomassRemovalAcres { get; set; }
        public decimal TreatmentActivityHandPileAcres { get; set; }
        public decimal TreatmentActivityBroadcastBurnAcres { get; set; }
        public decimal TreatmentActivityHandPileBurnAcres { get; set; }
        public decimal TreatmentActivityMachinePileBurnAcres { get; set; }
        public decimal TreatmentActivityOtherTreatmentAcres { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditTreatmentActivitiesViewModel()
        {
        }

        public EditTreatmentActivitiesViewModel(Models.Project project,
            TreatmentActivity treatmentActivity)
        {
            ProjectID = project.ProjectID;
            TreatmentActivityID = TreatmentActivityID;
            TreatmentActivityContactID = treatmentActivity.TreatmentActivityContactID;
            TreatmentActivityStartDate = treatmentActivity.TreatmentActivityStartDate;
            TreatmentActivityEndDate = treatmentActivity.TreatmentActivityEndDate;
            TreatmentActivityProgramIndex = treatmentActivity.TreatmentActivityProgramIndex;
            TreatmentActivityProjectCode = treatmentActivity.TreatmentActivityProjectCode;
            TreatmentActivityStatusID = treatmentActivity.TreatmentActivityStatusID;
            TreatmentActivityNotes = treatmentActivity.TreatmentActivityNotes;
            TreatmentActivityFootprintAcres = treatmentActivity.TreatmentActivityFootprintAcres;
            TreatmentActivityChippingAcres = treatmentActivity.TreatmentActivityChippingAcres;
            TreatmentActivityPruningAcres = treatmentActivity.TreatmentActivityPruningAcres;
            TreatmentActivityThinningAcres = treatmentActivity.TreatmentActivityThinningAcres;
            TreatmentActivityMasticationAcres = treatmentActivity.TreatmentActivityMasticationAcres;
            TreatmentActivityGrazingAcres = treatmentActivity.TreatmentActivityGrazingAcres;
            TreatmentActivityLopAndScatterAcres = treatmentActivity.TreatmentActivityLopAndScatterAcres;
            TreatmentActivityBiomassRemovalAcres = treatmentActivity.TreatmentActivityBiomassRemovalAcres;
            TreatmentActivityHandPileAcres = treatmentActivity.TreatmentActivityHandPileAcres;
            TreatmentActivityBroadcastBurnAcres = treatmentActivity.TreatmentActivityBroadcastBurnAcres;
            TreatmentActivityHandPileBurnAcres = treatmentActivity.TreatmentActivityHandPileBurnAcres;
            TreatmentActivityMachinePileBurnAcres = treatmentActivity.TreatmentActivityMachinePileBurnAcres;
            TreatmentActivityOtherTreatmentAcres = treatmentActivity.TreatmentActivityOtherTreatmentAcres;

        }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TreatmentActivityEndDate != null && TreatmentActivityEndDate.Value < TreatmentActivityStartDate)
            {
                yield return new ValidationResult("End Date cannot be before Start Date");
            }
        }
    }
}
