ALTER table dbo.GisUploadSourceOrganization ADD AdjustProjectTypeBasedOnTreatmentTypes bit

go

update dbo.GisUploadSourceOrganization
set AdjustProjectTypeBasedOnTreatmentTypes = 0

update dbo.GisUploadSourceOrganization
set AdjustProjectTypeBasedOnTreatmentTypes = 1
where GisUploadSourceOrganizationID = 5

go


ALTER table dbo.GisUploadSourceOrganization ALTER COLUMN AdjustProjectTypeBasedOnTreatmentTypes bit not null
