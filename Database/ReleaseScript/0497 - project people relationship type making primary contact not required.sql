--select * from dbo.Project
--where ProjectID = 12719

ALTER TABLE dbo.Project
DROP CONSTRAINT FK_Project_Person_PrimaryContactPersonID_PersonID
go
alter table dbo.Project
drop column PrimaryContactPersonID
go

ALTER TABLE dbo.ProjectUpdate
DROP CONSTRAINT FK_ProjectUpdate_Person_PrimaryContactPersonID_PersonID
go
alter table dbo.ProjectUpdate
drop column PrimaryContactPersonID
go


update dbo.ProjectPersonRelationshipType
set IsRequired = 0
where ProjectPersonRelationshipTypeName = 'PrimaryContact'