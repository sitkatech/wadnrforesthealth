delete from dbo.GisUploadAttemptWorkflowSectionGrouping

insert into dbo.GisUploadAttemptWorkflowSectionGrouping (GisUploadAttemptWorkflowSectionGroupingID, GisUploadAttemptWorkflowSectionGroupingName, GisUploadAttemptWorkflowSectionGroupingDisplayName, SortOrder)
values
(1, 'GeospatialValidation', 'Geospatial Validation', 10),
(2, 'MetadataMapping', 'Metadata Mapping', 20)
