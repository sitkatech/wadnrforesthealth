if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerPclBoundaryLine'))
	drop view dbo.[vGeoServerPclBoundaryLine]
go

create view [dbo].[vGeoServerPclBoundaryLine]
as
select
	bl.PclBoundaryLineID,
	bl.PclBoundaryLineID as PrimaryKey,
	bl.PriorityLandscapeID,
	bl.Feature,
	bl.Feature as Ogr_Geometry
	
from
	dbo.PclBoundaryLine as bl
GO
