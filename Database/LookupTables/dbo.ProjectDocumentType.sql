delete from dbo.ProjectDocumentType

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