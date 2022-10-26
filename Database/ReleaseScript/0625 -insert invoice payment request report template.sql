
insert into dbo.ReportTemplateModel(ReportTemplateModelID, ReportTemplateModelName, ReportTemplateModelDisplayName, ReportTemplateModelDescription)
values(2, 'InvoicePaymentRequest', 'Invoice Payment Request', 'Templates will be with the "Invoice Payment Request" model.')



insert into dbo.ReportTemplate(FileResourceID, DisplayName, Description, ReportTemplateModelTypeID, ReportTemplateModelID, IsSystemTemplate)
select 
	FileResourceID,
	'Project Invoice Payment Request' as DisplayName,
	NULL as Description,
	2 as ReportTemplateModelTypeID,
	1 as ReportTemplateModelID,
	1 as IsSystemTemplate
 from
	dbo.FileResource where OriginalBaseFilename = 'Project Invoice Payment Request'


insert into dbo.ReportTemplate(FileResourceID, DisplayName, Description, ReportTemplateModelTypeID, ReportTemplateModelID, IsSystemTemplate)
select 
	FileResourceID,
	'Invoice Payment Request' as DisplayName,
	NULL as Description,
	2 as ReportTemplateModelTypeID,
	2 as ReportTemplateModelID,
	1 as IsSystemTemplate
 from
	dbo.FileResource where OriginalBaseFilename = 'Invoice Payment Request'

go 

alter table dbo.Invoice 
alter column InvoiceNumber varchar(50) not null


