delete from dbo.GisUploadAttemptWorkflowSection

insert into dbo.GisUploadAttemptWorkflowSection (GisUploadAttemptWorkflowSectionID, GisUploadAttemptWorkflowSectionName, GisUploadAttemptWorkflowSectionDisplayName, SortOrder, HasCompletionStatus, GisUploadAttemptWorkflowSectionGroupingID)
values
(2, 'UploadGisFile', 'Upload GIS File', 20, 1, 1),
(3, 'ValidateFeatures', 'Validate Features', 30, 1, 1),
(4, 'ValidateMetadata', 'Validate Metadata', 40, 1, 2),
(6, 'ReviewMapping', 'Review Mapping', 60, 1, 2),
(7, 'RviewStagedImport', 'Review Staged Import', 70, 1, 2)