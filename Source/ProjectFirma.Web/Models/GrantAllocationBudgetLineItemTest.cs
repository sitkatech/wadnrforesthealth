using System.Collections.Generic;
using System.Linq;
using ApprovalUtilities.Utilities;
using NUnit.Framework;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.UnitTestCommon;

namespace ProjectFirma.Web.Models
{
    [TestFixture]
    public class GrantAllocationBudgetLineItemTest
    {
        [Test]
        public void CheckForMissingGrantAllocationBudgetLineItems()
        {
            var grantAllocations = HttpRequestStorage.DatabaseEntities.GrantAllocations;
            var costTypeIdsForLineItems = CostType.GetLineItemCostTypes().Select(x => x.CostTypeID).ToList();
            int countOfMissingBudgetLineItems = 0;

            //The key is the grantAllocationID and the value is a list of CostTypeIDs that the GrantAllocation is missing budget line items for
            Dictionary<int, List<int>> grantAllocationsAndTheCostTypesMissing = new Dictionary<int, List<int>>();

            foreach (var grantAllocation in grantAllocations)
            {
                var currentGrantAllocationBudgetLineItemCostTypeIds = grantAllocation.GrantAllocationBudgetLineItems.Select(x => x.CostTypeID).ToList();
                var idsNeeded = costTypeIdsForLineItems.Except(currentGrantAllocationBudgetLineItemCostTypeIds).ToList();
                if (idsNeeded.Any())
                {
                    grantAllocationsAndTheCostTypesMissing.Add(grantAllocation.GrantAllocationID, idsNeeded);
                    countOfMissingBudgetLineItems += idsNeeded.Count;
                }
            }

            var message = grantAllocationsAndTheCostTypesMissing.Select(x => $"grantAllocationID:{x.Key} costTypeID:{string.Join(",",x.Value)}");
            Assert.That(!grantAllocationsAndTheCostTypesMissing.Any(), $"There are GrantAllocations missing GrantAllocationBudgetLineItems. There are {countOfMissingBudgetLineItems} missing BudgetLineItems. Here they are: {string.Join(" | ",message)}");

            /*
             *  Here is the SQL needed to find and add all missing GrantAllocationBudgetLineItems:
                select *
                into
                    #tmpGrantAllocationBudgetLineItemsToCreate
                from
                (
                    select
                        ga.GrantAllocationID,
                        ct.CostTypeID
                    from
                        dbo.GrantAllocation as ga
                    cross join dbo.CostType as ct
                    left outer join dbo.GrantAllocationBudgetLineItem as bli
                        on bli.GrantAllocationID = ga.GrantAllocationID and bli.CostTypeID = ct.CostTypeID
                    where 
                        bli.GrantAllocationBudgetLineItemID IS NULL AND 
                        ct.IsValidInvoiceLineItemCostType = 1
                ) as myData

                INSERT INTO dbo.GrantAllocationBudgetLineItem(GrantAllocationID, CostTypeID, GrantAllocationBudgetLineItemAmount)
                SELECT
                        GrantAllocationID as GrantAllocationID,
                        CostTypeID as CostTypeID,
                        0 as GrantAllocationBudgetLineItemAmount
                FROM
                    #tmpGrantAllocationBudgetLineItemsToCreate
             *
             */

        }
    }
}