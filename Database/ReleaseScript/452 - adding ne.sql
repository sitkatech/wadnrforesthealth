
insert into dbo.GisUploadSourceOrganization(GisUploadSourceOrganizationName)
select 'DNR LOA NE'


go

insert into dbo.GisUploadSourceOrganization(GisUploadSourceOrganizationName)
select 'DNR LOA SE'

go

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 3,36,'Completed', 'Completed'

insert into dbo.GisCrossWalkDefault(GisUploadSourceOrganizationID, FieldDefinitionID, GisCrossWalkSourceValue, GisCrossWalkMappedValue)
select 4,36,'Completed Treatment', 'Completed'