if exists (select * from dbo.sysobjects where id = object_id('dbo.vLoaStageGrantAllocationByProgramIndexProjectCode'))
	drop view dbo.vLoaStageGrantAllocationByProgramIndexProjectCode
go

create view dbo.vLoaStageGrantAllocationByProgramIndexProjectCode
as


select x.LoaStageID, min(x.GrantAllocationID) as GrantAllocationID, min(x.GrantID) as GrantID, x.IsNortheast, x.IsSoutheast, x.ProgramIndex, x.ProjectCode from (

select distinct x.LoaStageID, ga.GrantAllocationID, ga.GrantID , x.IsNortheast, x.IsSoutheast, x.ProgramIndex, x.ProjectCode
from dbo.LoaStage x
join dbo.ProgramIndex pri on pri.ProgramIndexCode = cast(x.ProgramIndex as varchar)
join dbo.ProjectCode pc on pc.ProjectCodeName = x.ProjectCode
join dbo.GrantAllocationProgramIndexProjectCode y on y.ProgramIndexID = pri.ProgramIndexID and y.ProjectCodeID = pc.ProjectCodeID
join dbo.GrantAllocation ga on y.GrantAllocationID = ga.GrantAllocationID
) x 
where isnull(ltrim(rtrim(x.ProgramIndex)), '') != '99C'
group by x.LoaStageID, x.IsNortheast, x.IsSoutheast, x.ProgramIndex, x.ProjectCode having count(*) = 1

go

/*
select * from dbo.vLoaStageGrantAllocationByProgramIndexProjectCode

*/