create table dbo.ProjectLocationType (
	ProjectLocationTypeID int not null constraint PK_ProjectLocationType_ProjectLocationTypeID primary key,
	ProjectLocationTypeName varchar(50) constraint AK_ProjectLocationType_ProjectLocationTypeName unique,
	ProjectLocationTypeDisplayName varchar(50) constraint AK_ProjectLocationType_ProjectLocationTypeDisplayName unique
);

insert dbo.ProjectLocationType (ProjectLocationTypeID, ProjectLocationTypeName, ProjectLocationTypeDisplayName) 
values 
(1, 'ProjectArea', 'Project Area'),
(2, 'TreatmentActivity', 'Treatment Activity'),
(3, 'ResearchPlot', 'Research Plot'),
(4, 'TestSite', 'Test Site'),
(5, 'Other', 'Other')

Alter table dbo.ProjectLocation add 
	ProjectLocationTypeID int not null 
		constraint FK_ProjectLocation_ProjectLocationType_ProjectLocationTypeID foreign key references dbo.ProjectLocationType(ProjectLocationTypeID),
	ProjectLocationName varchar(100) not null 
		constraint AK_ProjectLocation_ProjectID_ProjectLocationName unique(ProjectID, ProjectLocationName);


Alter table dbo.ProjectLocationUpdate add 
	ProjectLocationTypeID int not null 
		constraint FK_ProjectLocationUpdate_ProjectLocationType_ProjectLocationTypeID foreign key references dbo.ProjectLocationType(ProjectLocationTypeID),
	ProjectLocationUpdateName varchar(100) not null 
		constraint AK_ProjectLocationUpdate_ProjectUpdateBatchID_ProjectLocationUpdateName unique(ProjectUpdateBatchID, ProjectLocationUpdateName);



exec sp_rename 'dbo.ProjectLocation.Annotation', 'ProjectLocationNotes', 'COLUMN'

exec sp_rename 'dbo.ProjectLocationUpdate.Annotation', 'ProjectLocationUpdateNotes', 'COLUMN'