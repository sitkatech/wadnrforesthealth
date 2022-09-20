delete from dbo.ProjectCustomGridType

insert into dbo.ProjectCustomGridType
	(ProjectCustomGridTypeID, ProjectCustomGridTypeName, ProjectCustomGridTypeDisplayName)
select 1, 'Default', 'Default'
union
select 2, 'Full', 'Full'
union
select 3, 'Reports', 'Reports'