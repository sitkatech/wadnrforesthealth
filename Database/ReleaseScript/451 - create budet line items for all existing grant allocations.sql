

/*
select 
    * 
from 
    dbo.GrantAllocation as ga
left outer join dbo.GrantAllocationBudgetLineItem as bli
    on ga.GrantAllocationID = bli.GrantAllocationID




--All GrantAllocationBudgetLineItems that exist
select 
    ga.GrantAllocationID,
    bli.GrantAllocationBudgetLineItemID,
    ct.CostTypeID
from 
    dbo.GrantAllocationBudgetLineItem as bli
join dbo.GrantAllocation as ga
    on ga.GrantAllocationID = bli.GrantAllocationID
join dbo.CostType as ct
    on bli.CostTypeID = ct.CostTypeID




select 
    ga.GrantAllocationID,
    bli.GrantAllocationBudgetLineItemID,
    ct.CostTypeID
from 
    dbo.GrantAllocationBudgetLineItem as bli
full outer join dbo.GrantAllocation as ga
    on ga.GrantAllocationID = bli.GrantAllocationID
full outer join dbo.CostType as ct
    on bli.CostTypeID = ct.CostTypeID
where 
    ct.IsValidInvoiceLineItemCostType = 1


*/


select
    *
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


