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
	pa.TreatmentFeature as Ogr_Geometry,
    pa.TreatmentFeature
	
from
	dbo.Treatment as pa
    join dbo.Project p on pa.ProjectID = p.ProjectID
    where pa.TreatmentFeature is not null



