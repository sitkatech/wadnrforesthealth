﻿/*-----------------------------------------------------------------------
<copyright file="PerformanceMeasureReportedValuesGroupedViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

namespace ProjectFirma.Web.Views.Shared.PerformanceMeasureControls
{
    public class PerformanceMeasureReportedValuesGroupedViewData : FirmaUserControlViewData
    {
        public List<CalendarYearString> CalendarYearsForPerformanceMeasures { get; }
        public List<string> ExemptReportingYears { get; }
        public string ExemptionExplanation { get; }
        public List<PerformanceMeasureSubcategoriesCalendarYearReportedValue> PerformanceMeasureSubcategoriesCalendarYearReportedValues { get; }
        public bool HideByDefault { get; }

        public PerformanceMeasureReportedValuesGroupedViewData(List<PerformanceMeasureSubcategoriesCalendarYearReportedValue> performanceMeasureSubcategoriesCalendarYearReportedValues,
            List<string> exemptReportingYears,
            string exemptionExplanation,
            List<CalendarYearString> calendarYearsForPerformanceMeasures,
            bool hideByDefault)
        {
            CalendarYearsForPerformanceMeasures = calendarYearsForPerformanceMeasures;
            PerformanceMeasureSubcategoriesCalendarYearReportedValues = performanceMeasureSubcategoriesCalendarYearReportedValues;
            ExemptReportingYears = exemptReportingYears;
            ExemptionExplanation = exemptionExplanation;
            HideByDefault = hideByDefault;
        }

        public string HideByDefaultStyle()
        {
            return $"display: {(HideByDefault ? "none" : "table")};";
        }
    }
}
