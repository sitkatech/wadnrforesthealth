if exists (select * from dbo.sysobjects where id = object_id('dbo.vLoaStageProjectGrantAllocation'))
	drop view dbo.vLoaStageProjectGrantAllocation
go

create view dbo.vLoaStageProjectGrantAllocation
as

select p.ProjectID
, lan.MatchAmount
, lan.PayAmount 
, lan.ProjectStatus
, x.GrantAllocationID
, lan.LetterDate as LetterDate
, lan.ProjectExpirationDate as ProjectExpirationDate
from dbo.vLoaStageGrantAllocation x
join dbo.LoaStage lan on x.LoaStageID = lan.LoaStageID
join dbo.Project p on p.ProjectGisIdentifier = lan.ProjectIdentifier
where p.ProgramID = 3



union

select 
p.ProjectID
, lan.MatchAmount
, lan.PayAmount
, lan.ProjectStatus
, null
, lan.LetterDate as LetterDate
, lan.ProjectExpirationDate as ProjectExpirationDate
 from dbo.Project p
join dbo.LoaStage lan on lan.ProjectIdentifier = p.ProjectGisIdentifier
left join dbo.vLoaStageGrantAllocation x on x.LoaStageID = lan.LoaStageID
where p.ProgramID = 3 and x.LoaStageID is null

go

/*
select * from dbo.vLoaStageProjectGrantAllocation

*/