if exists (select * from dbo.sysobjects where id = object_id('dbo.vLoaStageGrantAllocationByProgramIndexProjectCode'))
	drop view dbo.vLoaStageGrantAllocationByProgramIndexProjectCode
go

create view dbo.vLoaStageGrantAllocationByProgramIndexProjectCode
as

select distinct x.LoaStageID, ga.GrantAllocationID, gm.GrantID 
from dbo.LoaStage x
join dbo.ProgramIndex pri on pri.ProgramIndexCode = cast(x.ProgramIndex as varchar)
join dbo.ProjectCode pc on pc.ProjectCodeName = x.ProjectCode
join dbo.GrantAllocationProgramIndexProjectCode y on y.ProgramIndexID = pri.ProgramIndexID and y.ProjectCodeID = pc.ProjectCodeID
join dbo.GrantAllocation ga on y.GrantAllocationID = ga.GrantAllocationID
join dbo.GrantModification gm on ga.GrantModificationID = gm.GrantModificationID

go

/*
select * from dbo.vLoaStageGrantAllocationByProgramIndexProjectCode

*/