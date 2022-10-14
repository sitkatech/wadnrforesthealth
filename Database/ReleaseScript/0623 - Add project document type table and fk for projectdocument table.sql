create table dbo.ProjectDocumentType(
	ProjectDocumentTypeID int not null identity(1,1), 
	ProjectDocumentTypeName varchar(100) not null,
	ProjectDocumentTypeDescription varchar(200) null,
	ProjectDocumentParentTypeID int null, 
	constraint PK_ProjectDocumentType_ProjectDocumentTypeID primary key(ProjectDocumentTypeID),
	constraint FK_ProjectDocumentType_ProjectDocumentType_ProjectDocumentParentTypeID_ProjectDocumentTypeID foreign key(ProjectDocumentParentTypeID) 
		references ProjectDocumentType(ProjectDocumentTypeID),
	constraint AK_ProjectDocumentType_ProjectDocumentParentTypeID_ProjectDocumentTypeID unique(ProjectDocumentParentTypeID, ProjectDocumentTypeID)
)

go


Alter table dbo.ProjectDocument 
	add ProjectDocumentTypeID int null 
	constraint FK_ProjectDocument_ProjectDocumentType_ProjectDocumentTypeID foreign key(ProjectDocumentTypeID) 
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
	('Other')


insert into dbo.ProjectDocumentType(ProjectDocumentParentTypeID, ProjectDocumentTypeName)
values
(7,'Management Plan'),
(7,'Monitoring Report'),
(7,'Project Scoring Matrix'),
(7,'Site Visit Notes'),
(7,'Approval Checklist'),
(7,'Self-Cost Statement')


