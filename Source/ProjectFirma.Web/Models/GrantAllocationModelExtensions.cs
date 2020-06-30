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

        public static List<BudgetVsActualLineItem> GetAllBudgetVsActualLineItemsByCostType(this GrantAllocation grantAllocation)
        {
            var budgetVsActualsLineItemList = new List<BudgetVsActualLineItem>();

            foreach (var costType in CostType.GetLineItemCostTypes())
            {
                var budget = grantAllocation.GrantAllocationBudgetLineItems.Where(bli => bli.CostTypeID == costType.CostTypeID).Select(bli => bli.GrantAllocationBudgetLineItemAmount).Sum();

                var expendituresFromDatamart = grantAllocation.GrantAllocationExpenditures
                    .Where(gae => gae.CostTypeID == costType.CostTypeID).Select(gae => gae.ExpenditureAmount).Sum();

                var invoicedToDate = grantAllocation.GrantAllocationAwards.Sum(grantAllocationAward => grantAllocationAward.GetInvoicedToDateByCostType(costType));
                

                var budgetVsActualLineItem = new BudgetVsActualLineItem(costType, budget, expendituresFromDatamart, invoicedToDate);
                budgetVsActualsLineItemList.Add(budgetVsActualLineItem);
            }

            return budgetVsActualsLineItemList;
        }

        public static BudgetVsActualLineItem GetTotalBudgetVsActualLineItem(this GrantAllocation grantAllocation)
        {
            var budget = grantAllocation.GrantAllocationBudgetLineItems.Select(bli => bli.GrantAllocationBudgetLineItemAmount).Sum();
            var expendituresFromDatamart = grantAllocation.GrantAllocationExpenditures.Select(gae => gae.ExpenditureAmount).Sum();
            var invoicedToDate = grantAllocation.GrantAllocationAwards.Sum(grantAllocationAward => grantAllocationAward.GetTotalInvoicedToDate());

            var budgetVsActualLineItem = new BudgetVsActualLineItem(budget, expendituresFromDatamart, invoicedToDate);
            return budgetVsActualLineItem;
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

        public static List<GrantAllocationBudgetLineItem> GetOrCreateBudgetLineItemsForEachCostType(this GrantAllocation grantAllocation)
        {
            var grantAllocationBudgetLineItems = new List<GrantAllocationBudgetLineItem>();
            var shouldSaveChanges = false;
            foreach (var costType in CostType.GetLineItemCostTypes())
            {
                var lineItemByCostType = grantAllocation.GrantAllocationBudgetLineItems.SingleOrDefault(bli => bli.CostTypeID == costType.CostTypeID);
                if (lineItemByCostType == null)
                {
                    var tempLineItem = new GrantAllocationBudgetLineItem(grantAllocation.GrantAllocationID, costType.CostTypeID, 0);
                    lineItemByCostType = HttpRequestStorage.DatabaseEntities.GrantAllocationBudgetLineItems.Add(tempLineItem);

                    shouldSaveChanges = true;
                }
                grantAllocationBudgetLineItems.Add(lineItemByCostType);
            }

            if (shouldSaveChanges)
            {
                // This is a rare spot where we can potentially be calling a write operation in an anonymous user context. This is arguably not a good thing,
                // but as I type this, https://wadnrforesthealth.localhost.projectfirma.com/GrantAllocation/GrantAllocationDetail/134 will crash if called when
                // not logged in, at least until that URL is visited successfully at least once.
                //
                // I believe this is because the AuditLog code below here in DatabaseEntities.SaveChangesImpl tries to write an anonymous Person object to the DB, and that
                // anonymous person fails validation since it's half-baked (no valid FirstName or LastName).
                //
                // So, instead, IF we are anonymous here, we attribute THESE changes only to the System User. This seemed the least-worst option without revisiting
                // the write-when-anonymous issue. -- SLG 6/29/2020
                var personToAttributeWriteTo = HttpRequestStorage.Person;
                if (personToAttributeWriteTo.IsAnonymousOrUnassigned)
                {
                    personToAttributeWriteTo = HttpRequestStorage.DatabaseEntities.People.GetSystemUser();
                }

                HttpRequestStorage.DatabaseEntities.SaveChanges(personToAttributeWriteTo);
            }

            return grantAllocationBudgetLineItems;
        }

    }
}
