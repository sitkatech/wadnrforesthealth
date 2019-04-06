/*-----------------------------------------------------------------------
<copyright file="ProjectGrantAllocationExpenditure.DatabaseContextExtensions.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System;
using System.Collections.Generic;
using System.Linq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Views.Shared;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static List<int> CalculateCalendarYearRangeForExpenditures(this IList<ProjectGrantAllocationExpenditure> projectGrantAllocationExpenditures, Project project)
        {
            var existingYears = projectGrantAllocationExpenditures.Select(x => x.CalendarYear).ToList();
            return FirmaDateUtilities.CalculateCalendarYearRangeForExpendituresAccountingForExistingYears(existingYears, project, DateTime.Today.Year);
        }

        public static List<int> CalculateCalendarYearRangeForExpenditures(this IEnumerable<ProjectGrantAllocationExpenditure> projectGrantAllocationExpenditures, GrantAllocation grantAllocation)
        {
            var existingYears = projectGrantAllocationExpenditures.Select(x => x.CalendarYear).ToList();
            var grantAllocationProjectsWhereYouAreTheGrantAllocationMinCalendarYear = grantAllocation.ProjectsWhereYouAreTheGrantAllocationMinCalendarYear;
            return FirmaDateUtilities.CalculateCalendarYearRangeAccountingForExistingYears(existingYears,
                grantAllocationProjectsWhereYouAreTheGrantAllocationMinCalendarYear.HasValue ?
                (DateTime?) new DateTime(grantAllocationProjectsWhereYouAreTheGrantAllocationMinCalendarYear.Value, 1, 1) : null,
                grantAllocation.ProjectsWhereYouAreTheGrantAllocationMaxCalendarYear,
                DateTime.Today.Year,
                MultiTenantHelpers.GetMinimumYear(),
                null);
        }

        public static GoogleChartJson ToGoogleChart(this IEnumerable<ProjectGrantAllocationExpenditure> projectGrantAllocationExpenditures,
            Func<ProjectGrantAllocationExpenditure, string> filterFunction,
            List<string> filterValues,
            Func<ProjectGrantAllocationExpenditure, IComparable> sortFunction,
            string chartContainerID,
            string chartTitle)
        {
            var projectGrantAllocationExpenditureList = projectGrantAllocationExpenditures.ToList();
            if (!projectGrantAllocationExpenditureList.Any())
            {
                return null;
            }
            var beginCalendarYear = projectGrantAllocationExpenditureList.Min(x => x.CalendarYear);
            var endCalendarYear = projectGrantAllocationExpenditureList.Max(x => x.CalendarYear);
            var rangeOfYears = FirmaDateUtilities.GetRangeOfYears(beginCalendarYear, endCalendarYear);

            return projectGrantAllocationExpenditureList.ToGoogleChart(filterFunction, filterValues, sortFunction, rangeOfYears, chartContainerID, chartTitle, GoogleChartType.AreaChart, true);
        }

        //TODO: The GetFullCategoryYearDictionary and GetGoogleChartDataTable functions are probably fine in this Extension class, but the ToGoogleChart functions are more about display and probably could be in a better location
        public static GoogleChartJson ToGoogleChart(this IEnumerable<ProjectGrantAllocationExpenditure> projectGrantAllocationExpenditures,
            Func<ProjectGrantAllocationExpenditure, string> filterFunction,
            List<string> filterValues,
            Func<ProjectGrantAllocationExpenditure, IComparable> sortFunction,
            List<int> rangeOfYears,
            string chartContainerID,
            string chartTitle,
            GoogleChartType googleChartType,
            bool isStacked)
        {
            var fullCategoryYearDictionary = GetFullCategoryYearDictionary(projectGrantAllocationExpenditures, filterFunction, filterValues, sortFunction, rangeOfYears);
            var googleChartDataTable = GetGoogleChartDataTable(fullCategoryYearDictionary, rangeOfYears, googleChartType);
            var googleChartAxis = new GoogleChartAxis("Annual Expenditures", MeasurementUnitTypeEnum.Dollars, GoogleChartAxisLabelFormat.Short);
            var googleChartConfiguration = new GoogleChartConfiguration(chartTitle,
                isStacked,
                googleChartType,
                googleChartDataTable,
                new GoogleChartAxis(FieldDefinition.ReportingYear.GetFieldDefinitionLabel(), null, null),
                new List<GoogleChartAxis> { googleChartAxis });
            var googleChart = new GoogleChartJson(chartTitle, chartContainerID, googleChartConfiguration,
                googleChartType, googleChartDataTable, null);
            return googleChart;
        }

        public static Dictionary<string, Dictionary<int, decimal>> GetFullCategoryYearDictionary(this IEnumerable<ProjectGrantAllocationExpenditure> projectGrantAllocationExpenditures,
                                                                                                 Func<ProjectGrantAllocationExpenditure, string> filterFunction,
                                                                                                 List<string> filterValues,
                                                                                                 Func<ProjectGrantAllocationExpenditure, IComparable> sortFunction,
                                                                                                 List<int> rangeOfYears)
        {
            var fullCategoryYearDictionary = filterValues.ToDictionary(x => x, x => rangeOfYears.ToDictionary(y => y, y => 0m));

            var projectGrantAllocationExpendituresByYear = projectGrantAllocationExpenditures.OrderBy(sortFunction.Invoke).Where(x => rangeOfYears.Contains(x.CalendarYear)).GroupBy(x => x.CalendarYear).ToList();
            projectGrantAllocationExpendituresByYear.ForEach(x =>
            {
                filterValues.ForEach(s =>
                {
                    var expenditures = x.Where(y => filterFunction.Invoke(y) == s).ToList();
                    fullCategoryYearDictionary[s][x.Key] = expenditures.Sum(y => y.MonetaryAmount ?? 0);
                });
            });
            return fullCategoryYearDictionary;
        }

        private static GoogleChartDataTable GetGoogleChartDataTable(Dictionary<string, Dictionary<int, decimal>> fullCategoryYearDictionary, List<int> rangeOfYears, GoogleChartType columnDisplayType)
        {
            var googleChartRowCs = new List<GoogleChartRowC>();
            var sortedYearCategoryDictionary =
                fullCategoryYearDictionary.OrderBy(x => x.Value.Sum(y => y.Value)).ThenBy(x => x.Key).ToList();

            foreach (var year in rangeOfYears.OrderBy(x => x))
            {
                var googleChartRowVs = new List<GoogleChartRowV> { new GoogleChartRowV(year, year.ToString()) };
                googleChartRowVs.AddRange(
                    sortedYearCategoryDictionary
                        .Select(x => x.Key)
                        .Select(category => fullCategoryYearDictionary[category][year])
                        .Select(value => new GoogleChartRowV(value, GoogleChartJson.GetFormattedValue((double)value, MeasurementUnitType.Dollars))));

                googleChartRowCs.Add(new GoogleChartRowC(googleChartRowVs));
            }

            var columnLabel = FieldDefinition.ReportingYear.GetFieldDefinitionLabel();
            var googleChartColumns = new List<GoogleChartColumn> { new GoogleChartColumn(columnLabel, columnLabel, "string") };
            googleChartColumns.AddRange(
                sortedYearCategoryDictionary.Select(
                        x => new GoogleChartColumn(x.Key, x.Key, "number", new GoogleChartSeries(columnDisplayType, GoogleChartAxisType.Primary), null, null)));

            return new GoogleChartDataTable(googleChartColumns, googleChartRowCs);
        }
    }
}
