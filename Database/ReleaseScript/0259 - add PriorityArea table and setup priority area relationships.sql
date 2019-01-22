CREATE table dbo.PriorityArea (
	PriorityAreaID int not null constraint PK_PriorityArea_PriorityAreaID primary key,
	PriorityAreaName varchar(100) not null constraint AK_PriorityArea_PriorityAreaName unique,
	PriorityAreaLocation geometry
)

CREATE table dbo.ProjectPriorityArea(
	ProjectPriorityAreaID int not null identity(1,1) constraint PK_ProjectPriorityArea_ProjectPriorityAreaID primary key,
	ProjectID int not null constraint FK_ProjectPriorityArea_Project_ProjectID foreign key references dbo.Project(ProjectID),
	PriorityAreaID int not null constraint FK_ProjectPriorityArea_PriorityArea_PriorityAreaID foreign key references dbo.PriorityArea(PriorityAreaID),
	CONSTRAINT [AK_ProjectPriorityArea_ProjectID_PriorityAreaID] UNIQUE ([ProjectID], [PriorityAreaID])
)

CREATE table dbo.ProjectPriorityAreaUpdate(
	ProjectPriorityAreaUpdateID int not null identity(1,1) constraint PK_ProjectPriorityAreaUpdate_ProjectPriorityAreaUpdateID primary key,
	ProjectUpdateBatchID int not null constraint FK_ProjectPriorityAreaUpdate_ProjectUpdateBatch_ProjectUpdateBatchID foreign key references dbo.ProjectUpdateBatch(ProjectUpdateBatchID),
	PriorityAreaID int not null constraint FK_ProjectPriorityAreaUpdate_PriorityArea_PriorityAreaID foreign key references dbo.PriorityArea(PriorityAreaID),
	CONSTRAINT [AK_ProjectPriorityAreaUpdate_ProjectUpdateBatchID_PriorityAreaID] UNIQUE ([ProjectUpdateBatchID], [PriorityAreaID])
)

alter table dbo.Project add NoPriorityAreasExplanation varchar(4000);
alter table dbo.ProjectUpdateBatch add NoPriorityAreasExplanation varchar(4000);
GO

declare @GeospatialAreaTypeID int
set @GeospatialAreaTypeID = 12

insert into dbo.PriorityArea (PriorityAreaID, PriorityAreaName, PriorityAreaLocation)
select GeospatialAreaID, GeospatialAreaName, GeospatialAreaFeature from dbo.GeospatialArea where GeospatialAreaTypeID = @GeospatialAreaTypeID;

insert into dbo.ProjectPriorityArea (ProjectID, PriorityAreaID)
select pga.ProjectID, pga.GeospatialAreaID
from dbo.ProjectGeospatialArea as pga 
join dbo.GeospatialArea as ga 
on pga.GeospatialAreaID = ga.GeospatialAreaID
where ga.GeospatialAreaTypeID = @GeospatialAreaTypeID;


insert into dbo.ProjectPriorityAreaUpdate (ProjectUpdateBatchID, PriorityAreaID)
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
set p.NoPriorityAreasExplanation = pgatn.Notes
from dbo.ProjectGeospatialAreaTypeNote pgatn
join dbo.Project p on pgatn.ProjectID = p.ProjectID and pgatn.GeospatialAreaTypeID = @GeospatialAreaTypeID

update p
set p.NoPriorityAreasExplanation = pgatn.Notes
from dbo.ProjectGeospatialAreaTypeNoteUpdate pgatn
join dbo.ProjectUpdateBatch p on pgatn.ProjectUpdateBatchID = p.ProjectUpdateBatchID and pgatn.GeospatialAreaTypeID = @GeospatialAreaTypeID


delete from dbo.ProjectGeospatialAreaTypeNote where GeospatialAreaTypeID = @GeospatialAreaTypeID;
delete from dbo.ProjectGeospatialAreaTypeNoteUpdate where GeospatialAreaTypeID = @GeospatialAreaTypeID;

delete from dbo.GeospatialArea where GeospatialAreaTypeID = @GeospatialAreaTypeID;

delete from dbo.GeospatialAreaType where GeospatialAreaTypeID = @GeospatialAreaTypeID;

drop table dbo.ProjectGeospatialArea, dbo.ProjectGeospatialAreaTypeNote, dbo.ProjectGeospatialAreaUpdate, dbo.ProjectGeospatialAreaTypeNoteUpdate;
drop table dbo.PersonStewardGeospatialArea;
drop table dbo.GeospatialArea, dbo.GeospatialAreaType;


