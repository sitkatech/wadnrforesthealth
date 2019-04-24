if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerProjectLocationDetailed'))
	drop view dbo.vGeoServerProjectLocationDetailed
go

create view dbo.vGeoServerProjectLocationDetailed
as
select
	p.ProjectID,
	pl.ProjectLocationID as PrimaryKey,
	p.ProjectName,
	pl.ProjectLocationGeometry
from
	dbo.Project as p join dbo.ProjectLocation as pl on p.ProjectID = pl.ProjectID
where pl.ProjectLocationGeometry is not null