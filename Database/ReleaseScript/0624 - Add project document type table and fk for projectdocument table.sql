create table dbo.ProjectDocumentType(
	ProjectDocumentTypeID int not null identity(1,1), 
	ProjectDocumentTypeName varchar(100) not null,
	ProjectDocumentTypeDescription varchar(200) null, 
	constraint PK_ProjectDocumentType_ProjectDocumentTypeID primary key(ProjectDocumentTypeID),
)

go


Alter table dbo.ProjectDocument 
	add ProjectDocumentTypeID int null 
	constraint FK_ProjectDocument_ProjectDocumentType_ProjectDocumentTypeID foreign key(ProjectDocumentTypeID) 
		references dbo.ProjectDocumentType(ProjectDocumentTypeID)

go


Alter table dbo.ProjectDocumentUpdate 
	add ProjectDocumentTypeID int null 
	constraint FK_ProjectDocumentUpdate_ProjectDocumentType_ProjectDocumentTypeID foreign key(ProjectDocumentTypeID) 
		references dbo.ProjectDocumentType(ProjectDocumentTypeID)

go



insert dbo.ProjectDocumentType (ProjectDocumentTypeName) 
values
	('Cost Share Application'),
	('Cost Share Sheet'),
	('Treatment Specs'),
	('Map'),
	('Approval Letter'),
	('Claim Form'),
	('Other'),
	('Management Plan'),
	('Monitoring Report'),
	('Project Scoring Matrix'),
	('Site Visit Notes'),
	('Approval Checklist'),
	('Self-Cost Statement')


