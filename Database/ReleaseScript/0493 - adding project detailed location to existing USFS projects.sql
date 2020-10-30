



select p.ProjectID, x.GisFeatureGeometry
into #existingProjectDetailedLocations
 from dbo.Project p
join dbo.GisUploadAttempt gua on p.CreateGisUploadAttemptID = gua.GisUploadAttemptID and gua.GisUploadSourceOrganizationID = 2
 join 

(
select x.GisFeatureMetadataAttributeValue, gf.GisFeatureGeometry from dbo.GisUploadAttempt gua
join dbo.GisFeature gf on gf.GisUploadAttemptID = gua.GisUploadAttemptID and gf.GisUploadAttemptID = 5
join dbo.GisFeatureMetadataAttribute x on x.GisFeatureID = gf.GisFeatureID and x.GisMetadataAttributeID = 147

) x on x.GisFeatureMetadataAttributeValue = p.ProjectName



insert into dbo.ProjectLocation (ProjectID, ProjectLocationTypeID, ProjectLocationGeometry, ProjectLocationName)
select ProjectID, 1, GisFeatureGeometry, 'Imported Project Location Name'
from #existingProjectDetailedLocations