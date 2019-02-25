EXEC sp_RENAME 'dbo.FocusAreaLocationStaging.FocusAreaLocationStaggingID', 'FocusAreaLocationStagingID', 'COLUMN'

exec sp_rename 'FK_ProjectDocument_DisplayName_ProjectID', 'AK_ProjectDocument_DisplayName_ProjectID', 'OBJECT'
exec sp_rename 'FK_ProjectDocumentUpdate_DisplayName_ProjectUpdateBatchID', 'AK_ProjectDocumentUpdate_DisplayName_ProjectUpdateBatchID', 'OBJECT'