/*-----------------------------------------------------------------------
<copyright file="ProjectModelExtensions.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System;
using System.Linq;
using System.Web;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class FundSourceModelExtensions
    {

        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<FundSourceController>.BuildUrlFromExpression(t => t.DeleteFundSource(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this FundSource fundSource)
        {
            return DeleteUrlTemplate.ParameterReplace(fundSource.FundSourceID);
        }


        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<FundSourceController>.BuildUrlFromExpression(t => t.FundSourceDetail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this FundSource fundSource)
        {
            return DetailUrlTemplate.ParameterReplace(fundSource.FundSourceID);
        }

        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<FundSourceController>.BuildUrlFromExpression(t => t.Edit(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this FundSource fundSource)
        {
            return EditUrlTemplate.ParameterReplace(fundSource.FundSourceID);
        }

        public static readonly UrlTemplate<int> DuplicateUrlTemplate = new UrlTemplate<int>(SitkaRoute<FundSourceController>.BuildUrlFromExpression(t => t.Duplicate(UrlTemplate.Parameter1Int)));
        public static string GetDuplicateUrl(this FundSource fundSource)
        {
            return DuplicateUrlTemplate.ParameterReplace(fundSource.FundSourceID);
        }

        public static readonly UrlTemplate<int> NewNoteUrlTemplate = new UrlTemplate<int>(SitkaRoute<FundSourceController>.BuildUrlFromExpression(t => t.NewFundSourceNote(UrlTemplate.Parameter1Int)));
        public static string GetNewNoteUrl(this FundSource fundSource)
        {
            return NewNoteUrlTemplate.ParameterReplace(fundSource.FundSourceID);
        }

        public static HtmlString GetFundSourceNumberAsUrl(this FundSource fundSource)
        {
            return fundSource != null ? UrlTemplate.MakeHrefString(fundSource.GetDetailUrl(), fundSource.FundSourceNumber) : new HtmlString(null);
        }

        public static Money GetCurrentBalanceOfFundSourceBasedOnAllFundSourceAllocationExpenditures(this FundSource fundSource)
        {
            var fundSourceAllocations = fundSource.FundSourceAllocations.ToList();
            var allBudgetLineItemVsActualItems = fundSourceAllocations.Select(ga => ga.GetTotalBudgetVsActualLineItem());
            var currentBalanceOfAllFundSourceAllocations = allBudgetLineItemVsActualItems.Select(blai => blai.BudgetMinusExpendituresFromDatamart).ToList();
            Money total = 0;
            foreach (var fundSourceAllocationTotal in currentBalanceOfAllFundSourceAllocations)
            {
                total += fundSourceAllocationTotal;
            }

            return total;
        }
    }
}
