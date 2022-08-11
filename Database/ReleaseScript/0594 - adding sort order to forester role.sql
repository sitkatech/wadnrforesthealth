



alter table dbo.ForesterRole
add SortOrder int null;
go

update dbo.ForesterRole
set SortOrder = 10;

alter table dbo.ForesterRole
alter column SortOrder int not null




