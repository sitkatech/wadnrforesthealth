/*-----------------------------------------------------------------------
<copyright file="GrantAllocationCalendarYearExpenditure.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Web;
using ProjectFirma.Web.Models;
using LtInfo.Common.Models;
using ProjectFirma.Web.Views.Shared;

namespace ProjectFirma.Web.Views.Project
{
    public class GrantAllocationCalendarYearExpenditure
    {
        //Grant Allocation area
        public readonly Models.GrantAllocation GrantAllocation;
        public int GrantAllocationID
        {
            get { return GrantAllocation == null ? ModelObjectHelpers.NotYetAssignedID : GrantAllocation.GrantAllocationID; }
        }
        public string GrantAllocationName
        {
            get { return GrantAllocation == null ? "Unknown" : GrantAllocation.DisplayName; }
        }
        public string GrantAllocationOrganizationName
        {
            get { return GrantAllocation == null ? "Unknown" : GrantAllocation.Organization.DisplayName; }
        }
        public HtmlString GrantAllocationNameAsUrl
        {
            get { return GrantAllocation == null ? new HtmlString("Unknown") : GrantAllocation.DisplayNameAsUrl; }
        }
        public HtmlString GrantAllocationOrganizationNameAsUrl
        {
            get { return GrantAllocation == null ? new HtmlString("Unknown") : GrantAllocation.Organization.GetDisplayNameAsUrl(); }
        }



        public readonly Dictionary<int, decimal?> CalendarYearExpenditure;
        public string DisplayCssClass;

        public GrantAllocationCalendarYearExpenditure(Models.GrantAllocation grantAllocation, Dictionary<int, decimal?> calendarYearExpenditure, string displayCssClass)
        {
            GrantAllocation = grantAllocation;
            CalendarYearExpenditure = calendarYearExpenditure;
            DisplayCssClass = displayCssClass;
        }

        public GrantAllocationCalendarYearExpenditure(Dictionary<int, decimal?> calendarYearExpenditure) : this(null, calendarYearExpenditure, null)
        {
        }

        public static List<GrantAllocationCalendarYearExpenditure> CreateFromGrantAllocationsAndCalendarYears(List<IGrantAllocationExpenditure> grantAllocationExpenditures, List<int> calendarYears)
        {
            var distinctGrantAllocations = grantAllocationExpenditures.Select(x => x.GrantAllocation).Distinct(new HavePrimaryKeyComparer<Models.GrantAllocation>());
            var grantAllocationsCrossJoinCalendarYears =
                distinctGrantAllocations.Select(ga => new GrantAllocationCalendarYearExpenditure(ga, calendarYears.ToDictionary<int, int, decimal?>(calendarYear => calendarYear, calendarYear => null), null))
                    .ToList();

            foreach (var grantAllocationExpenditure in grantAllocationExpenditures.GroupBy(x => x.GrantAllocationID))
            {
                var current = grantAllocationsCrossJoinCalendarYears.Single(x => x.GrantAllocationID == grantAllocationExpenditure.Key);
                foreach (var calendarYear in calendarYears)
                {
                    current.CalendarYearExpenditure[calendarYear] =
                        grantAllocationExpenditure.Where(gae => gae.CalendarYear == calendarYear).Select(x => x.MonetaryAmount).Sum();
                }
            }
            return grantAllocationsCrossJoinCalendarYears;
        }

        public static GrantAllocationCalendarYearExpenditure Clone(GrantAllocationCalendarYearExpenditure grantAllocationCalendarYearExpenditureToDiff, string displayCssClass)
        {
            return new GrantAllocationCalendarYearExpenditure(grantAllocationCalendarYearExpenditureToDiff.GrantAllocation,
                                                            grantAllocationCalendarYearExpenditureToDiff.CalendarYearExpenditure.ToDictionary(x => x.Key, x => x.Value),
                                                            displayCssClass);
        }
    }
}
