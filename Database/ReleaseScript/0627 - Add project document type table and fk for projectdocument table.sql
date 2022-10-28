create table dbo.ProjectDocumentType(
	ProjectDocumentTypeID int not null identity(1,1), 
	ProjectDocumentTypeName varchar(100) not null,
	ProjectDocumentTypeDisplayName varchar(200) null, 
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



insert dbo.ProjectDocumentType (ProjectDocumentTypeName, ProjectDocumentTypeDisplayName) 
values
	('CostShareApplication', 'Cost Share Application'),
	('CostShareSheet', 'Cost Share Sheet'),
	('TreatmentSpecs', 'Treatment Specs'),
	('Map', 'Map'),
	('ApprovalLetter', 'Approval Letter'),
	('ClaimForm', 'Claim Form'),
	('Other', 'Other'),
	('ManagementPlan', 'Management Plan'),
	('MonitoringReport', 'Monitoring Report'),
	('ProjectScoringMatrix', 'Project Scoring Matrix'),
	('SiteVisitNotes', 'Site Visit Notes'),
	('ApprovalChecklist', 'Approval Checklist'),
	('Self-CostStatement', 'Self-Cost Statement')


