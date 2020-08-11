/*-----------------------------------------------------------------------
<copyright file="BasicsViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

        [DisplayName("Project Type Column")]
        public int? ProjectTypeMetadataAttributeID { get; set; }

        [Required]
        [DisplayName("Completion Date Column")]
        public int CompletionDateMetadataAttributeID { get; set; }

        [DisplayName("Start Date Column")]
        public int? StartDateMetadataAttributeID { get; set; }

        [DisplayName("Project Stage Column")]
        public int? ProjectStageMetadataAttributeID { get; set; }

        [DisplayName("Other Treatment Acres Column")]
        public int? OtherTreatmentAcresMetadataAttributeID { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public GisMetadataViewModel(GisUploadAttempt gisUploadAttempt, List<Models.GisMetadataAttribute> gisMetadataAttributes)
        {
            var organization = gisUploadAttempt.GisUploadSourceOrganization;
            var defaults = organization.GisDefaultMappings;
            ProjectIdentifierMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.ProjectIdentifier) ?? 0;
            ProjectNameMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.ProjectName) ?? 0;
            ProjectTypeMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.ProjectType);
            CompletionDateMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.CompletionDate) ?? 0;
            StartDateMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.PlannedDate);
            ProjectStageMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.ProjectStage);
            OtherTreatmentAcresMetadataAttributeID = GetPossibleDefaultMetadataAttributeID(gisMetadataAttributes, defaults, Models.FieldDefinition.TreatedAcres);
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
                        projectIdentifierDefault.GisDefaultMappingColumnName));
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
