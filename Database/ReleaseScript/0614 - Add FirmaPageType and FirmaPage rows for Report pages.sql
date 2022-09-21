insert into dbo.FirmaPageType
	(FirmaPageTypeID, FirmaPageTypeName, FirmaPageTypeDisplayName, FirmaPageRenderTypeID)
values
(71, 'Reports', 'Reports', 1),
(72, 'ReportProjects', 'Report Projects', 1),
(73, 'ReportAddReport', 'Add a Report', 2)

insert into dbo.FirmaPage
	(FirmaPageTypeID, FirmaPageContent)
values 
(71, '<p>In the Reports area you can review and upload report Word Document (.docx) templates. Report templates uploaded here can be used on their appropriate pages within the report center based on the model that is selected.</p>'),
(72, '<p>Filter, sort, and select projects from the grid below using the checkboxes. The selected projects will be provided to the chosen report template in the order that they appear in the grid.</p>'),
(73, '<p>When you upload your report template to the Reports area it will be validated to ensure that the report runs successfully. After successfully uploading a report, you can run it on the projects selected from the reports projects grid.</p>')