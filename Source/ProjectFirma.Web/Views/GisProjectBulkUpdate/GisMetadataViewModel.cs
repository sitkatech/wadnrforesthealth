/*-----------------------------------------------------------------------
<copyright file="BasicsViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Web;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;

namespace ProjectFirma.Web.Views.GisProjectBulkUpdate
{
    public class GisMetadataViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        [DisplayName("Project Identifier Column")]
        public int ProjectIdentifierMetadataAttributeID { get; set; }

        [Required]
        [DisplayName("Project Name Column")]
        public int ProjectNameMetadataAttributeID { get; set; }

        [DisplayName("Treatment Type Column")]
        public int? TreatmentTypeMetadataAttributeID { get; set; }

        [DisplayName("Completion Date Column")]
        public int? CompletionDateMetadataAttributeID { get; set; }

        [DisplayName("Start Date Column")]
        public int? StartDateMetadataAttributeID { get; set; }

        [DisplayName("Project Stage Column")]
        public int? ProjectStageMetadataAttributeID { get; set; }

        [DisplayName("Lead Implementer Column")]
        public int? LeadImplementerMetadataAttributeID { get; set; }


        [DisplayName("Footprint Acres Column")]
        public int? FootprintAcresMetadataAttributeID { get; set; }

        [DisplayName("Private Landowner Column")]
        public int? PrivateLandownerMetadataAttributeID { get; set; }

        [DisplayName("Primary Contact Column")]
        public int? PrimaryContactMetadataAttributeID { get; set; }

        [DisplayName("Treatment Detailed Activity Type Column")]
        public int? TreatmentDetailedActivityTypeMetadataAttributeID { get; set; }

        [DisplayName("Treated Acres Column")]
        public int? TreatedAcresMetadataAttributeID { get; set; }

        [DisplayName("Pruning Acres Column")]
        public int? PruningAcresMetadataAttributeID { get; set; }

        [DisplayName("Thinning Acres Column")]
        public int? ThinningAcresMetadataAttributeID { get; set; }

        [DisplayName("Chipping Acres Column")]
        public int? ChippingAcresMetadataAttributeID { get; set; }

        [DisplayName("Mastication Acres Column")]
        public int? MasticationAcresMetadataAttributeID { get; set; }

        [DisplayName("Grazing Acres Column")]
        public int? GrazingAcresMetadataAttributeID { get; set; }

        [DisplayName("Lop and Scatter Acres Column")]
        public int? LopScatAcresMetadataAttributeID { get; set; }

        [DisplayName("Biomass Removal Acres Column")]
        public int? BiomassRemovalAcresMetadataAttributeID { get; set; }

        [DisplayName("Hand Pile Acres Column")]
        public int? HandPileAcresMetadataAttributeID { get; set; }

        [DisplayName("Hand Pile Burn Acres Column")]
        public int? HandPileBurnAcresMetadataAttributeID { get; set; }

        [DisplayName("Machine Pile Burn Acres Column")]
        public int? MachinePileBurnAcresMetadataAttributeID { get; set; }

        [DisplayName("Broadcast Burn Acres Column")]
        public int? BroadcastBurnAcresMetadataAttributeID { get; set; }

        [DisplayName("Other Acres Column")]
        public int? OtherAcresMetadataAttributeID { get; set; }


        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public GisMetadataViewModel() { }


        public GisMetadataViewModel(GisUploadAttempt gisUploadAttempt, List<Models.GisMetadataAttribute> gisMetadataAttributes)
        {
            var organization = gisUploadAttempt.GisUploadSourceOrganization;
            var defaults = organization.GisDefaultMappings;
            ProjectIdentifierMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.ProjectIdentifier) ?? 0;
            ProjectNameMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.ProjectName) ?? 0;
            TreatmentTypeMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.TreatmentType);
            CompletionDateMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.CompletionDate) ?? 0;
            StartDateMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.PlannedDate);
            ProjectStageMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.ProjectStage);
            LeadImplementerMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.LeadImplementerOrganization);

            FootprintAcresMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.FootprintAcres);
            PrivateLandownerMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.Landowner);
            PrimaryContactMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.ProjectPrimaryContact);
            TreatedAcresMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.TreatedAcres);
            TreatmentDetailedActivityTypeMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.TreatmentDetailedActivityType);
            PruningAcresMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.PruningAcres);
            ThinningAcresMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.ThinningAcres);
            ChippingAcresMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.ChippingAcres);
            MasticationAcresMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.MasticationAcres);
            GrazingAcresMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.GrazingAcres);
            LopScatAcresMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.LopAndScatterAcres);
            BiomassRemovalAcresMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.BiomassRemovalAcres);
            HandPileAcresMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.HandPileAcres);
            HandPileBurnAcresMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.HandPileBurnAcres);
            MachinePileBurnAcresMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.MachinePileBurnAcres);
            BroadcastBurnAcresMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.BroadcastBurnAcres);
            OtherAcresMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.OtherTreatmentAcres);
        }

        private static int? GetPossibleDefaultMetadataAttributeID(List<GisMetadataAttribute> gisMetadataAttributes, ICollection<GisDefaultMapping> defaults, Models.FieldDefinition fieldDefinition)
        {
            int? defaultMetadataAttributeID = null;
            var projectIdentifierDefault =
                defaults.SingleOrDefault(x => x.FieldDefinition == fieldDefinition);
            if (projectIdentifierDefault != null)
            {
                var projectIdentifierGisMetadataAttribute = gisMetadataAttributes
                    .SingleOrDefault(x => string.Equals(x.GisMetadataAttributeName,
                        projectIdentifierDefault.GisDefaultMappingColumnName, StringComparison.InvariantCultureIgnoreCase));

                if (projectIdentifierGisMetadataAttribute == null)
                {
                    projectIdentifierGisMetadataAttribute = gisMetadataAttributes
                        .FirstOrDefault(x => string.Equals(x.GisMetadataAttributeName,
                            projectIdentifierDefault.GisDefaultMappingColumnName) || (projectIdentifierDefault.GisDefaultMappingColumnName.Length > 10 && string.Equals(x.GisMetadataAttributeName,
                            projectIdentifierDefault.GisDefaultMappingColumnName.Substring(0, 10), StringComparison.InvariantCultureIgnoreCase)));
                }


                
                if (projectIdentifierGisMetadataAttribute != null)
                {
                    defaultMetadataAttributeID = projectIdentifierGisMetadataAttribute.GisMetadataAttributeID;
                }
            }

            return defaultMetadataAttributeID;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
           return GetValidationResults();
        }

        public IEnumerable<ValidationResult> GetValidationResults()
        {
            var somethingBadWithTheFile = false;

            if (somethingBadWithTheFile)
            {
                yield return new SitkaValidationResult<UploadGisFileViewModel, HttpPostedFileBase>("There is something wrong with the file", m => m.FileResourceData);
            }

        }
    }
}
