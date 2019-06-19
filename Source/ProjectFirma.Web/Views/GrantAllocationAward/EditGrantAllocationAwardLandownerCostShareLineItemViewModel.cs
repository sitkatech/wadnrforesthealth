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
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.GrantAllocationAward
{
    public class EditGrantAllocationAwardLandownerCostShareLineItemViewModel : FormViewModel, IValidatableObject
    {
        public int GrantAllocationAwardLandownerCostShareLineItemID { get; set; }
        public int GrantAllocationAwardID { get; set; }


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

        [StringLength(GrantAllocationAwardLandownerCostShareLineItem.FieldLengths.GrantAllocationAwardLandownerCostShareLineItemNotes)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardLandownerCostShareNotes)]
        public string Notes { get; set; }

        

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditGrantAllocationAwardLandownerCostShareLineItemViewModel()
        {

        }

        public EditGrantAllocationAwardLandownerCostShareLineItemViewModel(Models.GrantAllocationAwardLandownerCostShareLineItem grantAllocationAwardLandownerCostShareLineItem)
        {
            GrantAllocationAwardLandownerCostShareLineItemID = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemID;
            GrantAllocationAwardID = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardID;
            StartDate = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemStartDate ?? DateTime.Today;
            EndDate = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemEndDate ?? DateTime.Today;

            ProjectID = grantAllocationAwardLandownerCostShareLineItem.ProjectID;
            StatusID = grantAllocationAwardLandownerCostShareLineItem.LandownerCostShareLineItemStatusID;

            FootprintAcres = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemFootprintAcres;
            SlashAcres = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemSlashAcres;
            ChippingAcres = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemChippingAcres;
            PruningAcres = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemPruningAcres;
            ThinningAcres = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemThinningAcres;
            MasticationAcres = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemMasticationAcres;
            GrazingAcres = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemGrazingAcres;
            LopAndScatterAcres = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemLopAndScatterAcres;
            BiomassRemovalAcres = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemBiomassRemovalAcres;
            HandPileAcres = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemHandPileAcres;
            BroadcastBurnAcres = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemBroadcastBurnAcres;
            HandPileBurnAcres = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemHandPileBurnAcres;
            MachinePileBurnAcres = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemMachinePileBurnAcres;
            OtherTreatmentAcres = grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemOtherTreatmentAcres;

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

            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemStartDate = StartDate;
            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemEndDate = EndDate;

            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemFootprintAcres = FootprintAcres;
            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemChippingAcres = ChippingAcres;
            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemPruningAcres = PruningAcres;
            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemThinningAcres = ThinningAcres;
            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemMasticationAcres = MasticationAcres;
            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemGrazingAcres = GrazingAcres;
            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemLopAndScatterAcres = LopAndScatterAcres;
            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemBiomassRemovalAcres = BiomassRemovalAcres;
            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemHandPileAcres = HandPileAcres;
            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemBroadcastBurnAcres = BroadcastBurnAcres;
            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemHandPileBurnAcres = HandPileBurnAcres;
            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemMachinePileBurnAcres = MachinePileBurnAcres;
            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemOtherTreatmentAcres = OtherTreatmentAcres;

            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemAllocatedAmount = AllocatedAmount;
            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemTotalCost = TotalCost;
            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemNotes = Notes;

        }
    }

}