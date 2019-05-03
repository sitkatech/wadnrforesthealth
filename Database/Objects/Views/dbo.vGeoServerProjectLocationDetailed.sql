if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerProjectLocationDetailed'))
	drop view dbo.vGeoServerProjectLocationDetailed
go

create view dbo.vGeoServerProjectLocationDetailed
as
select
	p.ProjectID,
	pl.ProjectLocationID as PrimaryKey,
	p.ProjectName,
	plt.ProjectLocationTypeDisplayName,
	pl.ProjectLocationGeometry
from
	dbo.Project as p 
	join dbo.ProjectLocation as pl on p.ProjectID = pl.ProjectID
	join dbo.ProjectLocationType as plt on pl.ProjectLocationTypeID = plt.ProjectLocationTypeID
where pl.ProjectLocationGeometry is not null