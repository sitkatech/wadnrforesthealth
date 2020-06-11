/*

select distinct GrantAllocationID, CostTypeID, count(*) as total from dbo.GrantAllocationBudgetLineItem
group by GrantAllocationID, CostTypeID
order by total desc

select * from dbo.GrantAllocationBudgetLineItem

where GrantAllocationID = 188


*/
--ALTER TABLE dbo.GrantAllocationBudgetLineItem
--  ADD CONSTRAINT AK_GrantAllocationBudgetLineItem_GrantAllocationID_CostTypeID UNIQUE(GrantAllocationID, CostTypeID);

