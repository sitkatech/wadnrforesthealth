if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerPclWildfireResponseBenefit'))
	drop view dbo.[vGeoServerPclWildfireResponseBenefit]
go

create view [dbo].[vGeoServerPclWildfireResponseBenefit]
as
select
	wrb.PclWildfireResponseBenefitID,
	wrb.PclWildfireResponseBenefitID as PrimaryKey,
	wrb.PriorityLandscapeID,
	wrb.Color,
	wrb.Feature,
	wrb.Feature as Ogr_Geometry
	
from
	dbo.PclWildfireResponseBenefit as wrb
GO
