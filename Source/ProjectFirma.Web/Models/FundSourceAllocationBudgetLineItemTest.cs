using System.Collections.Generic;
using System.Linq;
using ApprovalUtilities.Utilities;
using NUnit.Framework;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.UnitTestCommon;

namespace ProjectFirma.Web.Models
{
    [TestFixture]
    public class FundSourceAllocationBudgetLineItemTest
    {
        [Test]
        public void CheckForMissingFundSourceAllocationBudgetLineItems()
        {
            var fundSourceAllocations = HttpRequestStorage.DatabaseEntities.FundSourceAllocations;
            var costTypeIdsForLineItems = CostType.GetLineItemCostTypes().Select(x => x.CostTypeID).ToList();
            int countOfMissingBudgetLineItems = 0;

            //The key is the fundSourceAllocationID and the value is a list of CostTypeIDs that the FundSourceAllocation is missing budget line items for
            Dictionary<int, List<int>> fundSourceAllocationsAndTheCostTypesMissing = new Dictionary<int, List<int>>();

            foreach (var fundSourceAllocation in fundSourceAllocations)
            {
                var currentFundSourceAllocationBudgetLineItemCostTypeIds = fundSourceAllocation.FundSourceAllocationBudgetLineItems.Select(x => x.CostTypeID).ToList();
                var idsNeeded = costTypeIdsForLineItems.Except(currentFundSourceAllocationBudgetLineItemCostTypeIds).ToList();
                if (idsNeeded.Any())
                {
                    fundSourceAllocationsAndTheCostTypesMissing.Add(fundSourceAllocation.FundSourceAllocationID, idsNeeded);
                    countOfMissingBudgetLineItems += idsNeeded.Count;
                }
            }

            var message = fundSourceAllocationsAndTheCostTypesMissing.Select(x => $"fundSourceAllocationID:{x.Key} costTypeID:{string.Join(",",x.Value)}");
            Assert.That(!fundSourceAllocationsAndTheCostTypesMissing.Any(), $"There are FundSourceAllocations missing FundSourceAllocationBudgetLineItems. There are {countOfMissingBudgetLineItems} missing BudgetLineItems. Here they are: {string.Join(" | ",message)}");

            /*
             *  Here is the SQL needed to find and add all missing FundSourceAllocationBudgetLineItems:
                select *
                into
                    #tmpFundSourceAllocationBudgetLineItemsToCreate
                from
                (
                    select
                        ga.FundSourceAllocationID,
                        ct.CostTypeID
                    from
                        dbo.FundSourceAllocation as ga
                    cross join dbo.CostType as ct
                    left outer join dbo.FundSourceAllocationBudgetLineItem as bli
                        on bli.FundSourceAllocationID = ga.FundSourceAllocationID and bli.CostTypeID = ct.CostTypeID
                    where 
                        bli.FundSourceAllocationBudgetLineItemID IS NULL AND 
                        ct.IsValidInvoiceLineItemCostType = 1
                ) as myData

                INSERT INTO dbo.FundSourceAllocationBudgetLineItem(FundSourceAllocationID, CostTypeID, FundSourceAllocationBudgetLineItemAmount)
                SELECT
                        FundSourceAllocationID as FundSourceAllocationID,
                        CostTypeID as CostTypeID,
                        0 as FundSourceAllocationBudgetLineItemAmount
                FROM
                    #tmpFundSourceAllocationBudgetLineItemsToCreate
             *
             */

        }
    }
}