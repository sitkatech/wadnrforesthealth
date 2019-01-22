if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerRegion'))
	drop view dbo.vGeoServerRegion
go

create view dbo.vGeoServerRegion
as
select
	r.RegionID,
	r.RegionID as PrimaryKey,
	r.RegionName,
	r.RegionLocation,
	r.RegionLocation as Ogr_Geometry
	
from
	dbo.Region as r
