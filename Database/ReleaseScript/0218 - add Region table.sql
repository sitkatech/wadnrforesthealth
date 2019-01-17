

CREATE table dbo.Region (
	RegionID int not null identity(1, 1) constraint PK_Region_RegionID primary key,
	RegionName varchar(200) not null constraint AK_Region_RegionName unique,
	RegionLocation geometry
)


CREATE table dbo.ProjectRegion(
	ProjectRegionID int not null identity(1,1) constraint PK_ProjectRegion_ProjectRegionID primary key,
	ProjectID int not null constraint FK_ProjectRegion_Project_ProjectID foreign key references dbo.Project(ProjectID),
	RegionID int not null constraint FK_ProjectRegion_Region_RegionID foreign key references dbo.Region(RegionID)

)

GO
ALTER TABLE [dbo].[ProjectRegion] ADD  CONSTRAINT [AK_ProjectRegion_ProjectID_RegionID] UNIQUE NONCLUSTERED 
(
	[ProjectID],
	[RegionID]
)
