/*-----------------------------------------------------------------------
<copyright file="EditProjectViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectIdentifier)]
        [StringLength(Models.Project.FieldLengths.ProjectGisIdentifier)]
        public string ProjectGisIdentifier { get; set; }

        

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectDescription)]
        [StringLength(Models.Project.MaxLengthForProjectDescription)]
        public string ProjectDescription { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectStage)]
        [Required]
        public int ProjectStageID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ExpirationDate)]
        public DateTime? ExpirationDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectInitiationDate)]
        public DateTime? ProjectInitiationDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.CompletionDate)]
        public DateTime? CompletionDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectType)]
        [Required(ErrorMessage = "This field is required.")]
        public int? ProjectTypeID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.EstimatedTotalCost)]
        public Money? EstimatedTotalCost { get; set; }

        public bool HasExistingProjectUpdate { get; set; }

        public int? OldProjectStageID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FocusArea)]
        public int? FocusAreaID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.PercentageMatch)]
        public int? PercentageMatch { get; set; }

        public List<ProjectProgramSimple> ProjectProgramSimples { get; set; }

        /// <summary>
        /// Only used to display the Project Stage Name when the ProjectStage is not editable
        /// </summary>
        public string ProjectStageName { get; set; }

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
            ProjectStageName = project.ProjectStage.ProjectStageDisplayName;
            ExpirationDate = project.ExpirationDate;
            ProjectInitiationDate = project.PlannedDate;
            CompletionDate = project.CompletionDate;
            EstimatedTotalCost = project.EstimatedTotalCost;
            HasExistingProjectUpdate = hasExistingProjectUpdate;
            FocusAreaID = project.FocusAreaID;
            ProjectProgramSimples = project.ProjectPrograms.Select(x => new ProjectProgramSimple(x)).ToList();
            ProjectGisIdentifier = project.ProjectGisIdentifier;
            PercentageMatch = project.PercentageMatch;
        }

        public void UpdateModel(Models.Project project, Person currentPerson)
        {

            if (ProjectProgramSimples == null)
            {
                ProjectProgramSimples = new List<ProjectProgramSimple>();
            }

            project.ProjectName = ProjectName;
            project.ProjectDescription = ProjectDescription;
            project.ProjectTypeID = ProjectTypeID ?? ModelObjectHelpers.NotYetAssignedID;
            project.ProjectStageID = ProjectStageID;
            project.ExpirationDate = ExpirationDate;
            project.PlannedDate = ProjectInitiationDate;
            project.CompletionDate = CompletionDate;
            project.EstimatedTotalCost = EstimatedTotalCost;
            project.FocusAreaID = FocusAreaID;
            project.PercentageMatch = PercentageMatch;
            // 2022-7-18 TK&RO - adding this trim to prevent GIS Identifiers with leading or trailing spaces as this caused comparison issues in the bulk import
            project.ProjectGisIdentifier = string.IsNullOrEmpty(ProjectGisIdentifier) ? null : ProjectGisIdentifier.Trim();
            var projectType =
                HttpRequestStorage.DatabaseEntities.ProjectTypes.SingleOrDefault(x =>
                    x.ProjectTypeID == project.ProjectTypeID);

            var existingProjectPrograms = project.ProjectPrograms.Select(x => x.ProjectProgramID).ToList();

            var projectProgramSimplesNew = ProjectProgramSimples.Where(x => x.ProjectProgramID < 0).ToList();
            var projectProgramsNew = projectProgramSimplesNew.Select(x => new ProjectProgram(x.ProjectID, x.ProgramID))
                .ToList();
            var programs = HttpRequestStorage.DatabaseEntities.Programs.ToList();
            foreach (var projectProgram in projectProgramsNew)
            {
                var program = programs.Single(x => x.ProgramID == projectProgram.ProgramID);
                projectProgram.Program = program;
                projectProgram.Project = project;
                project.ProjectPrograms.Add(projectProgram);
            }

  
            

            var projectProgramsCurrentList = ProjectProgramSimples.Select(x => x.ProjectProgramID).ToList();
            var deleteIDList = existingProjectPrograms.Where(x => !projectProgramsCurrentList.Contains(x)).ToList();
            var deleteList = project.ProjectPrograms.Where(x => deleteIDList.Contains(x.ProjectProgramID)).ToList();
            deleteList.ForEach(x => x.Delete(HttpRequestStorage.DatabaseEntities));




        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var projects = HttpRequestStorage.DatabaseEntities.Projects.ToList();
            if (!Models.Project.IsProjectNameUnique(projects, ProjectName, ProjectID))
            {
                yield return new SitkaValidationResult<EditProjectViewModel, string>(
                    FirmaValidationMessages.ProjectNameUnique, m => m.ProjectName);
            }

            if (CompletionDate < ProjectInitiationDate)
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
                var landownerCostShareLineItemsOnProject = HttpRequestStorage.DatabaseEntities.GrantAllocationAwardLandownerCostShareLineItems.Where(x => x.ProjectID == ProjectID).ToList();
                if (landownerCostShareLineItemsOnProject.Any(x => x.LandownerCostShareLineItemStatus == LandownerCostShareLineItemStatus.Planned))
                {
                    yield return new SitkaValidationResult<EditProjectViewModel, int>($"Before marking the {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} completed, all {Models.FieldDefinition.GrantAllocationAwardLandownerCostShareLineItem.GetFieldDefinitionLabel()} Treatments must be Completed or Cancelled.", m => m.ProjectStageID);
                }
            }

            var isCompleted = ProjectStageID == ProjectStage.Completed.ProjectStageID;
            if (isCompleted && (CompletionDate > DateTime.Now || CompletionDate == null))
            {
                var errorMessage = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} is in the Completed stage: " +
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

            if (PercentageMatch.HasValue && (PercentageMatch.Value < 0 || PercentageMatch.Value > 100))
            {
                yield return new SitkaValidationResult<EditProjectViewModel, int?>("Percentage Match must be blank or between 0 and 100 inclusive.", m => m.PercentageMatch);
            }
        }
    }
}