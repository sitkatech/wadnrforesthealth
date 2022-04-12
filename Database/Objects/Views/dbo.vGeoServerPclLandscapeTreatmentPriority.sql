if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerPclLandscapeTreatmentPriority'))
	drop view dbo.[vGeoServerPclLandscapeTreatmentPriority]
go

create view [dbo].[vGeoServerPclLandscapeTreatmentPriority]
as
select
	ltp.PclLandscapeTreatmentPriorityID,
	IsNull(ltp.PclLandscapeTreatmentPriorityID, -1) as PrimaryKey,
	ltp.PriorityLandscapeID,
	ltp.Color,
	COALESCE (ltp.Feature, '') as Feature,
	COALESCE (ltp.Feature, '') as Ogr_Geometry	
	
from
	dbo.PclLandscapeTreatmentPriority as ltp
GO
