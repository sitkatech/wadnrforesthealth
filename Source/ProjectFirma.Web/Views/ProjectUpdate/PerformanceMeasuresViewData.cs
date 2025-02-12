﻿/*-----------------------------------------------------------------------
<copyright file="PerformanceMeasuresViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Project;
using ProjectFirma.Web.Views.Shared;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Views.Shared.PerformanceMeasureControls;

namespace ProjectFirma.Web.Views.ProjectUpdate
{
    public class PerformanceMeasuresViewData : ProjectUpdateViewData
    {
        public string RefreshUrl { get; }
        public PerformanceMeasureReportedValuesSummaryViewData PerformanceMeasureReportedValuesSummaryViewData { get; }
        public ViewDataForAngularEditor ViewDataForAngular { get; }
        public SectionCommentsViewData SectionCommentsViewData { get; }
        public bool IsImplementationStartYearValid { get; }
        public string DiffUrl { get; }
        public string ReportingYearLabel { get; }

        public PerformanceMeasuresViewData(Person currentPerson, ProjectUpdateBatch projectUpdateBatch, ViewDataForAngularEditor viewDataForAngularEditor, UpdateStatus updateStatus, PerformanceMeasuresValidationResult performanceMeasuresValidationResult)
            : base(currentPerson, projectUpdateBatch, updateStatus, performanceMeasuresValidationResult.GetWarningMessages(), ProjectUpdateSection.PerformanceMeasures.ProjectUpdateSectionDisplayName)
        {
            RefreshUrl = SitkaRoute<ProjectUpdateController>.BuildUrlFromExpression(x => x.RefreshPerformanceMeasures(projectUpdateBatch.Project));
            DiffUrl = SitkaRoute<ProjectUpdateController>.BuildUrlFromExpression(x => x.DiffPerformanceMeasures(projectUpdateBatch.Project));
            var performanceMeasureActualUpdates = projectUpdateBatch.PerformanceMeasureActualUpdates;
            var performanceMeasureSubcategoriesCalendarYearReportedValues =
                PerformanceMeasureSubcategoriesCalendarYearReportedValue.CreateFromPerformanceMeasuresAndCalendarYears(new List<IPerformanceMeasureReportedValue>(performanceMeasureActualUpdates));
            PerformanceMeasureReportedValuesSummaryViewData = new PerformanceMeasureReportedValuesSummaryViewData(performanceMeasureSubcategoriesCalendarYearReportedValues,
                projectUpdateBatch.GetPerformanceMeasuresExemptReportingYears().Select(x => x.CalendarYear).ToList(),
                projectUpdateBatch.PerformanceMeasureActualYearsExemptionExplanation,
                performanceMeasureActualUpdates.Select(x => x.CalendarYear).Distinct().Select(x => new CalendarYearString(x)).ToList());
            ViewDataForAngular = viewDataForAngularEditor;
            SectionCommentsViewData = new SectionCommentsViewData(projectUpdateBatch.PerformanceMeasuresComment, projectUpdateBatch.IsReturned);
            IsImplementationStartYearValid = projectUpdateBatch.ProjectUpdate.GetImplementationStartYear().HasValue &&
                                             projectUpdateBatch.ProjectUpdate.GetImplementationStartYear() < projectUpdateBatch.ProjectUpdate.GetCompletionYear();

            ReportingYearLabel = "Year";
        }

        public class ViewDataForAngularEditor
        {
            public readonly int ProjectUpdateBatchID;
            public readonly List<PerformanceMeasureSimple> AllPerformanceMeasures;
            public readonly List<PerformanceMeasureSubcategorySimple> AllPerformanceMeasureSubcategories;
            public readonly List<PerformanceMeasureSubcategoryOptionSimple> AllPerformanceMeasureSubcategoryOptions;
            public readonly List<CalendarYearString> CalendarYearStrings;
            public readonly int MaxSubcategoryOptions;
            public readonly bool ShowExemptYears;

            public ViewDataForAngularEditor(int projectUpdateBatchID,
                List<PerformanceMeasureSimple> allPerformanceMeasures,
                List<PerformanceMeasureSubcategorySimple> allPerformanceMeasureSubcategories,
                List<PerformanceMeasureSubcategoryOptionSimple> allPerformanceMeasureSubcategoryOptions,
                List<CalendarYearString> calendarYearStrings,
                bool showExemptYears)
            {
                ProjectUpdateBatchID = projectUpdateBatchID;
                AllPerformanceMeasures = allPerformanceMeasures;
                AllPerformanceMeasureSubcategories = allPerformanceMeasureSubcategories;
                AllPerformanceMeasureSubcategoryOptions = allPerformanceMeasureSubcategoryOptions;
                CalendarYearStrings = calendarYearStrings;
                ShowExemptYears = showExemptYears;
                MaxSubcategoryOptions = allPerformanceMeasureSubcategories.GroupBy(x => x.PerformanceMeasureID).Max(x => x.Count());
            }
        }
    }
}
