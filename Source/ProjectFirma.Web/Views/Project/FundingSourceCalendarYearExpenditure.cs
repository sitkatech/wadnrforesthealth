/*-----------------------------------------------------------------------
<copyright file="FundingSourceCalendarYearExpenditure.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
    public class FundingSourceCalendarYearExpenditure
    {
        public readonly Models.FundingSource FundingSource;
        public int FundingSourceID
        {
            get { return FundingSource == null ? ModelObjectHelpers.NotYetAssignedID : FundingSource.FundingSourceID; }
        }
        public string FundingSourceName
        {
            get { return FundingSource == null ? "Unknown" : FundingSource.DisplayName; }
        }
        public string FundinSourceOrganizationName
        {
            get { return FundingSource == null ? "Unknown" : FundingSource.Organization.DisplayName; }
        }
        public HtmlString FundingSourceNameAsUrl
        {
            get { return FundingSource == null ? new HtmlString("Unknown") : FundingSource.DisplayNameAsUrl; }
        }
        public HtmlString FundingSourceOrganizationNameAsUrl
        {
            get { return FundingSource == null ? new HtmlString("Unknown") : FundingSource.Organization.GetDisplayNameAsUrl(); }
        }

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

        public FundingSourceCalendarYearExpenditure(Models.FundingSource fundingSource, Models.GrantAllocation grantAllocation, Dictionary<int, decimal?> calendarYearExpenditure, string displayCssClass)
        {
            FundingSource = fundingSource;
            GrantAllocation = grantAllocation;
            CalendarYearExpenditure = calendarYearExpenditure;
            DisplayCssClass = displayCssClass;
        }

        public FundingSourceCalendarYearExpenditure(Dictionary<int, decimal?> calendarYearExpenditure) : this(null, null, calendarYearExpenditure, null)
        {
        }

        public static List<FundingSourceCalendarYearExpenditure> CreateFromFundingSourcesAndCalendarYears(List<IFundingSourceExpenditure> fundingSourceExpenditures, List<int> calendarYears)
        {
            var distinctGrantAllocations = fundingSourceExpenditures.Select(x => x.GrantAllocation).Distinct(new HavePrimaryKeyComparer<Models.GrantAllocation>());
            var fundingSourcesCrossJoinCalendarYears =
                distinctGrantAllocations.Select(ga => new FundingSourceCalendarYearExpenditure(null, ga, calendarYears.ToDictionary<int, int, decimal?>(calendarYear => calendarYear, calendarYear => null), null))
                    .ToList();

            foreach (var projectFundingSourceExpenditure in fundingSourceExpenditures.GroupBy(x => x.GrantAllocationID))
            {
                var current = fundingSourcesCrossJoinCalendarYears.Single(x => x.GrantAllocationID == projectFundingSourceExpenditure.Key);
                foreach (var calendarYear in calendarYears)
                {
                    current.CalendarYearExpenditure[calendarYear] =
                        projectFundingSourceExpenditure.Where(fundingSourceExpenditure => fundingSourceExpenditure.CalendarYear == calendarYear).Select(x => x.MonetaryAmount).Sum();
                }
            }
            return fundingSourcesCrossJoinCalendarYears;
        }

        public static FundingSourceCalendarYearExpenditure Clone(FundingSourceCalendarYearExpenditure fundingSourceCalendarYearExpenditureToDiff, string displayCssClass)
        {
            return new FundingSourceCalendarYearExpenditure(fundingSourceCalendarYearExpenditureToDiff.FundingSource,
                                                            fundingSourceCalendarYearExpenditureToDiff.GrantAllocation,
                                                            fundingSourceCalendarYearExpenditureToDiff.CalendarYearExpenditure.ToDictionary(x => x.Key, x => x.Value),
                                                            displayCssClass);
        }
    }
}
