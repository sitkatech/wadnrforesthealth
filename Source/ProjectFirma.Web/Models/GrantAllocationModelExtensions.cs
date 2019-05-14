/*-----------------------------------------------------------------------
<copyright file="ProjectModelExtensions.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ProjectFirma.Web.Controllers;
using LtInfo.Common;
using LtInfo.Common.Models;
using Microsoft.Ajax.Utilities;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Views.GrantAllocation;

namespace ProjectFirma.Web.Models
{
    public static class GrantAllocationModelExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(t => t.DeleteGrantAllocation(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this GrantAllocation grantAllocation)
        {
            return DeleteUrlTemplate.ParameterReplace(grantAllocation.GrantAllocationID);
        }

        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(t => t.GrantAllocationDetail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this GrantAllocation grantAllocation)
        {
            return DetailUrlTemplate.ParameterReplace(grantAllocation.GrantAllocationID);
        }


        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(t => t.Edit(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this GrantAllocation grantAllocation)
        {
            return EditUrlTemplate.ParameterReplace(grantAllocation.GrantAllocationID);
        }


        public static readonly UrlTemplate<int> NewNoteUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(t => t.NewGrantAllocationNote(UrlTemplate.Parameter1Int)));
        public static string GetNewNoteUrl(this GrantAllocation grantAllocation)
        {
            return NewNoteUrlTemplate.ParameterReplace(grantAllocation.GrantAllocationID);
        }

        public static List<BudgetVsActualLineItem> GetAllBudgetVsActualLineItems(this GrantAllocation grantAllocation)
        {
            var budgetVsActualsLineItemList = new List<BudgetVsActualLineItem>();

            foreach (var costType in CostType.All.Where(ct => ct.IsValidInvoiceLineItemCostType))
            {
                var budget = grantAllocation.GrantAllocationBudgetLineItems.Where(bli => bli.CostTypeID == costType.CostTypeID).Select(bli => bli.GrantAllocationBudgetLineItemAmount).Sum();
                //TODO: get expenditures from datamart for this grant allocation of this cost type
                var expendituresFromDatamart = 0m;

                var invoicedToDate = grantAllocation.InvoiceLineItems.Where(ili => ili.CostTypeID == costType.CostTypeID).Select(ili => ili.InvoiceLineItemAmount).Sum();
                

                var budgetVsActualLineItem = new BudgetVsActualLineItem(costType, budget, expendituresFromDatamart, invoicedToDate);
                budgetVsActualsLineItemList.Add(budgetVsActualLineItem);
            }

            return budgetVsActualsLineItemList;
        }

        public static string GetAssociatedProgramIndexProjectCodePairsCommaDelimited(this GrantAllocation grantAllocation)
        {
            List<string> programIndexProjectCodePairs = new List<string>();

            foreach (var pair in grantAllocation.GrantAllocationProgramIndexProjectCodes)
            {
                programIndexProjectCodePairs.Add(pair.ProgramIndexProjectCodeDisplayString);
            }

            return string.Join(", ", programIndexProjectCodePairs);
        }

    }
}
