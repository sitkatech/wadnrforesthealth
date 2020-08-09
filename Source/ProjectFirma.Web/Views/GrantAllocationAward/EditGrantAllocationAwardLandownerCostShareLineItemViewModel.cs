/*-----------------------------------------------------------------------
<copyright file="EditGrantAllocationAwardLandownerCostShareLineItemViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.GrantAllocationAward
{
    public class EditGrantAllocationAwardLandownerCostShareLineItemViewModel : FormViewModel, IValidatableObject
    {
        public int GrantAllocationAwardLandownerCostShareLineItemID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAward)]
        [Required]
        public int? GrantAllocationAwardID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.Project)]
        [Required]
        public int ProjectID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareStatus)]
        [Required]
        public int StatusID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareStartDate)]
        [Required]
        public DateTime StartDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareEndDate)]
        [Required]
        public DateTime EndDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareFootprintAcres)]
        public decimal FootprintAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareChippingAcres)]
        public decimal ChippingAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostSharePruningAcres)]
        public decimal PruningAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareThinningAcres)]
        public decimal ThinningAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareMasticationAcres)]
        public decimal MasticationAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareGrazingAcres)]
        public decimal GrazingAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareLopAndScatterAcres)]
        public decimal LopAndScatterAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareBiomassRemovalAcres)]
        public decimal BiomassRemovalAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareHandPileAcres)]
        public decimal HandPileAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareBroadcastBurnAcres)]
        public decimal BroadcastBurnAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareHandPileBurnAcres)]
        public decimal HandPileBurnAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareMachinePileBurnAcres)]
        public decimal MachinePileBurnAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareOtherTreatmentAcres)]
        public decimal OtherTreatmentAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareSlashAcres)]
        public decimal SlashAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareAllocatedAmount)]
        public Money AllocatedAmount { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareTotalCost)]
        public Money TotalCost { get; set; }

        [StringLength(Models.GrantAllocationAwardLandownerCostShareLineItem.FieldLengths.GrantAllocationAwardLandownerCostShareLineItemNotes)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareNotes)]
        public string Notes { get; set; }

        

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditGrantAllocationAwardLandownerCostShareLineItemViewModel()
        {
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
        }

        public EditGrantAllocationAwardLandownerCostShareLineItemViewModel(Models.GrantAllocationAwardLandownerCostShareLineItem grantAllocationAwardLandownerCostShareLineItem)
        {
            GrantAllocationAwardLandownerCostShareLineItemID = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemID;
            GrantAllocationAwardID = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardID;
            var treatment = grantAllocationAwardLandownerCostShareLineItem.Treatments.SingleOrDefault();
           
            StartDate = treatment.TreatmentStartDate ?? DateTime.Today;
            EndDate = treatment.TreatmentEndDate ?? DateTime.Today;

            ProjectID = treatment.ProjectID;
            StatusID = grantAllocationAwardLandownerCostShareLineItem.LandownerCostShareLineItemStatusID;

            FootprintAcres = (treatment?.TreatmentFootprintAcres ?? 0 ).ToDecimalFormatted();
            SlashAcres = (treatment?.TreatmentSlashAcres ?? 0).ToDecimalFormatted();
            ChippingAcres = (treatment?.TreatmentChippingAcres ?? 0).ToDecimalFormatted();
            PruningAcres = (treatment?.TreatmentPruningAcres ?? 0).ToDecimalFormatted();
            ThinningAcres = (treatment?.TreatmentThinningAcres ?? 0).ToDecimalFormatted();
            MasticationAcres = (treatment?.TreatmentMasticationAcres ?? 0).ToDecimalFormatted();
            GrazingAcres = (treatment?.TreatmentGrazingAcres ?? 0).ToDecimalFormatted();
            LopAndScatterAcres = (treatment?.TreatmentLopAndScatterAcres ?? 0).ToDecimalFormatted();
            BiomassRemovalAcres = (treatment?.TreatmentBiomassRemovalAcres ?? 0).ToDecimalFormatted();
            HandPileAcres = (treatment?.TreatmentHandPileAcres ?? 0).ToDecimalFormatted();
            BroadcastBurnAcres = (treatment?.TreatmentBroadcastBurnAcres ?? 0).ToDecimalFormatted();
            HandPileBurnAcres = (treatment?.TreatmentHandPileBurnAcres ?? 0).ToDecimalFormatted();
            MachinePileBurnAcres = (treatment?.TreatmentMachinePileBurnAcres ?? 0).ToDecimalFormatted();
            OtherTreatmentAcres = (treatment?.TreatmentOtherTreatmentAcres ?? 0).ToDecimalFormatted();

            TotalCost = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemTotalCost;
            AllocatedAmount = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemAllocatedAmount;
            Notes = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemNotes;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate != DateTime.MinValue && EndDate < StartDate)
            {
                yield return new ValidationResult("End Date cannot be before Start Date");
            }

        }



        public void UpdateModel(Models.GrantAllocationAwardLandownerCostShareLineItem grantAllocationAwardLandownerCostShareLineItem)
        {
            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemID = GrantAllocationAwardLandownerCostShareLineItemID;
            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardID = GrantAllocationAwardID;

            grantAllocationAwardLandownerCostShareLineItem.ProjectID = ProjectID;
            grantAllocationAwardLandownerCostShareLineItem.LandownerCostShareLineItemStatusID = StatusID;

            var treatment = grantAllocationAwardLandownerCostShareLineItem.Treatments.SingleOrDefault();
            if (treatment == null)
            {
                treatment = new Treatment(grantAllocationAwardLandownerCostShareLineItem.Project.ProjectID, FootprintAcres,ChippingAcres, PruningAcres, ThinningAcres, MasticationAcres,GrazingAcres
                ,LopAndScatterAcres,BiomassRemovalAcres,HandPileAcres,BroadcastBurnAcres
                , HandPileBurnAcres, MachinePileBurnAcres, OtherTreatmentAcres, SlashAcres, TreatmentType.Other.TreatmentTypeID);

                treatment.GrantAllocationAwardLandownerCostShareLineItemID =
                    grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemID;
                treatment.GrantAllocationAwardLandownerCostShareLineItem =
                    grantAllocationAwardLandownerCostShareLineItem;
                treatment.Project = grantAllocationAwardLandownerCostShareLineItem.Project;
                grantAllocationAwardLandownerCostShareLineItem.Treatments.Add(treatment);
            }

            treatment.TreatmentStartDate = StartDate;
            treatment.TreatmentEndDate = EndDate;

            treatment.TreatmentFootprintAcres = FootprintAcres;
            treatment.TreatmentChippingAcres = ChippingAcres;
            treatment.TreatmentPruningAcres = PruningAcres;
            treatment.TreatmentThinningAcres = ThinningAcres;
            treatment.TreatmentMasticationAcres = MasticationAcres;
            treatment.TreatmentGrazingAcres = GrazingAcres;
            treatment.TreatmentLopAndScatterAcres = LopAndScatterAcres;
            treatment.TreatmentBiomassRemovalAcres = BiomassRemovalAcres;
            treatment.TreatmentHandPileAcres = HandPileAcres;
            treatment.TreatmentBroadcastBurnAcres = BroadcastBurnAcres;
            treatment.TreatmentHandPileBurnAcres = HandPileBurnAcres;
            treatment.TreatmentMachinePileBurnAcres = MachinePileBurnAcres;
            treatment.TreatmentOtherTreatmentAcres = OtherTreatmentAcres;
            treatment.TreatmentSlashAcres = SlashAcres;

            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemAllocatedAmount = AllocatedAmount;
            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemTotalCost = TotalCost;
            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemNotes = Notes;

        }
    }

}