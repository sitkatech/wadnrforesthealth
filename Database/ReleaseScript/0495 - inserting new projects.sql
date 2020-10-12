
select gua.GisUploadAttemptID
, gf.GisFeatureGeometry
, x.GisFeatureMetadataAttributeValue
,RANK () OVER ( 
		ORDER BY gf.GisFeatureID
	) rank_no 
into #newProjectsToAdd
from dbo.GisUploadAttempt gua
join dbo.GisFeature gf on gf.GisUploadAttemptID = gua.GisUploadAttemptID and gua.GisUploadAttemptID = 5
join dbo.GisFeatureMetadataAttribute x on x.GisFeatureID = gf.GisFeatureID and x.GisMetadataAttributeID = 147
join dbo.StateProvince sp on sp.StateProvinceName = 'washington' and sp.StateProvinceFeature.STIntersects(gf.GisFeatureGeometry) = 1

left join (



select * from dbo.Project p
join dbo.GisUploadAttempt gua on p.CreateGisUploadAttemptID = gua.GisUploadAttemptID
where gua.GisUploadSourceOrganizationID = 2

) y on y.ProjectName = x.GisFeatureMetadataAttributeValue
where y.ProjectID is null


select * from #newProjectsToAdd


insert into dbo.Project (ProjectTypeID, ProjectStageID, ProjectName, IsFeatured, ProjectLocationSimpleTypeID, ProjectApprovalStatusID, FhtProjectNumber, CreateGisUploadAttemptID)

SELECT 2226, --integrated forest health
       3, --implementation
       x.GisFeatureMetadataAttributeValue,
       0,
       3, --none
       3, --approved

 'FHT-2020-' + RIGHT('000000'+CAST((x.rank_no + (select max( cast(replace(p.FhtProjectNumber,'fht-2020-','') as int)) from dbo.Project p ))  AS VARCHAR(6)),6)
 , x.GisUploadAttemptID
from #newProjectsToAdd x



insert into dbo.ProjectLocation (ProjectID, ProjectLocationTypeID, ProjectLocationGeometry, ProjectLocationName)
select p.ProjectID, 1, x.GisFeatureGeometry, 'Imported Project Location Name'
from #newProjectsToAdd x
join dbo.Project p on x.GisFeatureMetadataAttributeValue = p.ProjectName and x.GisUploadAttemptID = p.CreateGisUploadAttemptID