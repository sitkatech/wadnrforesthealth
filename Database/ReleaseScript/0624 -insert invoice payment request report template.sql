insert into dbo.ReportTemplate(FileResourceID, DisplayName, Description, ReportTemplateModelTypeID, ReportTemplateModelID, IsSystemTemplate)
select 
	FileResourceID,
	'Invoice Payment Request' as DisplayName,
	NULL as Description,
	2 as ReportTemplateModelTypeID,
	1 as ReportTemplateModelID,
	1 as IsSystemTemplate
 from
	dbo.FileResource where OriginalBaseFilename = 'Invoice Payment Request'
