//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationExpenditure]

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
       public static GoogleChartJson ToGoogleChart(this IEnumerable<GrantAllocationExpenditure> grantAllocationExpenditures,
            Func<GrantAllocationExpenditure, string> filterFunction,
            List<string> filterValues,
            Func<GrantAllocationExpenditure, IComparable> sortFunction,
            string chartContainerID,
            string chartTitle)
        {
            var grantAllocationExpenditureList = grantAllocationExpenditures.ToList();
            if (!grantAllocationExpenditureList.Any())
            {
                return null;
            }
            var beginCalendarYear = grantAllocationExpenditureList.Min(x => x.CalendarYear);
            var endCalendarYear = grantAllocationExpenditureList.Max(x => x.CalendarYear);
            var rangeOfYears = FirmaDateUtilities.GetRangeOfYears(beginCalendarYear, endCalendarYear);

            return grantAllocationExpenditureList.ToGoogleChart(filterFunction, filterValues, sortFunction, rangeOfYears, chartContainerID, chartTitle, GoogleChartType.AreaChart, true);
        }

        public static GoogleChartJson ToGoogleChart(this IEnumerable<GrantAllocationExpenditure> grantAllocationExpenditures,
            Func<GrantAllocationExpenditure, string> filterFunction,
            List<string> filterValues,
            Func<GrantAllocationExpenditure, IComparable> sortFunction,
            List<int> rangeOfYears,
            string chartContainerID,
            string chartTitle,
            GoogleChartType googleChartType,
            bool isStacked)
        {
            var fullCategoryYearDictionary = GetFullCategoryYearDictionary(grantAllocationExpenditures, filterFunction, filterValues, sortFunction, rangeOfYears);
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

        public static Dictionary<string, Dictionary<int, decimal>> GetFullCategoryYearDictionary(this IEnumerable<GrantAllocationExpenditure> grantAllocationExpenditures,
                                                                                                 Func<GrantAllocationExpenditure, string> filterFunction,
                                                                                                 List<string> filterValues,
                                                                                                 Func<GrantAllocationExpenditure, IComparable> sortFunction,
                                                                                                 List<int> rangeOfYears)
        {
            // Not absolutely sure these are happening in the real world, so I'm trapping for them. -- SLG
            Check.Ensure(filterValues.Count == filterValues.Distinct().Count(), $"Found repeated filterValues: {string.Join(", ", filterValues)}. This will cause problems with dictionary generation below.");
            Check.Ensure(rangeOfYears.Count == rangeOfYears.Distinct().Count(), $"Found repeated years in rangeOfYears: {string.Join(", ", rangeOfYears)}. This is a bug.");

            var fullCategoryYearDictionary = filterValues.ToDictionary(x => x, x => rangeOfYears.ToDictionary(y => y, y => 0m));
            var grantAllocationExpendituresByYear = grantAllocationExpenditures.OrderBy(sortFunction.Invoke).Where(x => rangeOfYears.Contains(x.CalendarYear)).GroupBy(x => x.CalendarYear).ToList();
            grantAllocationExpendituresByYear.ForEach(x =>
            {
                filterValues.ForEach(s =>
                {
                    var expenditures = x.Where(y => filterFunction.Invoke(y) == s).ToList();
                    fullCategoryYearDictionary[s][x.Key] = expenditures.Sum(y => y.ExpenditureAmount);
                });
            });
            return fullCategoryYearDictionary;
        }

    }
}