/*-----------------------------------------------------------------------
<copyright file="EditProjectViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.Shared.ProjectControls
{
    public class EditProjectViewModel : FormViewModel, IValidatableObject
    {
        public int ProjectID { get; set; }

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
        public int ProjectStageID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ExpirationDate)]
        public DateTime? ExpirationDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.StartApprovalDate)]
        public DateTime? PlannedDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.CompletionDate)]
        public DateTime? CompletionDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectType)]
        [Required(ErrorMessage = "This field is required.")]
        public int? ProjectTypeID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.EstimatedTotalCost)]
        public Money? EstimatedTotalCost { get; set; }

        public bool HasExistingProjectUpdate { get; set; }

        public int? OldProjectStageID { get; set; }

        public int FocusAreaID { get; set; }


        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditProjectViewModel()
        {
        }

        public EditProjectViewModel(Models.Project project, bool hasExistingProjectUpdate)
        {
            ProjectTypeID = project.ProjectTypeID;
            ProjectID = project.ProjectID;
            ProjectName = project.ProjectName;
            ProjectDescription = project.ProjectDescription;
            ProjectStageID = project.ProjectStageID;
            OldProjectStageID = project.ProjectStageID;
            ExpirationDate = project.ExpirationDate;
            PlannedDate = project.PlannedDate;
            CompletionDate = project.CompletionDate;
            EstimatedTotalCost = project.EstimatedTotalCost;
            HasExistingProjectUpdate = hasExistingProjectUpdate;
            FocusAreaID = project.FocusAreaID;
        }

        public void UpdateModel(Models.Project project, Person currentPerson)
        {
            project.ProjectName = ProjectName;
            project.ProjectDescription = ProjectDescription;
            project.ProjectTypeID = ProjectTypeID ?? ModelObjectHelpers.NotYetAssignedID;
            project.ProjectStageID = ProjectStageID;
            project.ExpirationDate = ExpirationDate;
            project.PlannedDate = PlannedDate;
            project.CompletionDate = CompletionDate;
            project.EstimatedTotalCost = EstimatedTotalCost;
            project.FocusAreaID = FocusAreaID;
            var projectType =
                HttpRequestStorage.DatabaseEntities.ProjectTypes.SingleOrDefault(x =>
                    x.ProjectTypeID == project.ProjectTypeID);
            var appropriateTypesOfProjectAttributes = projectType != null
                ? projectType.GetProjectCustomAttributeTypesForThisProjectType()
                : HttpRequestStorage.DatabaseEntities.ProjectCustomAttributeTypes.Where(x => x.ApplyToAllProjectTypes)
                    .ToList();
            var badProjectAttributes = project.ProjectCustomAttributes.Where(pca =>
                !appropriateTypesOfProjectAttributes.Select(x => x.ProjectCustomAttributeTypeID)
                    .Contains(pca.ProjectCustomAttributeTypeID)).ToList();
            var values = badProjectAttributes.SelectMany(x => x.ProjectCustomAttributeValues).ToList();
            HttpRequestStorage.DatabaseEntities.ProjectCustomAttributeValues.DeleteProjectCustomAttributeValue(values);
            HttpRequestStorage.DatabaseEntities.ProjectCustomAttributes.DeleteProjectCustomAttribute(badProjectAttributes);

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var projects = HttpRequestStorage.DatabaseEntities.Projects.ToList();
            if (!Models.Project.IsProjectNameUnique(projects, ProjectName, ProjectID))
            {
                yield return new SitkaValidationResult<EditProjectViewModel, string>(
                    FirmaValidationMessages.ProjectNameUnique, m => m.ProjectName);
            }

            if (CompletionDate < PlannedDate)
            {
                yield return new SitkaValidationResult<EditProjectViewModel, DateTime?>(
                    FirmaValidationMessages.CompletionDateGreaterThanEqualToImplementationStartYear,
                    m => m.CompletionDate);
            }

            if (ProjectStageID == ProjectStage.Completed.ProjectStageID && !CompletionDate.HasValue)
            {
                yield return new SitkaValidationResult<EditProjectViewModel, DateTime?>($"Since the {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} is in the Completed stage, the Completion year is required", m => m.CompletionDate);
            }

            if (ProjectStageID == ProjectStage.Completed.ProjectStageID)
            {
                var treatmentActivitiesOnProject = HttpRequestStorage.DatabaseEntities.TreatmentActivities.Where(x => x.ProjectID == ProjectID).ToList();
                if (treatmentActivitiesOnProject.Any(x => x.TreatmentActivityStatus == TreatmentActivityStatus.Planned))
                {
                    yield return new SitkaValidationResult<EditProjectViewModel, int>($"Before marking the {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} completed, all Treatment Activities must be Completed or Cancelled.", m => m.ProjectStageID);
                }
            }

            var isCompletedOrPostImplementation = ProjectStageID == ProjectStage.Completed.ProjectStageID || ProjectStageID == ProjectStage.PostImplementation.ProjectStageID;
            if (isCompletedOrPostImplementation && CompletionDate > DateTime.Now)
            {
                var errorMessage = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} is in the Completed or Post-Implementation stage: " +
                                   $"the {Models.FieldDefinition.CompletionDate.GetFieldDefinitionLabel()} must be less than or equal to the current year";
                yield return new SitkaValidationResult<EditProjectViewModel, DateTime?>(errorMessage, m => m.CompletionDate);
            }

            if (HasExistingProjectUpdate && OldProjectStageID != ProjectStageID)
            {
                var errorMessage = $"There are updates to this {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} that have not been submitted.<br />" +
                                   "Making this change can potentially affect that update in process.<br />" +
                                   $"Please delete the update if you want to change this {Models.FieldDefinition.Project.GetFieldDefinitionLabel()}'s stage.";
                yield return new SitkaValidationResult<EditProjectViewModel, int>(errorMessage, m => m.ProjectStageID);
            }

            var projectTypeIDsWhereFocusAreaRequired = Models.ProjectType.GetAllProjectTypeIDsWhereFocusAreaRequired();

            //if (FocusAreaID == null && projectTypeIDsWhereFocusAreaRequired.Contains(ProjectTypeID.Value))
            //{
            //    var errorMessage = $"Focus Area is required for your selected {Models.FieldDefinition.ProjectType.GetFieldDefinitionLabel()}";
            //    yield return new SitkaValidationResult<EditProjectViewModel, int?>(errorMessage, m => m.FocusAreaID);
            //}
        }
    }
}