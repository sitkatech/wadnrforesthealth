if exists (select * from dbo.sysobjects where id = object_id('dbo.vLoaNortheastGrantAllocationByProgramIndexProjectCode'))
	drop view dbo.vLoaNortheastGrantAllocationByProgramIndexProjectCode
go

create view dbo.vLoaNortheastGrantAllocationByProgramIndexProjectCode
as

select distinct x.LoaRawNortheastID, ga.GrantAllocationID, gm.GrantID from dbo.LoaRawNortheast x
join dbo.ProgramIndex pri on pri.ProgramIndexCode = cast(x.[Index] as varchar)
join dbo.ProjectCode pc on pc.ProjectCodeName = x.Code
join dbo.GrantAllocationProgramIndexProjectCode y on y.ProgramIndexID = pri.ProgramIndexID and y.ProjectCodeID = pc.ProjectCodeID
join dbo.GrantAllocation ga on y.GrantAllocationID = ga.GrantAllocationID
join dbo.GrantModification gm on ga.GrantModificationID = gm.GrantModificationID

go

/*
select * from dbo.vLoaNortheastGrantAllocationByProgramIndexProjectCode

*/