if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerPriorityArea'))
	drop view dbo.vGeoServerPriorityArea
go

create view dbo.vGeoServerPriorityArea
as
select
	pa.PriorityAreaID,
	pa.PriorityAreaID as PrimaryKey,
	pa.PriorityAreaName,
	pa.PriorityAreaLocation,
	pa.PriorityAreaLocation as Ogr_Geometry
	
from
	dbo.PriorityArea as pa
