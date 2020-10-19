update dbo.Project
set ProjectLocationPoint = SimplePoint, ProjectLocationSimpleTypeID = 1
from dbo.Project x join (

select p.ProjectID,  geometry::UnionAggregate(ta.TreatmentAreaFeature).STCentroid() as SimplePoint  from dbo.Project p
join dbo.Treatment t on p.ProjectID = t.ProjectID
join dbo.TreatmentArea ta on ta.TreatmentAreaID = t.TreatmentAreaID
group by p.ProjectID) y on x.ProjectID = y.ProjectID


update dbo.Project
set ProjectLocationPoint = SimplePoint, ProjectLocationSimpleTypeID = 1
from dbo.Project x join (

select p.ProjectID,  geometry::UnionAggregate(pl.ProjectLocationGeometry).STCentroid() as SimplePoint  from dbo.Project p
join dbo.ProjectLocation pl on p.ProjectID = pl.ProjectID
where pl.ProjectLocationTypeID = 1
group by p.ProjectID) y on x.ProjectID = y.ProjectID



