CREATE table dbo.Region (
	RegionID int not null constraint PK_Region_RegionID primary key,
	RegionName varchar(100) not null constraint AK_Region_RegionName unique,
	RegionLocation geometry
)

CREATE table dbo.ProjectRegion(
	ProjectRegionID int not null identity(1,1) constraint PK_ProjectRegion_ProjectRegionID primary key,
	ProjectID int not null constraint FK_ProjectRegion_Project_ProjectID foreign key references dbo.Project(ProjectID),
	RegionID int not null constraint FK_ProjectRegion_Region_RegionID foreign key references dbo.Region(RegionID),
	CONSTRAINT [AK_ProjectRegion_ProjectID_RegionID] UNIQUE ([ProjectID], [RegionID])
)

CREATE table dbo.ProjectRegionUpdate(
	ProjectRegionUpdateID int not null identity(1,1) constraint PK_ProjectRegionUpdate_ProjectRegionUpdateID primary key,
	ProjectUpdateBatchID int not null constraint FK_ProjectRegionUpdate_ProjectUpdateBatch_ProjectUpdateBatchID foreign key references dbo.ProjectUpdateBatch(ProjectUpdateBatchID),
	RegionID int not null constraint FK_ProjectRegionUpdate_Region_RegionID foreign key references dbo.Region(RegionID),
	CONSTRAINT [AK_ProjectRegionUpdate_ProjectUpdateBatchID_RegionID] UNIQUE ([ProjectUpdateBatchID], [RegionID])
)

alter table dbo.Project add NoRegionsExplanation varchar(4000);
alter table dbo.ProjectUpdateBatch add NoRegionsExplanation varchar(4000);

ALTER TABLE dbo.FocusArea ADD RegionID int NOT NULL CONSTRAINT FK_FocusArea_Region_RegionID foreign key references dbo.Region(RegionID)
GO

declare @GeospatialAreaTypeID int
set @GeospatialAreaTypeID = 10

insert into dbo.Region (RegionID, RegionName, RegionLocation)
select GeospatialAreaID, GeospatialAreaName, GeospatialAreaFeature from dbo.GeospatialArea where GeospatialAreaTypeID = @GeospatialAreaTypeID;

insert into dbo.ProjectRegion (ProjectID, RegionID)
select pga.ProjectID, pga.GeospatialAreaID
from dbo.ProjectGeospatialArea as pga 
join dbo.GeospatialArea as ga 
on pga.GeospatialAreaID = ga.GeospatialAreaID
where ga.GeospatialAreaTypeID = @GeospatialAreaTypeID;


insert into dbo.ProjectRegionUpdate (ProjectUpdateBatchID, RegionID)
select pgau.ProjectUpdateBatchID, pgau.GeospatialAreaID
from dbo.ProjectGeospatialAreaUpdate as pgau 
join dbo.GeospatialArea as ga 
on pgau.GeospatialAreaID = ga.GeospatialAreaID 
where ga.GeospatialAreaTypeID = @GeospatialAreaTypeID;

delete pgau from dbo.ProjectGeospatialAreaUpdate as pgau
join dbo.GeospatialArea as b on pgau.GeospatialAreaID = b.GeospatialAreaID
where b.GeospatialAreaTypeID = @GeospatialAreaTypeID;

delete pga from dbo.ProjectGeospatialArea as pga
join dbo.GeospatialArea as b on pga.GeospatialAreaID = b.GeospatialAreaID
where b.GeospatialAreaTypeID = @GeospatialAreaTypeID;

delete psga from dbo.PersonStewardGeospatialArea as psga
join dbo.GeospatialArea as b on psga.GeospatialAreaID = b.GeospatialAreaID
where b.GeospatialAreaTypeID = @GeospatialAreaTypeID;

update p
set p.NoRegionsExplanation = pgatn.Notes
from dbo.ProjectGeospatialAreaTypeNote pgatn
join dbo.Project p on pgatn.ProjectID = p.ProjectID and pgatn.GeospatialAreaTypeID = @GeospatialAreaTypeID

update p
set p.NoRegionsExplanation = pgatn.Notes
from dbo.ProjectGeospatialAreaTypeNoteUpdate pgatn
join dbo.ProjectUpdateBatch p on pgatn.ProjectUpdateBatchID = p.ProjectUpdateBatchID and pgatn.GeospatialAreaTypeID = @GeospatialAreaTypeID


delete from dbo.ProjectGeospatialAreaTypeNote where GeospatialAreaTypeID = @GeospatialAreaTypeID;
delete from dbo.ProjectGeospatialAreaTypeNoteUpdate where GeospatialAreaTypeID = @GeospatialAreaTypeID;

delete from dbo.GeospatialArea where GeospatialAreaTypeID = @GeospatialAreaTypeID;

delete from dbo.GeospatialAreaType where GeospatialAreaTypeID = @GeospatialAreaTypeID;
