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

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectType)]
        [Required]
        public int? ProjectTypeID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectName)]
        [StringLength(Models.Project.FieldLengths.ProjectName)]
        [Required]
        public string ProjectName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectDescription)]
        [StringLength(Models.Project.MaxLengthForProjectDescription)]
        public string ProjectDescription { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectStage)]
        [Required]
        public int? ProjectStageID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.StartApprovalDate)]
        [Required]
        public DateTime? PlannedDate { get; set; }
        
        [FieldDefinitionDisplay(FieldDefinitionEnum.ExpirationDate)]
        public DateTime? ExpirationDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.CompletionDate)]
        public DateTime? CompletionDate { get; set; }

		[FieldDefinitionDisplay(FieldDefinitionEnum.FocusArea)]
        public int? FocusAreaID { get; set; }

        public int? ImportExternalProjectStagingID { get; set; }


        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public BasicsViewModel()
        {
        }

        public BasicsViewModel(Models.Project project)
        {
            ProjectTypeID = project.ProjectTypeID;
            ProjectID = project.ProjectID;
            ProjectName = project.ProjectName;
            ProjectDescription = project.ProjectDescription;
            ProjectStageID = project.ProjectStageID;
            PlannedDate = project.PlannedDate;
            ExpirationDate = project.ExpirationDate;
            CompletionDate = project.CompletionDate;
            FocusAreaID = project.FocusAreaID;
        }

        public void UpdateModel(Models.Project project, Person person)
        {
            project.ProposingPersonID = person.PersonID;
            project.ProjectTypeID = ProjectTypeID ?? ModelObjectHelpers.NotYetAssignedID;
            project.ProjectID = ProjectID ?? ModelObjectHelpers.NotYetAssignedID;
            project.ProjectName = ProjectName;
            project.ProjectDescription = ProjectDescription;
            project.ProjectStageID = ProjectStageID ?? ModelObjectHelpers.NotYetAssignedID;

            project.PlannedDate = PlannedDate;
            project.ExpirationDate = ExpirationDate;
            project.CompletionDate = CompletionDate;
            project.FocusAreaID = FocusAreaID;

            var nonAllTypeAttributes = project.ProjectCustomAttributes.Where(x => !x.ProjectCustomAttributeType.ApplyToAllProjectTypes).ToList();
            if (nonAllTypeAttributes.Any())
            {
                foreach (var projectCustomAttribute in nonAllTypeAttributes)
                {
                    var projectTypeList = projectCustomAttribute.ProjectCustomAttributeType
                        .ProjectTypeProjectCustomAttributeTypes.Select(x => x.ProjectTypeID).ToList();
                    if (!projectTypeList.Contains(project.ProjectTypeID))
                    {
                        projectCustomAttribute.DeleteFull(HttpRequestStorage.DatabaseEntities);
                    }
                }
            }


        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
           return GetValidationResults();
        }

        public IEnumerable<ValidationResult> GetValidationResults()
        {
            var projects = HttpRequestStorage.DatabaseEntities.Projects.ToList();

            if (ProjectTypeID == -1)
            {
                yield return new SitkaValidationResult<BasicsViewModel, int?>($"{MultiTenantHelpers.GetProjectTypeDisplayNameForProject()} is required.", m => m.ProjectTypeID);
            }

            if (!Models.Project.IsProjectNameUnique(projects, ProjectName, ProjectID))
            {
                yield return new SitkaValidationResult<BasicsViewModel, string>(FirmaValidationMessages.ProjectNameUnique, m => m.ProjectName);
            }

            if (CompletionDate < PlannedDate)
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
                if (PlannedDate?.Year > currentYear)
                {
                    yield return new SitkaValidationResult<BasicsViewModel, DateTime?>(
                        FirmaValidationMessages.ImplementationYearMustBePastOrPresentForImplementationProjects,
                        m => m.PlannedDate);
                }
            }
            
            if (PlannedDate == null && ProjectStageID != ProjectStage.Cancelled.ProjectStageID)
            {
                yield return new SitkaValidationResult<BasicsViewModel, DateTime?>(
                    $"Implementation year is required when the {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} stage is not Deferred or Terminated",
                    m => m.PlannedDate);
            }

            if (ProjectStageID == ProjectStage.Completed.ProjectStageID && !CompletionDate.HasValue)
            {
                yield return new SitkaValidationResult<BasicsViewModel, DateTime?>($"Since the {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} is in the Completed stage, the Completion year is required", m => m.CompletionDate);
            }
            if (ProjectStageID == ProjectStage.Completed.ProjectStageID)
            {
                var landownerCostShareLineItemsOnProject = HttpRequestStorage.DatabaseEntities.GrantAllocationAwardLandownerCostShareLineItems.Where(x => x.ProjectID == ProjectID).ToList();
                if (landownerCostShareLineItemsOnProject.Any(x => x.LandownerCostShareLineItemStatus == LandownerCostShareLineItemStatus.Planned))
                {
                    yield return new SitkaValidationResult<BasicsViewModel, int?>($"Before marking the {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} completed, all {Models.FieldDefinition.GrantAllocationAwardLandownerCostShareLineItem} Treatments must be Completed or Cancelled.", m => m.ProjectStageID);
                }
            }


            if ((ProjectStageID == ProjectStage.Completed.ProjectStageID) && CompletionDate?.Year > currentYear)
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
