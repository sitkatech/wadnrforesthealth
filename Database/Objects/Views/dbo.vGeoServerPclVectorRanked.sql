if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerPclVectorRanked'))
	drop view dbo.[vGeoServerPclVectorRanked]
go

create view [dbo].[vGeoServerPclVectorRanked]
as
select
	pvr.PclVectorRankedID,
	IsNull(pvr.PclVectorRankedID, -1) as PrimaryKey,
	pvr.PriorityLandscapeID,
	pvr.Color,
	COALESCE (pvr.Feature, '') as Feature,
	COALESCE (pvr.Feature, '') as Ogr_Geometry	
	
from
	dbo.PclVectorRanked as pvr
GO
