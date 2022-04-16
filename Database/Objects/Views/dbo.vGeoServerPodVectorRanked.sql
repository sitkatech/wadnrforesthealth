if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerPodVectorRanked'))
	drop view dbo.[vGeoServerPodVectorRanked]
go

create view [dbo].[vGeoServerPodVectorRanked]
as
select
	pvr.PodVectorRankedID,
	IsNull(pvr.PodVectorRankedID, -1) as PrimaryKey,
	pvr.PriorityLandscapeID,
	pvr.Color,
	COALESCE (pvr.Feature, '') as Feature,
	COALESCE (pvr.Feature, '') as Ogr_Geometry	
	
from
	dbo.PodVectorRanked as pvr
GO
