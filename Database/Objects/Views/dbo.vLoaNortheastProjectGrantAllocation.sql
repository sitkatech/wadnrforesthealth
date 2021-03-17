if exists (select * from dbo.sysobjects where id = object_id('dbo.vLoaNortheastProjectGrantAllocation'))
	drop view dbo.vLoaNortheastProjectGrantAllocation
go

create view dbo.vLoaNortheastProjectGrantAllocation
as

select p.ProjectID
, lan.Match
, lan.Pay 
, lan.Status
, x.GrantAllocationID
, lan.[Letter Date] as LetterDate
, lan.[Project Expiration Date] as ProjectExpirationDate
from dbo.vLoaNortheastGrantAllocation x
join dbo.LoaRawNortheast lan on x.LoaRawNortheastID = lan.LoaRawNortheastID
join dbo.Project p on p.ProjectGisIdentifier = lan.[Project ID]
where p.ProgramID = 3



union

select 
p.ProjectID
, lan.Match
, lan.Pay
, lan.Status
, null
, lan.[Letter Date] as LetterDate
, lan.[Project Expiration Date] as ProjectExpirationDate
 from dbo.Project p
join dbo.LoaRawNortheast lan on lan.[Project ID] = p.ProjectGisIdentifier
left join dbo.vLoaNortheastGrantAllocation x on x.LoaRawNortheastID = lan.LoaRawNortheastID
where p.ProgramID = 3 and x.LoaRawNortheastID is null

go

/*
select * from dbo.vLoaNortheastProjectGrantAllocation

*/