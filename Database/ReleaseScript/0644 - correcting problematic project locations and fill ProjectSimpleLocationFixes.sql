create table ProjectSimpleLocationFixes
(
    ProjectID int not null,
    OldProjectLocationPoint geometry null,
    NewProjectLocationPoint geometry not null,
    UpdatedDate datetime not null, 
    CONSTRAINT [PK_ProjectSimpleLocationFixes_ProjectID] PRIMARY KEY CLUSTERED 
    (
	    ProjectID ASC
    )
)

ALTER TABLE dbo.ProjectSimpleLocationFixes  WITH CHECK ADD  CONSTRAINT [FK_ProjectSimpleLocationFixes_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES dbo.Project (ProjectID)
GO

drop table if exists #tmpA
select  p.ProjectID,
    p.ProjectName, 
    p.ProjectLocationPoint.STDistance(l.ProjectLocationGeometry.STCentroid()) as Distance,
    p.ProjectLocationPoint, l.ProjectLocationGeometry, l.ProjectLocationGeometry.STCentroid() as BetterPoint
into #tmpA
from dbo.Project p
join (
    SELECT l.ProjectID, geometry::UnionAggregate(l.ProjectLocationGeometry) as ProjectLocationGeometry
    from dbo.vGeoServerProjectLocationDetailed l
    group by l.ProjectID
) l on p.ProjectID = l.ProjectID
where p.ProjectLocationPoint.STIntersects(l.ProjectLocationGeometry) = 0
and p.ProjectLocationPoint.STDistance(l.ProjectLocationGeometry.STCentroid())
> 0.001 

insert into ProjectSimpleLocationFixes
    (ProjectID, OldProjectLocationPoint, NewProjectLocationPoint, UpdatedDate)
select x.ProjectID, p.ProjectLocationPoint as OldProjectLocationPoint, x.BetterPoint as NewProjectLocationPoint, getdate()
from #tmpA x
    join dbo.Project p on x.ProjectID = p.ProjectID 
order by x.ProjectID


update dbo.Project
set ProjectLocationPoint = BetterPoint
from #tmpA x
join dbo.Project p on x.ProjectID = p.ProjectID 
where p.CreateGisUploadAttemptID is not null

