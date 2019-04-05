/*-----------------------------------------------------------------------
<copyright file="GrantAllocationCalendarYearExpenditureTest.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ApprovalTests.Reporters;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.UnitTestCommon;
using NUnit.Framework;

namespace ProjectFirma.Web.Views.Project
{
    [TestFixture]
    public class GrantAllocationCalendarYearExpenditureTest
    {
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void CreateFromGrantAllocationsAndCalendarYearsTest()
        {
            // Arrange
            var grantAllocation1 = TestFramework.TestGrantAllocation.Create();
            grantAllocation1.GrantAllocationName = "Grant Allocation 1";
            var grantAllocation2 = TestFramework.TestGrantAllocation.Create();
            grantAllocation2.GrantAllocationName = "Grant Allocation 2";
            var grantAllocation3 = TestFramework.TestGrantAllocation.Create();
            grantAllocation3.GrantAllocationName = "Grant Allocation 3";
            var grantAllocation4 = TestFramework.TestGrantAllocation.Create();
            grantAllocation4.GrantAllocationName = "Grant Allocation 4";
            var calendarYears = new List<int> {2010, 2011, 2012, 2013, 2014};
            var grantAllocations = new List<Models.GrantAllocation> {grantAllocation1, grantAllocation2, grantAllocation3, grantAllocation4};

            var project = TestFramework.TestProject.Create();

            var projectGrantAllocationExpenditure1 = TestFramework.TestProjectGrantAllocationExpenditure.Create(project, grantAllocation1, 2010, 1000);
            var projectGrantAllocationExpenditure2 = TestFramework.TestProjectGrantAllocationExpenditure.Create(project, grantAllocation1, 2011, 2000);
            var projectGrantAllocationExpenditure3 = TestFramework.TestProjectGrantAllocationExpenditure.Create(project, grantAllocation2, 2012, 3000);
            var projectGrantAllocationExpenditure4 = TestFramework.TestProjectGrantAllocationExpenditure.Create(project, grantAllocation3, 2014, 4000);
            var projectGrantAllocationExpenditure5 = TestFramework.TestProjectGrantAllocationExpenditure.Create(project, grantAllocation4, 2012, 5000);

            var projectGrantAllocationExpenditures = new List<Models.ProjectGrantAllocationExpenditure>
            {
                projectGrantAllocationExpenditure1,
                projectGrantAllocationExpenditure2,
                projectGrantAllocationExpenditure3,
                projectGrantAllocationExpenditure4,
                projectGrantAllocationExpenditure5
            };

            // Act
            var result = GrantAllocationCalendarYearExpenditure.CreateFromGrantAllocationsAndCalendarYears(new List<IGrantAllocationExpenditure>(projectGrantAllocationExpenditures), calendarYears);

            // Assert
            Assert.That(result.Count, Is.EqualTo(grantAllocations.Count));
            ObjectApproval.ObjectApprover.VerifyWithJson(result.Select(x => new {x.GrantAllocationName, x.CalendarYearExpenditure}));
        }
        
    }
}
