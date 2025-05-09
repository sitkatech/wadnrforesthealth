﻿/*-----------------------------------------------------------------------
<copyright file="TestProjectGrantAllocationExpenditure.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.UnitTestCommon
{
    public static partial class TestFramework
    {
        public static class TestProjectGrantAllocationExpenditure
        {
            public static ProjectGrantAllocationExpenditure Create()
            {
                var project = TestProject.Create();
                var grantAllocation = TestGrantAllocation.Create();
                return Create(project, grantAllocation);
            }

            public static ProjectGrantAllocationExpenditure Create(Project project, GrantAllocation grantAllocation)
            {
                var projectGrantAllocationExpenditure = ProjectGrantAllocationExpenditure.CreateNewBlank(project, grantAllocation);
                return projectGrantAllocationExpenditure;
            }

            public static ProjectGrantAllocationExpenditure Create(Project project, GrantAllocation grantAllocation, int calendarYear)
            {
                var projectGrantAllocationExpenditure = Create(project, grantAllocation);
                projectGrantAllocationExpenditure.CalendarYear = calendarYear;
                return projectGrantAllocationExpenditure;
            }

            public static ProjectGrantAllocationExpenditure Create(Project project, GrantAllocation grantAllocation, int calendarYear, decimal expenditureAmount)
            {
                var projectGrantAllocationExpenditure = Create(project, grantAllocation, calendarYear);
                projectGrantAllocationExpenditure.ExpenditureAmount = expenditureAmount;
                return projectGrantAllocationExpenditure;
            }
        }
    }
}
