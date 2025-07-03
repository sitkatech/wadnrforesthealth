if exists (select * from dbo.sysobjects where id = object_id('dbo.vSingularGrantAllocation'))
	drop view dbo.vSingularGrantAllocation
go

create view dbo.vSingularGrantAllocation
as



select g.GrantID, ga.GrantAllocationID from dbo.[Grant] g
join dbo.GrantAllocation ga on ga.GrantID = g.GrantID
join (
    select g.GrantID from dbo.[Grant] g
    join dbo.GrantAllocation ga on ga.GrantID = g.GrantID
    group by g.GrantID having count(*) = 1)
x on x.GrantID = g.GrantID

go

/*
select * from dbo.vSingularGrantAllocation

*/