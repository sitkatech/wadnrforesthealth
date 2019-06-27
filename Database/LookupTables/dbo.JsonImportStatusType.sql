delete from dbo.JsonImportStatusType
go

insert into dbo.JsonImportStatusType(JsonImportStatusTypeID, JsonImportStatusTypeName)
values
(1, 'NotYetProcessed'),
(2, 'ProcessingFailed'),
(3, 'ProcessingSuceeded'),
(4, 'ProcessingIndeterminate')