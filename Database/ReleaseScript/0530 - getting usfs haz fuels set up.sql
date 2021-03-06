
insert into dbo.Program(OrganizationID, ProgramName, ProgramShortName, ProgramIsActive, ProgramCreateDate, ProgramCreatePersonID, IsDefaultProgramForImportOnly)
select 4708, 'USFS Hazardous Fuels', 'Hazardous Fuels',1,GetDate(), 5285, 0

go

insert into dbo.GisUploadSourceOrganization(GisUploadSourceOrganizationName, ProjectTypeDefaultName, ImportIsFlattened, AdjustProjectTypeBasedOnTreatmentTypes, ProjectStageDefaultID, DataDeriveProjectStage, DefaultLeadImplementerOrganizationID, RelationshipTypeForDefaultOrganizationID, ImportAsDetailedLocationInsteadOfTreatments, ApplyCompletedDateToProject, ApplyStartDateToProject, ProgramID, ImportAsDetailedLocationInAdditionToTreatments, ApplyStartDateToTreatments, ApplyEndDateToTreatments)
select 'USFS Hazardous Fuels', 'Integrated forest health project', 0,0,3,0,4708,33,0,0,0,(select top 1 ProgramID from dbo.Program where ProgramName = 'USFS Hazardous Fuels'), 0,1,1

go

update dbo.GisDefaultMapping 
set GisDefaultMappingColumnName = 'activity'
where GisDefaultMappingID = 3

go

update dbo.GisDefaultMapping 
set GisUploadSourceOrganizationID = (select top 1 GisUploadSourceOrganizationID from dbo.GisUploadSourceOrganization where GisUploadSourceOrganizationName = 'USFS Hazardous Fuels')
where GisUploadSourceOrganizationID = 2

go

delete from dbo.GisDefaultMapping where GisDefaultMappingID = 9
