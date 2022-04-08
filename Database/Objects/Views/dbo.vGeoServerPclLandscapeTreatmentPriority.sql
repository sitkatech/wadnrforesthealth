if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerPclLandscapeTreatmentPriority'))
	drop view dbo.[vGeoServerPclLandscapeTreatmentPriority]
go

create view [dbo].[vGeoServerPclLandscapeTreatmentPriority]
as
select
	ltp.PclLandscapeTreatmentPriorityID,
	ltp.PclLandscapeTreatmentPriorityID as PrimaryKey,
	ltp.PriorityLandscapeID,
	ltp.Color,
	ltp.Feature,
	ltp.Feature as Ogr_Geometry
	
from
	dbo.PclLandscapeTreatmentPriority as ltp
GO
