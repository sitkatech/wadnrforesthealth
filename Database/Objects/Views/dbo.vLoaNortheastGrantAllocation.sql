if exists (select * from dbo.sysobjects where id = object_id('dbo.vLoaNortheastGrantAllocation'))
	drop view dbo.vLoaNortheastGrantAllocation
go

create view dbo.vLoaNortheastGrantAllocation
as



select y.LoaRawNortheastID, g.GrantID, x.GrantAllocationID
from dbo.[Grant] g
join dbo.vSingularGrantAllocation x on x.GrantID = g.GrantID
join dbo.LoaRawNortheast y on y.[Grant #] =   RIGHT(g.GrantNumber, LEN(g.GrantNumber)-2)


union

select x.LoaRawNortheastID, x.GrantID,x.GrantAllocationID from dbo.vLoaNortheastGrantAllocationAward x
join (
select x.LoaRawNortheastID 
from dbo.vLoaNortheastGrantAllocationAward x
group by x.LoaRawNortheastID having count(*) = 1)
y on x.LoaRawNortheastID = y.LoaRawNortheastID


union

select x.LoaRawNortheastID, x.GrantID,x.GrantAllocationID from dbo.vLoaNortheastGrantAllocationByProgramIndexProjectCode x
join (
select x.LoaRawNortheastID 
from dbo.vLoaNortheastGrantAllocationByProgramIndexProjectCode x
group by x.LoaRawNortheastID having count(*) = 1)
y on x.LoaRawNortheastID = y.LoaRawNortheastID


go

/*
select * from dbo.vLoaNortheastGrantAllocation

*/