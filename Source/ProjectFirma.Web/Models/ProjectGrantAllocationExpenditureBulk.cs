/*-----------------------------------------------------------------------
<copyright file="ProjectGrantAllocationExpenditureBulk.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using LtInfo.Common.DesignByContract;

namespace ProjectFirma.Web.Models
{
    public class ProjectGrantAllocationExpenditureBulk
    {
        public int ProjectID { get; set; }
        public int GrantAllocationID { get; set; }
        public List<CalendarYearMonetaryAmount> CalendarYearExpenditures { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public ProjectGrantAllocationExpenditureBulk()
        {
        }

        public ProjectGrantAllocationExpenditureBulk(ProjectGrantAllocationExpenditure projectGrantAllocationExpenditure,
            List<ProjectGrantAllocationExpenditure> projectGrantAllocationExpenditures,
            IEnumerable<int> calendarYearsToPopulate)
        {
            ProjectID = projectGrantAllocationExpenditure.ProjectID;
            GrantAllocationID = projectGrantAllocationExpenditure.GrantAllocationID;
            CalendarYearExpenditures = new List<CalendarYearMonetaryAmount>();
            Add(projectGrantAllocationExpenditures);
            // we need to fill in the other calendar years with blanks
            var usedCalendarYears = projectGrantAllocationExpenditures.Select(x => x.CalendarYear).ToList();
            CalendarYearExpenditures.AddRange(calendarYearsToPopulate.Where(x => !usedCalendarYears.Contains(x)).ToList().Select(x => new CalendarYearMonetaryAmount(x, null)));
        }

        private ProjectGrantAllocationExpenditureBulk(ProjectGrantAllocationExpenditureUpdate projectGrantAllocationExpenditureUpdate,
                                                      List<ProjectGrantAllocationExpenditureUpdate> projectGrantAllocationExpenditureUpdates,
                                                      IEnumerable<int> calendarYearsToPopulate)
        {
            ProjectID = projectGrantAllocationExpenditureUpdate.ProjectUpdateBatch.ProjectID;
            GrantAllocationID = projectGrantAllocationExpenditureUpdate.GrantAllocationID;
            CalendarYearExpenditures = new List<CalendarYearMonetaryAmount>();
            Add(projectGrantAllocationExpenditureUpdates);
            // we need to fill in the other calendar years with blanks
            var usedCalendarYears = projectGrantAllocationExpenditureUpdates.Select(x => x.CalendarYear).ToList();
            CalendarYearExpenditures.AddRange(calendarYearsToPopulate.Where(x => !usedCalendarYears.Contains(x)).ToList().Select(x => new CalendarYearMonetaryAmount(x, null)));
        }

        public static List<ProjectGrantAllocationExpenditureBulk> MakeFromList(List<ProjectGrantAllocationExpenditure> projectGrantAllocationExpenditures, List<int> calendarYears)
        {
            var groupedByProjectGrantAllocation = projectGrantAllocationExpenditures.GroupBy(x => new {x.ProjectID, x.GrantAllocationID});
            return groupedByProjectGrantAllocation.Select(grouping => new ProjectGrantAllocationExpenditureBulk(grouping.First(), grouping.ToList(), calendarYears)).ToList();
        }

        public static List<ProjectGrantAllocationExpenditureBulk> MakeFromList(List<ProjectGrantAllocationExpenditureUpdate> projectGrantAllocationExpenditureUpdates, List<int> calendarYearsToPopulate)
        {
            var projectGrantAllocationExpenditureUpdatesGrouped = projectGrantAllocationExpenditureUpdates.GroupBy(x => new {x.ProjectUpdateBatchID, x.GrantAllocationID});
            return projectGrantAllocationExpenditureUpdatesGrouped.Select(grouping => new ProjectGrantAllocationExpenditureBulk(grouping.First(), grouping.ToList(), calendarYearsToPopulate)).ToList();
        }

        public void Add(List<ProjectGrantAllocationExpenditure> projectGrantAllocationExpenditures)
        {
            projectGrantAllocationExpenditures.ForEach(Add);
        }

        public void Add(List<ProjectGrantAllocationExpenditureUpdate> projectGrantAllocationExpenditureUpdates)
        {
            projectGrantAllocationExpenditureUpdates.ForEach(Add);
        }

        public void Add(ProjectGrantAllocationExpenditure projectGrantAllocationExpenditure)
        {
            Check.Require(projectGrantAllocationExpenditure.ProjectID == ProjectID && projectGrantAllocationExpenditure.GrantAllocationID == GrantAllocationID,
                "Row doesn't align with collection mismatch ProjectID and GrantAllocationID");
            CalendarYearExpenditures.Add(new CalendarYearMonetaryAmount(projectGrantAllocationExpenditure.CalendarYear, projectGrantAllocationExpenditure.ExpenditureAmount));
        }

        public void Add(ProjectGrantAllocationExpenditureUpdate projectGrantAllocationExpenditureUpdate)
        {
            Check.Require(projectGrantAllocationExpenditureUpdate.ProjectUpdateBatch.ProjectID == ProjectID && projectGrantAllocationExpenditureUpdate.GrantAllocationID == GrantAllocationID,
                "Row doesn't align with collection mismatch ProjectID and GrantAllocationID");
            CalendarYearExpenditures.Add(new CalendarYearMonetaryAmount(projectGrantAllocationExpenditureUpdate.CalendarYear, projectGrantAllocationExpenditureUpdate.ExpenditureAmount));
        }

        public List<ProjectGrantAllocationExpenditure> ToProjectGrantAllocationExpenditures()
        {
            // ReSharper disable PossibleInvalidOperationException
            return
                CalendarYearExpenditures.Where(x => x.MonetaryAmount.HasValue)
                    .Select(x => new ProjectGrantAllocationExpenditure(ProjectID, x.CalendarYear, x.MonetaryAmount.Value, GrantAllocationID))
                    .ToList();
            // ReSharper restore PossibleInvalidOperationException
        }

        public List<ProjectGrantAllocationExpenditureUpdate> ToProjectGrantAllocationExpenditureUpdates(ProjectUpdateBatch projectUpdateBatch)
        {
            // ReSharper disable PossibleInvalidOperationException
            return
                CalendarYearExpenditures.Where(x => x.MonetaryAmount.HasValue)
                    .Select(x => new ProjectGrantAllocationExpenditureUpdate(projectUpdateBatch.ProjectUpdateBatchID, x.CalendarYear, x.MonetaryAmount.Value, GrantAllocationID))
                    .ToList();
            // ReSharper restore PossibleInvalidOperationException
        }
    }
}
