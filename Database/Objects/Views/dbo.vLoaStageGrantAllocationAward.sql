if exists (select * from dbo.sysobjects where id = object_id('dbo.vLoaStageGrantAllocationAward'))
	drop view dbo.vLoaStageGrantAllocationAward
go

create view dbo.vLoaStageGrantAllocationAward
as

select distinct x.LoaStageID, gaa.GrantAllocationAwardID, ga.GrantAllocationID, g.GrantID , x.IsNortheast, x.IsSoutheast
from dbo.LoaStage x
join dbo.FocusArea fa on (fa.FocusAreaName like '%'+ x.FocusAreaName + '%') or (fa.FocusAreaName like '%' +LEFT(x.FocusAreaName, LEN(x.FocusAreaName)-5) + '%' and x.IsSoutheast = 1)
join dbo.[Grant] g on x.GrantNumber = RIGHT(g.GrantNumber, LEN(g.GrantNumber)-2) or x.GrantNumber = g.GrantNumber
join dbo.GrantModification gm on gm.GrantID = g.GrantID
join dbo.GrantAllocation ga on gm.GrantModificationID = ga.GrantModificationID
join dbo.GrantAllocationAward gaa on gaa.FocusAreaID = fa.FocusAreaID and gaa.GrantAllocationID = ga.GrantAllocationID


go

/*
select * from dbo.vLoaStageGrantAllocationAward

*/

