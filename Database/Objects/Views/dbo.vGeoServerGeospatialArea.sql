if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerGeospatialArea'))
	drop view dbo.vGeoServerGeospatialArea
go

create view dbo.vGeoServerGeospatialArea
as
select
	ga.GeospatialAreaID,
	ga.GeospatialAreaID as PrimaryKey,
	ga.GeospatialAreaName,
	ga.GeospatialAreaFeature,
	ga.GeospatialAreaFeature as Ogr_Geometry,
	gat.GeospatialAreaTypeName
	
from
	dbo.GeospatialArea ga
	join dbo.GeospatialAreaType gat on ga.GeospatialAreaTypeID = gat.GeospatialAreaTypeID
