if exists (select * from dbo.sysobjects where id = object_id('dbo.vLoaNortheastGrantAllocationAward'))
	drop view dbo.vLoaNortheastGrantAllocationAward
go

create view dbo.vLoaNortheastGrantAllocationAward
as

select distinct x.LoaRawNortheastID, gaa.GrantAllocationAwardID, ga.GrantAllocationID, g.GrantID from dbo.LoaRawNortheast x
join dbo.FocusArea fa on fa.FocusAreaName like '%'+ x.[Grant] + '%'
join dbo.[Grant] g on x.[Grant #] =   RIGHT(g.GrantNumber, LEN(g.GrantNumber)-2)
join dbo.GrantModification gm on gm.GrantID = g.GrantID
join dbo.GrantAllocation ga on gm.GrantModificationID = ga.GrantModificationID
join dbo.GrantAllocationAward gaa on gaa.FocusAreaID = fa.FocusAreaID and gaa.GrantAllocationID = ga.GrantAllocationID


go

/*
select * from dbo.vLoaNortheastGrantAllocationAward

*/