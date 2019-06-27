-- Represent the status of a JSON import
CREATE TABLE [dbo].JsonImportStatusType
(
    JsonImportStatusTypeID [int] NOT NULL,
    JsonImportStatusTypeName [varchar](100) NOT NULL,
 CONSTRAINT [PK_JsonImportStatusType_JsonImportStatusTypeID] PRIMARY KEY CLUSTERED 
(
    JsonImportStatusTypeID ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_JsonImportStatusType_JsonImportStatusTypeName] UNIQUE NONCLUSTERED 
(
    JsonImportStatusTypeName ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


-- pre-load 
delete from dbo.JsonImportStatusType
go

insert into dbo.JsonImportStatusType(JsonImportStatusTypeID, JsonImportStatusTypeName)
values
(1, 'NotYetProcessed'),
(2, 'ProcessingFailed'),
(3, 'ProcessingSuceeded'),
(4, 'ProcessingIndeterminate')
GO

-- Add columnt to raw JSON import table
alter table dbo.SocrataDataMartRawJsonImport
add JsonImportStatusTypeID int null
GO

-- FK it
ALTER TABLE [dbo].[SocrataDataMartRawJsonImport]  WITH CHECK ADD  CONSTRAINT [FK_SocrataDataMartRawJsonImport_JsonImportStatusType_JsonImportStatusTypeID] FOREIGN KEY(JsonImportStatusTypeID)
REFERENCES [dbo].JsonImportStatusType (JsonImportStatusTypeID)
GO

-- Assume all previous records have been been successful, just for simplicity
update dbo.SocrataDataMartRawJsonImport 
set JsonImportStatusTypeID = 3
GO

-- Now we can tighten up the key to be not-null
alter table dbo.SocrataDataMartRawJsonImport
alter column JsonImportStatusTypeID int not null
GO
