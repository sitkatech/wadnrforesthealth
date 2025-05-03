﻿/*-----------------------------------------------------------------------
<copyright file="ProjectUpdateBatchTest.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Linq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.UnitTestCommon;
using ProjectFirma.Web.Views.ProjectUpdate;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using NUnit.Framework;
// ReSharper disable PossibleInvalidOperationException
// ReSharper disable PossibleNullReferenceException

namespace ProjectFirma.Web.Models
{
    [TestFixture]
    public class ProjectUpdateBatchTest
    {
        [Test]
        public void CreateProjectUpdateBatchAndLogTransitionTest()
        {
            var person = TestFramework.TestPerson.Create();
            var project = TestFramework.TestProject.Create();
            var projectUpdateBatch = ProjectUpdateBatch.CreateProjectUpdateBatchAndLogTransition(project, person);
            Assert.That(projectUpdateBatch, Is.Not.Null, "Should have created one");
            Assert.That(projectUpdateBatch.ProjectUpdateHistories.Count, Is.EqualTo(1), $"Should have created a {FieldDefinition.Project.GetFieldDefinitionLabel()} update history record");
            var projectUpdateHistory = projectUpdateBatch.ProjectUpdateHistories.First();
            Assert.That(projectUpdateHistory.ProjectUpdateState, Is.EqualTo(ProjectUpdateState.Created), $"Should have created a {FieldDefinition.Project.GetFieldDefinitionLabel()} update history record in transition: Created");
            Assert.That(projectUpdateHistory.TransitionDate.ToShortDateString(),
                Is.EqualTo(DateTime.Today.ToShortDateString()),
                $"Should have created a {FieldDefinition.Project.GetFieldDefinitionLabel()} update history record and the date should be today");
        }

        [Test]
        [Ignore]
        public void ProjectUpdateBatchStatesTest()
        {
            var person = TestFramework.TestPerson.Create();
            var project = TestFramework.TestProject.Create();
            var projectUpdateBatch = ProjectUpdateBatch.CreateProjectUpdateBatchAndLogTransition(project, person);
            var projectUpdate = TestFramework.TestProjectUpdate.Create(projectUpdateBatch);
            var currentYear = FirmaDateUtilities.CalculateCurrentYearToUseForRequiredReporting();
            projectUpdate.PlannedDate = new DateTime(currentYear, 1, 1);
            projectUpdate.ExpirationDate = new DateTime(currentYear, 1, 1);
            projectUpdate.CompletionDate = new DateTime(currentYear,1,1);

            Assert.That(projectUpdateBatch.IsApproved, Is.False);
            Assert.That(projectUpdateBatch.IsSubmitted, Is.False);
            Assert.That(projectUpdateBatch.IsReadyToSubmit, Is.False);
            Assert.That(projectUpdateBatch.IsCreated, Is.True);
            Assert.That(projectUpdateBatch.InEditableState, Is.True);

            var preconditionException = Assert.Catch<PreconditionException>(() => projectUpdateBatch.SubmitToReviewer(person, DateTime.Now.AddDays(1)), "Should not be allowed to submit yet");
            Assert.That(preconditionException.Message, Is.StringContaining($"You cannot submit a {FieldDefinition.Project.GetFieldDefinitionLabel()} update that is not ready to be submitted"));
            TestFramework.TestPerformanceMeasureActualUpdate.Create(projectUpdateBatch, currentYear, 1000);
            var organization1 = TestFramework.TestOrganization.Create();
            var grantModification1 = TestFramework.TestGrantModification.Create("grant modification 1");
            var grantAllocation1 = TestFramework.TestGrantAllocation.Create(grantModification1, "Grant Allocation 1");

            TestFramework.TestProjectGrantAllocationExpenditureUpdate.Create(projectUpdateBatch, grantAllocation1, currentYear, 2000);
            projectUpdate.ProjectLocationSimpleTypeID = ProjectLocationSimpleType.None.ProjectLocationSimpleTypeID;
            projectUpdate.ProjectLocationNotes = "No location for now";

            projectUpdateBatch.SubmitToReviewer(person, DateTime.Now.AddDays(1));
            Assert.That(projectUpdateBatch.IsApproved, Is.False);
            Assert.That(projectUpdateBatch.IsReadyToSubmit, Is.False);
            Assert.That(projectUpdateBatch.IsSubmitted, Is.True);
            Assert.That(projectUpdateBatch.IsCreated, Is.False);
            Assert.That(projectUpdateBatch.InEditableState, Is.False);

            projectUpdateBatch.RejectSubmission(person, DateTime.Now.AddDays(2));
            Assert.That(projectUpdateBatch.IsApproved, Is.False);
            Assert.That(projectUpdateBatch.IsReadyToSubmit, Is.True);
            Assert.That(projectUpdateBatch.IsSubmitted, Is.False);
            Assert.That(projectUpdateBatch.IsCreated, Is.False);
            Assert.That(projectUpdateBatch.InEditableState, Is.True);

            preconditionException =
                Assert.Catch<PreconditionException>(
                    () =>
                        projectUpdateBatch.Approve(person,
                            DateTime.Now.AddDays(4),
                            new List<ProjectExemptReportingYear>(),
                            new List<ProjectGrantAllocationExpenditure>(),
                            new List<ProjectFundingSource>(), 
                            new List<PerformanceMeasureActual>(),
                            new List<PerformanceMeasureActualSubcategoryOption>(),
                            new List<ProjectExternalLink>(),
                            new List<ProjectNote>(),
                            new List<ProjectImage>(),
                            new List<ProjectLocation>(),
                            new List<ProjectPriorityLandscape>(),
                            new List<ProjectRegion>(),
                            new List<ProjectCounty>(),
                            new List<ProjectGrantAllocationRequest>(),
                            new List<ProjectOrganization>(),
                            new List<ProjectDocument>(),
                            new List<ProjectPerson>(),
                            new List<ProjectProgram>(),
                            new List<Treatment>()),
                    "Should not be allowed to approve yet");
            Assert.That(preconditionException.Message, Is.StringContaining($"You cannot approve a {FieldDefinition.Project.GetFieldDefinitionLabel()} update that has not been submitted"));

            // we have to re submit to get to approve
            projectUpdateBatch.SubmitToReviewer(person, DateTime.Now.AddDays(3));
            Assert.That(projectUpdateBatch.IsApproved, Is.False);
            Assert.That(projectUpdateBatch.IsReadyToSubmit, Is.False);
            Assert.That(projectUpdateBatch.IsSubmitted, Is.True);
            Assert.That(projectUpdateBatch.IsCreated, Is.False);
            Assert.That(projectUpdateBatch.InEditableState, Is.False);

            projectUpdateBatch.Approve(person,
                DateTime.Now.AddDays(4),
                new List<ProjectExemptReportingYear>(),
                new List<ProjectGrantAllocationExpenditure>(),
                new List<ProjectFundingSource>(), 
                new List<PerformanceMeasureActual>(),
                new List<PerformanceMeasureActualSubcategoryOption>(),
                new List<ProjectExternalLink>(),
                new List<ProjectNote>(),
                new List<ProjectImage>(),
                new List<ProjectLocation>(),
                new List<ProjectPriorityLandscape>(),
                new List<ProjectRegion>(),
                new List<ProjectCounty>(),
                new List<ProjectGrantAllocationRequest>(),
                new List<ProjectOrganization>(),
                new List<ProjectDocument>(),
                new List<ProjectPerson>(),
                new List<ProjectProgram>(),
                new List<Treatment>());
            Assert.That(projectUpdateBatch.IsApproved, Is.True);
            Assert.That(projectUpdateBatch.IsReadyToSubmit, Is.False);
            Assert.That(projectUpdateBatch.IsSubmitted, Is.False);
            Assert.That(projectUpdateBatch.IsCreated, Is.False);
            Assert.That(projectUpdateBatch.InEditableState, Is.False);
        }

        [Test]
        [Ignore]
        public void GetNewOrCurrentNotApprovedProjectUpdateBatchForProjectNoneExistingTest()
        {
            var person = TestFramework.TestPerson.Create();
            var project = TestFramework.TestProject.Create();
            var currentNotApprovedProjectUpdateBatchForProject = project.GetLatestNotApprovedUpdateBatch() ?? ProjectUpdateBatch.CreateNewProjectUpdateBatchForProject(project, person);
            Assert.That(currentNotApprovedProjectUpdateBatchForProject, Is.Not.Null, "Should have returned a new ProjectUpdateBatch");
            Assert.That(currentNotApprovedProjectUpdateBatchForProject.IsCreated, Is.True, "Should have returned a new ProjectUpdateBatch that is in draft");
            Assert.That(currentNotApprovedProjectUpdateBatchForProject.ProjectID, Is.EqualTo(project.ProjectID), "Should have returned a new ProjectUpdateBatch that is in draft for the given project");
        }

        [Test]
        public void GetNewOrCurrentNotApprovedProjectUpdateBatchForProjectExistingTest()
        {
            var person = TestFramework.TestPerson.Create();
            var project = TestFramework.TestProject.Create();
            var projectUpdateBatch = TestFramework.TestProjectUpdateBatch.Create(project);
            var currentNotApprovedProjectUpdateBatchForProject = project.GetLatestNotApprovedUpdateBatch() ?? ProjectUpdateBatch.CreateNewProjectUpdateBatchForProject(project, person);
            Assert.That(currentNotApprovedProjectUpdateBatchForProject, Is.Not.Null, "Should have returned the existing ProjectUpdateBatch");
            Assert.That(currentNotApprovedProjectUpdateBatchForProject.ProjectUpdateBatchID, Is.EqualTo(projectUpdateBatch.ProjectUpdateBatchID), "Should have returned the existing ProjectUpdateBatch");

            // flip it to submitted
            TestFramework.TestProjectUpdateHistory.Create(projectUpdateBatch, ProjectUpdateState.Submitted, person, DateTime.Now.AddDays(1));
            currentNotApprovedProjectUpdateBatchForProject = project.GetLatestNotApprovedUpdateBatch() ?? ProjectUpdateBatch.CreateNewProjectUpdateBatchForProject(project, person);
            Assert.That(currentNotApprovedProjectUpdateBatchForProject, Is.Not.Null, "Should have returned the existing ProjectUpdateBatch, even if it is submitted");
            Assert.That(currentNotApprovedProjectUpdateBatchForProject.ProjectUpdateBatchID,
                Is.EqualTo(projectUpdateBatch.ProjectUpdateBatchID),
                "Should have returned the existing ProjectUpdateBatch, even if it is submitted");

            // flip it to approved
            TestFramework.TestProjectUpdateHistory.Create(projectUpdateBatch, ProjectUpdateState.Approved, person, DateTime.Now.AddDays(2));
            currentNotApprovedProjectUpdateBatchForProject = project.GetLatestNotApprovedUpdateBatch() ?? ProjectUpdateBatch.CreateNewProjectUpdateBatchForProject(project, person);
            Assert.That(currentNotApprovedProjectUpdateBatchForProject, Is.Not.Null, "Should have returned a new ProjectUpdateBatch, since the existing one we had has now been Approved");
            Assert.That(currentNotApprovedProjectUpdateBatchForProject.IsCreated,
                Is.True,
                "Should have returned a new ProjectUpdateBatch that is in draft, since the existing one we had has now been Approved");
            Assert.That(currentNotApprovedProjectUpdateBatchForProject.ProjectID,
                Is.EqualTo(project.ProjectID),
                "Should have returned a new ProjectUpdateBatch that is in draft for the given project, since the existing one we had has now been Approved");
        }

        [Test]
        [Ignore]
        public void GetProjectUpdateStartToCompletionDateRangeTest()
        {
            var project = TestFramework.TestProject.Create();
            var projectUpdateBatch = TestFramework.TestProjectUpdateBatch.Create(project);

            Assert.That(projectUpdateBatch.ProjectUpdate, Is.Null, $"Precondition: no {FieldDefinition.Project.GetFieldDefinitionLabel()} update record yet");

            // Should just have one year, current year
            var currentYear = FirmaDateUtilities.CalculateCurrentYearToUseForRequiredReporting();
            AssertYearRangeForPerformanceMeasuresCorrect(projectUpdateBatch, currentYear, currentYear);

            // create a project update record
            var projectUpdate = TestFramework.TestProjectUpdate.Create(projectUpdateBatch);
            Assert.That(projectUpdateBatch.ProjectUpdate.GetImplementationStartYear().HasValue, Is.False, $"Precondition: {FieldDefinition.Project.GetFieldDefinitionLabel()} update record has no start year");
            Assert.That(projectUpdateBatch.ProjectUpdate.GetCompletionYear().HasValue, Is.False, $"Precondition: {FieldDefinition.Project.GetFieldDefinitionLabel()} update record has no completion year");
            AssertYearRangeForPerformanceMeasuresCorrect(projectUpdateBatch, currentYear, currentYear);

            // now set a start year
            // start year before minimum year for reporting (2007), no completion year
            projectUpdate.PlannedDate = new DateTime(2004,1,1);
            AssertYearRangeForPerformanceMeasuresCorrect(projectUpdateBatch, MultiTenantHelpers.GetMinimumYear(), currentYear);

            // start year in the past but greater than minimum year for reporting (2007), no completion year
            projectUpdate.PlannedDate = new DateTime(currentYear - 1, 1, 1);
            AssertYearRangeForPerformanceMeasuresCorrect(projectUpdateBatch, projectUpdate.GetImplementationStartYear().Value, currentYear);

            // start year in the future, no completion year
            projectUpdate.PlannedDate = new DateTime(currentYear + 1, 1, 1);
            AssertYearRangeForPerformanceMeasuresCorrect(projectUpdateBatch, currentYear, currentYear);

            // now set a completion year that is less than current year; expect the range to be start year to completion year
            projectUpdate.PlannedDate = new DateTime(currentYear - 1, 1, 1);
            projectUpdate.CompletionDate = new DateTime(currentYear - 1,1,1);
            AssertYearRangeForPerformanceMeasuresCorrect(projectUpdateBatch, projectUpdate.GetImplementationStartYear().Value, projectUpdate.GetCompletionYear().Value);

            // now set a completion year that is greater than current year; expect the range to be start year to current year
            projectUpdate.CompletionDate = new DateTime(currentYear + 1,1,1);
            AssertYearRangeForPerformanceMeasuresCorrect(projectUpdateBatch, projectUpdate.GetImplementationStartYear().Value, currentYear);

            // No Start Year
            // 10/30/15 RL:  Rules have changed so that you should never not have a ImplementationStartYear when you get to the Performance Measures area; this is our best guess on what should happen if this anomaly happens
            // now set a completion year before the minimum year for reporting (2007); expect it to be minimum year for reporting (2007) to minimum year for reporting (2007)
            projectUpdate.PlannedDate = null;

            projectUpdate.CompletionDate = new DateTime(2006, 1, 1);
            AssertYearRangeForPerformanceMeasuresCorrect(projectUpdateBatch, MultiTenantHelpers.GetMinimumYear(), MultiTenantHelpers.GetMinimumYear());

            // now set a completion year to be <= curent year but greater than minimum year for reporting (2007); expect it to be completion year to completion year
            projectUpdate.CompletionDate = new DateTime(currentYear,1,1);
            AssertYearRangeForPerformanceMeasuresCorrect(projectUpdateBatch, projectUpdate.GetCompletionYear().Value, projectUpdate.GetCompletionYear().Value);

            projectUpdate.CompletionDate = new DateTime(currentYear - 1,1,1);
            AssertYearRangeForPerformanceMeasuresCorrect(projectUpdateBatch, projectUpdate.GetCompletionYear().Value, projectUpdate.GetCompletionYear().Value);

            // now set a completion year to be > curent year; expect it to be current year to current year
            projectUpdate.CompletionDate = new DateTime(currentYear + 1,1,1);
            AssertYearRangeForPerformanceMeasuresCorrect(projectUpdateBatch, currentYear, currentYear);

            // invalid year combo; should default to just using the start year
            projectUpdate.PlannedDate = new DateTime(2012, 1, 1);
            projectUpdate.CompletionDate = new DateTime(2011, 1, 1);

            var result = projectUpdateBatch.ProjectUpdate.GetProjectUpdateImplementationStartToCompletionDateRange();
            Assert.That(result, Is.Empty, "Both start and completion years before the minimum year; expect it to return an empty range");

            // both start and completion years before the minimum year; expect it to return an empty range
            projectUpdate.PlannedDate = new DateTime(2003, 1, 1);
            projectUpdate.CompletionDate = new DateTime(2005, 1, 1);
            result = projectUpdateBatch.ProjectUpdate.GetProjectUpdateImplementationStartToCompletionDateRange();
            Assert.That(result, Is.Empty, "Both start and completion years before the minimum year; expect it to return an empty range");

            // both start and completion years after the current year; expect it to return an empty range
            projectUpdate.PlannedDate = new DateTime(currentYear + 2, 1, 1);
            projectUpdate.CompletionDate = new DateTime(currentYear + 4,1,1);
            result = projectUpdateBatch.ProjectUpdate.GetProjectUpdateImplementationStartToCompletionDateRange();
            Assert.That(result, Is.Empty, "Both start and completion years after the current year; expect it to return an empty range");
        }

        [Test]
        [Ignore]
        public void GetProjectUpdatePlanningDesignStartToCompletionDateRangeTest()
        {
            var project = TestFramework.TestProject.Create();
            var projectUpdateBatch = TestFramework.TestProjectUpdateBatch.Create(project);

            Assert.That(projectUpdateBatch.ProjectUpdate, Is.Null, $"Precondition: no {FieldDefinition.Project.GetFieldDefinitionLabel()} update record yet");

            // Should just have one year, current year
            var currentYear = FirmaDateUtilities.CalculateCurrentYearToUseForRequiredReporting();
            AssertYearRangeForExpendituresCorrect(projectUpdateBatch, currentYear, currentYear);

            // create a project update record
            var projectUpdate = TestFramework.TestProjectUpdate.Create(projectUpdateBatch);
            Assert.That(projectUpdateBatch.ProjectUpdate.PlannedDate.HasValue, Is.False, $"Precondition: {FieldDefinition.Project.GetFieldDefinitionLabel()} update record has no start year");
            Assert.That(projectUpdateBatch.ProjectUpdate.GetCompletionYear().HasValue, Is.False, $"Precondition: {FieldDefinition.Project.GetFieldDefinitionLabel()} update record has no completion year");
            AssertYearRangeForExpendituresCorrect(projectUpdateBatch, currentYear, currentYear);

            // now set a start year
            // start year before minimum year for reporting (2007), no completion year
            projectUpdate.PlannedDate = new DateTime(2004, 1, 1);
            AssertYearRangeForExpendituresCorrect(projectUpdateBatch, MultiTenantHelpers.GetMinimumYear(), currentYear);

            // start year in the past but greater than minimum year for reporting (2007), no completion year
            projectUpdate.PlannedDate = new DateTime(currentYear - 1, 1, 1);
            AssertYearRangeForExpendituresCorrect(projectUpdateBatch, projectUpdate.PlannedDate.GetValueOrDefault().Year, currentYear);

            // start year in the future, no completion year
            projectUpdate.PlannedDate = new DateTime(currentYear + 1, 1, 1);
            AssertYearRangeForExpendituresCorrect(projectUpdateBatch, currentYear, currentYear);

            // now set a completion year that is less than current year; expect the range to be start year to completion year
            projectUpdate.PlannedDate = new DateTime(currentYear - 1, 1, 1);
            projectUpdate.CompletionDate = new DateTime(currentYear - 1,1,1);
            AssertYearRangeForExpendituresCorrect(projectUpdateBatch, projectUpdate.PlannedDate.GetValueOrDefault().Year, projectUpdate.GetCompletionYear().Value);

            // now set a completion year that is greater than current year; expect the range to be start year to current year
            projectUpdate.CompletionDate = new DateTime(currentYear + 1,1,1);
            AssertYearRangeForExpendituresCorrect(projectUpdateBatch, projectUpdate.PlannedDate.GetValueOrDefault().Year, currentYear);

            // No Start Year
            // 10/30/15 RL:  Rules have changed so that you should never not have a PlannedDate when you get to the Expenditures area; this is our best guess on what should happen if this anomaly happens
            // now set a completion year before the minimum year for reporting (2007); expect it to be minimum year for reporting (2007) to minimum year for reporting (2007)
            projectUpdate.PlannedDate = null;

            projectUpdate.CompletionDate = new DateTime(2006, 1, 1);
            AssertYearRangeForExpendituresCorrect(projectUpdateBatch, MultiTenantHelpers.GetMinimumYear(), MultiTenantHelpers.GetMinimumYear());

            // now set a completion year to be <= curent year but greater than minimum year for reporting (2007); expect it to be completion year to completion year
            projectUpdate.CompletionDate = new DateTime(currentYear,1,1);
            AssertYearRangeForExpendituresCorrect(projectUpdateBatch, projectUpdate.GetCompletionYear().Value, projectUpdate.GetCompletionYear().Value);

            projectUpdate.CompletionDate = new DateTime(currentYear - 1,1,1);
            AssertYearRangeForExpendituresCorrect(projectUpdateBatch, projectUpdate.GetCompletionYear().Value, projectUpdate.GetCompletionYear().Value);

            // now set a completion year to be > curent year; expect it to be current year to current year
            projectUpdate.CompletionDate = new DateTime(currentYear + 1,1,1);
            AssertYearRangeForExpendituresCorrect(projectUpdateBatch, currentYear, currentYear);

            // invalid year combo; should default to just using the start year
            projectUpdate.PlannedDate = new DateTime(2012, 1, 1);

            projectUpdate.CompletionDate = new DateTime(2011, 1, 1);

            var result = projectUpdateBatch.ProjectUpdate.GetProjectUpdatePlanningDesignStartToCompletionDateRange();
            Assert.That(result, Is.Empty, "Completion year is before start year; expect it to return an empty range");

            // both start and completion years before the minimum year; expect it to return an empty range
            projectUpdate.PlannedDate = new DateTime(2003, 1, 1);
            projectUpdate.CompletionDate = new DateTime(2005, 1, 1);
            result = projectUpdateBatch.ProjectUpdate.GetProjectUpdatePlanningDesignStartToCompletionDateRange();
            Assert.That(result, Is.Empty, "Both start and completion years before the minimum year; expect it to return an empty range");

            // both start and completion years after the current year; expect it to return an empty range
            projectUpdate.PlannedDate = new DateTime(currentYear + 2, 1, 1);
            projectUpdate.CompletionDate = new DateTime(currentYear + 4, 1, 1);
            result = projectUpdateBatch.ProjectUpdate.GetProjectUpdatePlanningDesignStartToCompletionDateRange();
            Assert.That(result, Is.Empty, "Both start and completion years after the current year; expect it to return an empty range");
        }

        [Test]
        [Ignore]
        public void GetProjectUpdatePlanningDesignStartToCompletionDateRangeForProjectBudgetsTest()
        {
            var project = TestFramework.TestProject.Create();
            var projectUpdateBatch = TestFramework.TestProjectUpdateBatch.Create(project);

            Assert.That(projectUpdateBatch.ProjectUpdate, Is.Null, $"Precondition: no {FieldDefinition.Project.GetFieldDefinitionLabel()} update record yet");

            // Should just have one year, current year
            var currentYear = FirmaDateUtilities.CalculateCurrentYearToUseForRequiredReporting();
            AssertYearRangeForBudgetsCorrect(projectUpdateBatch, currentYear, currentYear);

            // create a project update record
            var projectUpdate = TestFramework.TestProjectUpdate.Create(projectUpdateBatch);
            Assert.That(projectUpdateBatch.ProjectUpdate.PlannedDate.HasValue, Is.False, $"Precondition: {FieldDefinition.Project.GetFieldDefinitionLabel()} update record has no start year");
            Assert.That(projectUpdateBatch.ProjectUpdate.GetCompletionYear().HasValue, Is.False, $"Precondition: {FieldDefinition.Project.GetFieldDefinitionLabel()} update record has no completion year");
            AssertYearRangeForBudgetsCorrect(projectUpdateBatch, currentYear, currentYear);

            // now set a start year
            // start year before minimum year for reporting (2007), no completion year, expect the range to be start year to current year
            projectUpdate.PlannedDate = new DateTime(2004, 1, 1);
            AssertYearRangeForBudgetsCorrect(projectUpdateBatch, projectUpdate.PlannedDate.GetValueOrDefault().Year, currentYear);

            // start year in the past but greater than minimum year for reporting (2007), expect the range to be start year to current year
            projectUpdate.PlannedDate = new DateTime(currentYear - 3, 1, 1);
            AssertYearRangeForBudgetsCorrect(projectUpdateBatch, projectUpdate.PlannedDate.GetValueOrDefault().Year, currentYear);

            // start year in the future, no completion year, expect the range to be start year to start year
            projectUpdate.PlannedDate = new DateTime(currentYear + 1, 1, 1);
            AssertYearRangeForBudgetsCorrect(projectUpdateBatch, projectUpdate.PlannedDate.GetValueOrDefault().Year, projectUpdate.PlannedDate.GetValueOrDefault().Year);

            // now set a completion year that is less than current year; expect the range to be start year to completion year
            projectUpdate.PlannedDate = new DateTime(currentYear - 2, 1, 1);
            projectUpdate.CompletionDate = new DateTime(currentYear - 1,1,1);
            AssertYearRangeForBudgetsCorrect(projectUpdateBatch, projectUpdate.PlannedDate.GetValueOrDefault().Year, projectUpdate.GetCompletionYear().Value);

            // now set a completion year that is greater than current year; expect the range to be start year to completion year
            projectUpdate.CompletionDate = new DateTime(currentYear + 3, 1, 1);
            AssertYearRangeForBudgetsCorrect(projectUpdateBatch, projectUpdate.PlannedDate.GetValueOrDefault().Year, projectUpdate.GetCompletionYear().Value);

            // No Start Year
            // 10/30/15 RL:  Rules have changed so that you should never not have a PlannedDate when you get to the Budgets area; this is our best guess on what should happen if this anomaly happens
            // now set a completion year before the minimum year for reporting (2007); expect it to be completion year to completion year
            projectUpdate.PlannedDate = null;

            projectUpdate.CompletionDate = new DateTime(2006, 1, 1);
            AssertYearRangeForBudgetsCorrect(projectUpdateBatch, projectUpdate.GetCompletionYear().Value, projectUpdate.GetCompletionYear().Value);

            // now set a completion year to be <= curent year but greater than minimum year for reporting (2007); expect it to be completion year to completion year
            projectUpdate.CompletionDate = new DateTime(currentYear,1,1);
            AssertYearRangeForBudgetsCorrect(projectUpdateBatch, projectUpdate.GetCompletionYear().Value, projectUpdate.GetCompletionYear().Value);

            projectUpdate.CompletionDate = new DateTime(currentYear - 1,1,1);
            AssertYearRangeForBudgetsCorrect(projectUpdateBatch, projectUpdate.GetCompletionYear().Value, projectUpdate.GetCompletionYear().Value);

            // now set a completion year to be > curent year; expect it to be current year to completion year
            projectUpdate.CompletionDate = new DateTime(currentYear + 1,1,1);
            AssertYearRangeForBudgetsCorrect(projectUpdateBatch, currentYear, projectUpdate.GetCompletionYear().Value);

            // invalid year combo; should throw an exception
            projectUpdate.PlannedDate = new DateTime(2012, 1, 1);
            projectUpdate.CompletionDate = new DateTime(2011, 1, 1);
            Assert.Throws<PreconditionException>(() => FirmaDateUtilities.CalculateCalendarYearRangeForBudgetsAccountingForExistingYears(new List<int>(), projectUpdateBatch.ProjectUpdate, FirmaDateUtilities.CalculateCurrentYearToUseForRequiredReporting()));

            // both start and completion years before the minimum year; expect it to return start to completion year
            projectUpdate.PlannedDate = new DateTime(2003, 1, 1);
            projectUpdate.CompletionDate = new DateTime(2005, 1, 1);
            AssertYearRangeForBudgetsCorrect(projectUpdateBatch, projectUpdate.PlannedDate.GetValueOrDefault().Year, projectUpdate.GetCompletionYear().Value);

            // both start and completion years after the current year; expect it to return start to completion year
            projectUpdate.PlannedDate = new DateTime(currentYear + 2, 1, 1);
            projectUpdate.CompletionDate = new DateTime(currentYear + 4, 1, 1);
            AssertYearRangeForBudgetsCorrect(projectUpdateBatch, projectUpdate.PlannedDate.GetValueOrDefault().Year, projectUpdate.GetCompletionYear().Value);
        }

        [Test]
        [Ignore]
        public void ValidateExpendituresAndForceValidationTest()
        {
            var projectUpdate = TestFramework.TestProjectUpdate.Create();
            var projectUpdateBatch = projectUpdate.ProjectUpdateBatch;
            Assert.That(projectUpdate.PlannedDate, Is.Null, "Should not have a Planning/Design Start Year set");

            var result = projectUpdateBatch.ValidateExpendituresAndForceValidation();
            Assert.That(result, Is.Not.Empty, "Should not be valid since we do not have a Planning/Design Start Year set");
            Assert.That(result, Is.EquivalentTo(new List<string> { FirmaValidationMessages.UpdateSectionIsDependentUponBasicsSection }));

            var currentYear = FirmaDateUtilities.CalculateCurrentYearToUseForRequiredReporting();
            projectUpdate.PlannedDate = new DateTime(2005, 1 ,1);
            AssertExpenditureYears(projectUpdateBatch.ProjectGrantAllocationExpenditureUpdates.ToList(),
                MultiTenantHelpers.GetMinimumYear(),
                currentYear,
                projectUpdateBatch,
                false,
                "Has start year before 2007 but no completion year, expect range of 2007 to be at least current year to be missing");

            projectUpdate.PlannedDate = new DateTime(currentYear - 1,1,1);
            AssertExpenditureYears(projectUpdateBatch.ProjectGrantAllocationExpenditureUpdates.ToList(),
                projectUpdate.PlannedDate.GetValueOrDefault().Year,
                currentYear,
                projectUpdateBatch,
                false,
                "Has start year but no completion year, expect range of start year to be at least current year to be missing");

            projectUpdate.CompletionDate = new DateTime(currentYear - 1,1,1);
            AssertExpenditureYears(projectUpdateBatch.ProjectGrantAllocationExpenditureUpdates.ToList(),
                projectUpdate.PlannedDate.GetValueOrDefault().Year,
                projectUpdate.GetCompletionYear().Value,
                projectUpdateBatch,
                false,
                "Has start year and completion year before current year, expect range of start year to completion year to be missing");

            projectUpdate.CompletionDate = new DateTime(currentYear + 1,1,1);
            AssertExpenditureYears(projectUpdateBatch.ProjectGrantAllocationExpenditureUpdates.ToList(),
                projectUpdate.PlannedDate.GetValueOrDefault().Year,
                currentYear,
                projectUpdateBatch,
                false,
                "Has start year and completion year after current year, expect range of start year to current year to be missing");

            projectUpdate.PlannedDate = new DateTime(2002,1,1);
            projectUpdate.CompletionDate = new DateTime(2006, 1, 1);
            result = projectUpdateBatch.ValidateExpendituresAndForceValidation();
            Assert.That(result, Is.Empty, $"Should be valid since the {FieldDefinition.Project.GetFieldDefinitionLabel()} start and completion year is before 2007");
            Assert.That(result, Is.Empty, "Should not have any validation warnings");

            // now add some expenditure update records
            projectUpdate.PlannedDate = new DateTime(currentYear - 1,1,1);
            projectUpdate.CompletionDate = new DateTime(currentYear + 2,1,1);
            var organization1 = TestFramework.TestOrganization.Create();
            var grantModification1 = TestFramework.TestGrantModification.Create("Grant Modification 1");
            var grantAllocation1 = TestFramework.TestGrantAllocation.Create(grantModification1, "Grant Allocation 1");

            TestFramework.TestProjectGrantAllocationExpenditureUpdate.Create(projectUpdateBatch, grantAllocation1, currentYear + 2, 1000); // record after current year
            TestFramework.TestProjectGrantAllocationExpenditureUpdate.Create(projectUpdateBatch, grantAllocation1, projectUpdate.PlannedDate.GetValueOrDefault().Year - 2, 2000); // record before start year
            AssertExpenditureYears(projectUpdateBatch.ProjectGrantAllocationExpenditureUpdates.ToList(),
                projectUpdate.PlannedDate.GetValueOrDefault().Year,
                currentYear,
                projectUpdateBatch,
                false,
                "Has start year and completion year after current year, expenditure record outside of validatable range, expect range of start year to current year to be missing");

            TestFramework.TestProjectGrantAllocationExpenditureUpdate.Create(projectUpdateBatch, grantAllocation1, projectUpdate.PlannedDate.GetValueOrDefault().Year, 3000); // record at start year
            TestFramework.TestProjectGrantAllocationExpenditureUpdate.Create(projectUpdateBatch, grantAllocation1, projectUpdate.GetCompletionYear().Value, 4000); // record at completion year
            AssertExpenditureYears(projectUpdateBatch.ProjectGrantAllocationExpenditureUpdates.ToList(),
                projectUpdate.PlannedDate.GetValueOrDefault().Year,
                currentYear,
                projectUpdateBatch,
                false,
                "Has start year and completion year after current year, expenditure records inside validatable range, expect range of start year to current year to be missing except for the start year and completion year");

            // fill in the other years missing
            FirmaDateUtilities.GetRangeOfYears(projectUpdate.PlannedDate.GetValueOrDefault().Year, projectUpdate.GetCompletionYear().Value)
                .GetMissingYears(projectUpdateBatch.ProjectGrantAllocationExpenditureUpdates.ToList().Select(x => x.CalendarYear)).ToList()
                .ForEach(x => TestFramework.TestProjectGrantAllocationExpenditureUpdate.Create(projectUpdateBatch, grantAllocation1, x, 5000));
            AssertExpenditureYears(projectUpdateBatch.ProjectGrantAllocationExpenditureUpdates.ToList(),
                projectUpdate.PlannedDate.GetValueOrDefault().Year,
                currentYear,
                projectUpdateBatch,
                true,
                "Has start year and completion year after current year, all years filled, should be valid");
        }

        [Test]
        [Ignore]
        public void ValidatePerformanceMeasuresAndForceValidationTest()
        {
            var projectUpdate = TestFramework.TestProjectUpdate.Create();
            projectUpdate.ProjectStageID = ProjectStage.Implementation.ProjectStageID;
            var projectUpdateBatch = projectUpdate.ProjectUpdateBatch;

            Assert.That(projectUpdate.ProjectStage.RequiresPerformanceMeasureActuals(), Is.True, "Should be in stage that requires performance measure actual values");
            Assert.That(projectUpdate.ProjectStage, Is.Not.EqualTo(ProjectStage.Planned), "Should not be in Planning/Design");
            Assert.That(projectUpdate.GetImplementationStartYear(), Is.Null, "Should not have an Implementation Start Year set");

            var result = projectUpdateBatch.ValidatePerformanceMeasures();
            Assert.That(result.IsValid, Is.False, "Should not be valid since we do not have an Implementation Start Year set");
            Assert.That(result.GetWarningMessages(), Is.EquivalentTo(new List<string> { FirmaValidationMessages.UpdateSectionIsDependentUponBasicsSection }));

            var currentYear = FirmaDateUtilities.CalculateCurrentYearToUseForRequiredReporting();
            projectUpdate.PlannedDate = new DateTime(2004,1,1);
            AssertPerformanceMeasures(projectUpdateBatch.PerformanceMeasureActualUpdates.ToList(),
                MultiTenantHelpers.GetMinimumYear(),
                currentYear,
                projectUpdateBatch,
                false,
                "Has start year before 2007 but no completion year, expect range of 2007 to at least current year to be missing");
            
            AssertPerformanceMeasures(projectUpdateBatch.PerformanceMeasureActualUpdates.ToList(),
                projectUpdate.GetImplementationStartYear().Value,
                currentYear,
                projectUpdateBatch,
                false,
                "Has start year but no completion year, expect range of start year to at least current year to be missing");

            projectUpdate.CompletionDate = new DateTime(currentYear - 1,1,1);
            AssertPerformanceMeasures(projectUpdateBatch.PerformanceMeasureActualUpdates.ToList(),
                projectUpdate.GetImplementationStartYear().Value,
                projectUpdate.GetCompletionYear().Value,
                projectUpdateBatch,
                false,
                "Has start year and completion year before current year, expect range of start year to completion year to be missing");

            projectUpdate.CompletionDate = new DateTime(currentYear + 1,1,1);
            AssertPerformanceMeasures(projectUpdateBatch.PerformanceMeasureActualUpdates.ToList(),
                projectUpdate.GetImplementationStartYear().Value,
                currentYear,
                projectUpdateBatch,
                false,
                "Has start year and completion year after current year, expect range of start year to current year to be missing");

            projectUpdate.PlannedDate = new DateTime(2001,1,1);
            projectUpdate.CompletionDate = new DateTime(2006, 1, 1);
            result = projectUpdateBatch.ValidatePerformanceMeasures();
            Assert.That(result.IsValid, Is.EqualTo(true), $"Should be valid since the {FieldDefinition.Project.GetFieldDefinitionLabel()} start and completion year is before 2007");
            Assert.That(result.GetWarningMessages(), Is.Empty, "Should not have any validation warnings");
            Assert.That(result.PerformanceMeasureActualUpdatesWithWarnings, Is.Empty, "Should have no warnings");

            // now add some performance measure reported value records
            projectUpdate.CompletionDate = new DateTime(currentYear + 2, 1, 1);
            TestFramework.TestPerformanceMeasureActualUpdate.Create(projectUpdateBatch, currentYear + 2); // record after current year
            TestFramework.TestPerformanceMeasureActualUpdate.Create(projectUpdateBatch, projectUpdate.GetImplementationStartYear().Value - 2); // record before start year
            AssertPerformanceMeasures(projectUpdateBatch.PerformanceMeasureActualUpdates.ToList(),
                projectUpdate.GetImplementationStartYear().Value,
                currentYear,
                projectUpdateBatch,
                false,
                "Has start year and completion year after current year, expenditure record outside of validatable range, expect range of start year to current year to be missing");

            TestFramework.TestPerformanceMeasureActualUpdate.Create(projectUpdateBatch, projectUpdate.GetImplementationStartYear().Value); // record at start year
            TestFramework.TestPerformanceMeasureActualUpdate.Create(projectUpdateBatch, projectUpdate.GetCompletionYear().Value); // record at completion year
            AssertPerformanceMeasures(projectUpdateBatch.PerformanceMeasureActualUpdates.ToList(),
                projectUpdate.GetImplementationStartYear().Value,
                currentYear,
                projectUpdateBatch,
                false,
                "Has start year and completion year after current year, expenditure records inside validatable range, expect range of start year to current year to be missing except for the start year and completion year");

            // fill in the other years missing
            FirmaDateUtilities.GetRangeOfYears(projectUpdate.GetImplementationStartYear().Value, projectUpdate.GetCompletionYear().Value)
                .GetMissingYears(projectUpdateBatch.PerformanceMeasureActualUpdates.ToList().Select(x => x.CalendarYear)).ToList()
                .ForEach(x => TestFramework.TestPerformanceMeasureActualUpdate.Create(projectUpdateBatch, x));
            AssertPerformanceMeasures(projectUpdateBatch.PerformanceMeasureActualUpdates.ToList(),
                projectUpdate.GetImplementationStartYear().Value,
                currentYear,
                projectUpdateBatch,
                false,
                "Has start year and completion year after current year, all years filled, just incomplete rows");

            var index = 0;
            foreach (var performanceMeasureActualUpdate in projectUpdateBatch.PerformanceMeasureActualUpdates)
            {
                performanceMeasureActualUpdate.ActualValue = index * 10;
                index++;
            }
            result = projectUpdateBatch.ValidatePerformanceMeasures();
            Assert.That(result.IsValid, Is.True, "Should have no warnings");
            Assert.That(result.GetWarningMessages(), Is.Empty, "Should have no warnings");
            Assert.That(result.PerformanceMeasureActualUpdatesWithWarnings, Is.Empty, "Should have no warnings");
        }

        [Test]
        [Ignore]
        public void ValidatePerformanceMeasuresAndForceValidationProjectUpdateInPlanningDesignTest()
        {
            var projectUpdate = TestFramework.TestProjectUpdate.Create();
            projectUpdate.ProjectStageID = ProjectStage.Planned.ProjectStageID;
            var projectUpdateBatch = projectUpdate.ProjectUpdateBatch;

            Assert.That(projectUpdate.ProjectStage.RequiresPerformanceMeasureActuals(), Is.False, "Should be in stage that requires performance measure actual values");
            Assert.That(projectUpdate.ProjectStage, Is.EqualTo(ProjectStage.Planned), "Should not be in Planning/Design");

            Assert.That(projectUpdate.GetImplementationStartYear(), Is.Null, "Should not have an Implementation Start Year set");
            var result = projectUpdateBatch.ValidatePerformanceMeasures();
            Assert.That(result.IsValid, Is.False, "Should not be valid since we do not have a Implementation Start Year set");
            Assert.That(result.GetWarningMessages(), Is.EquivalentTo(new List<string> { FirmaValidationMessages.UpdateSectionIsDependentUponBasicsSection }));

            var currentYear = DateTime.Today.Year;
            projectUpdate.PlannedDate = new DateTime(currentYear - 1,1,1);
            result = projectUpdateBatch.ValidatePerformanceMeasures();
            Assert.That(result.IsValid, Is.True, "ProjectUpdate in Planning/Design stage, ignore the missing years validation");
            Assert.That(result.GetWarningMessages(), Is.Empty, "ProjectUpdate in Planning/Design stage, ignore the missing years validation");

            // now add some performance measure reported value records
            var performanceMeasureActualUpdate1 = TestFramework.TestPerformanceMeasureActualUpdate.Create(projectUpdateBatch, currentYear); // record after current year
            var performanceMeasureActualUpdate2 = TestFramework.TestPerformanceMeasureActualUpdate.Create(projectUpdateBatch, currentYear - 1); // record before start year
            result = projectUpdateBatch.ValidatePerformanceMeasures();
            Assert.That(result.IsValid, Is.False, "Should have warning about incomplete rows");
            Assert.That(result.GetWarningMessages(),
                Is.EquivalentTo(new List<string> { PerformanceMeasuresValidationResult.FoundIncompletePerformanceMeasureRowsMessage }),
                "Should have warning about incomplete rows");
            Assert.That(result.PerformanceMeasureActualUpdatesWithWarnings,
                Is.EquivalentTo(new HashSet<int>
                {
                    performanceMeasureActualUpdate1.PerformanceMeasureActualUpdateID,
                    performanceMeasureActualUpdate2.PerformanceMeasureActualUpdateID
                }),
                "Should have warning about incomplete rows");

            performanceMeasureActualUpdate1.ActualValue = 10;
            result = projectUpdateBatch.ValidatePerformanceMeasures();
            Assert.That(result.IsValid, Is.False, "Should have warning about incomplete rows");
            Assert.That(result.GetWarningMessages(),
                Is.EquivalentTo(new List<string> { PerformanceMeasuresValidationResult.FoundIncompletePerformanceMeasureRowsMessage }),
                "Should have warning about incomplete rows");
            Assert.That(result.PerformanceMeasureActualUpdatesWithWarnings,
                Is.EquivalentTo(new HashSet<int> { performanceMeasureActualUpdate2.PerformanceMeasureActualUpdateID }),
                "Should have warning about incomplete rows");

            performanceMeasureActualUpdate2.ActualValue = 20;
            result = projectUpdateBatch.ValidatePerformanceMeasures();
            Assert.That(result.IsValid, Is.True, "Should have no warnings");
            Assert.That(result.GetWarningMessages(), Is.Empty, "Should have no warnings");
            Assert.That(result.PerformanceMeasureActualUpdatesWithWarnings, Is.Empty, "Should have no warnings");
        }

        private static void AssertExpenditureYears(List<ProjectGrantAllocationExpenditureUpdate> projectGrantAllocationExpenditureUpdates,
                                                   int startYear,
                                                   int currentYear,
                                                   ProjectUpdateBatch projectUpdateBatch,
                                                   bool isValid,
                                                   string assertionMessage)
        {
            var result = projectUpdateBatch.ValidateExpendituresAndForceValidation();
            if (isValid)
            {
                Assert.That(result, Is.Empty, "Should be valid");
            }
            else
            {
                Assert.That(result, Is.Not.Empty, "Should be not valid");
            }

            var currentYearsEntered = projectGrantAllocationExpenditureUpdates.Select(y => y.CalendarYear).Distinct().ToList();
            var expectedMissingYears = FirmaDateUtilities.GetRangeOfYears(startYear, currentYear).Where(x => !currentYearsEntered.Contains(x)).ToList();
            var grantAllocations = projectGrantAllocationExpenditureUpdates.Select(x => x.GrantAllocation).Distinct().ToList();
            if (!grantAllocations.Any())
            {
                if (expectedMissingYears.Any())
                {
                    Assert.That(result,
                        Is.EquivalentTo(new List<string> { $"Missing Expenditures for {string.Join(", ", expectedMissingYears)}"
                        }),
                        assertionMessage);
                }
                else
                {
                    Assert.That(result, Is.Empty, assertionMessage);
                }
            }
            else
            {
                // right now the test is constrained to just one grant allocation
                if (expectedMissingYears.Any())
                {
                    Assert.That(result,
                        Is.EquivalentTo(new List<string>
                        {
                            $"Missing Expenditures for Grant Allocation '{grantAllocations.First().DisplayName}' for the following years: {string.Join(", ", expectedMissingYears)}"
                        }),
                        assertionMessage);
                }
                else
                {
                    Assert.That(result, Is.Empty, assertionMessage);
                }
            }
        }

        private static void AssertPerformanceMeasures(List<PerformanceMeasureActualUpdate> performanceMeasureActualUpdates,
                                                      int startYear,
                                                      int currentYear,
                                                      ProjectUpdateBatch projectUpdateBatch,
                                                      bool isValid,
                                                      string assertionMessage)
        {
            var result = projectUpdateBatch.ValidatePerformanceMeasures();
            Assert.That(result.IsValid, Is.EqualTo(isValid), $"Should be {(isValid ? " valid" : "not valid")}");

            var currentYearsEntered = performanceMeasureActualUpdates.Select(y => y.CalendarYear).Distinct().ToList();
            var missingReportedValues = performanceMeasureActualUpdates.Where(x => !x.ActualValue.HasValue).ToList();
            var expectedMissingYears = FirmaDateUtilities.GetRangeOfYears(startYear, currentYear).Where(x => !currentYearsEntered.Contains(x)).ToList();
            var missingYearsMessage = $"for {string.Join(", ", expectedMissingYears)}";
            if (expectedMissingYears.Any() && missingReportedValues.Any())
            {
                Assert.That(result.GetWarningMessages(),
                    Has.Count.EqualTo(2));

                Assert.That(result.GetWarningMessages()[0], Is.StringEnding(missingYearsMessage));

                Assert.That(result.GetWarningMessages()[1], Is.StringEnding("You must either delete irrelevant rows, or provide complete information for each row."));



            }
            else if (expectedMissingYears.Any())
            {
                Assert.That(result.GetWarningMessages(), Has.Count.EqualTo(1));
                Assert.That(result.GetWarningMessages()[0], Is.StringEnding(missingYearsMessage));
            }
            else if (missingReportedValues.Any())
            {
                Assert.That(result.GetWarningMessages(), Is.EquivalentTo(new List<string> { PerformanceMeasuresValidationResult.FoundIncompletePerformanceMeasureRowsMessage }), assertionMessage);
            }
            else
            {
                Assert.That(result.GetWarningMessages(), Is.Empty, assertionMessage);
            }
        }

        private static void AssertYearRangeForPerformanceMeasuresCorrect(ProjectUpdateBatch projectUpdateBatch, int startYear, int currentYear)
        {
            var result = projectUpdateBatch.ProjectUpdate.GetProjectUpdateImplementationStartToCompletionDateRange();
            var expectedRange = FirmaDateUtilities.GetRangeOfYears(startYear, currentYear);
            Assert.That(result, Is.EquivalentTo(expectedRange));
        }

        private static void AssertYearRangeForExpendituresCorrect(ProjectUpdateBatch projectUpdateBatch, int startYear, int currentYear)
        {
            var result = projectUpdateBatch.ProjectUpdate.GetProjectUpdatePlanningDesignStartToCompletionDateRange();
            var expectedRange = FirmaDateUtilities.GetRangeOfYears(startYear, currentYear);
            Assert.That(result, Is.EquivalentTo(expectedRange));
        }

        private static void AssertYearRangeForBudgetsCorrect(ProjectUpdateBatch projectUpdateBatch, int startYear, int currentYear)
        {
            var result = FirmaDateUtilities.CalculateCalendarYearRangeForBudgetsAccountingForExistingYears(new List<int>(), projectUpdateBatch.ProjectUpdate, FirmaDateUtilities.CalculateCurrentYearToUseForRequiredReporting());
            var expectedRange = FirmaDateUtilities.GetRangeOfYears(startYear, currentYear);
            Assert.That(result, Is.EquivalentTo(expectedRange));
        }
    }
}
