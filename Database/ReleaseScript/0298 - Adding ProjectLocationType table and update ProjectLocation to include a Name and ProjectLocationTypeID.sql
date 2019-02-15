create table dbo.ProjectLocationType (
	ProjectLocationTypeID int not null constraint PK_ProjectLocationType_ProjectLocationTypeID primary key,
	ProjectLocationTypeName varchar(50) constraint AK_ProjectLocationType_ProjectLocationTypeName unique,
	ProjectLocationTypeDisplayName varchar(50) constraint AK_ProjectLocationType_ProjectLocationTypeDisplayName unique,
	ProjectLocationTypeMapLayerColor varchar(20) not null
);

insert dbo.ProjectLocationType (ProjectLocationTypeID, ProjectLocationTypeName, ProjectLocationTypeDisplayName, ProjectLocationTypeMapLayerColor) 
values 
(1, 'ProjectArea', 'Project Area', '#2c96c3'),
(2, 'TreatmentActivity', 'Treatment Activity', '#2b7ac3'),
(3, 'ResearchPlot', 'Research Plot', '#2a44c3'),
(4, 'TestSite', 'Test Site', '#3e29c3'),
(5, 'Other', 'Other', '#6928c3')

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