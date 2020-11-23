ALTER TABLE dbo.GisUploadSourceOrganization ADD ImportAsDetailedLocationInAdditionToTreatments bit

go

update dbo.GisUploadSourceOrganization
set ImportAsDetailedLocationInAdditionToTreatments = 0

go

 ALTER TABLE dbo.GisUploadSourceOrganization ALTER COLUMN ImportAsDetailedLocationInAdditionToTreatments bit not null

 go




insert into dbo.Organization (OrganizationName, OrganizationShortName, IsActive, OrganizationTypeID, IsEditable)
select 'U.S. Fish and Wildlife Service', 'USFWS',1,1063,1

go

insert into dbo.Program(OrganizationID, ProgramName, ProgramShortName,ProgramIsActive,ProgramCreateDate,ProgramCreatePersonID)
select (select OrganizationID from dbo.Organization o where o.OrganizationName = 'U.S. Fish and Wildlife Service'), 'U.S. Fish and Wildlife Service Washington Treatments', 'USFWS WA', 1, getdate(), 5285


go


insert into dbo.GisUploadSourceOrganization (GisUploadSourceOrganizationName
, ProjectTypeDefaultName
, TreatmentTypeDefaultName
, ImportIsFlattened
, RequireCompletionDate
, AdjustProjectTypeBasedOnTreatmentTypes
, ProjectStageDefaultID
, DataDeriveProjectStage
, DefaultLeadImplementerOrganizationID
, RelationshipTypeForDefaultOrganizationID
, ImportAsDetailedLocationInsteadOfTreatments
, ProjectDescriptionDefaultText
, ApplyCompletedDateToProject
, ApplyStartDateToProject
, ProgramID
, ImportAsDetailedLocationInAdditionToTreatments)


select 'USFWS'
, null
, null
, 0
, 1
, 1
, 4
, 0
, (select OrganizationID from dbo.Organization o where o.OrganizationName = 'U.S. Fish and Wildlife Service')
, 33
, 0
, null
, 1
, 0
, 7
, 1
