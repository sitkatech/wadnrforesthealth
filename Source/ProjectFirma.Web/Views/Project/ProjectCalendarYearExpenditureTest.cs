﻿/*-----------------------------------------------------------------------
<copyright file="ProjectCalendarYearExpenditureTest.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Collections.Generic;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using Newtonsoft.Json;
using ProjectFirma.Web.UnitTestCommon;
using NUnit.Framework;

namespace ProjectFirma.Web.Views.Project
{
    [TestFixture]
    public class ProjectCalendarYearExpenditureTest
    {

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void CreateFromProjectsAndCalendarYearsTest()
        {
            // Arrange
            var project1 = TestFramework.TestProject.Create();
            project1.ProjectName = "Project 1";
            var project2 = TestFramework.TestProject.Create();
            project2.ProjectName = "Project 2";
            var project3 = TestFramework.TestProject.Create();
            project3.ProjectName = "Project 3";
            var project4 = TestFramework.TestProject.Create();
            project4.ProjectName = "Project 4";
            var calendarYears = new List<int> {2010, 2011, 2012, 2013, 2014};
            var projects = new List<Models.Project> {project1, project2, project3, project4};

            var grantAllocation = TestFramework.TestGrantAllocation.Create();

            var projectProjectExpenditure1 = TestFramework.TestProjectGrantAllocationExpenditure.Create(project1, grantAllocation, 2010, 1000);
            var projectProjectExpenditure2 = TestFramework.TestProjectGrantAllocationExpenditure.Create(project1, grantAllocation, 2011, 2000);
            var projectProjectExpenditure3 = TestFramework.TestProjectGrantAllocationExpenditure.Create(project2, grantAllocation, 2012, 3000);
            var projectProjectExpenditure4 = TestFramework.TestProjectGrantAllocationExpenditure.Create(project3, grantAllocation, 2014, 4000);
            var projectProjectExpenditure5 = TestFramework.TestProjectGrantAllocationExpenditure.Create(project4, grantAllocation, 2012, 5000);

            var projectGrantAllocationExpenditures = new List<Models.ProjectGrantAllocationExpenditure>
            {
                projectProjectExpenditure1,
                projectProjectExpenditure2,
                projectProjectExpenditure3,
                projectProjectExpenditure4,
                projectProjectExpenditure5
            };

            // Act
            var result = ProjectCalendarYearExpenditure.CreateFromProjectsAndCalendarYears(projectGrantAllocationExpenditures, calendarYears);

            // Assert
            Assert.That(result.Count, Is.EqualTo(projects.Count));
            Approvals.Verify(JsonConvert.SerializeObject(result.Select(x => new { x.Project.DisplayName, x.CalendarYearExpenditure }), Formatting.Indented));
        }

    }
}
