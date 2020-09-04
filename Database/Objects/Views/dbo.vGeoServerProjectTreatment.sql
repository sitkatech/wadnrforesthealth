if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerProjectTreatmentArea'))
	drop view dbo.vGeoServerProjectTreatmentArea
go

create view dbo.vGeoServerProjectTreatmentArea
as
select
	pa.TreatmentID,
	pa.TreatmentID as PrimaryKey,
	pa.ProjectID,
	p.ProjectName,
    p.FhtProjectNumber,
	ta.TreatmentAreaFeature as Ogr_Geometry,
    ta.TreatmentAreaFeature as TreatmentFeature
	
from
	dbo.Treatment as pa
    join dbo.Project p on pa.ProjectID = p.ProjectID
    join dbo.TreatmentArea ta on pa.TreatmentAreaID = ta.TreatmentAreaID
    where ta.TreatmentAreaFeature is not null



