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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common.Models;
using System.Data.Entity;

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

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectInitiationDate)]
        [Required]
        public DateTime? PlannedDate { get; set; }
        
        [FieldDefinitionDisplay(FieldDefinitionEnum.ExpirationDate)]
        public DateTime? ExpirationDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.CompletionDate)]
        public DateTime? CompletionDate { get; set; }

		[FieldDefinitionDisplay(FieldDefinitionEnum.FocusArea)]
        public int? FocusAreaID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.PercentageMatch)]
        public int? PercentageMatch { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.LeadImplementerOrganization)]
        public int LeadImplementerID { get; set; }


        public int? ImportExternalProjectStagingID { get; set; }

        public List<ProjectProgramSimple> ProjectProgramSimples { get; set; }


        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public BasicsViewModel()
        {
            ProjectProgramSimples = new List<ProjectProgramSimple>();
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
            ProjectProgramSimples = project.ProjectPrograms.Select(x => new ProjectProgramSimple(x)).ToList();
            PercentageMatch = project.PercentageMatch;
            LeadImplementerID = project.ProjectOrganizations.SingleOrDefault(x => x.RelationshipTypeID == RelationshipType.LeadImplementerID)?.OrganizationID ?? -1;

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
            project.PercentageMatch = PercentageMatch;

            

            if (LeadImplementerID > 0)
            {
                HttpRequestStorage.DatabaseEntities.ProjectOrganizations.Load();
                var allProjectOrgs = HttpRequestStorage.DatabaseEntities.ProjectOrganizations.Local;
                var newLeadImplementerOrg = new ProjectOrganization(project.ProjectID, LeadImplementerID, RelationshipType.LeadImplementerID);
                var updatedListOfProjectOrgs = project.ProjectOrganizations.ToList();

                var previousLeadImplementerProjectOrganization = project.ProjectOrganizations.SingleOrDefault(x => x.RelationshipTypeID == RelationshipType.LeadImplementerID);
                if (previousLeadImplementerProjectOrganization != null)
                {
                    updatedListOfProjectOrgs = updatedListOfProjectOrgs.Where(x => x.ProjectOrganizationID != previousLeadImplementerProjectOrganization.ProjectOrganizationID).ToList();
                }

                updatedListOfProjectOrgs.Add(newLeadImplementerOrg);
                project.ProjectOrganizations.Merge(updatedListOfProjectOrgs, allProjectOrgs, (x, y) => x.ProjectID == y.ProjectID && x.OrganizationID == y.OrganizationID && x.RelationshipTypeID == y.RelationshipTypeID );
            }

            if (ProjectProgramSimples == null)
            {
                ProjectProgramSimples = new List<ProjectProgramSimple>();
            }

            var projectProgramsUpdatedList = ProjectProgramSimples.Select(x => new ProjectProgram(x.ProjectID, x.ProgramID))
                .ToList();
            HttpRequestStorage.DatabaseEntities.ProjectPrograms.Load();
            var allProjectPrograms = HttpRequestStorage.DatabaseEntities.ProjectPrograms.Local;

            project.ProjectPrograms
                .Merge(projectProgramsUpdatedList,
                    allProjectPrograms,
                    (x, y) => x.ProjectID == y.ProjectID && x.ProgramID == y.ProgramID);



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

            //if (!Models.Project.IsProjectNameUnique(projects, ProjectName, ProjectID))
            //{
            //    yield return new SitkaValidationResult<BasicsViewModel, string>(FirmaValidationMessages.ProjectNameUnique, m => m.ProjectName);
            //}

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
                yield return new SitkaValidationResult<BasicsViewModel, DateTime?>($"Since the {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} is in the Completed stage, the Completion Date is required", m => m.CompletionDate);
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

            if (PercentageMatch.HasValue && (PercentageMatch.Value < 0 || PercentageMatch.Value > 100))
            {
                yield return new SitkaValidationResult<BasicsViewModel, int?>("Percentage Match must be blank or between 0 and 100 inclusive.", m => m.PercentageMatch);
            }
        }
    }
}
