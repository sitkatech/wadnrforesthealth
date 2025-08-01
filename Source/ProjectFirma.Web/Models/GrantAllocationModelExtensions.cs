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
using System.Collections.Generic;
using System.Linq;
using ProjectFirma.Web.Controllers;
using LtInfo.Common;
using LtInfo.Common.Models;
using Microsoft.Ajax.Utilities;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Views.GrantAllocation;
using LtInfo.Common.AgGridWrappers;

namespace ProjectFirma.Web.Models
{
    public static class GrantAllocationModelExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(t => t.DeleteGrantAllocation(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this FundSourceAllocation fundSourceAllocation)
        {
            return DeleteUrlTemplate.ParameterReplace(fundSourceAllocation.GrantAllocationID);
        }

        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(t => t.GrantAllocationDetail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this FundSourceAllocation fundSourceAllocation)
        {
            return DetailUrlTemplate.ParameterReplace(fundSourceAllocation.GrantAllocationID);
        }


        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(t => t.Edit(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this FundSourceAllocation fundSourceAllocation)
        {
            return EditUrlTemplate.ParameterReplace(fundSourceAllocation.GrantAllocationID);
        }

        public static readonly UrlTemplate<int> DuplicateUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(t => t.Duplicate(UrlTemplate.Parameter1Int)));
        public static string GetDuplicateUrl(this FundSourceAllocation fundSourceAllocation)
        {
            return DuplicateUrlTemplate.ParameterReplace(fundSourceAllocation.GrantAllocationID);
        }

        public static readonly UrlTemplate<int> NewNoteUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(t => t.NewGrantAllocationNote(UrlTemplate.Parameter1Int)));
        public static string GetNewNoteUrl(this FundSourceAllocation fundSourceAllocation)
        {
            return NewNoteUrlTemplate.ParameterReplace(fundSourceAllocation.GrantAllocationID);
        }

        public static List<BudgetVsActualLineItem> GetAllBudgetVsActualLineItemsByCostType(this FundSourceAllocation fundSourceAllocation)
        {
            var budgetVsActualsLineItemList = new List<BudgetVsActualLineItem>();

            foreach (var costType in CostType.GetLineItemCostTypes())
            {
                var budget = fundSourceAllocation.GrantAllocationBudgetLineItems.Where(bli => bli.CostTypeID == costType.CostTypeID).Select(bli => bli.GrantAllocationBudgetLineItemAmount).Sum();

                var expendituresFromDatamart = fundSourceAllocation.GrantAllocationExpenditures
                    .Where(gae => gae.CostTypeID == costType.CostTypeID).Select(gae => gae.ExpenditureAmount).Sum();
                
                var budgetVsActualLineItem = new BudgetVsActualLineItem(costType, budget, expendituresFromDatamart);
                budgetVsActualsLineItemList.Add(budgetVsActualLineItem);
            }

            return budgetVsActualsLineItemList;
        }

        public static BudgetVsActualLineItem GetTotalBudgetVsActualLineItem(this FundSourceAllocation fundSourceAllocation)
        {
            var budget = fundSourceAllocation.GrantAllocationBudgetLineItems.Select(bli => bli.GrantAllocationBudgetLineItemAmount).Sum();
            var expendituresFromDatamart = fundSourceAllocation.GrantAllocationExpenditures.Select(gae => gae.ExpenditureAmount).Sum();

            var budgetVsActualLineItem = new BudgetVsActualLineItem(budget, expendituresFromDatamart);
            return budgetVsActualLineItem;
        }

        public static string GetAssociatedProgramIndexProjectCodePairsCommaDelimited(this FundSourceAllocation fundSourceAllocation)
        {
            List<string> programIndexProjectCodePairs = new List<string>();

            foreach (var pair in fundSourceAllocation.GrantAllocationProgramIndexProjectCodes)
            {
                programIndexProjectCodePairs.Add(pair.ProgramIndexProjectCodeDisplayString);
            }

            return string.Join(", ", programIndexProjectCodePairs);
        }

        public static string GetAssociatedProgramIndexProjectCodePairsForAgGrid(this FundSourceAllocation fundSourceAllocation)
        {
            List<HtmlLinkObject> programIndexProjectCodePairs = new List<HtmlLinkObject>();

            foreach (var pair in fundSourceAllocation.GrantAllocationProgramIndexProjectCodes)
            {
                var pairText = new HtmlLinkObject(pair.ProgramIndexProjectCodeDisplayString, string.Empty);
                programIndexProjectCodePairs.Add(pairText);
            }

            return programIndexProjectCodePairs.ToJsonArrayForAgGrid();
        }


        public static List<GrantAllocationBudgetLineItem> CreateAllGrantAllocationBudgetLineItemsByCostType(this FundSourceAllocation fundSourceAllocation)
        {
            var grantAllocationBudgetLineItems = new List<GrantAllocationBudgetLineItem>();
            var shouldSaveChanges = false;
            foreach (var costType in CostType.GetLineItemCostTypes())
            {
                var lineItemByCostType = fundSourceAllocation.GrantAllocationBudgetLineItems.SingleOrDefault(bli => bli.CostTypeID == costType.CostTypeID);
                if (lineItemByCostType == null)
                {
                    var tempLineItem = new GrantAllocationBudgetLineItem(fundSourceAllocation.GrantAllocationID, costType.CostTypeID, 0);
                    lineItemByCostType = HttpRequestStorage.DatabaseEntities.GrantAllocationBudgetLineItems.Add(tempLineItem);

                    shouldSaveChanges = true;
                }
                grantAllocationBudgetLineItems.Add(lineItemByCostType);
            }

            if (shouldSaveChanges)
            {
                HttpRequestStorage.DatabaseEntities.SaveChanges();
            }

            return grantAllocationBudgetLineItems;
        }



    }
}
