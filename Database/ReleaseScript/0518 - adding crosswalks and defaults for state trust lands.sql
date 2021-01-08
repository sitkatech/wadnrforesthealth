

insert into dbo.GisDefaultMapping (GisUploadSourceOrganizationID, FieldDefinitionID, GisDefaultMappingColumnName)
select 1,465,'fma_id'

insert into dbo.GisDefaultMapping (GisUploadSourceOrganizationID, FieldDefinitionID, GisDefaultMappingColumnName)
select 1,30,'fma_nm'


insert into dbo.GisDefaultMapping (GisUploadSourceOrganizationID, FieldDefinitionID, GisDefaultMappingColumnName)
select 1,36,'fma_status_cd'


insert into dbo.GisDefaultMapping (GisUploadSourceOrganizationID, FieldDefinitionID, GisDefaultMappingColumnName)
select 1,468,'fma_type_cd'

insert into dbo.GisDefaultMapping (GisUploadSourceOrganizationID, FieldDefinitionID, GisDefaultMappingColumnName)
select 1,469,'technique_cd'

insert into dbo.GisDefaultMapping (GisUploadSourceOrganizationID, FieldDefinitionID, GisDefaultMappingColumnName)
select 1,467,'acres_treated'


go

delete from dbo.GisCrossWalkDefault where GisCrossWalkDefaultID = 2

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 36, 'COMPLETED', 'Completed'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 468, 'TIM_HARV', 'Commercial'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 468, 'PEST_MGMT', 'Non-commercial'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 468, 'PRUNING', 'Non-commercial'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 468, 'REGEN', 'Non-commercial'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 468, 'SITE_PREP', 'Non-commercial'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 468, 'PCT', 'Non-commercial'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 468, 'VEG_MGMT', 'Non-commercial'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 469, 'HAND_PRUNE', 'Pruning'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 469, 'PILE_BURN', 'Pile Burn'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 469, 'COMMRCL_THIN', 'Thinning'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 469, 'HAND_CUT', 'Thinning'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 469, 'SEEDTREE_INT', 'Thinning'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 469, 'SEEDTREE_REM', 'Thinning'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 469, 'UNEVNAGE_MGT', 'Thinning'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 469, 'VARIABL_THIN', 'Thinning'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 469, 'VRH', 'Thinning'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 469, 'APPLY_BARRIER', 'Other'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 469, 'FOLIAR_BROAD', 'Other'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 469, 'FOLIAR_DIRECT', 'Other'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 469, 'FOLIAR_SPOT', 'Other'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 469, 'FUELS_MGMT', 'Other'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 469, 'GROUND_HERB', 'Other'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 469, 'GROUND_MECH', 'Other'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 469, 'HAND_PLANT', 'Other'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 469, 'NATURAL', 'Other'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 1, 469, 'SEED_GRASS', 'Other'

update dbo.GisUploadSourceOrganization
set AdjustProjectTypeBasedOnTreatmentTypes = 1,
    ProjectTypeDefaultName = 'Integrated forest health project'
where GisUploadSourceOrganizationID = 1