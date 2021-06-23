if exists (select * from dbo.sysobjects where id = object_id('dbo.vLoaStageGrantAllocationByProgramIndexProjectCode'))
	drop view dbo.vLoaStageGrantAllocationByProgramIndexProjectCode
go

create view dbo.vLoaStageGrantAllocationByProgramIndexProjectCode
as


select x.LoaStageID, min(x.GrantAllocationID) as GrantAllocationID, min(x.GrantID) as GrantID, x.IsNortheast, x.IsSoutheast from (

select distinct x.LoaStageID, ga.GrantAllocationID, gm.GrantID , x.IsNortheast, x.IsSoutheast
from dbo.LoaStage x
join dbo.ProgramIndex pri on pri.ProgramIndexCode = cast(x.ProgramIndex as varchar)
join dbo.ProjectCode pc on pc.ProjectCodeName = x.ProjectCode
join dbo.GrantAllocationProgramIndexProjectCode y on y.ProgramIndexID = pri.ProgramIndexID and y.ProjectCodeID = pc.ProjectCodeID
join dbo.GrantAllocation ga on y.GrantAllocationID = ga.GrantAllocationID
join dbo.GrantModification gm on ga.GrantModificationID = gm.GrantModificationID) x 
group by x.LoaStageID, x.IsNortheast, x.IsSoutheast having count(*) = 1

go

/*
select * from dbo.vLoaStageGrantAllocationByProgramIndexProjectCode

*/