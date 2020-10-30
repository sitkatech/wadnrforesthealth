alter table dbo.ProjectPersonRelationshipType
add SortOrder int null;
go

--just setting sort order to a value so I can make it not null. SortOrder will be defined in the LookupTable script
update dbo.ProjectPersonRelationshipType
set SortOrder = 1
go

alter table dbo.ProjectPersonRelationshipType
alter column SortOrder int not null