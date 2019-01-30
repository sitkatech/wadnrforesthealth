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
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using FluentValidation.Attributes;
using LtInfo.Common;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.ProjectUpdate
{
    [Validator(typeof(BasicsViewModelValidator))]
    public class BasicsViewModel : FormViewModel, IValidatableObject
    {
        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectDescription)]
        [StringLength(Models.Project.MaxLengthForProjectDescription)]
        public string ProjectDescription { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectStage)]
        public int ProjectStageID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.StartApprovalDate)]
        public DateTime? PlannedDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ExpirationDate)]
        public DateTime? ExpirationDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.CompletionDate)]
        public DateTime? CompletionDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FocusArea)]
        public int? FocusAreaID { get; set; }
        [DisplayName("Reviewer Comments")]
        [StringLength(ProjectUpdateBatch.FieldLengths.BasicsComment)]
        public string Comments { get; set; }

        public ProjectCustomAttributes ProjectCustomAttributes { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public BasicsViewModel()
        {
        }

        public BasicsViewModel(Models.ProjectUpdate projectUpdate, string comments)
        {
            ProjectDescription = projectUpdate.ProjectDescription;
            ProjectStageID = projectUpdate.ProjectStageID;
            PlannedDate = projectUpdate.PlannedDate;
            ExpirationDate = projectUpdate.ExpirationDate;
            CompletionDate = projectUpdate.CompletionDate;
            FocusAreaID = projectUpdate.FocusAreaID;
            Comments = comments;
        }

        public void UpdateModel(Models.ProjectUpdate projectUpdate, Person currentPerson)
        {
            projectUpdate.ProjectDescription = ProjectDescription;
            projectUpdate.ProjectStageID = ProjectStageID;
            projectUpdate.PlannedDate = PlannedDate;
            projectUpdate.ExpirationDate = ExpirationDate;
            projectUpdate.CompletionDate = CompletionDate;
            projectUpdate.FocusAreaID = FocusAreaID;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CompletionDate < PlannedDate)
            {
                yield return new SitkaValidationResult<BasicsViewModel, DateTime?>(
                    FirmaValidationMessages.CompletionDateGreaterThanEqualToImplementationStartYear,
                    m => m.CompletionDate);
            }

            if (ProjectStageID == ProjectStage.Completed.ProjectStageID && !CompletionDate.HasValue)
            {
                yield return new SitkaValidationResult<BasicsViewModel, DateTime?>($"Since the {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} is in the Completed stage, the Completion year is required", m => m.CompletionDate);
            }

            var isCompletedOrPostImplementation = ProjectStageID == ProjectStage.Completed.ProjectStageID || ProjectStageID == ProjectStage.PostImplementation.ProjectStageID;
            if (isCompletedOrPostImplementation && CompletionDate > DateTime.Now)
            {
                yield return new SitkaValidationResult<BasicsViewModel, DateTime?>(
                    $"Since the {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} is in Completed or Post-Implementation stage, the Completion Year needs to be less than or equal to the current year",
                    m => m.CompletionDate);
            }
        }
    }
}
