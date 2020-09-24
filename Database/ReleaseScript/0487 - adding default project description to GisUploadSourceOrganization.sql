


alter table dbo.GisUploadSourceOrganization
add ProjectDescriptionDefaultText varchar(4000)
go

update dbo.GisUploadSourceOrganization
set ProjectDescriptionDefaultText = 'This project is a Washington Department of Natural Resource Landowner Assistance project.'
where GisUploadSourceOrganizationName = 'DNR LOA NE'

update dbo.GisUploadSourceOrganization
set ProjectDescriptionDefaultText = 'This project is a Washington Department of Natural Resources Forest Stewardship Plan. Forest Stewardship is a nationwide program providing advice and assistance to help family forest owners manage their lands. The program is a cooperative effort between the USDA Forest Service and state forestry agencies. In Washington state, the program is administered by DNR''s Forest Health and Resiliency Division. A Forest Stewardship Plan is prepared by an interested private landowner in coordination with a professional forester to guide current and future management actions and help qualify for financial assistance, current use taxation, recognition, and certification programs.'
where GisUploadSourceOrganizationName = 'WADNR Stewardship'

update dbo.GisUploadSourceOrganization
set ProjectDescriptionDefaultText = 'This project is a Washington Department of Natural Resources State Trust lands forest health project.'
where GisUploadSourceOrganizationName = 'DNR'