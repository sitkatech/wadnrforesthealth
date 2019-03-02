--No null FocusAreaID records in Project, so locking down FocusAreaID
alter table dbo.Project
alter column FocusAreaID int not null
GO

alter table dbo.ProjectUpdate
alter column FocusAreaID int not null
GO

--