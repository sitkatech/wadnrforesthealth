

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
, ApplyStartDateToProject)


select 'WA State Parks'
, 'Integrated forest health project'
, null
, 0
, 0
, 1
, 3
, 1
, 5793
, 33
, 0
, null
, 0
, 1

