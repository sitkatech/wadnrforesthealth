
/*
select * from dbo.TreatmentArea --33,946


select * from dbo.ProjectLocation where ProjectLocationTypeID = 2 order by ProjectID

select * from dbo.ProjectLocationType


select * from dbo.Treatment

*/


	----34832 starts the dupes
	--INSERT INTO dbo.ProjectLocation(ProjectID, ProjectLocationGeometry, ProjectLocationTypeID, ProjectLocationName, ProgramID, ImportedFromGisUpload, TreatmentAreaID)
	--SELECT distinct --44,368
	--	t.ProjectID as ProjectID,
	--	--ta.TreatmentAreaFeature as ProjectLocationGeometry,
	--	2 as ProjectLocationTypeID,
	--	concat('Imported Project Location Name ', t.TreatmentAreaID) as ProjectLocationName,
	--	t.ProgramID as ProgramID,
	--	1 as ImportedFromGisUpload,
	--	ta.TreatmentAreaID
	--  FROM dbo.TreatmentArea as ta
	--  inner join dbo.Treatment as t on ta.TreatmentAreaID = t.TreatmentAreaID
	--  order by ProjectLocationName

alter table dbo.ProjectLocation
add TreatmentAreaID int null
go

;with UniqueTreatmentAreas as
(
	select distinct t.ProjectID as ProjectID,
		2 as ProjectLocationTypeID,
		concat('Imported Project Location Name ', t.TreatmentAreaID) as ProjectLocationName,
		t.ProgramID as ProgramID,
		1 as ImportedFromGisUpload,
		ta.TreatmentAreaID
	from dbo.TreatmentArea as ta
		inner join dbo.Treatment as t on ta.TreatmentAreaID = t.TreatmentAreaID
)
insert into ProjectLocation 
(ProjectID, ProjectLocationGeometry, ProjectLocationTypeID, ProjectLocationName, ProgramID, ImportedFromGisUpload, TreatmentAreaID)
select uta.ProjectID, ta.TreatmentAreaFeature as ProjectLocationGeometry, ProjectLocationTypeID, ProjectLocationName, ProgramID, ImportedFromGisUpload, uta.TreatmentAreaID
from UniqueTreatmentAreas uta
	inner join TreatmentArea ta on ta.TreatmentAreaID = uta.TreatmentAreaID


/*

add ProjectLocationID to Treatment table

insert ProjectLocationID where TreatmentAreaID matches

delete TreatmentAreaID from ProjectLocation table and Treatment Table(remove FKs too)

*/


alter table Treatment
add ProjectLocationID int constraint FK_Treatment_ProjectLocation_ProjectLocationID foreign key REFERENCES dbo.ProjectLocation(ProjectLocationID)
GO

update Treatment
set ProjectLocationID = pl.ProjectLocationID
from Treatment t
	inner join ProjectLocation pl on pl.TreatmentAreaID = t.TreatmentAreaID
where t.TreatmentAreaID is not null



ALTER TABLE [dbo].[Treatment] DROP CONSTRAINT [FK_Treatment_TreatmentArea_TreatmentAreaID]
GO

ALTER TABLE [dbo].[Treatment] DROP COLUMN [TreatmentAreaID]
GO

ALTER TABLE [dbo].ProjectLocation DROP COLUMN [TreatmentAreaID]
GO

ALTER TABLE dbo.Treatment   
ADD CONSTRAINT AK_Treatment_ProjectID_ProjectLocationID_TreatmentDetailedActivityTypeID UNIQUE (ProjectID, ProjectLocationID, TreatmentDetailedActivityTypeID); 



alter table dbo.ProjectLocation
add TemporaryTreatmentCacheID int null

drop table dbo.TreatmentArea