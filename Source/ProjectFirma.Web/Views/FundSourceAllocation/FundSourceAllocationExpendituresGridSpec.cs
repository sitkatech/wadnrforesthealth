/*-----------------------------------------------------------------------
<copyright file="FundSourceAllocationExpendituresGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.FundSourceAllocation
{
    public class FundSourceAllocationExpendituresGridSpec : GridSpec<FundSourceAllocationExpenditure>
    {
        public FundSourceAllocationExpendituresGridSpec()
        {
            Add(Models.FieldDefinition.CostType.ToGridHeaderString(), gae => gae.CostType?.CostTypeDisplayName, 150, AgGridColumnFilterType.SelectFilterStrict);
            Add( "Biennium", gae => gae.Biennium.ToString() , 90, AgGridColumnFilterType.SelectFilterStrict);
            Add("Fiscal Month", gae => gae.FiscalMonth.ToString(), 100, AgGridColumnFilterType.SelectFilterStrict);
            Add("Calendar Year", gae => gae.CalendarYear.ToString(), 100, AgGridColumnFilterType.SelectFilterStrict);
            Add("Calendar Month", gae => gae.CalendarMonth.ToString(), 100, AgGridColumnFilterType.SelectFilterStrict);
            Add("Expenditure", gae => gae.ExpenditureAmount, 100, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
        }
    }
}
