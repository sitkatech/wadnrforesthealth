﻿/*-----------------------------------------------------------------------
<copyright file="ProjectExpendituresDetailViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Views.Project;

namespace ProjectFirma.Web.Views.Shared.ExpenditureAndBudgetControls
{
    public class ProjectExpendituresDetailViewData : FirmaUserControlViewData
    {
        public List<CalendarYearString> CalendarYearStrings { get; }
        public List<GrantAllocationCalendarYearExpenditure> GrantAllocationCalendarYearExpenditures { get; }
        public List<string> ExemptReportingYears { get; }
        public string ExemptionExplanation { get; }

        public ProjectExpendituresDetailViewData(List<GrantAllocationCalendarYearExpenditure> grantAllocationCalendarYearExpenditures, List<CalendarYearString> calendarYearStrings, List<string> exemptReportingYears, string exemptionExplanation)
        {
            GrantAllocationCalendarYearExpenditures = grantAllocationCalendarYearExpenditures;
            CalendarYearStrings = calendarYearStrings;
            ExemptReportingYears = exemptReportingYears;
            ExemptionExplanation = exemptionExplanation;
        }
    }
}
