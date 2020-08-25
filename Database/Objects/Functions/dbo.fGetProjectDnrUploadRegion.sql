IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.fGetProjectDnrUploadRegion'))
drop function dbo.fGetProjectDnrUploadRegion
GO
CREATE FUNCTION dbo.fGetProjectDnrUploadRegion
(
    @piGisUploadAttemptID int
)
returns table
as return(


    select number = ROW_NUMBER() OVER (ORDER BY x.ProjectID, x.DNRUplandRegionID), x.ProjectID, x.DNRUplandRegionID 
    
    from(

    select distinct x.ProjectID, region.DNRUplandRegionID
    
    from dbo.TreatmentArea ta
        join (
        select distinct p.CreateGisUploadAttemptID,p.ProjectID, ta.TreatmentAreaID from dbo.Treatment t
        join dbo.TreatmentArea ta on t.TreatmentAreaID = ta.TreatmentAreaID
        join dbo.Project p on t.ProjectID = p.ProjectID
        ) x on x.TreatmentAreaID = ta.TreatmentAreaID
        join dbo.DNRUplandRegion region on  region.DNRUplandRegionLocation.STIntersects(ta.TreatmentAreaFeature) = 1
        where @piGisUploadAttemptID = x.CreateGisUploadAttemptID
        ) x

)
go

