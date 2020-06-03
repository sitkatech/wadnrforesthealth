if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerPriorityLandscape'))
	drop view dbo.vGeoServerPriorityLandscape
go

create view dbo.vGeoServerPriorityLandscape
as
select
	pa.PriorityLandscapeID,
	pa.PriorityLandscapeID as PrimaryKey,
	pa.PriorityLandscapeName,
	pa.PriorityLandscapeLocation,
	pa.PriorityLandscapeLocation as Ogr_Geometry
	
from
	dbo.PriorityLandscape as pa
