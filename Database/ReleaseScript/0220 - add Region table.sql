

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
select pga.ProjectGeospatialAreaID, pga.ProjectID, pga.GeospatialAreaID
from dbo.ProjectGeospatialArea as pga 
join dbo.GeospatialArea as ga 
on pga.GeospatialAreaID = ga.GeospatialAreaID
where ga.GeospatialAreaTypeID = 10;
go


SET IDENTITY_INSERT dbo.ProjectRegion OFF;  
GO

ALTER TABLE [dbo].[ProjectRegion] ADD  CONSTRAINT [AK_ProjectRegion_ProjectID_RegionID] UNIQUE NONCLUSTERED 
(
	[ProjectID],
	[RegionID]
);
go

CREATE table dbo.ProjectRegionUpdate(
	ProjectRegionUpdateID int not null identity(1,1) constraint PK_ProjectRegionUpdate_ProjectRegionUpdateID primary key,
	ProjectUpdateBatchID int not null constraint FK_ProjectRegionUpdate_ProjectUpdateBatch_ProjectUpdateBatchID foreign key references dbo.ProjectUpdateBatch(ProjectUpdateBatchID),
	RegionID int not null constraint FK_ProjectRegionUpdate_Region_RegionID foreign key references dbo.Region(RegionID)
);

SET IDENTITY_INSERT dbo.ProjectRegionUpdate ON;  
GO

insert into dbo.ProjectRegionUpdate (ProjectRegionUpdateID, ProjectUpdateBatchID, RegionID)
select pgau.ProjectGeospatialAreaUpdateID, pgau.ProjectUpdateBatchID, pgau.GeospatialAreaID
from dbo.ProjectGeospatialAreaUpdate as pgau 
join dbo.GeospatialArea as ga 
on pgau.GeospatialAreaID = ga.GeospatialAreaID 
where ga.GeospatialAreaTypeID = 10;
go


SET IDENTITY_INSERT dbo.ProjectRegionUpdate OFF;  
GO


GO
ALTER TABLE [dbo].[ProjectRegionUpdate] ADD  CONSTRAINT [AK_ProjectRegionUpdate_ProjectUpdateBatchID_RegionID] UNIQUE NONCLUSTERED 
(
	[ProjectUpdateBatchID],
	[RegionID]
);
go


delete pgau from dbo.ProjectGeospatialAreaUpdate as pgau
join dbo.GeospatialArea as b on pgau.GeospatialAreaID = b.GeospatialAreaID
where b.GeospatialAreaTypeID = 10;

delete psga from dbo.PersonStewardGeospatialArea as psga
join dbo.GeospatialArea as b on psga.GeospatialAreaID = b.GeospatialAreaID
where b.GeospatialAreaTypeID = 10;

delete from dbo.ProjectGeospatialAreaTypeNote where GeospatialAreaTypeID = 10;

delete pga from dbo.ProjectGeospatialArea as pga
join dbo.GeospatialArea as b on pga.GeospatialAreaID = b.GeospatialAreaID
where b.GeospatialAreaTypeID = 10;

delete from dbo.GeospatialArea where GeospatialAreaTypeID = 10;

delete from dbo.GeospatialAreaType where GeospatialAreaTypeID = 10;

ALTER TABLE dbo.FocusArea ADD RegionID int NOT NULL CONSTRAINT FK_FocusArea_Region_RegionID foreign key references dbo.Region(RegionID);

alter table dbo.Project add NoRegionsExplanation varchar(4000);
alter table dbo.ProjectUpdateBatch add NoRegionsExplanation varchar(4000);

