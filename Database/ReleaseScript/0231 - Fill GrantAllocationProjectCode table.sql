-- Add data to GrantAllocationProjectCode
insert into dbo.GrantAllocationProjectCode(GrantAllocationID, ProjectCodeID)
select x1.GrantAllocationID, pc.ProjectCodeID
from
(
	select x.GrantAllocationID, x.[Project Codes], x.CleanProjectCode
	from
	(
		select GA.GrantAllocationID, TCG.[Project Codes], ltrim(rtrim(x.splitdata)) as CleanProjectCode
		from GrantAllocation ga
		join [Grant] g on ga.GrantID = g.GrantID
		join dbo.tmpChildrenGrantsInGrantsTab tcg on ga.ProjectName = tcg.Title and g.GrantNumber = tcg.[Grant #] and ga.AllocationAmount =isnull(cast(REPLACE(tcg.[Funds Awarded], '"','') as money),0)
		cross apply dbo.fnSplitString(replace(TCG.[Project Codes], '"',''),',') x
	) x
	where x.CleanProjectCode not in ('', '---')
) x1
join dbo.ProjectCode pc on x1.CleanProjectCode = pc.ProjectCodeAbbrev
