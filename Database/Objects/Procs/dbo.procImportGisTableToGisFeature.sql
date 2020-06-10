IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.procImportGisTableToGisFeature'))
    drop procedure dbo.procImportGisTableToGisFeature
go
create procedure dbo.procImportGisTableToGisFeature
(
    @piGisUploadAttemptID int
)
as
 
insert into dbo.GisFeature (GisUploadAttemptID, GisFeatureGeometry, GisImportFeatureKey) 
select @piGisUploadAttemptID, Shape, GisImportFeatureID from dbo.gisimport
 
go  

/*

exec dbo.procImportGisTableToGisFeature @piGisUploadAttemptID = 1

*/