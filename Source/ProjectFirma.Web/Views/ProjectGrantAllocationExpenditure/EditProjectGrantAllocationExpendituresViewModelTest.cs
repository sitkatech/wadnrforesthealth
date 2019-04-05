/*-----------------------------------------------------------------------
<copyright file="EditProjectGrantAllocationExpendituresViewModelTest.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.UnitTestCommon;

namespace ProjectFirma.Web.Views.ProjectGrantAllocationExpenditure
{
    [TestFixture]
    public class EditProjectGrantAllocationExpendituresViewModelTest
    {
        [Ignore]
        [Test]
        public void AllViewModelFieldsAreSetFromConstructorTest()
        {
            // Arrange
            var grantAllocation1 = TestFramework.TestGrantAllocation.Create();
            var grantAllocation2 = TestFramework.TestGrantAllocation.Create();
            var grantAllocation3 = TestFramework.TestGrantAllocation.Create();
            var grantAllocation4 = TestFramework.TestGrantAllocation.Create();

            var project = TestFramework.TestProject.Create();
            TestFramework.TestProjectGrantAllocationExpenditure.Create(project, grantAllocation1);
            TestFramework.TestProjectGrantAllocationExpenditure.Create(project, grantAllocation2);
            TestFramework.TestProjectGrantAllocationExpenditure.Create(project, grantAllocation3);
            TestFramework.TestProjectGrantAllocationExpenditure.Create(project, grantAllocation4);

            var allGrantAllocations = new List<Models.GrantAllocation> {grantAllocation1, grantAllocation2, grantAllocation3, grantAllocation4};

            // Act
            var projectGrantAllocationExpenditures = project.ProjectGrantAllocationExpenditures.ToList();
            var calendarYearRangeForExpenditures = projectGrantAllocationExpenditures.CalculateCalendarYearRangeForExpenditures(project);
            var viewModel = new EditProjectGrantAllocationExpendituresViewModel(null, ProjectGrantAllocationExpenditureBulk.MakeFromList(projectGrantAllocationExpenditures, calendarYearRangeForExpenditures), new List<ProjectExemptReportingYearSimple>());

            // Assert
            Assert.That(viewModel.ProjectGrantAllocationExpenditures.Select(x => x.GrantAllocationID), Is.EquivalentTo(allGrantAllocations.Select(x => x.GrantAllocationID)));
        }

    }
}
