--begin tran

-- Cleaning up some messy $ value fields.
-- This is a good idea to to anyway, but we need it here specficially to do the matchup to previously imported GrantAllocations below.
update tmpChildrenGrantsInGrantsTab
set [Funds Awarded] = isnull(cast(REPLACE([Funds Awarded], '"','') as money),0) 
GO

update tmpChildrenGrantsInGrantsTab
set [Matching Funds] = isnull(cast(REPLACE([Matching Funds], '"','') as money),0) 
GO

update tmpChildrenGrantsInGrantsTab
set [Grant Total] = isnull(cast(REPLACE([Grant Total], '"','') as money),0) 
GO

update tmpChildrenGrantsInGrantsTab
set [Landowner Match] = isnull(cast(REPLACE([Landowner Match], '"','') as money),0) 
GO


-- Adding GrantAllocationID to tmp table to make returning to the well easier.

alter table dbo.tmpChildrenGrantsInGrantsTab
add GrantAllocationID int null
GO

ALTER TABLE dbo.tmpChildrenGrantsInGrantsTab with check ADD  CONSTRAINT FK_tmpChildrenGrantsInGrantsTab_GrantAllocation_GrantAllocationID_GrantAllocationID FOREIGN KEY(GrantAllocationID)
REFERENCES [dbo].GrantAllocation (GrantAllocationID)

GO

update tmpChildrenGrantsInGrantsTab
set GrantAllocationID = (
                         select ga.GrantAllocationID from dbo.GrantAllocation as ga 
						 where 
					     ga.ProjectName = Title 
					       and 
						 (select g.GrantID from [Grant] as g where g.GrantNumber = [Grant #]) = GrantID
						   and
					     ga.AllocationAmount =  case when [Funds Awarded] != '' then PARSE([Funds Awarded] AS MONEY) else null end
						)
GO

--rollback tran

