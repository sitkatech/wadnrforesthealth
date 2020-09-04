ALTER TABLE dbo.GisUploadSourceOrganization ADD RequireCompletionDate bit

go

update dbo.GisUploadSourceOrganization 
set RequireCompletionDate = 0

go

update dbo.GisUploadSourceOrganization 
set RequireCompletionDate = 0
where GisUploadSourceOrganizationID = 5



ALTER TABLE dbo.GisUploadSourceOrganization ALTER COLUMN RequireCompletionDate bit not null
