if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerWashingtonCounty'))
	drop view dbo.vGeoServerWashingtonCounty
go

create view dbo.vGeoServerWashingtonCounty
as
select
    wc.WashingtonCountyID,
	wc.WashingtonCountyID as PrimaryKey,
	wc.WashingtonCountyFullName,
    wc.WashingtonCountyLocation,
	wc.WashingtonCountyLocation as Ogr_Geometry
	
from
	dbo.WashingtonCounty as wc
GO
