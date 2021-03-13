 update dbo.GisCrossWalkDefault
 set GisCrossWalkMappedValue = 'Regen'
 where GisCrossWalkDefaultID = 79


update dbo.GisUploadSourceOrganization
set GisUploadSourceOrganizationName = 'DNR State Lands'
where GisUploadSourceOrganizationID = 1


insert into dbo.GisExcludeIncludeColumn(GisUploadSourceOrganizationID, GisDefaultMappingColumnName, IsWhitelist)
select 1, 'technique_cd', 0

go

insert into dbo.GisExcludeIncludeColumnValue(GisExcludeIncludeColumnID, GisExcludeIncludeColumnValueForFiltering)
select 1, 'NATURAL'

insert into dbo.GisExcludeIncludeColumnValue(GisExcludeIncludeColumnID, GisExcludeIncludeColumnValueForFiltering)
select 1, 'SEED_GRASS'

insert into dbo.GisExcludeIncludeColumnValue(GisExcludeIncludeColumnID, GisExcludeIncludeColumnValueForFiltering)
select 1, 'APPLY_BARRIER'