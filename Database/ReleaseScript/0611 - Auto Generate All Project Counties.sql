IF NOT EXISTS (SELECT name FROM sysindexes WHERE name = 'SPATIAL_County_CountyFeature')
                          create spatial index [SPATIAL_County_CountyFeature] on [dbo].[County]([CountyFeature])
                          with (BOUNDING_BOX=(-125, 45, -116, 50))

IF NOT EXISTS (SELECT name FROM sysindexes WHERE name = 'SPATIAL_ProjectLocation_ProjectLocationGeometry')
                          create spatial index [SPATIAL_ProjectLocation_ProjectLocationGeometry] on [dbo].[ProjectLocation]([ProjectLocationGeometry])
                          with (BOUNDING_BOX=(-125, 45, -117, 50))

IF NOT EXISTS (SELECT name FROM sysindexes WHERE name = 'SPATIAL_Project_ProjectLocationPoint')
                          create spatial index [SPATIAL_Project_ProjectLocationPoint] on [dbo].[Project]([ProjectLocationPoint])
                          with (BOUNDING_BOX=(-125, 45, -117, 50))
			
delete from dbo.ProjectCounty

insert into dbo.ProjectCounty(ProjectID, CountyID)
select distinct projectID, CountyID from (select projectID, ProjectLocationGeometry from dbo.ProjectLocation
union all
select ProjectID, ProjectLocationPoint from dbo.Project where ProjectLocationPoint is not null) as geos
join County on County.CountyFeature.STIntersects(geos.ProjectLocationGeometry) = 1

update dbo.Project set NoCountiesExplanation = 'Neither the simple location nor the detailed location on this project intersects with any County.' 
where projectID not in (Select distinct ProjectID from ProjectCounty)

update dbo.Project set NoCountiesExplanation = null 
where projectID in (Select distinct ProjectID from ProjectCounty)
