
/*
select * from dbo.TreatmentArea --33,946


select * from dbo.ProjectLocation where ProjectLocationTypeID = 2 order by ProjectID

select * from dbo.ProjectLocationType


select * from dbo.Treatment

*/
	alter table dbo.ProjectLocation
	add TreatmentAreaID int null
	go

	--34832 starts the dupes
	INSERT INTO dbo.ProjectLocation(ProjectID, ProjectLocationGeometry, ProjectLocationTypeID, ProjectLocationName, ProgramID, ImportedFromGisUpload, TreatmentAreaID)
	SELECT --44,368
		t.ProjectID as ProjectID,
		ta.TreatmentAreaFeature as ProjectLocationGeometry,
		2 as ProjectLocationTypeID,
		concat('Imported Project Location Name ', t.TreatmentID) as ProjectLocationName,
		t.ProgramID as ProgramID,
		1 as ImportedFromGisUpload,
		ta.TreatmentAreaID
	  FROM dbo.TreatmentArea as ta
	  inner join dbo.Treatment as t on ta.TreatmentAreaID = t.TreatmentAreaID
	  order by ProjectLocationName

