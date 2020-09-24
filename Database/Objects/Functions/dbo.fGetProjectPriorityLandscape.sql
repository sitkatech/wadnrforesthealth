IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.fGetProjectPriorityLandscape'))
drop function dbo.fGetProjectPriorityLandscape
GO
CREATE FUNCTION dbo.fGetProjectPriorityLandscape
(
    @piGisUploadAttemptID int
)
returns table
as return(


select  number = ROW_NUMBER() OVER (ORDER BY x.ProjectID, x.PriorityLandscapeID), x.ProjectID, x.PriorityLandscapeID

from (

    select distinct x.ProjectID, landscape.PriorityLandscapeID 
    
    
    
    
    from dbo.TreatmentArea ta
        join (
        select distinct p.CreateGisUploadAttemptID, p.ProjectID, ta.TreatmentAreaID from dbo.Treatment t
        join dbo.TreatmentArea ta on t.TreatmentAreaID = ta.TreatmentAreaID
        join dbo.Project p on t.ProjectID = p.ProjectID
        ) x on x.TreatmentAreaID = ta.TreatmentAreaID
        join dbo.PriorityLandscape landscape on  landscape.PriorityLandscapeLocation.STIntersects(ta.TreatmentAreaFeature) = 1
        where @piGisUploadAttemptID = x.CreateGisUploadAttemptID
        



    union
    
     select distinct x.ProjectID, landscape.PriorityLandscapeID 

    from dbo.ProjectLocation ta
        join (
        select distinct p.CreateGisUploadAttemptID, p.ProjectID, pl.ProjectLocationID from dbo.ProjectLocation pl
        join dbo.Project p on pl.ProjectID = p.ProjectID
        ) x on x.ProjectLocationID = ta.ProjectLocationID
        join dbo.PriorityLandscape landscape on  landscape.PriorityLandscapeLocation.STIntersects(ta.ProjectLocationGeometry) = 1
        where @piGisUploadAttemptID = x.CreateGisUploadAttemptID
        ) x

        )
go



