ALTER TABLE dbo.GrantAllocation
ADD LikelyToUse bit null;

go

update dbo.GrantAllocation
set LikelyToUse = 0

update dbo.GrantAllocation
set LikelyToUse = 1 where GrantAllocationID in (select distinct GrantAllocationID from GrantAllocationLikelyPerson)


