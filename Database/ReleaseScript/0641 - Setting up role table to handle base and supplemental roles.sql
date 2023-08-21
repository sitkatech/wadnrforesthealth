


alter table dbo.[Role]
add IsBaseRole bit
go

update dbo.[Role]
set IsBaseRole = 1
where RoleID != 10

update dbo.[Role]
set IsBaseRole = 0
where RoleID = 10

alter table dbo.[Role]
alter column IsBaseRole bit not null
go


delete from dbo.PersonRole
where PersonID = 5411 and RoleID = 2

delete from dbo.PersonRole
where PersonID = 6440 and RoleID = 2

delete from dbo.PersonRole
where PersonID = 6446 and RoleID = 7

delete from dbo.PersonRole
where PersonID = 6910 and RoleID = 2

delete from dbo.PersonRole
where PersonID = 10482 and RoleID = 7

delete from dbo.PersonRole
where PersonID = 10535 and RoleID = 7

/*

select * 
from 
dbo.PersonRole as pr
join dbo.Person as p on pr.PersonID = p.PersonID
where pr.PersonID in (10535)


select * from dbo.[Role]

*/