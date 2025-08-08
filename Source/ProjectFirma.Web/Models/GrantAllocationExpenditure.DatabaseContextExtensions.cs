//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundSourceAllocationExpenditure]

using System;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Views.Shared;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
       public static GoogleChartJson ToGoogleChart(this IEnumerable<FundSourceAllocationExpenditure> fundSourceAllocationExpenditures,
            Func<FundSourceAllocationExpenditure, string> filterFunction,
            List<string> filterValues,
            Func<FundSourceAllocationExpenditure, IComparable> sortFunction,
            string chartContainerID,
            string chartTitle)
        {
            var fundSourceAllocationExpenditureList = fundSourceAllocationExpenditures.ToList();
            if (!fundSourceAllocationExpenditureList.Any())
            {
                return null;
            }
            var beginCalendarYear = fundSourceAllocationExpenditureList.Min(x => x.CalendarYear);
            var endCalendarYear = fundSourceAllocationExpenditureList.Max(x => x.CalendarYear);
            var rangeOfYears = FirmaDateUtilities.GetRangeOfYears(beginCalendarYear, endCalendarYear);

            return fundSourceAllocationExpenditureList.ToGoogleChart(filterFunction, filterValues, sortFunction, rangeOfYears, chartContainerID, chartTitle, GoogleChartType.AreaChart, true);
        }

        public static GoogleChartJson ToGoogleChart(this IEnumerable<FundSourceAllocationExpenditure> fundSourceAllocationExpenditures,
            Func<FundSourceAllocationExpenditure, string> filterFunction,
            List<string> filterValues,
            Func<FundSourceAllocationExpenditure, IComparable> sortFunction,
            List<int> rangeOfYears,
            string chartContainerID,
            string chartTitle,
            GoogleChartType googleChartType,
            bool isStacked)
        {
            var fullCategoryYearDictionary = GetFullCategoryYearDictionary(fundSourceAllocationExpenditures, filterFunction, filterValues, sortFunction, rangeOfYears);
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




        public static Dictionary<string, Dictionary<int, decimal>> GetFullCategoryYearDictionary(this IEnumerable<FundSourceAllocationExpenditure> fundSourceAllocationExpenditures,
                                                                                                 Func<FundSourceAllocationExpenditure, string> filterFunction,
                                                                                                 List<string> filterValues,
                                                                                                 Func<FundSourceAllocationExpenditure, IComparable> sortFunction,
                                                                                                 List<int> rangeOfYears)
        {
            // Not absolutely sure these are happening in the real world, so I'm trapping for them. -- SLG
            Check.Ensure(filterValues.Count == filterValues.Distinct().Count(), $"Found repeated filterValues: {string.Join(", ", filterValues)}. This will cause problems with dictionary generation below.");
            Check.Ensure(rangeOfYears.Count == rangeOfYears.Distinct().Count(), $"Found repeated years in rangeOfYears: {string.Join(", ", rangeOfYears)}. This is a bug.");

            var fullCategoryYearDictionary = filterValues.ToDictionary(x => x, x => rangeOfYears.ToDictionary(y => y, y => 0m));
            var fundSourceAllocationExpendituresByYear = fundSourceAllocationExpenditures.OrderBy(sortFunction.Invoke).Where(x => rangeOfYears.Contains(x.CalendarYear)).GroupBy(x => x.CalendarYear).ToList();
            fundSourceAllocationExpendituresByYear.ForEach(x =>
            {
                filterValues.ForEach(s =>
                {
                    var expenditures = x.Where(y => filterFunction.Invoke(y) == s).ToList();
                    fullCategoryYearDictionary[s][x.Key] = expenditures.Sum(y => y.ExpenditureAmount);
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