IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.pClearGisImportTables'))
    DROP PROCEDURE dbo.pClearGisImportTables
GO

CREATE PROCEDURE dbo.pClearGisImportTables
AS
begin

    delete from dbo.GisFeatureMetadataAttribute;
    delete from dbo.GisFeature;
end

/*

exec dbo.pClearGisImportTables

*/

