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

namespace ProjectFirma.Web.Views.ProjectCreate
{
    public class PerformanceMeasuresViewData : ProjectCreateViewData
    {
        public string RefreshUrl { get; }
        public PerformanceMeasureReportedValuesSummaryViewData PerformanceMeasureReportedValuesSummaryViewData { get; }
        public ViewDataForAngularEditor ViewDataForAngular { get; }

        public bool IsImplementationStartYearValid { get; }
        public string DiffUrl { get; }

        public string ReportingYearLabel { get; }

        public PerformanceMeasuresViewData(Person currentPerson, Models.Project project, ViewDataForAngularEditor viewDataForAngularEditor, ProposalSectionsStatus proposalSectionsStatus)
            : base(currentPerson, project, ProjectCreateSection.ReportedPerformanceMeasures.ProjectCreateSectionDisplayName, proposalSectionsStatus)
        {
            RefreshUrl = SitkaRoute<ProjectUpdateController>.BuildUrlFromExpression(x => x.RefreshPerformanceMeasures(project));
            DiffUrl = SitkaRoute<ProjectUpdateController>.BuildUrlFromExpression(x => x.DiffPerformanceMeasures(project));
            var performanceMeasureActuals = project.GetReportedPerformanceMeasures();
            var performanceMeasureSubcategoriesCalendarYearReportedValues =
                PerformanceMeasureSubcategoriesCalendarYearReportedValue.CreateFromPerformanceMeasuresAndCalendarYears(new List<IPerformanceMeasureReportedValue>(performanceMeasureActuals));
            PerformanceMeasureReportedValuesSummaryViewData = new PerformanceMeasureReportedValuesSummaryViewData(performanceMeasureSubcategoriesCalendarYearReportedValues,
                project.GetPerformanceMeasuresExemptReportingYears().Select(x => x.CalendarYear).ToList(),
                project.PerformanceMeasureActualYearsExemptionExplanation,
                performanceMeasureActuals.Select(x => x.CalendarYear).Distinct().Select(x => new CalendarYearString(x)).ToList());
            ViewDataForAngular = viewDataForAngularEditor;
            
            IsImplementationStartYearValid = project.GetImplementationStartYear().HasValue && project.GetImplementationStartYear() < project.GetCompletionYear();
            ReportingYearLabel = "Year";
        }

        public class ViewDataForAngularEditor
        {
            public readonly int ProjectID;
            public readonly List<PerformanceMeasureSimple> AllPerformanceMeasures;
            public readonly List<PerformanceMeasureSubcategorySimple> AllPerformanceMeasureSubcategories;
            public readonly List<PerformanceMeasureSubcategoryOptionSimple> AllPerformanceMeasureSubcategoryOptions;
            public readonly List<CalendarYearString> CalendarYearStrings;
            public readonly int MaxSubcategoryOptions;
            public readonly bool ShowExemptYears;

            public ViewDataForAngularEditor(int projectBatchID,
                List<PerformanceMeasureSimple> allPerformanceMeasures,
                List<PerformanceMeasureSubcategorySimple> allPerformanceMeasureSubcategories,
                List<PerformanceMeasureSubcategoryOptionSimple> allPerformanceMeasureSubcategoryOptions,
                List<CalendarYearString> calendarYearStrings,
                bool showExemptYears)
            {
                ProjectID = projectBatchID;
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
