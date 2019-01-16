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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.ProjectCreate
{
    public class BasicsViewModel : FormViewModel, IValidatableObject
    {
        public int? ProjectID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.TaxonomyLeaf)]
        [Required]
        public int? TaxonomyLeafID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectName)]
        [StringLength(Models.Project.FieldLengths.ProjectName)]
        [Required]
        public string ProjectName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectDescription)]
        [StringLength(Models.Project.MaxLengthForProjectDescription)]
        [Required]
        public string ProjectDescription { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectStage)]
        [Required]
        public int? ProjectStageID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.PlannedDate)]
        [Required]
        public DateTime? PlannedDate { get; set; }
        
        [FieldDefinitionDisplay(FieldDefinitionEnum.ApprovalStartDate)]
        [Required]
        public DateTime? ApprovalStartDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.CompletionDate)]
        public DateTime? CompletionDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FocusArea)]
        public int? FocusAreaID { get; set; }

        public int? ImportExternalProjectStagingID { get; set; }

        public ProjectCustomAttributes ProjectCustomAttributes { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public BasicsViewModel()
        {
        }

        public BasicsViewModel(Models.Project project)
        {
            TaxonomyLeafID = project.TaxonomyLeafID;
            ProjectID = project.ProjectID;
            ProjectName = project.ProjectName;
            ProjectDescription = project.ProjectDescription;
            ProjectStageID = project.ProjectStageID;
            PlannedDate = project.PlannedDate;
            ApprovalStartDate = project.ApprovalStartDate;
            CompletionDate = project.CompletionDate;
            FocusAreaID = project.FocusAreaID;
            ProjectCustomAttributes = new ProjectCustomAttributes(project);
        }

        public void UpdateModel(Models.Project project, Person person)
        {
            if (ImportExternalProjectStagingID.HasValue)
            {
                var importExternalProjectStagingToDelete = HttpRequestStorage.DatabaseEntities.ImportExternalProjectStagings.Single(x =>
                    x.ImportExternalProjectStagingID == ImportExternalProjectStagingID);
                HttpRequestStorage.DatabaseEntities.AllImportExternalProjectStagings.Remove(importExternalProjectStagingToDelete);
            }

            project.ProposingPersonID = person.PersonID;
            project.TaxonomyLeafID = TaxonomyLeafID ?? ModelObjectHelpers.NotYetAssignedID;
            project.ProjectID = ProjectID ?? ModelObjectHelpers.NotYetAssignedID;
            project.ProjectName = ProjectName;
            project.ProjectDescription = ProjectDescription;
            project.ProjectStageID = ProjectStageID ?? ModelObjectHelpers.NotYetAssignedID;

            project.PlannedDate = PlannedDate;
            project.ApprovalStartDate = ApprovalStartDate;
            project.CompletionDate = CompletionDate;
            project.FocusAreaID = FocusAreaID;
            ProjectCustomAttributes?.UpdateModel(project, person);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
           return GetValidationResults();
        }

        public IEnumerable<ValidationResult> GetValidationResults()
        {
            var projects = HttpRequestStorage.DatabaseEntities.Projects.ToList();

            if (TaxonomyLeafID == -1)
            {
                yield return new SitkaValidationResult<BasicsViewModel, int?>($"{MultiTenantHelpers.GetTaxonomyLeafDisplayNameForProject()} is required.", m => m.TaxonomyLeafID);
            }

            if (!Models.Project.IsProjectNameUnique(projects, ProjectName, ProjectID))
            {
                yield return new SitkaValidationResult<BasicsViewModel, string>(FirmaValidationMessages.ProjectNameUnique, m => m.ProjectName);
            }

            if (ApprovalStartDate < PlannedDate)
            {
                yield return new SitkaValidationResult<BasicsViewModel, DateTime?>(FirmaValidationMessages.ImplementationStartYearGreaterThanPlannedDate, m => m.ApprovalStartDate);
            }

            if (CompletionDate < ApprovalStartDate)
            {
                yield return new SitkaValidationResult<BasicsViewModel, DateTime?>(FirmaValidationMessages.CompletionDateGreaterThanEqualToImplementationStartYear, m => m.CompletionDate);
            }

            if (CompletionDate < PlannedDate)
            {
                yield return new SitkaValidationResult<BasicsViewModel, DateTime?>(FirmaValidationMessages.CompletionDateGreaterThanEqualToPlannedDate, m => m.CompletionDate);
            }

            var currentYear = FirmaDateUtilities.CalculateCurrentYearToUseForUpToAllowableInputInReporting();
            if (ProjectStageID == ProjectStage.Implementation.ProjectStageID)
            {
                if (ApprovalStartDate?.Year > currentYear)
                {
                    yield return new SitkaValidationResult<BasicsViewModel, DateTime?>(
                        FirmaValidationMessages.ImplementationYearMustBePastOrPresentForImplementationProjects,
                        m => m.ApprovalStartDate);
                }
            }
            
            if (ApprovalStartDate == null && ProjectStageID != ProjectStage.Cancelled.ProjectStageID && ProjectStageID != ProjectStage.Deferred.ProjectStageID)
            {
                yield return new SitkaValidationResult<BasicsViewModel, DateTime?>(
                    $"Implementation year is required when the {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} stage is not Deferred or Terminated",
                    m => m.ApprovalStartDate);
            }

            if (ProjectStageID == ProjectStage.Completed.ProjectStageID && !CompletionDate.HasValue)
            {
                yield return new SitkaValidationResult<BasicsViewModel, DateTime?>($"Since the {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} is in the Completed stage, the Completion year is required", m => m.CompletionDate);
            }

            if ((ProjectStageID == ProjectStage.Completed.ProjectStageID ||
                ProjectStageID == ProjectStage.PostImplementation.ProjectStageID) && CompletionDate?.Year > currentYear)
            {
                yield return new SitkaValidationResult<BasicsViewModel, DateTime?>(FirmaValidationMessages.CompletionDateMustBePastOrPresentForCompletedProjects, m => m.CompletionDate);
            }

            if (ProjectStageID == ProjectStage.Planned.ProjectStageID && PlannedDate?.Year > currentYear)
            {
                yield return new SitkaValidationResult<BasicsViewModel, DateTime?>(
                    $"Since the {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} is in the Planning / Design stage, the Planning / Design start year must be less than or equal to the current year",
                    m => m.PlannedDate);
            }
        }
    }
}
