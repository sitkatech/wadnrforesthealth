/*-----------------------------------------------------------------------
<copyright file="GrantDetailViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Shared.FileResourceControls;
using ProjectFirma.Web.Views.Shared.TextControls;

namespace ProjectFirma.Web.Views.Program
{
    public class DetailViewData : ProgramViewData
    {
        private ICollection<GisDefaultMapping> Defaults { get; set; }

        public DetailViewData(Person currentPerson,
                                    Models.Program program
                                   )
            : base(currentPerson, program)
        {
            PageTitle = program.ProgramName;
            BreadCrumbTitle = $"{Models.FieldDefinition.Program.GetFieldDefinitionLabel()} Detail";
            Defaults = GisUploadSourceOrganization != null ? GisUploadSourceOrganization.GisDefaultMappings : new List<GisDefaultMapping>();

        }
        private string GetPossibleDefaultMetadataAttributeString(Models.FieldDefinition fieldDefinition)
        {
            var projectIdentifierDefault =
                Defaults.SingleOrDefault(x => x.FieldDefinition == fieldDefinition);
            if (projectIdentifierDefault != null)
            {
                return projectIdentifierDefault.GisDefaultMappingColumnName;
            }

            return null;
        }

        public string ProjectIdentifierColumnMapping()
        {
            if (GisUploadSourceOrganization != null)
            {
                return GetPossibleDefaultMetadataAttributeString(Models.FieldDefinition.ProjectIdentifier);
            }

            return null;
        }

        public string ProjectNameColumnMapping()
        {
            if (GisUploadSourceOrganization != null)
            {
                return GetPossibleDefaultMetadataAttributeString(Models.FieldDefinition.ProjectName);
            }

            return null;
        }

        public string TreatmentTypeColumnMapping()
        {
            if (GisUploadSourceOrganization != null)
            {
                return GetPossibleDefaultMetadataAttributeString(Models.FieldDefinition.TreatmentType);
            }

            return null;
        }

        public string CompletionDateColumnMapping()
        {
            if (GisUploadSourceOrganization != null)
            {
                return GetPossibleDefaultMetadataAttributeString(Models.FieldDefinition.CompletionDate);
            }

            return null;
        }


        public string StartDateColumnMapping()
        {
            if (GisUploadSourceOrganization != null)
            {
                return GetPossibleDefaultMetadataAttributeString(Models.FieldDefinition.PlannedDate);
            }

            return null;
        }

        public string ProjectStageColumnMapping()
        {
            if (GisUploadSourceOrganization != null)
            {
                return GetPossibleDefaultMetadataAttributeString(Models.FieldDefinition.ProjectStage);
            }

            return null;
        }


        public string FootprintAcresColumnMapping()
        {
            if (GisUploadSourceOrganization != null)
            {
                return GetPossibleDefaultMetadataAttributeString(Models.FieldDefinition.FootprintAcres);
            }

            return null;
        }

        public string PrivateLandownerColumnMapping()
        {
            if (GisUploadSourceOrganization != null)
            {
                return GetPossibleDefaultMetadataAttributeString(Models.FieldDefinition.Landowner);
            }

            return null;
        }

        public string TreatmentDetailedActivityTypeColumnMapping()
        {
            if (GisUploadSourceOrganization != null)
            {
                return GetPossibleDefaultMetadataAttributeString(Models.FieldDefinition.TreatmentDetailedActivityType);
            }

            return null;
        }

        public string TreatedAcresColumnMapping()
        {
            if (GisUploadSourceOrganization != null)
            {
                return GetPossibleDefaultMetadataAttributeString(Models.FieldDefinition.TreatedAcres);
            }

            return null;
        }


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
    }
}
