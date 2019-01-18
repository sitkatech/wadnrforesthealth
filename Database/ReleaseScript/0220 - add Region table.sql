

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
	RegionID int not null constraint FK_ProjectRegion_Region_RegionID foreign key references dbo.Region(RegionID),
	ProjectRegionNotes varchar(4000)

);

SET IDENTITY_INSERT dbo.ProjectRegion ON;  
GO

insert into dbo.ProjectRegion (ProjectRegionID, ProjectID, RegionID, ProjectRegionNotes)
select pga.ProjectGeospatialAreaID, pga.ProjectID, pga.GeospatialAreaID, gat.Notes 
from dbo.ProjectGeospatialArea as pga 
join dbo.GeospatialArea as ga 
on pga.GeospatialAreaID = ga.GeospatialAreaID 
join dbo.ProjectGeospatialAreaTypeNote as gat
on pga.ProjectID = gat.ProjectID and ga.GeospatialAreaTypeID = gat.GeospatialAreaTypeID
where ga.GeospatialAreaTypeID = 10;
go


SET IDENTITY_INSERT dbo.ProjectRegion OFF;  
GO


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
	RegionID int not null constraint FK_ProjectRegionUpdate_Region_RegionID foreign key references dbo.Region(RegionID),
	ProjectRegionUpdateNotes varchar(4000)
);

SET IDENTITY_INSERT dbo.ProjectRegionUpdate ON;  
GO

insert into dbo.ProjectRegionUpdate (ProjectRegionUpdateID, ProjectUpdateBatchID, RegionID, ProjectRegionUpdateNotes)
select pgau.ProjectGeospatialAreaUpdateID, pgau.ProjectUpdateBatchID, pgau.GeospatialAreaID, gatnu.Notes
from dbo.ProjectGeospatialAreaUpdate as pgau 
join dbo.GeospatialArea as ga 
on pgau.GeospatialAreaID = ga.GeospatialAreaID 
join dbo.ProjectGeospatialAreaTypeNoteUpdate as gatnu
on pgau.ProjectUpdateBatchID = gatnu.ProjectUpdateBatchID and ga.GeospatialAreaTypeID = gatnu.GeospatialAreaTypeID
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

ALTER TABLE dbo.FocusArea ADD RegionID int NOT NULL   
    CONSTRAINT FK_FocusArea_Region_RegionID foreign key references dbo.Region(RegionID);


