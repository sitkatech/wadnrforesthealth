/*-----------------------------------------------------------------------
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
                            new List<ProjectFundingSource>(), 
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
                new List<ProjectFundingSource>(), 
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

            // create a project update record
            var projectUpdate = TestFramework.TestProjectUpdate.Create(projectUpdateBatch);
            Assert.That(projectUpdateBatch.ProjectUpdate.GetImplementationStartYear().HasValue, Is.False, $"Precondition: {FieldDefinition.Project.GetFieldDefinitionLabel()} update record has no start year");
            Assert.That(projectUpdateBatch.ProjectUpdate.GetCompletionYear().HasValue, Is.False, $"Precondition: {FieldDefinition.Project.GetFieldDefinitionLabel()} update record has no completion year");

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
