delete from dbo.TreatmentDetailedActivityType

--TODO
Insert into dbo.TreatmentDetailedActivityType (TreatmentDetailedActivityTypeID, TreatmentDetailedActivityTypeName, TreatmentDetailedActivityTypeDisplayName)
values
(1, 'Chipping', 'Chipping'),
(2, 'Pruning', 'Pruning'),
(3, 'Thinning', 'Thinning'),
(4, 'Mastication', 'Mastication'),
(5, 'Grazing', 'Grazing'),
(6, 'LopAndScatter', 'Lop and Scatter'),
(7, 'BiomassRemoval', 'Biomass Removal'),
(8, 'HandPile', 'Hand Pile'),
(9, 'BroadcastBurn', 'Broadcast Burn'),
(10, 'HandPileBurn', 'Hand Pile Burn'),
(11, 'MachinePileBurn', 'Machine Pile Burn'),
(12, 'Slash', 'Slash'),
(13, 'Other', 'Other'),
(14, 'JackpotBurn', 'Jackpot Burn'),
(15, 'MachinePile', 'Machine Pile'),
(16, 'FuelBreak', 'Fuel Break'),
(17, 'Planting', 'Planting')

go






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

insert into dbo.GisDefaultMapping (GisUploadSourceOrganizationID, FieldDefinitionID, GisDefaultMappingColumnName)
select 5, 1, 'treatmentmethodcode'



go

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 469, 'ClearCut', 'Other'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 469, 'Commercial Thin', 'Thinning'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 469, 'MasticateChip', 'Mastication'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 469, 'PileAndBurn', 'Hand Pile Burn'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 469, 'Planting', 'Planting'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 469, 'PreCommercialThin', 'Thinning'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 469, 'PrescribedFire', 'Broadcast Burn'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 469, 'ShadedFuelBreak', 'Fuel Break'


go



insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 468, 'ClearCut', 'Commercial'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 468, 'Commercial Thin', 'Commercial'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 468, 'MasticateChip', 'Non-Commercial'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 468, 'PileAndBurn', 'Prescribed Fire'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 468, 'Planting', 'Non-Commercial'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 468, 'PreCommercialThin', 'Non-commercial'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 468, 'PrescribedFire', 'Prescribed Fire'

insert into dbo.GisCrossWalkDefault (GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 5, 468, 'ShadedFuelBreak', 'Non-Commercial'

