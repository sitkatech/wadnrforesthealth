if exists (select * from dbo.sysobjects where id = object_id('dbo.vLoaStageGrantAllocation'))
	drop view dbo.vLoaStageGrantAllocation
go

create view dbo.vLoaStageGrantAllocation
as



select y.LoaStageID, g.GrantID, x.GrantAllocationID, y.IsNortheast, y.IsSoutheast
from dbo.[Grant] g
join dbo.vSingularGrantAllocation x on x.GrantID = g.GrantID
join dbo.LoaStage y on y.GrantNumber =   RIGHT(g.GrantNumber, LEN(g.GrantNumber)-2) or y.GrantNumber = g.GrantNumber

union

select x.LoaStageID, x.GrantID,x.GrantAllocationID, x.IsNortheast, x.IsSoutheast from dbo.vLoaStageGrantAllocationAward x
join (
select x.LoaStageID 
from dbo.vLoaStageGrantAllocationAward x
group by x.LoaStageID having count(*) = 1)
y on x.LoaStageID = y.LoaStageID


union

select x.LoaStageID, min(x.GrantID), min(x.GrantAllocationID), x.IsNortheast, x.IsSoutheast
from dbo.vLoaStageGrantAllocationByProgramIndexProjectCode x
group by x.LoaStageID, x.IsNortheast, x.IsSoutheast having count(*) = 1


go

/*
select * from dbo.vLoaStageGrantAllocation

*/


