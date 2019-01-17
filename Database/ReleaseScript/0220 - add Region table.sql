

CREATE table dbo.Region (
	RegionID int not null constraint PK_Region_RegionID primary key,
	RegionName varchar(100) not null constraint AK_Region_RegionName unique,
	RegionLocation geometry
)

insert into dbo.Region (RegionID, RegionName, RegionLocation)
select GeospatialAreaID, GeospatialAreaName, GeospatialAreaFeature from dbo.GeospatialArea where GeospatialAreaTypeID = 10;
go


CREATE table dbo.ProjectRegion(
	ProjectRegionID int not null identity(1,1) constraint PK_ProjectRegion_ProjectRegionID primary key,
	ProjectID int not null constraint FK_ProjectRegion_Project_ProjectID foreign key references dbo.Project(ProjectID),
	RegionID int not null constraint FK_ProjectRegion_Region_RegionID foreign key references dbo.Region(RegionID)

);

SET IDENTITY_INSERT dbo.ProjectRegion ON;  
GO

insert into dbo.ProjectRegion (ProjectRegionID, ProjectID, RegionID)
select a.ProjectGeospatialAreaID, a.ProjectID, a.GeospatialAreaID from dbo.ProjectGeospatialArea as a join dbo.GeospatialArea as b on a.GeospatialAreaID = b.GeospatialAreaID where b.GeospatialAreaTypeID = 10;
go


SET IDENTITY_INSERT dbo.ProjectRegion OFF;  
GO


GO
ALTER TABLE [dbo].[ProjectRegion] ADD  CONSTRAINT [AK_ProjectRegion_ProjectID_RegionID] UNIQUE NONCLUSTERED 
(
	[ProjectID],
	[RegionID]
)


