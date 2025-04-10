if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerPclWildfireResponseBenefit'))
	drop view dbo.[vGeoServerPclWildfireResponseBenefit]
go

create view [dbo].[vGeoServerPclWildfireResponseBenefit]
as
select
	wrb.PclWildfireResponseBenefitID,
	IsNull(wrb.PclWildfireResponseBenefitID, -1) as PrimaryKey,
	wrb.PriorityLandscapeID,
	wrb.Color,
	COALESCE (wrb.Feature, '') as Feature,
	COALESCE (wrb.Feature, '') as Ogr_Geometry	
from
	dbo.PclWildfireResponseBenefit as wrb
GO
