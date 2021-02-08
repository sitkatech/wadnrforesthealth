

insert into dbo.Program (OrganizationID, ProgramName, ProgramShortName, ProgramIsActive, ProgramCreateDate, ProgramCreatePersonID, IsDefaultProgramForImportOnly)
select 4708, 'USFS Silviculture', 'Silviculture', 1, getdate(), 5285,0

go



insert into dbo.GisUploadSourceOrganization (GisUploadSourceOrganizationName, ProjectTypeDefaultName, TreatmentTypeDefaultName, ImportIsFlattened, AdjustProjectTypeBasedOnTreatmentTypes
                                             , ProjectStageDefaultID, DataDeriveProjectStage, DefaultLeadImplementerOrganizationID, RelationshipTypeForDefaultOrganizationID, ImportAsDetailedLocationInsteadOfTreatments
                                             , ProjectDescriptionDefaultText, ApplyCompletedDateToProject, ApplyStartDateToProject, ProgramID, ImportAsDetailedLocationInAdditionToTreatments)

select 'USFS Silviculture', 'Integrated forest health project', 'Non-Commercial', 0, 0, 3, 0, 4708, 33, 0, null,1,0,(select top 1 ProgramID from dbo.Program where ProgramName = 'USFS Silviculture'),1


