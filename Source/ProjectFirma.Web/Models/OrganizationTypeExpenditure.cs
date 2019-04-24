/*-----------------------------------------------------------------------
<copyright file="OrganizationTypeExpenditure.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

namespace ProjectFirma.Web.Models
{
    public class OrganizationTypeExpenditure
    {
        private readonly OrganizationTypeSimple _organizationType;
        public int OrganizationTypeID
        {
            get { return _organizationType.OrganizationTypeID; }
        }
        public string OrganizationTypeName
        {
            get { return _organizationType.OrganizationTypeName; }
        }
        public string LegendColor
        {
            get { return _organizationType.LegendColor; }
        }
        public readonly decimal CalendarYearExpenditureAmount;
        public readonly int GrantAllocationCount;
        public readonly int GrantAllocationOrganizationCount;
        public readonly int? CalendarYear;

        public OrganizationTypeExpenditure(OrganizationType organizationType, decimal calendarYearExpenditureAmount, int grantAllocationCount, int grantAllocationOrganizationCount, int? calendarYear)
        {
            _organizationType = new OrganizationTypeSimple(organizationType);
            CalendarYear = calendarYear;
            CalendarYearExpenditureAmount = calendarYearExpenditureAmount;
            GrantAllocationCount = grantAllocationCount;
            GrantAllocationOrganizationCount = grantAllocationOrganizationCount;
        }

        public OrganizationTypeExpenditure(OrganizationType organizationType, List<ProjectGrantAllocationExpenditure> projectGrantAllocationExpendituresForThisOrganizationTypeAndCalendarYear, int? calendarYear)
        {
            _organizationType = new OrganizationTypeSimple(organizationType);
            CalendarYear = calendarYear;
            CalendarYearExpenditureAmount = projectGrantAllocationExpendituresForThisOrganizationTypeAndCalendarYear.Sum(x => x.ExpenditureAmount);
            GrantAllocationCount = projectGrantAllocationExpendituresForThisOrganizationTypeAndCalendarYear.Select(x => x.GrantAllocationID).Distinct().Count();
            GrantAllocationOrganizationCount = projectGrantAllocationExpendituresForThisOrganizationTypeAndCalendarYear.Select(x => x.GrantAllocation.BottommostOrganization.OrganizationID).Distinct().Count();
        }
    }
}
