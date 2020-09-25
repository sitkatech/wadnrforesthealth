
alter table dbo.GisUploadSourceOrganization ADD ImportAsDetailedLocationInsteadOfTreatments bit

go

update dbo.GisUploadSourceOrganization
set ImportAsDetailedLocationInsteadOfTreatments = 0

update dbo.GisUploadSourceOrganization
set ImportAsDetailedLocationInsteadOfTreatments = 1
where GisUploadSourceOrganizationID = 6

go

alter table dbo.GisUploadSourceOrganization ALTER COLUMN ImportAsDetailedLocationInsteadOfTreatments bit not null



