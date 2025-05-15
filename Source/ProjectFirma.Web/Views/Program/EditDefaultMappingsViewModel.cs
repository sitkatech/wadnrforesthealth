/*-----------------------------------------------------------------------
<copyright file="EditDefaultMappingsViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectFirma.Web.Views.Program
{
    public class EditDefaultMappingsViewModel : FormViewModel, IValidatableObject
    {

        public int GisUploadSourceOrganizationID { get; set; }

        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Project Identifier Column")]
        public string ProjectIdentifierColumn { get; set; }

        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Project Name Column")]
        public string ProjectNameColumn { get; set; }

        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Completion Date Column")]
        public string CompletionDateColumn { get; set; }

        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Start Date Column")]
        public string StartDateColumn { get; set; }

        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Project Stage Column")]
        public string ProjectStageColumn { get; set; }

        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Footprint Acres Column")]
        public string FootprintAcresColumn { get; set; }

        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Private Landowner Column")]
        public string PrivateLandownerColumn { get; set; }

        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Lead Implementer Column")]
        public string LeadImplementerColumn { get; set; }



        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Treatment Type Column")]
        public string TreatmentTypeColumn { get; set; }

        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Treatment Detailed Activity Type Column")]
        public string TreatmentDetailedActivityTypeColumn { get; set; }

        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Treated Acres Column")]
        public string TreatedAcresColumn { get; set; }


        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Pruning Acres Column")]
        public string PruningAcresColumn { get; set; }

        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Thinning Acres Column")]
        public string ThinningAcresColumn { get; set; }

        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Chipping Acres Column")]
        public string ChippingAcresColumn { get; set; }

        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Mastication Acres Column")]
        public string MasticationAcresColumn { get; set; }

        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Grazing Acres Column")]
        public string GrazingAcresColumn { get; set; }

        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Lop And Scatter Acres Column")]
        public string LopAndScatterAcresColumn { get; set; }

        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Biomass Removal Acres Column")]
        public string BiomassRemovalAcresColumn { get; set; }

        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Hand Pile Acres Column")]
        public string HandPileAcresColumn { get; set; }

        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Hand Burn Pile Acres Column")]
        public string HandBurnPileAcresColumn { get; set; }

        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Machine Burn Pile Acres Column")]
        public string MachineBurnPileAcresColumn { get; set; }

        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Broadcast Burn Acres Column")]
        public string BroadcastBurnAcresColumn { get; set; }

        [StringLength(GisDefaultMapping.FieldLengths.GisDefaultMappingColumnName)]
        [DisplayName("Other Acres Column")]
        public string OtherAcresColumn { get; set; }



        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditDefaultMappingsViewModel()
        {
        }

        public EditDefaultMappingsViewModel(Models.GisUploadSourceOrganization gisUploadSourceOrganization)
        {
            GisUploadSourceOrganizationID = gisUploadSourceOrganization.GisUploadSourceOrganizationID;
            ProjectIdentifierColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.ProjectIdentifier.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;
            ProjectNameColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.ProjectName.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;
            CompletionDateColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.CompletionDate.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;

            StartDateColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.PlannedDate.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;
            ProjectStageColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.ProjectStage.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;
            FootprintAcresColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.FootprintAcres.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;
            PrivateLandownerColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.Landowner.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;
            LeadImplementerColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.LeadImplementerOrganization.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;


            TreatmentTypeColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.TreatmentType.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;
            TreatmentDetailedActivityTypeColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.TreatmentDetailedActivityType.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;
            TreatedAcresColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.TreatedAcres.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;

            PruningAcresColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.PruningAcres.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;
            ThinningAcresColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.ThinningAcres.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;
            ChippingAcresColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.ChippingAcres.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;
            MasticationAcresColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.MasticationAcres.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;
            GrazingAcresColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.GrazingAcres.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;
            LopAndScatterAcresColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.LopAndScatterAcres.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;
            BiomassRemovalAcresColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.BiomassRemovalAcres.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;
            HandPileAcresColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.HandPileAcres.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;
            HandBurnPileAcresColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.HandPileBurnAcres.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;
            MachineBurnPileAcresColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.MachinePileBurnAcres.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;
            BroadcastBurnAcresColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.BroadcastBurnAcres.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;
            OtherAcresColumn = gisUploadSourceOrganization.GisDefaultMappings.SingleOrDefault(x => x.FieldDefinitionID == Models.FieldDefinition.OtherTreatmentAcres.FieldDefinitionID)?.GisDefaultMappingColumnName ?? string.Empty;




        }

        public void UpdateModel(Models.GisUploadSourceOrganization gisUploadSourceOrganization)
        {
            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.ProjectIdentifier.FieldDefinitionID, ProjectIdentifierColumn);
            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.ProjectName.FieldDefinitionID, ProjectNameColumn);
            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.CompletionDate.FieldDefinitionID, CompletionDateColumn);
            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.PlannedDate.FieldDefinitionID, StartDateColumn);
            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.ProjectStage.FieldDefinitionID, ProjectStageColumn);
            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.FootprintAcres.FieldDefinitionID, FootprintAcresColumn);
            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.Landowner.FieldDefinitionID, PrivateLandownerColumn);
            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.LeadImplementerOrganization.FieldDefinitionID, LeadImplementerColumn);

            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.TreatmentType.FieldDefinitionID, TreatmentTypeColumn);
            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.TreatmentDetailedActivityType.FieldDefinitionID, TreatmentDetailedActivityTypeColumn);
            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.TreatedAcres.FieldDefinitionID, TreatedAcresColumn);


            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.PruningAcres.FieldDefinitionID, PruningAcresColumn);
            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.ThinningAcres.FieldDefinitionID, ThinningAcresColumn);
            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.ChippingAcres.FieldDefinitionID, ChippingAcresColumn);
            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.MasticationAcres.FieldDefinitionID, MasticationAcresColumn);
            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.GrazingAcres.FieldDefinitionID, GrazingAcresColumn);
            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.LopAndScatterAcres.FieldDefinitionID, LopAndScatterAcresColumn);
            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.BiomassRemovalAcres.FieldDefinitionID, BiomassRemovalAcresColumn);
            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.HandPileAcres.FieldDefinitionID, HandPileAcresColumn);
            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.HandPileBurnAcres.FieldDefinitionID, HandBurnPileAcresColumn);
            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.MachinePileBurnAcres.FieldDefinitionID, MachineBurnPileAcresColumn);
            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.BroadcastBurnAcres.FieldDefinitionID, BroadcastBurnAcresColumn);
            gisUploadSourceOrganization.UpdateMappingColumnValue(Models.FieldDefinition.OtherTreatmentAcres.FieldDefinitionID, OtherAcresColumn);

        }

        

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            return new List<ValidationResult>();

        }
    }
}
