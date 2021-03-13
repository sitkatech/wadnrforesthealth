
insert into dbo.Program(OrganizationID, ProgramName, ProgramShortName, ProgramIsActive, ProgramCreateDate, ProgramCreatePersonID, IsDefaultProgramForImportOnly)
select 4708, 'USFS Timber Harvest', 'Timber Harvest',1,GetDate(), 5285, 0

go

insert into dbo.GisUploadSourceOrganization(GisUploadSourceOrganizationName, ProjectTypeDefaultName, ImportIsFlattened, AdjustProjectTypeBasedOnTreatmentTypes, ProjectStageDefaultID, DataDeriveProjectStage, DefaultLeadImplementerOrganizationID, RelationshipTypeForDefaultOrganizationID, ImportAsDetailedLocationInsteadOfTreatments, ApplyCompletedDateToProject, ApplyStartDateToProject, ProgramID, ImportAsDetailedLocationInAdditionToTreatments, ApplyStartDateToTreatments, ApplyEndDateToTreatments)
select 'USFS Timber Harvest', 'Integrated forest health project', 0,0,3,0,4708,33,0,0,0,(select top 1 ProgramID from dbo.Program where ProgramName = 'USFS Timber Harvest'), 0,1,1

go

update dbo.GisUploadSourceOrganization
set TreatmentTypeDefaultName = 'Non-Commercial'
where GisUploadSourceOrganizationID = 11


insert into dbo.GisDefaultMapping(GisUploadSourceOrganizationID, FieldDefinitionID, GisDefaultMappingColumnName)
select 11,30, 'nepa_doc_name'

insert into dbo.GisDefaultMapping(GisUploadSourceOrganizationID, FieldDefinitionID, GisDefaultMappingColumnName)
select 11,465, 'nepa_project_id'

insert into dbo.GisDefaultMapping(GisUploadSourceOrganizationID, FieldDefinitionID, GisDefaultMappingColumnName)
select 11,469, 'treatment_type'

insert into dbo.GisDefaultMapping(GisUploadSourceOrganizationID, FieldDefinitionID, GisDefaultMappingColumnName)
select 11,28, 'date_completed'

insert into dbo.GisDefaultMapping(GisUploadSourceOrganizationID, FieldDefinitionID, GisDefaultMappingColumnName)
select 11,467, 'gis_acres'

