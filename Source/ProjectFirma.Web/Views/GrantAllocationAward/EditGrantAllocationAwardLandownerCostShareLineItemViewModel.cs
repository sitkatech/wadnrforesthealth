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
            var treatment = grantAllocationAwardLandownerCostShareLineItem.Treatments.FirstOrDefault();
           
            StartDate = treatment?.TreatmentStartDate ?? DateTime.Today;
            EndDate = treatment?.TreatmentEndDate ?? DateTime.Today;
            FootprintAcres = (treatment?.TreatmentFootprintAcres ?? 0).ToDecimalFormatted();

            ProjectID = grantAllocationAwardLandownerCostShareLineItem.ProjectID;
            StatusID = grantAllocationAwardLandownerCostShareLineItem.LandownerCostShareLineItemStatusID;


            var slashTreatment =
                grantAllocationAwardLandownerCostShareLineItem.Treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Slash);
            SlashAcres = (slashTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var chippingTreatment =
                grantAllocationAwardLandownerCostShareLineItem.Treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Chipping);
            ChippingAcres = (chippingTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var pruningTreatment =
                grantAllocationAwardLandownerCostShareLineItem.Treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Pruning);
            PruningAcres = (pruningTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var thinningTreatment =
                grantAllocationAwardLandownerCostShareLineItem.Treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Thinning);
            ThinningAcres = (thinningTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var masticationTreatment =
                grantAllocationAwardLandownerCostShareLineItem.Treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Mastication);
            MasticationAcres = (masticationTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var grazingTreatment =
                grantAllocationAwardLandownerCostShareLineItem.Treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Grazing);
            GrazingAcres = (grazingTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var lopAndScatterTreatment =
                grantAllocationAwardLandownerCostShareLineItem.Treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.LopAndScatter);
            LopAndScatterAcres = (lopAndScatterTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var biomassRemovalTreatment =
                grantAllocationAwardLandownerCostShareLineItem.Treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.BiomassRemoval);
            BiomassRemovalAcres = (biomassRemovalTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var handPileTreatment =
                grantAllocationAwardLandownerCostShareLineItem.Treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.HandPile);
            HandPileAcres = (handPileTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var broadCastBurnTreatment =
                grantAllocationAwardLandownerCostShareLineItem.Treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.BroadcastBurn);
            BroadcastBurnAcres = (broadCastBurnTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var handPileBurnTreatment =
                grantAllocationAwardLandownerCostShareLineItem.Treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.HandPileBurn);
            HandPileBurnAcres = (handPileBurnTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var machinePileBurnTreatment =
                grantAllocationAwardLandownerCostShareLineItem.Treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.MachinePileBurn);
            MachinePileBurnAcres = (machinePileBurnTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var otherTreatment =
                grantAllocationAwardLandownerCostShareLineItem.Treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Other);
            OtherTreatmentAcres = (otherTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

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


            var treatments = grantAllocationAwardLandownerCostShareLineItem.Treatments;
            var projectID = grantAllocationAwardLandownerCostShareLineItem.Project.ProjectID;
            var grantAllocationAwardLandownerCostShareLineItemID = grantAllocationAwardLandownerCostShareLineItem
                .GrantAllocationAwardLandownerCostShareLineItemID;

            UpdateTreatmentByTreatmentType(grantAllocationAwardLandownerCostShareLineItem, treatments, projectID, grantAllocationAwardLandownerCostShareLineItemID, TreatmentDetailedActivityType.Chipping, ChippingAcres);
            UpdateTreatmentByTreatmentType(grantAllocationAwardLandownerCostShareLineItem, treatments, projectID, grantAllocationAwardLandownerCostShareLineItemID, TreatmentDetailedActivityType.Pruning, PruningAcres);
            UpdateTreatmentByTreatmentType(grantAllocationAwardLandownerCostShareLineItem, treatments, projectID, grantAllocationAwardLandownerCostShareLineItemID, TreatmentDetailedActivityType.Thinning, ThinningAcres);
            UpdateTreatmentByTreatmentType(grantAllocationAwardLandownerCostShareLineItem, treatments, projectID, grantAllocationAwardLandownerCostShareLineItemID, TreatmentDetailedActivityType.Mastication, MasticationAcres);
            UpdateTreatmentByTreatmentType(grantAllocationAwardLandownerCostShareLineItem, treatments, projectID, grantAllocationAwardLandownerCostShareLineItemID, TreatmentDetailedActivityType.Grazing, GrazingAcres);
            UpdateTreatmentByTreatmentType(grantAllocationAwardLandownerCostShareLineItem, treatments, projectID, grantAllocationAwardLandownerCostShareLineItemID, TreatmentDetailedActivityType.LopAndScatter, LopAndScatterAcres);
            UpdateTreatmentByTreatmentType(grantAllocationAwardLandownerCostShareLineItem, treatments, projectID, grantAllocationAwardLandownerCostShareLineItemID, TreatmentDetailedActivityType.BiomassRemoval, BiomassRemovalAcres);
            UpdateTreatmentByTreatmentType(grantAllocationAwardLandownerCostShareLineItem, treatments, projectID, grantAllocationAwardLandownerCostShareLineItemID, TreatmentDetailedActivityType.HandPile, HandPileAcres);
            UpdateTreatmentByTreatmentType(grantAllocationAwardLandownerCostShareLineItem, treatments, projectID, grantAllocationAwardLandownerCostShareLineItemID, TreatmentDetailedActivityType.BroadcastBurn, BroadcastBurnAcres);
            UpdateTreatmentByTreatmentType(grantAllocationAwardLandownerCostShareLineItem, treatments, projectID, grantAllocationAwardLandownerCostShareLineItemID, TreatmentDetailedActivityType.HandPileBurn, HandPileBurnAcres);
            UpdateTreatmentByTreatmentType(grantAllocationAwardLandownerCostShareLineItem, treatments, projectID, grantAllocationAwardLandownerCostShareLineItemID, TreatmentDetailedActivityType.MachinePileBurn, MachinePileBurnAcres);
            UpdateTreatmentByTreatmentType(grantAllocationAwardLandownerCostShareLineItem, treatments, projectID, grantAllocationAwardLandownerCostShareLineItemID, TreatmentDetailedActivityType.Other, OtherTreatmentAcres);
            UpdateTreatmentByTreatmentType(grantAllocationAwardLandownerCostShareLineItem, treatments, projectID, grantAllocationAwardLandownerCostShareLineItemID, TreatmentDetailedActivityType.Slash, SlashAcres);

            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemAllocatedAmount = AllocatedAmount;
            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemTotalCost = TotalCost;
            grantAllocationAwardLandownerCostShareLineItem.GrantAllocationAwardLandownerCostShareLineItemNotes = Notes;

        }

        private void UpdateTreatmentByTreatmentType(
            Models.GrantAllocationAwardLandownerCostShareLineItem grantAllocationAwardLandownerCostShareLineItem,
            ICollection<Treatment> treatments
            , int projectID
            , int grantAllocationAwardLandownerCostShareLineItemID
            , TreatmentDetailedActivityType treatmentDetailedActivityType
            , decimal treatedAcres)
        {
            var treatment = treatments.SingleOrDefault(x => x.TreatmentDetailedActivityType == treatmentDetailedActivityType);
            if (treatment == null)
            {
                treatment = new Treatment(projectID, FootprintAcres, TreatmentType.Other.TreatmentTypeID,treatmentDetailedActivityType.TreatmentDetailedActivityTypeID);
                treatment.GrantAllocationAwardLandownerCostShareLineItemID =
                    grantAllocationAwardLandownerCostShareLineItemID;
                treatment.GrantAllocationAwardLandownerCostShareLineItem =
                    grantAllocationAwardLandownerCostShareLineItem;
                treatment.Project = grantAllocationAwardLandownerCostShareLineItem.Project;
                grantAllocationAwardLandownerCostShareLineItem.Treatments.Add(treatment);
            }

            treatment.TreatmentFootprintAcres = FootprintAcres;
            treatment.TreatmentTreatedAcres = treatedAcres;
            treatment.TreatmentStartDate = StartDate;
            treatment.TreatmentEndDate = EndDate;
        }
    }

}