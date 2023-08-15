


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







/*


select * from dbo.[Role]

*/