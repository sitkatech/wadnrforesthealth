/*-----------------------------------------------------------------------
<copyright file="EditTreatmentsForAProjectViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Treatment
{
    public class EditTreatmentsForAProjectViewModel : FormViewModel, IValidatableObject
    {
        [FieldDefinitionDisplay(FieldDefinitionEnum.Project)]
        [Required]
        public int ProjectID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.TreatmentStartDate)]
        [Required]
        public DateTime StartDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.TreatmentEndDate)]
        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(Models.Treatment.FieldLengths.TreatmentNotes)]
        public string Notes { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.TreatmentType)]
        public int TreatmentTypeID { get; set; }

        [Required]
        [DisplayName("Project Location - Treatment Area")]
        public int ProjectLocationID { get; set; }


        [FieldDefinitionDisplay(FieldDefinitionEnum.FootprintAcres)]
        public decimal FootprintAcres { get; set; }





        [FieldDefinitionDisplay(FieldDefinitionEnum.ChippingAcres)]
        public decimal ChippingAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.PruningAcres)]
        public decimal PruningAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ThinningAcres)]
        public decimal ThinningAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.MasticationAcres)]
        public decimal MasticationAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrazingAcres)]
        public decimal GrazingAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.LopAndScatterAcres)]
        public decimal LopAndScatterAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.BiomassRemovalAcres)]
        public decimal BiomassRemovalAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.HandPileAcres)]
        public decimal HandPileAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.BroadcastBurnAcres)]
        public decimal BroadcastBurnAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.HandPileBurnAcres)]
        public decimal HandPileBurnAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.MachinePileBurnAcres)]
        public decimal MachinePileBurnAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.OtherTreatmentAcres)]
        public decimal OtherTreatmentAcres { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.SlashAcres)]
        public decimal SlashAcres { get; set; }


        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditTreatmentsForAProjectViewModel()
        {
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
        }

        public EditTreatmentsForAProjectViewModel(List<Models.Treatment> treatments)
        {
            var defaultTreatment = treatments.FirstOrDefault();
           
            StartDate = defaultTreatment?.TreatmentStartDate ?? DateTime.Today;
            EndDate = defaultTreatment?.TreatmentEndDate ?? DateTime.Today;
            FootprintAcres = (defaultTreatment?.TreatmentFootprintAcres ?? 0).ToDecimalFormatted();
            if (defaultTreatment.ProjectLocationID.HasValue)
            {
                ProjectLocationID = defaultTreatment.ProjectLocationID.Value;
            }
            
            ProjectID = defaultTreatment.ProjectID;
            Notes = defaultTreatment.TreatmentNotes;

            var slashTreatment =
                treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Slash);
            SlashAcres = (slashTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var chippingTreatment =
                treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Chipping);
            ChippingAcres = (chippingTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var pruningTreatment =
                treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Pruning);
            PruningAcres = (pruningTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var thinningTreatment =
                treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Thinning);
            ThinningAcres = (thinningTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var masticationTreatment =
                treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Mastication);
            MasticationAcres = (masticationTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var grazingTreatment =
                treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Grazing);
            GrazingAcres = (grazingTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var lopAndScatterTreatment =
                treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.LopAndScatter);
            LopAndScatterAcres = (lopAndScatterTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var biomassRemovalTreatment =
                treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.BiomassRemoval);
            BiomassRemovalAcres = (biomassRemovalTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var handPileTreatment =
                treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.HandPile);
            HandPileAcres = (handPileTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var broadCastBurnTreatment =
                treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.BroadcastBurn);
            BroadcastBurnAcres = (broadCastBurnTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var handPileBurnTreatment =
                treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.HandPileBurn);
            HandPileBurnAcres = (handPileBurnTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var machinePileBurnTreatment =
                treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.MachinePileBurn);
            MachinePileBurnAcres = (machinePileBurnTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();

            var otherTreatment =
                treatments.SingleOrDefault(x =>
                    x.TreatmentDetailedActivityType == TreatmentDetailedActivityType.Other);
            OtherTreatmentAcres = (otherTreatment?.TreatmentTreatedAcres ?? 0).ToDecimalFormatted();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate != DateTime.MinValue && EndDate < StartDate)
            {
                yield return new ValidationResult("End Date cannot be before Start Date");
            }



        }



        public void UpdateModel(List<Models.Treatment> treatments)
        {
            
            UpdateTreatmentByTreatmentType(treatments, TreatmentDetailedActivityType.Chipping, ChippingAcres);
            UpdateTreatmentByTreatmentType(treatments, TreatmentDetailedActivityType.Pruning, PruningAcres);
            UpdateTreatmentByTreatmentType(treatments, TreatmentDetailedActivityType.Thinning, ThinningAcres);
            UpdateTreatmentByTreatmentType(treatments, TreatmentDetailedActivityType.Mastication, MasticationAcres);
            UpdateTreatmentByTreatmentType(treatments, TreatmentDetailedActivityType.Grazing, GrazingAcres);
            UpdateTreatmentByTreatmentType(treatments, TreatmentDetailedActivityType.LopAndScatter, LopAndScatterAcres);
            UpdateTreatmentByTreatmentType(treatments, TreatmentDetailedActivityType.BiomassRemoval, BiomassRemovalAcres);
            UpdateTreatmentByTreatmentType(treatments, TreatmentDetailedActivityType.HandPile, HandPileAcres);
            UpdateTreatmentByTreatmentType(treatments, TreatmentDetailedActivityType.BroadcastBurn, BroadcastBurnAcres);
            UpdateTreatmentByTreatmentType(treatments, TreatmentDetailedActivityType.HandPileBurn, HandPileBurnAcres);
            UpdateTreatmentByTreatmentType(treatments, TreatmentDetailedActivityType.MachinePileBurn, MachinePileBurnAcres);
            UpdateTreatmentByTreatmentType(treatments, TreatmentDetailedActivityType.Other, OtherTreatmentAcres);
            UpdateTreatmentByTreatmentType(treatments, TreatmentDetailedActivityType.Slash, SlashAcres);

            

        }

        private void UpdateTreatmentByTreatmentType(
            ICollection<Models.Treatment> treatments
            , TreatmentDetailedActivityType treatmentDetailedActivityType
            , decimal treatedAcres)
        {
            bool needToAdd = false;
            var treatment = treatments.SingleOrDefault(x => x.TreatmentDetailedActivityType == treatmentDetailedActivityType && x.ProjectLocationID == ProjectLocationID);
            if (treatment == null)
            {
                treatment = new Models.Treatment(ProjectID, FootprintAcres, TreatmentType.Other.TreatmentTypeID, treatmentDetailedActivityType.TreatmentDetailedActivityTypeID);
                needToAdd = true;
            }

            treatment.TreatmentFootprintAcres = FootprintAcres;
            treatment.TreatmentTreatedAcres = treatedAcres;
            treatment.TreatmentStartDate = StartDate;
            treatment.TreatmentEndDate = EndDate;
            treatment.TreatmentNotes = Notes;
            treatment.ProjectID = ProjectID;
            treatment.ProjectLocationID = ProjectLocationID;
            treatment.TreatmentTypeID = TreatmentTypeID;


            if (needToAdd)
            {
                HttpRequestStorage.DatabaseEntities.Treatments.Add(treatment);
            }

        }


    }

}