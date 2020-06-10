IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.procImportGisTableToGisFeature'))
    drop procedure dbo.procImportGisTableToGisFeature
go
create procedure dbo.procImportGisTableToGisFeature
(
    @tableNameToImportFrom varchar(100),
    @piGisUploadAttemptID int
)
as

DECLARE @SQL nvarchar(1000)
 
SET @SQL = 'insert into dbo.GisFeature (GisUploadAttemptID, GisFeatureGeometry, GisImportFeatureKey) select ' + cast(@piGisUploadAttemptID as varchar) + ', Shape, GisImportFeatureID from dbo.' + @tableNameToImportFrom
 
--select @sql

EXEC (@SQL)
  
go  

/*

exec dbo.procImportGisTableToGisFeature @tableNameToImportFrom = 'gisimport1' , @piGisUploadAttemptID = 1

*/