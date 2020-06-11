/*

select GrantAllocationID, CostTypeID, count(*) as totalCount, sum(GrantAllocationBudgetLineItemAmount) as newAmount from dbo.GrantAllocationBudgetLineItem
group by GrantAllocationID, CostTypeID
having count(*) > 1
order by totalCount desc


select * from dbo.GrantAllocationBudgetLineItem

select * from dbo.CostType

where GrantAllocationID = 188


*/

IF OBJECT_ID('tempdb..#lineItemsToMerge') IS NOT NULL
    DROP TABLE #lineItemsToMerge


SELECT *
INTO #lineItemsToMerge
FROM
(
    select GrantAllocationID, CostTypeID, count(*) as totalCount, sum(GrantAllocationBudgetLineItemAmount) as newAmount from dbo.GrantAllocationBudgetLineItem
    group by GrantAllocationID, CostTypeID
    having count(*) > 1
    --order by totalCount desc
) as x


select * from #lineItemsToMerge


delete from dbo.GrantAllocationBudgetLineItem
where GrantAllocationBudgetLineItemID in (select GrantAllocationBudgetLineItemID 
                                            from dbo.GrantAllocationBudgetLineItem as bli 
                                            join #lineItemsToMerge as tmp on bli.GrantAllocationID = tmp.GrantAllocationID and bli.CostTypeID = tmp.CostTypeID )

insert into dbo.GrantAllocationBudgetLineItem (GrantAllocationID, CostTypeID, GrantAllocationBudgetLineItemAmount, GrantAllocationBudgetLineItemNote)
select
    GrantAllocationID,
    CostTypeID,
    newAmount as GrantAllocationBudgetLineItemAmount,
    null as GrantAllocationBudgetLineItemNote
from #lineItemsToMerge





ALTER TABLE dbo.GrantAllocationBudgetLineItem
  ADD CONSTRAINT AK_GrantAllocationBudgetLineItem_GrantAllocationID_CostTypeID UNIQUE(GrantAllocationID, CostTypeID);

