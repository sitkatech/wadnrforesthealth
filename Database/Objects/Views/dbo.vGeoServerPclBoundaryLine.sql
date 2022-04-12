if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerPclBoundaryLine'))
	drop view dbo.[vGeoServerPclBoundaryLine]
go

create view [dbo].[vGeoServerPclBoundaryLine]
as
select
	bl.PclBoundaryLineID,
	IsNull(bl.PclBoundaryLineID, -1) as PrimaryKey,
	bl.PriorityLandscapeID,
	COALESCE (bl.Feature, '') as Feature,
	COALESCE (bl.Feature, '') as Ogr_Geometry	
from
	dbo.PclBoundaryLine as bl
GO
