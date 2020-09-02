

insert into dbo.GisUploadSourceOrganization (GisUploadSourceOrganizationName, ProjectTypeDefaultName, TreatmentTypeDefaultName,ImportIsFlattened,RequireCompletionDate, AdjustProjectTypeBasedOnTreatmentTypes)
select 'WADNR Stewardship', 'Forest Stewardship Plan',null,0,0,0

go


ALTER TABLE dbo.GisUploadSourceOrganization add ProjectStageDefaultID int

ALTER TABLE dbo.GisUploadSourceOrganization add DataDeriveProjectStage bit

go

update dbo.GisUploadSourceOrganization
set ProjectStageDefaultID = 4, DataDeriveProjectStage = 1

go

update dbo.GisUploadSourceOrganization
set ProjectStageDefaultID = 2, DataDeriveProjectStage = 0
where GisUploadSourceOrganizationID = 6

USE [WADNRForestHealthDB]
GO

ALTER TABLE dbo.GisUploadSourceOrganization ALTER COLUMN ProjectStageDefaultID int not null

ALTER TABLE dbo.GisUploadSourceOrganization ALTER COLUMN DataDeriveProjectStage bit not null

go

ALTER TABLE [dbo].GisUploadSourceOrganization  WITH CHECK ADD  CONSTRAINT [FK_GisUploadSourceOrganization_ProjectStage_ProjectStageDefaultID_ProjectStageID] FOREIGN KEY(ProjectStageDefaultID)
REFERENCES [dbo].ProjectStage (ProjectStageID)
GO



insert into dbo.GisDefaultMapping(GisUploadSourceOrganizationID, FieldDefinitionID, GisDefaultMappingColumnName)
select 6, 30, 'plan_name'

insert into dbo.GisDefaultMapping(GisUploadSourceOrganizationID, FieldDefinitionID, GisDefaultMappingColumnName)
select 6, 465, 'smart_plan_id'

insert into dbo.GisDefaultMapping(GisUploadSourceOrganizationID, FieldDefinitionID, GisDefaultMappingColumnName)
select 6, 466, 'plan_start_date'

insert into dbo.GisDefaultMapping(GisUploadSourceOrganizationID, FieldDefinitionID, GisDefaultMappingColumnName)
select 6, 467, 'gis_acres'


