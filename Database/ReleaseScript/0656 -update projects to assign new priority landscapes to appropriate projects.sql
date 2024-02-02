     
insert into dbo.ProjectPriorityLandscape(ProjectID, PriorityLandscapeID)	 
select 
	distinct p.ProjectID, landscape.PriorityLandscapeID 
from 
	dbo.ProjectLocation pl
    join dbo.Project as p on p.ProjectID = pl.ProjectID
	join dbo.PriorityLandscape landscape on landscape.PriorityLandscapeLocation.STIntersects(pl.ProjectLocationGeometry) = 1 or landscape.PriorityLandscapeLocation.STIntersects(p.ProjectLocationPoint) = 1
where landscape.PriorityLandscapeID in (7578,7579,7580,7581,7582,7583,7584,7585,7586)



