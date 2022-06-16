
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
select uta.*, ta.TreatmentAreaFeature
from UniqueTreatmentAreas uta
	inner join TreatmentArea ta on ta.TreatmentAreaID = uta.TreatmentAreaID
