update dbo.GrantAllocation
set LikelyToUse = null

update dbo.GrantAllocation
set LikelyToUse = 1 where GrantAllocationID in (select distinct GrantAllocationID from GrantAllocationLikelyPerson)


