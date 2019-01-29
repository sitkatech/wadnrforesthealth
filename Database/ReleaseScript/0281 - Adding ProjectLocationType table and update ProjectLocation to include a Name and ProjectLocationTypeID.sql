create table dbo.ProjectLocationType (
	ProjectLocationTypeID int not null constraint PK_ProjectLocationType_ProjectLocationTypeID primary key,
	ProjectLocationTypeName varchar(50) constraint AK_ProjectLocationType_ProjectLocationTypeName unique,
	ProjectLocationTypeDisplayName varchar(50) constraint AK_ProjectLocationType_ProjectLocationTypeDisplayName unique
);

Alter table dbo.ProjectLocation add 
	ProjectLocationTypeID int not null 
		constraint FK_ProjectLocation_ProjectLocationType_ProjectLocationTypeID foreign key references dbo.ProjectLocationType(ProjectLocationTypeID),
	ProjectLocationName varchar(100) not null 
		constraint AK_ProjectLocation_ProjectID_ProjectLocationName unique(ProjectID, ProjectLocationName);

