if exists (select * from dbo.sysobjects where id = object_id('dbo.vLoaStageGrantAllocation'))
	drop view dbo.vLoaStageGrantAllocation
go

create view dbo.vLoaStageGrantAllocation
as



select y.LoaStageID, g.GrantID, x.GrantAllocationID, y.IsNortheast, y.IsSoutheast, y.ProgramIndex, y.ProjectCode
from dbo.[Grant] g
join dbo.vSingularGrantAllocation x on x.GrantID = g.GrantID
join dbo.LoaStage y on y.GrantNumber =   RIGHT(g.GrantNumber, LEN(g.GrantNumber)-2) or y.GrantNumber = g.GrantNumber
where isnull(ltrim(rtrim(y.ProgramIndex)), '') != '99C'


union

select x.LoaStageID, min(x.GrantID), min(x.GrantAllocationID), x.IsNortheast, x.IsSoutheast, x.ProgramIndex, x.ProjectCode
from dbo.vLoaStageGrantAllocationByProgramIndexProjectCode x
where isnull(ltrim(rtrim(x.ProgramIndex)), '') != '99C'
group by x.LoaStageID, x.IsNortheast, x.IsSoutheast, x.ProgramIndex, x.ProjectCode having count(*) = 1





-- custom logic
union

select x.LoaStageID
, 65 as GrantID -- 2019-2021 DNR Forest Hazard Reduction Capital
, 313 as GrantAllocationID -- 2019-2021 DNR Forest Hazard Reduction Capital - SE Region LOA
, x.IsNortheast
, x.IsSoutheast
, x.ProgramIndex
, x.ProjectCode
from dbo.LoaStage x
where (ltrim(rtrim(x.ProgramIndex)) = '99C' and ltrim(rtrim(x.ProjectCode)) = 'WAE') or ltrim(rtrim(x.ProgramIndex)) = '99C-WAE'

union

select x.LoaStageID
, 65 as GrantID -- 2019-2021 DNR Forest Hazard Reduction Capital
, 312 as GrantAllocationID -- 2019-2021 DNR Forest Hazard Reduction Capital - NE Region LOA
, x.IsNortheast
, x.IsSoutheast
, x.ProgramIndex
, x.ProjectCode
from dbo.LoaStage x
where (ltrim(rtrim(x.ProgramIndex)) = '99C' and ltrim(rtrim(x.ProjectCode)) = 'WAD') or ltrim(rtrim(x.ProgramIndex)) = '99C-WAD'

union

select x.LoaStageID
, 66 as GrantID -- 2017-2019 DNR Forest Hazard Reduction Capital
, 324 as GrantAllocationID -- 2017-2019 DNR Forest Hazard Reduction Capital - NE/SE Region Landowner Assistance
, x.IsNortheast
, x.IsSoutheast
, x.ProgramIndex
, x.ProjectCode
from dbo.LoaStage x
where ltrim(rtrim(x.ProgramIndex)) = '99C' and (ltrim(rtrim(x.ProjectCode)) = '---' or ltrim(rtrim(x.ProjectCode)) = 'N/A' or ltrim(rtrim(x.ProjectCode)) = '' or x.ProjectCode is null)


/*
select * from dbo.vLoaStageGrantAllocation x where x.ProgramIndex like '%99c%'

*/

