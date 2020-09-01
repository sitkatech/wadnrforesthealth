
delete from dbo.GisCrossWalkDefault where GisUploadSourceOrganizationID = 4

delete from dbo.GisUploadSourceOrganization where GisUploadSourceOrganizationID = 4



go


insert into dbo.GisUploadSourceOrganization (GisUploadSourceOrganizationName, ProjectTypeDefaultName, TreatmentTypeDefaultName, ImportIsFlattened)
select 'WDFW', 'Integrated forest health project', null, 0

go

insert into dbo.GisDefaultMapping (GisUploadSourceOrganizationID, FieldDefinitionID, GisDefaultMappingColumnName)
select 5, 30, 'projectname'

insert into dbo.GisDefaultMapping (GisUploadSourceOrganizationID, FieldDefinitionID, GisDefaultMappingColumnName)
select 5, 465, 'projectname'

insert into dbo.GisDefaultMapping (GisUploadSourceOrganizationID, FieldDefinitionID, GisDefaultMappingColumnName)
select 5, 468, 'treatmentmethodcode'

insert into dbo.GisDefaultMapping (GisUploadSourceOrganizationID, FieldDefinitionID, GisDefaultMappingColumnName)
select 5, 28, 'treatmentdate'

insert into dbo.GisDefaultMapping (GisUploadSourceOrganizationID, FieldDefinitionID, GisDefaultMappingColumnName)
select 5, 469, 'treatmentmethodcode'



go

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 469, 'ClearCut', 'Other'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 469, 'Commercial Thin', 'Thinning'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 469, 'MasticateChip', 'Mastication'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 469, 'PileAndBurn', 'Machine Pile Burn'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 469, 'Planting', 'Other'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 469, 'PreCommercialThin', 'Thinning'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 469, 'PrescribedFire', 'Broadcast Burn'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 469, 'ShadedFuelBreak', 'Thinning'


go



insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 468, 'ClearCut', 'Commercial'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 468, 'Commercial Thin', 'Commercial'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 468, 'MasticateChip', 'Non-Commercial'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 468, 'PileAndBurn', 'Fire'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 468, 'Planting', 'Non-Commercial'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 468, 'PreCommercialThin', 'Non-commercial'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 468, 'PrescribedFire', 'Fire'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 468, 'ShadedFuelBreak', 'Other'