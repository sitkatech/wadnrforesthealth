ALTER TABLE dbo.GisUploadSourceOrganization ADD ProjectTypeDefaultName varchar(100) 


go


update dbo.GisUploadSourceOrganization
set ProjectTypeDefaultName = 'Integrated forest health project'
where GisUploadSourceOrganizationID = 2


update dbo.GisUploadSourceOrganization
set ProjectTypeDefaultName = 'Non-commercial vegetation treatment'
where GisUploadSourceOrganizationID = 3


update dbo.GisUploadSourceOrganization
set ProjectTypeDefaultName = 'Non-commercial vegetation treatment'
where GisUploadSourceOrganizationID = 4


ALTER TABLE dbo.GisUploadSourceOrganization ADD TreatmentTypeDefaultName varchar(100) 

go

update dbo.GisUploadSourceOrganization
set TreatmentTypeDefaultName = 'Non-Commercial'
where GisUploadSourceOrganizationID = 3


ALTER TABLE dbo.GisUploadSourceOrganization ADD ImportIsFlattened bit


go

update dbo.GisUploadSourceOrganization
set ImportIsFlattened = 0
where GisUploadSourceOrganizationID = 2


update dbo.GisUploadSourceOrganization
set ImportIsFlattened = 0
where GisUploadSourceOrganizationID = 1

update dbo.GisUploadSourceOrganization
set ImportIsFlattened = 1
where GisUploadSourceOrganizationID = 3


update dbo.GisUploadSourceOrganization
set ImportIsFlattened = 1
where GisUploadSourceOrganizationID = 4
