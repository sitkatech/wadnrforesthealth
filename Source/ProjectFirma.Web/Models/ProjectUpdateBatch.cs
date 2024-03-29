﻿/*-----------------------------------------------------------------------
<copyright file="ProjectUpdateBatch.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using GeoJSON.Net.Feature;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Views.ProjectUpdate;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectUpdateBatch
    {
        public bool IsApproved => ProjectUpdateState == ProjectUpdateState.Approved;

        public bool IsReturned => ProjectUpdateState == ProjectUpdateState.Returned;

        public bool IsSubmitted => ProjectUpdateState == ProjectUpdateState.Submitted;

        public bool IsCreated => ProjectUpdateState == ProjectUpdateState.Created;

        public bool IsNew => ProjectUpdateState == ProjectUpdateState.Created &&
                             !ModelObjectHelpers.IsRealPrimaryKeyValue(ProjectUpdateBatchID);

        public ProjectUpdateHistory LatestProjectUpdateHistorySubmitted => ProjectUpdateHistories.GetLatestProjectUpdateHistory(ProjectUpdateState.Submitted);

        public DateTime? LatestSubmittalDate => LatestProjectUpdateHistorySubmitted?.TransitionDate;

        public ProjectUpdateHistory LatestProjectUpdateHistoryReturned => ProjectUpdateHistories.GetLatestProjectUpdateHistory(ProjectUpdateState.Returned);

        public bool IsReadyToSubmit => InEditableState && IsPassingAllValidationRules() && ModelObjectHelpers.IsRealPrimaryKeyValue(ProjectUpdateBatchID);

        public bool IsReadyToApprove => IsPassingAllValidationRules();

        private bool IsPassingAllValidationRules()
        {
            return AreProjectBasicsValid && AreExpendituresValid() &&
                  // ArePerformanceMeasuresValid() &&
                   IsProjectLocationSimpleValid() &&
                   IsProjectPriorityLandscapeValid() &&
                   IsProjectRegionValid();
        }

        public bool InEditableState => Project.IsActiveProject() && (IsCreated || IsReturned);

        public static ProjectUpdateBatch GetLatestNotApprovedProjectUpdateBatchOrCreateNew(Project project, Person currentPerson)
        {
            
            ProjectUpdateBatch projectUpdateBatch;
            if (project.GetLatestNotApprovedUpdateBatch() != null)
            {
                projectUpdateBatch = project.GetLatestNotApprovedUpdateBatch();
            }
            else
            {
                projectUpdateBatch = CreateNewProjectUpdateBatchForProject(project, currentPerson);
            }

            return projectUpdateBatch;
        }

        public static ProjectUpdateBatch GetLatestNotApprovedProjectUpdateBatchOrCreateNewAndSaveToDatabase(Project project, Person currentPerson)
        {
            var projectUpdateBatch = GetLatestNotApprovedProjectUpdateBatchOrCreateNew(project, currentPerson);
            if (!ModelObjectHelpers.IsRealPrimaryKeyValue(projectUpdateBatch.ProjectUpdateBatchID))
            {
                HttpRequestStorage.DatabaseEntities.SaveChanges();
            }
            return projectUpdateBatch;
        }

        public static ProjectUpdateBatch CreateNewProjectUpdateBatchForProject(Project project, Person currentPerson)
        {
            var projectUpdateBatch = CreateProjectUpdateBatchAndLogTransition(project, currentPerson);

            // basics & map
            ProjectUpdate.CreateFromProject(projectUpdateBatch);

            // expenditures
            ProjectGrantAllocationExpenditureUpdate.CreateFromProject(projectUpdateBatch);

            // project expenditures exempt reporting years
            ProjectExemptReportingYearUpdate.CreateExpendituresExemptReportingYearsFromProject(projectUpdateBatch);

            // expenditures exempt explanation
            projectUpdateBatch.SyncExpendituresYearsExemptionExplanation();

            // Expected Funding
            ProjectGrantAllocationRequestUpdate.CreateFromProject(projectUpdateBatch);

            // performance measures
            // TODO Neutered Per WA DNR #1446. May decide to bring it back later
            //PerformanceMeasureActualUpdate.CreateFromProject(projectUpdateBatch);

            // project performance measures exempt reporting years
            ProjectExemptReportingYearUpdate.CreatePerformanceMeasuresExemptReportingYearsFromProject(projectUpdateBatch);

            // project exempt reporting years reason
            projectUpdateBatch.SyncPerformanceMeasureActualYearsExemptionExplanation();

            // project locations - detailed
            ProjectLocationUpdate.CreateFromProject(projectUpdateBatch);

            // project priority landscape
            ProjectPriorityLandscapeUpdate.CreateFromProject(projectUpdateBatch);

            // project region
            ProjectRegionUpdate.CreateFromProject(projectUpdateBatch);

            // project county
            ProjectCountyUpdate.CreateFromProject(projectUpdateBatch);

            // treatments
            TreatmentUpdate.CreateFromProject(projectUpdateBatch);

            // photos
            ProjectImageUpdate.CreateFromProject(projectUpdateBatch);
            projectUpdateBatch.IsPhotosUpdated = false;

            // external links
            ProjectExternalLinkUpdate.CreateFromProject(projectUpdateBatch);

            // notes
            ProjectNoteUpdate.CreateFromProject(projectUpdateBatch);

            // organizations
            ProjectOrganizationUpdate.CreateFromProject(projectUpdateBatch);

            ProjectPersonUpdate.CreateFromProject(projectUpdateBatch);

            // Documents
            ProjectDocumentUpdate.CreateFromProject(projectUpdateBatch);

            // Custom attributes
            ProjectCustomAttributeUpdate.CreateFromProject(projectUpdateBatch);

            return projectUpdateBatch;
        }

        /// <summary>
        /// Only public for unit testing
        /// </summary>
        public static ProjectUpdateBatch CreateProjectUpdateBatchAndLogTransition(Project project, Person currentPerson)
        {
            var projectUpdateBatch = new ProjectUpdateBatch(project, DateTime.Now, currentPerson, ProjectUpdateState.Created, false);

            //move optional fields to the project update batch
            projectUpdateBatch.NoRegionsExplanation = project.NoRegionsExplanation;
            projectUpdateBatch.NoPriorityLandscapesExplanation = project.NoPriorityLandscapesExplanation;


            // create a project update history record
            CreateNewTransitionRecord(projectUpdateBatch, ProjectUpdateState.Created, currentPerson, DateTime.Now);
            return projectUpdateBatch;
        }

        public void SyncPerformanceMeasureActualYearsExemptionExplanation()
        {
            PerformanceMeasureActualYearsExemptionExplanation = Project.PerformanceMeasureActualYearsExemptionExplanation;
        }

        public void SyncExpendituresYearsExemptionExplanation()
        {
            NoExpendituresToReportExplanation = Project.NoExpendituresToReportExplanation;
        }

        public void TickleLastUpdateDate(Person currentPerson)
        {
            TickleLastUpdateDate(DateTime.Now, currentPerson);
        }

        private void TickleLastUpdateDate(DateTime transitionDate, Person currentPerson)
        {
            LastUpdateDate = transitionDate;
            LastUpdatePerson = currentPerson;
        }

        private static void RefreshFromDatabase(IEnumerable updates)
        {
            ((IObjectContextAdapter)HttpRequestStorage.DatabaseEntities).ObjectContext.Refresh(RefreshMode.StoreWins, updates);
        }

        public void DeleteProjectImageUpdates()
        {
            DeleteProjectImageUpdates(ProjectImageUpdates);
            RefreshFromDatabase(ProjectImageUpdates);
        }

        public static void DeleteProjectImageUpdates(ICollection<ProjectImageUpdate> projectImageUpdates)
        {
            var projectImageFileResourceIDsToDelete = projectImageUpdates.Where(x => x.FileResourceID.HasValue).Select(x => x.FileResourceID.Value).ToList();
            HttpRequestStorage.DatabaseEntities.ProjectImageUpdates.DeleteProjectImageUpdate(projectImageUpdates);
            HttpRequestStorage.DatabaseEntities.FileResources.DeleteFileResource(projectImageFileResourceIDsToDelete);
        }

        public void DeleteProjectExternalLinkUpdates()
        {
            HttpRequestStorage.DatabaseEntities.ProjectExternalLinkUpdates.DeleteProjectExternalLinkUpdate(ProjectExternalLinkUpdates);
            RefreshFromDatabase(ProjectExternalLinkUpdates);
        }

        public void DeleteProjectNoteUpdates()
        {
            HttpRequestStorage.DatabaseEntities.ProjectNoteUpdates.DeleteProjectNoteUpdate(ProjectNoteUpdates);
            RefreshFromDatabase(ProjectNoteUpdates);
        }
        
        public void DeleteProjectDocumentUpdates()
        {
            HttpRequestStorage.DatabaseEntities.ProjectDocumentUpdates.DeleteProjectDocumentUpdate(ProjectDocumentUpdates);
            RefreshFromDatabase(ProjectDocumentUpdates);
        }

        public void DeletePerformanceMeasuresProjectExemptReportingYearUpdates()
        {
            var performanceMeasuresExemptReportingYears = this.GetPerformanceMeasuresExemptReportingYears();
            HttpRequestStorage.DatabaseEntities.ProjectExemptReportingYearUpdates.DeleteProjectExemptReportingYearUpdate(performanceMeasuresExemptReportingYears);
            PerformanceMeasureActualYearsExemptionExplanation = null;
            RefreshFromDatabase(performanceMeasuresExemptReportingYears);
        }
        public void DeleteExpendituresProjectExemptReportingYearUpdates()
        {
            var performanceMeasuresExemptReportingYears = this.GetExpendituresExemptReportingYears();
            HttpRequestStorage.DatabaseEntities.ProjectExemptReportingYearUpdates.DeleteProjectExemptReportingYearUpdate(performanceMeasuresExemptReportingYears);
            NoExpendituresToReportExplanation = null;
            RefreshFromDatabase(performanceMeasuresExemptReportingYears);
        }

        public void DeleteProjectGrantAllocationExpenditureUpdates()
        {
            HttpRequestStorage.DatabaseEntities.ProjectGrantAllocationExpenditureUpdates.DeleteProjectGrantAllocationExpenditureUpdate(ProjectGrantAllocationExpenditureUpdates);
            RefreshFromDatabase(ProjectGrantAllocationExpenditureUpdates);
        }

        public void DeleteProjectGrantAllocationRequestUpdates()
        {
            HttpRequestStorage.DatabaseEntities.ProjectGrantAllocationRequestUpdates.DeleteProjectGrantAllocationRequestUpdate(ProjectGrantAllocationRequestUpdates);
            RefreshFromDatabase(ProjectGrantAllocationRequestUpdates);
        }

        public void DeletePerformanceMeasureActualUpdates()
        {
            HttpRequestStorage.DatabaseEntities.PerformanceMeasureActualSubcategoryOptionUpdates.DeletePerformanceMeasureActualSubcategoryOptionUpdate(PerformanceMeasureActualUpdates.SelectMany(x => x.PerformanceMeasureActualSubcategoryOptionUpdates.Select(y => y.PerformanceMeasureActualSubcategoryOptionUpdateID)).ToList());
            HttpRequestStorage.DatabaseEntities.PerformanceMeasureActualUpdates.DeletePerformanceMeasureActualUpdate(PerformanceMeasureActualUpdates);
            RefreshFromDatabase(PerformanceMeasureActualUpdates);
        }

        public void DeleteProjectLocationUpdates()
        {
            HttpRequestStorage.DatabaseEntities.ProjectLocationUpdates.DeleteProjectLocationUpdate(ProjectLocationUpdates);
            RefreshFromDatabase(ProjectLocationUpdates);
        }

        public void DeleteProjectLocationStagingUpdates()
        {
            HttpRequestStorage.DatabaseEntities.ProjectLocationStagingUpdates.DeleteProjectLocationStagingUpdate(ProjectLocationStagingUpdates);
            RefreshFromDatabase(ProjectLocationStagingUpdates);
        }

        public void DeleteProjectPriorityLandscapeUpdates()
        {
            HttpRequestStorage.DatabaseEntities.ProjectPriorityLandscapeUpdates.DeleteProjectPriorityLandscapeUpdate(ProjectPriorityLandscapeUpdates);
            RefreshFromDatabase(ProjectPriorityLandscapeUpdates);
        }

        public void DeleteProjectRegionUpdates()
        {
            HttpRequestStorage.DatabaseEntities.ProjectRegionUpdates.DeleteProjectRegionUpdate(ProjectRegionUpdates);
            RefreshFromDatabase(ProjectRegionUpdates);
        }
        
        public void DeleteProjectOrganizationUpdates()
        {
            HttpRequestStorage.DatabaseEntities.ProjectOrganizationUpdates.DeleteProjectOrganizationUpdate(ProjectOrganizationUpdates);
            RefreshFromDatabase(ProjectOrganizationUpdates);
        }

        public void DeleteProjectUpdatePrograms()
        {
            HttpRequestStorage.DatabaseEntities.ProjectUpdatePrograms.DeleteProjectUpdateProgram(ProjectUpdatePrograms);
            RefreshFromDatabase(ProjectUpdatePrograms);
        }

        public void DeleteProjectAttributeUpdates()
        {
            var values = ProjectCustomAttributeUpdates.SelectMany(x => x.ProjectCustomAttributeUpdateValues).ToList();
            HttpRequestStorage.DatabaseEntities.ProjectCustomAttributeUpdateValues.DeleteProjectCustomAttributeUpdateValue(values);
            HttpRequestStorage.DatabaseEntities.ProjectCustomAttributeUpdates.DeleteProjectCustomAttributeUpdate(ProjectCustomAttributeUpdates);
            RefreshFromDatabase(ProjectCustomAttributeUpdates);
        }

        public void DeleteProjectContactUpdates()
        {
            HttpRequestStorage.DatabaseEntities.ProjectPersonUpdates.DeleteProjectPersonUpdate(ProjectPersonUpdates);
            RefreshFromDatabase(ProjectPersonUpdates);
        }

        public void DeleteTreatmentUpdates()
        {
            HttpRequestStorage.DatabaseEntities.TreatmentUpdates.DeleteTreatmentUpdate(TreatmentUpdates);
            RefreshFromDatabase(TreatmentUpdates);
        }

        public void DeleteAll()
        {
            DeleteFull(HttpRequestStorage.DatabaseEntities);
        }

        public BasicsValidationResult ValidateProjectBasics()
        {
            return new BasicsValidationResult(ProjectUpdate);
        }

        private bool? _areProjectBasicsValid;
        public bool AreProjectBasicsValid
        {
            get
            {
                if (!_areProjectBasicsValid.HasValue)
                {
                    _areProjectBasicsValid = ValidateProjectBasics().IsValid;
                }
                return _areProjectBasicsValid.Value;
            }
            private set => _areProjectBasicsValid = value;
        }

        public ProjectAttributeValidationResult ValidateProjectAttributes()
        {
            return new ProjectAttributeValidationResult(ProjectUpdate);
        }

        private bool? _areProjectAttributesValid;
        public bool AreProjectAttributesValid
        {
            get
            {
                if (!_areProjectAttributesValid.HasValue)
                {
                    _areProjectAttributesValid = ValidateProjectAttributes().IsValid;
                }
                return _areProjectAttributesValid.Value;
            }
            private set => _areProjectAttributesValid = value;
        }

        public bool NewStageIsPlanningDesign => ProjectUpdate.ProjectStage == ProjectStage.Planned;

        public PerformanceMeasuresValidationResult ValidatePerformanceMeasures()
        {
            if (!AreProjectBasicsValid)
            {
                return new PerformanceMeasuresValidationResult(FirmaValidationMessages.UpdateSectionIsDependentUponBasicsSection);
            }
            
            // validation 1: ensure that we have PM values from ProjectUpdate start year to min(endyear, currentyear); if the ProjectUpdate record has a stage of Planning/Design, we do not do this validation
            var missingYears = new HashSet<int>();
            if (ProjectUpdate.ProjectStage.RequiresPerformanceMeasureActuals() || ProjectUpdate.ProjectStage == ProjectStage.Completed)
            {
                var exemptYears = this.GetPerformanceMeasuresExemptReportingYears().Select(x => x.CalendarYear).ToList();
                var yearsExpected = ProjectUpdate.GetProjectUpdateImplementationStartToCompletionDateRange().Where(x => !exemptYears.Contains(x)).ToList();
                var yearsEntered = PerformanceMeasureActualUpdates.Select(x => x.CalendarYear).Distinct();
                missingYears = yearsExpected.GetMissingYears(yearsEntered);
            }
            // validation 2: incomplete PM row (missing performanceMeasureSubcategory option id)
            var performanceMeasureActualUpdatesWithIncompleteWarnings = ValidateNoIncompletePerformanceMeasureActualUpdateRow();

            //validation 3: duplicate PM row
            var performanceMeasureActualUpdatesWithDuplicateWarnings = ValidateNoDuplicatePerformanceMeasureActualUpdateRow();

            //validation4: data entered for exempt years
            var performanceMeasureActualUpdatesWithExemptYear = ValidateNoExemptYearsWithReportedPerformanceMeasureRow();

            var performanceMeasuresValidationResult = new PerformanceMeasuresValidationResult(missingYears, performanceMeasureActualUpdatesWithIncompleteWarnings, performanceMeasureActualUpdatesWithDuplicateWarnings, performanceMeasureActualUpdatesWithExemptYear);
            return performanceMeasuresValidationResult;
        }

        private HashSet<int> ValidateNoIncompletePerformanceMeasureActualUpdateRow()
        {
            var performanceMeasureActualUpdatesWithMissingSubcategoryOptions =
                PerformanceMeasureActualUpdates.Where(
                    x => !x.ActualValue.HasValue || x.PerformanceMeasure.PerformanceMeasureSubcategories.Count != x.PerformanceMeasureActualSubcategoryOptionUpdates.Count).ToList();
            return new HashSet<int>(performanceMeasureActualUpdatesWithMissingSubcategoryOptions.Select(x => x.PerformanceMeasureActualUpdateID));
        }

        private HashSet<int> ValidateNoDuplicatePerformanceMeasureActualUpdateRow()
        {
            if (PerformanceMeasureActualUpdates == null)
            {
                return new HashSet<int>();
            }
            var duplicates =  PerformanceMeasureActualUpdates
                .GroupBy(x => new { x.PerformanceMeasureID, x.CalendarYear })
                .Select(x => x.ToList())
                .ToList()
                .Select(x => x)
                .Where(x => x.Select(m => m.PerformanceMeasureActualSubcategoryOptionUpdates).ToList().Select(z => String.Join("_", z.Select(s => s.PerformanceMeasureSubcategoryOptionID).ToList())).ToList().HasDuplicates()).ToList();

            return new HashSet<int>(duplicates.SelectMany(x => x).ToList().Select(x => x.PerformanceMeasureActualUpdateID));
        }

        private HashSet<int> ValidateNoExemptYearsWithReportedPerformanceMeasureRow()
        {
            if (PerformanceMeasureActualUpdates == null)
            {
                return new HashSet<int>();
            }
            var exemptYears = this.GetPerformanceMeasuresExemptReportingYears().Select(x => x.CalendarYear).ToList();

            var performanceMeasureActualUpdatesWithExemptYear =
                PerformanceMeasureActualUpdates.Where(x => exemptYears.Contains(x.CalendarYear)).ToList();            

            return new HashSet<int>(performanceMeasureActualUpdatesWithExemptYear.Select(x => x.PerformanceMeasureActualUpdateID));
        }

        public bool ArePerformanceMeasuresValid()
        {
            return NewStageIsPlanningDesign || ValidatePerformanceMeasures().IsValid;
        }

        public List<string> ValidateExpendituresAndForceValidation()
        {
            AreProjectBasicsValid = ValidateProjectBasics().IsValid;
            return ValidateExpenditures();
        }

        public ExpectedFundingValidationResult ValidateExpectedFunding(List<ProjectGrantAllocationRequestSimple> newprojectGrantAllocationRequests)
        {
            return new ExpectedFundingValidationResult();
        }

        public List<string> ValidateExpenditures()
        {
            if (!AreProjectBasicsValid)
            {
                return new List<string> {FirmaValidationMessages.UpdateSectionIsDependentUponBasicsSection};
            }

            if (ProjectUpdate.ProjectStage.RequiresReportedExpenditures() || ProjectUpdate.ProjectStage == ProjectStage.Completed)
            {
                // validation 1: ensure that we have expenditure values from ProjectUpdate start year to min(endyear, currentyear)
                var yearsExpected = ProjectUpdate.GetProjectUpdatePlanningDesignStartToCompletionDateRange();
                var validateExpenditures = ExpendituresValidationResult.ValidateImpl(
                    this.GetExpendituresExemptReportingYears().Select(x => new ProjectExemptReportingYearSimple(x)).ToList(),
                    NoExpendituresToReportExplanation, yearsExpected, new List<IGrantAllocationExpenditure>(ProjectGrantAllocationExpenditureUpdates));
                return validateExpenditures;
            }
            return new List<string>();
        }

        public bool AreExpendituresValid()
        {
            return ValidateExpenditures().Count == 0;
        }

        public OrganizationsValidationResult ValidateOrganizations()
        {
            return new OrganizationsValidationResult(ProjectOrganizationUpdates.Select(x => new ProjectOrganizationSimple(x))
                .ToList());
        }

        public bool AreOrganizationsValid()
        {
            return ValidateOrganizations().IsValid;
        }

        public ContactsValidationResult ValidateContacts()
        {
            return new ContactsValidationResult(ProjectPersonUpdates.Select(x => new ProjectPersonSimple(x))
                .ToList());
        }

        public bool AreContactsValid()
        {
            return ValidateContacts().IsValid;
        }

        public LocationSimpleValidationResult ValidateProjectLocationSimple()
        {           
            var incomplete = ProjectUpdate.ProjectLocationPoint == null &&
                             string.IsNullOrWhiteSpace(ProjectUpdate.ProjectLocationNotes);

            var locationSimpleValidationResult = new LocationSimpleValidationResult(incomplete);

            return locationSimpleValidationResult;
        }
        
        public bool IsProjectLocationSimpleValid()
        {
            return ValidateProjectLocationSimple().IsValid;
        }

        public DNRUplandRegionsValidationResult ValidateProjectRegion()
        {
            var incomplete = !ProjectRegionUpdates.Any() && string.IsNullOrWhiteSpace(NoRegionsExplanation);
            var regionValidationResult = new DNRUplandRegionsValidationResult(incomplete);
            return regionValidationResult;
        }

        public CountiesValidationResult ValidateProjectCounty()
        {
            var incomplete = !ProjectCountyUpdates.Any() && string.IsNullOrWhiteSpace(NoCountiesExplanation);
            var countyValidationResult = new CountiesValidationResult(incomplete);
            return countyValidationResult;
        }

        public bool IsProjectRegionValid()
        {
            return ValidateProjectRegion().IsValid;
        }

        public bool IsProjectCountyValid()
        {
            return ValidateProjectCounty().IsValid;
        }

        public void DeleteProjectCountyUpdates()
        {
            HttpRequestStorage.DatabaseEntities.ProjectCountyUpdates.DeleteProjectCountyUpdate(ProjectCountyUpdates);
            RefreshFromDatabase(ProjectCountyUpdates);
        }

        public PriorityLandscapesValidationResult ValidateProjectPriorityLandscape()
        {
            var incomplete = !ProjectPriorityLandscapeUpdates.Any() && string.IsNullOrWhiteSpace(NoPriorityLandscapesExplanation);
            var priorityLandscapeValidationResult = new PriorityLandscapesValidationResult(incomplete);
            return priorityLandscapeValidationResult;
        }

        public bool IsProjectPriorityLandscapeValid()
        {
            return ValidateProjectPriorityLandscape().IsValid;
        }

        public void SubmitToReviewer(Person currentPerson, DateTime transitionDate)
        {
            Check.Require(IsReadyToSubmit, $"You cannot submit a {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} update that is not ready to be submitted!");
            CreateNewTransitionRecord(this, ProjectUpdateState.Submitted, currentPerson, transitionDate);
        }

        public void Return(Person currentPerson, DateTime transitionDate)
        {
            Check.Require(IsSubmitted, $"You cannot return a {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} update that has not been submitted!");
            CreateNewTransitionRecord(this, ProjectUpdateState.Returned, currentPerson, transitionDate);
        }

        public void Approve(
            // TODO: Neutered per #1136; most likely will bring back when BOR project starts
            //IList<ProjectBudget> projectBudgets, 
            Person currentPerson, DateTime transitionDate,
            IList<ProjectExemptReportingYear> projectExemptReportingYears,
            IList<ProjectGrantAllocationExpenditure> projectGrantAllocationExpenditures,
            IList<ProjectFundingSource> projectFundingSources,
            IList<PerformanceMeasureActual> performanceMeasureActuals,
            IList<PerformanceMeasureActualSubcategoryOption> performanceMeasureActualSubcategoryOptions,
            IList<ProjectExternalLink> projectExternalLinks, IList<ProjectNote> projectNotes,
            IList<ProjectImage> projectImages, IList<ProjectLocation> projectLocations,
            IList<ProjectPriorityLandscape> projectPriorityLandscapes, 
            IList<ProjectRegion> projectRegions, 
            IList<ProjectCounty> projectCounties,
            IList<ProjectGrantAllocationRequest> projectGrantAllocationRequests,
            IList<ProjectOrganization> allProjectOrganizations,
            IList<ProjectDocument> allProjectDocuments,
            IList<ProjectCustomAttribute> allProjectCustomAttributes,
            IList<ProjectCustomAttributeValue> allProjectCustomAttributeValues,
            IList<ProjectPerson> allProjectPersons,
            IList<ProjectProgram> allProjectPrograms,
            IList<Treatment> allTreatments)
        {
            Check.Require(IsSubmitted, $"You cannot approve a {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} update that has not been submitted!");
            CommitChangesToProject(projectExemptReportingYears,
                projectGrantAllocationExpenditures,
                projectFundingSources,
                // TODO: Neutered per #1136; most likely will bring back when BOR project starts
                //                projectBudgets,
                performanceMeasureActuals,
                performanceMeasureActualSubcategoryOptions,
                projectExternalLinks,
                projectNotes,
                projectImages,
                projectLocations,
                projectPriorityLandscapes,
                projectRegions,
                projectCounties,
                projectGrantAllocationRequests,
                allProjectOrganizations,
                allProjectDocuments,
                allProjectCustomAttributes,
                allProjectCustomAttributeValues,
                allProjectPersons,
                allProjectPrograms,
                allTreatments);
            CreateNewTransitionRecord(this, ProjectUpdateState.Approved, currentPerson, transitionDate);
            PushTransitionRecordsToAuditLog();
        }

        private void PushTransitionRecordsToAuditLog()
        {
            foreach (var projectUpdateHistory in ProjectUpdateHistories.ToList())
            {
                var auditLog = new AuditLog(projectUpdateHistory.UpdatePerson,
                    projectUpdateHistory.TransitionDate,
                    AuditLogEventType.Added,
                    $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update",
                    projectUpdateHistory.ProjectUpdateHistoryID,
                    $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Update record",
                    projectUpdateHistory.ProjectUpdateState.ProjectUpdateStateDisplayName) {ProjectID = ProjectID};
            }
        }

        private void CommitChangesToProject( 
                // TODO: Neutered per #1136; most likely will bring back when BOR project starts
//            IList<ProjectBudget> projectBudgets,
                IList<ProjectExemptReportingYear> projectExemptReportingYears,
                IList<ProjectGrantAllocationExpenditure> projectGrantAllocationExpenditures,
                IList<ProjectFundingSource> projectFundingSources,
                IList<PerformanceMeasureActual> performanceMeasureActuals,
                IList<PerformanceMeasureActualSubcategoryOption> performanceMeasureActualSubcategoryOptions,
                IList<ProjectExternalLink> projectExternalLinks, IList<ProjectNote> projectNotes,
                IList<ProjectImage> projectImages, IList<ProjectLocation> projectLocations,
                IList<ProjectPriorityLandscape> projectPriorityLandscapes,
                IList<ProjectRegion> projectRegions,
                IList<ProjectCounty> projectCounties,
                IList<ProjectGrantAllocationRequest> projectGrantAllocationRequests,
                IList<ProjectOrganization> allProjectOrganizations,
                IList<ProjectDocument> allProjectDocuments,
                IList<ProjectCustomAttribute> allProjectCustomAttributes,
                IList<ProjectCustomAttributeValue> allProjectCustomAttributeValues,
                IList<ProjectPerson> allProjectPeople,
                IList<ProjectProgram> allProjectPrograms,
                IList<Treatment> allTreatments)
        {
            // basics
            ProjectUpdate.CommitChangesToProject(Project);

            //Programs
            ProjectUpdateProgram.CommitChangesToProject(this, allProjectPrograms);

            // expenditures
            ProjectGrantAllocationExpenditureUpdate.CommitChangesToProject(this, projectGrantAllocationExpenditures);

            // expected funding
            ProjectGrantAllocationRequestUpdate.CommitChangesToProject(this, projectGrantAllocationRequests);

            ProjectFundingSourceUpdate.CommitChangesToProject(this, projectFundingSources);

            // TODO: Neutered per #1136; most likely will bring back when BOR project starts
            //  project budgets
            //ProjectBudgetUpdate.CommitChangesToProject(this, projectBudgets);

            // only relevant for stages past planning/design
            if (!NewStageIsPlanningDesign)
            {
                //// performance measures
                // TODO Neutered Per WA DNR #1446. May decide to bring it back later 
                //PerformanceMeasureActualUpdate.CommitChangesToProject(this, performanceMeasureActuals,
                //    performanceMeasureActualSubcategoryOptions);

                // project exempt reporting years
                ProjectExemptReportingYearUpdate.CommitChangesToProject(this, projectExemptReportingYears);

                // project exempt reporting years reason
                Project.PerformanceMeasureActualYearsExemptionExplanation =
                    PerformanceMeasureActualYearsExemptionExplanation;
            }

            // project location simple
            ProjectUpdate.CommitSimpleLocationToProject(Project);

            // project location detailed
            ProjectLocationUpdate.CommitChangesToProject(this, projectLocations);

            // project priorityLandscape
            ProjectPriorityLandscapeUpdate.CommitChangesToProject(this, projectPriorityLandscapes);

            // project region
            ProjectRegionUpdate.CommitChangesToProject(this, projectRegions);

            // project county
            ProjectCountyUpdate.CommitChangesToProject(this, projectCounties);

            // photos
            ProjectImageUpdate.CommitChangesToProject(this, projectImages);
            IsPhotosUpdated = false;

            // external links
            ProjectExternalLinkUpdate.CommitChangesToProject(this, projectExternalLinks);

            // notes
            ProjectNoteUpdate.CommitChangesToProject(this, projectNotes);

            // Organizations
            ProjectOrganizationUpdate.CommitChangesToProject(this, allProjectOrganizations);

            // Organizations
            ProjectPersonUpdate.CommitChangesToProject(this, allProjectPeople);

            // Documents
            ProjectDocumentUpdate.CommitChangesToProject(this, allProjectDocuments);

            // Project Custom Attributes
            ProjectCustomAttributeUpdate.CommitChangesToProject(this, allProjectCustomAttributes, allProjectCustomAttributeValues);

            // Treatments
            TreatmentUpdate.CommitChangesToProject(this, allTreatments);
        }

        public void RejectSubmission(Person currentPerson, DateTime transitionDate)
        {
            Check.Require(IsSubmitted, "You cannot reject a batch that has not been submitted!");
            CreateNewTransitionRecord(this, ProjectUpdateState.Returned, currentPerson, transitionDate);
        }

        /// <summary>
        /// Note, the saving is done by the controller
        /// </summary>
        private static void CreateNewTransitionRecord(ProjectUpdateBatch projectUpdateBatch, ProjectUpdateState projectUpdateState, Person currentPerson, DateTime transitionDate)
        {
            var projectUpdateHistory = new ProjectUpdateHistory(projectUpdateBatch, projectUpdateState, currentPerson, transitionDate);
            HttpRequestStorage.DatabaseEntities.ProjectUpdateHistories.Add(projectUpdateHistory);
            projectUpdateBatch.ProjectUpdateStateID = projectUpdateState.ProjectUpdateStateID;
            projectUpdateBatch.TickleLastUpdateDate(transitionDate, currentPerson);
        }

        public bool AreAccomplishmentsRelevant()
        {
            var projectStage = ProjectUpdate == null ? Project.ProjectStage : ProjectUpdate.ProjectStage;
            return projectStage != ProjectStage.Planned;
        }

        public List<ProjectSectionSimple> GetApplicableWizardSections(bool ignoreStatus)
        {
            return ProjectWorkflowSectionGrouping.All.SelectMany(x => x.GetProjectUpdateSections(this, null, ignoreStatus)).OrderBy(x => x.ProjectWorkflowSectionGrouping.SortOrder).ThenBy(x => x.SortOrder).ToList();
        }

        public FeatureCollection AllDetailedLocationsToGeoJsonFeatureCollection()
        {
            return ProjectLocationUpdates.ToGeoJsonFeatureCollection();
        }

        public bool HasProjectLocationDetail => AllDetailedLocationsToGeoJsonFeatureCollection().Features.Any();

        public void AutoAssignProjectPriorityLandscapesAndDnrUplandRegionsAndCounties()
        {
            var detailedProjectLocations = ProjectLocationUpdates.Select(x => x.ProjectLocationGeometry).ToList();
            var projectHasDetailedLocations = HasProjectLocationDetail;
            var detailedProjectLocationsAggregated = HasProjectLocationDetail ? detailedProjectLocations.Aggregate((x, y) => x.Union(y)) : null;
            var detailedProjectLocationsAggregatedMadeValid = HasProjectLocationDetail ? (detailedProjectLocationsAggregated.IsValid ? detailedProjectLocationsAggregated : detailedProjectLocationsAggregated.ToSqlGeometry().MakeValid().ToDbGeometryWithCoordinateSystem()) : null;

            var projectLocationPoint = this.ProjectUpdate.ProjectLocationPoint;
            var projectLocationPointExists = this.ProjectUpdate.HasProjectLocationPoint;

            var updatedProjectPriorityLandscapes = HttpRequestStorage.DatabaseEntities.PriorityLandscapes
                .Where(x => (projectHasDetailedLocations && x.PriorityLandscapeLocation.Intersects(detailedProjectLocationsAggregatedMadeValid)) || (projectLocationPointExists && x.PriorityLandscapeLocation.Intersects(projectLocationPoint)))
                .ToList()
                .Select(x => new ProjectPriorityLandscapeUpdate(ProjectUpdateBatchID, x.PriorityLandscapeID))
                .ToList();

            if (!updatedProjectPriorityLandscapes.Any())
            {
                NoPriorityLandscapesExplanation =
                    "Neither the simple location nor the detailed location on this project intersects with any Priority Landscape.";
            }
            else
            {
                NoPriorityLandscapesExplanation = null;
            }


            ProjectPriorityLandscapeUpdates.Merge(updatedProjectPriorityLandscapes, HttpRequestStorage.DatabaseEntities.ProjectPriorityLandscapeUpdates.Local, (x, y) => x.ProjectUpdateBatchID == y.ProjectUpdateBatchID && x.PriorityLandscapeID == y.PriorityLandscapeID);

            var updatedProjectRegions = HttpRequestStorage.DatabaseEntities.DNRUplandRegions
                .Where(x => (projectHasDetailedLocations && x.DNRUplandRegionLocation.Intersects(detailedProjectLocationsAggregatedMadeValid)) || (projectLocationPointExists && x.DNRUplandRegionLocation.Intersects(projectLocationPoint)))
                .ToList()
                .Select(x => new ProjectRegionUpdate(ProjectUpdateBatchID, x.DNRUplandRegionID))
                .ToList();

            if (!updatedProjectRegions.Any())
            {
                NoRegionsExplanation =
                    "Neither the simple location nor the detailed location on this project intersects with any DNR Upland Region.";
            }
            else
            {
                NoRegionsExplanation = null;
            }

            ProjectRegionUpdates.Merge(updatedProjectRegions, HttpRequestStorage.DatabaseEntities.ProjectRegionUpdates.Local, (x, y) => x.ProjectUpdateBatchID == y.ProjectUpdateBatchID && x.DNRUplandRegionID == y.DNRUplandRegionID);

            var updatedProjectCounties = HttpRequestStorage.DatabaseEntities.Counties
                .Where(x => (projectHasDetailedLocations && x.CountyFeature.Intersects(detailedProjectLocationsAggregatedMadeValid)) || (projectLocationPointExists && x.CountyFeature.Intersects(projectLocationPoint)))
                .ToList()
                .Select(x => new ProjectCountyUpdate(ProjectUpdateBatchID, x.CountyID))
                .ToList();

            if (!updatedProjectCounties.Any())
            {
                NoCountiesExplanation =
                    "Neither the simple location nor the detailed location on this project intersects with any county.";
            }
            else
            {
                NoCountiesExplanation = null;
            }

            ProjectCountyUpdates.Merge(updatedProjectCounties, HttpRequestStorage.DatabaseEntities.ProjectCountyUpdates.Local, (x, y) => x.ProjectUpdateBatchID == y.ProjectUpdateBatchID && x.CountyID == y.CountyID);
        }
    }
}
