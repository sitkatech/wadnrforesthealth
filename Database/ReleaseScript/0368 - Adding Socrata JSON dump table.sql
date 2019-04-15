
--begin tran

-- Type of the upload (vendor, etc.)
CREATE TABLE dbo.SocrataDataMartRawJsonImportTableType
(
    SocrataDataMartRawJsonImportTableTypeID [int] NOT NULL,
    SocrataDataMartRawJsonImportTableTypeName [varchar](100) NOT NULL
 CONSTRAINT [PK_SocrataDataMartRawJsonImportTableType_SocrataDataMartRawJsonImportTableTypeID] PRIMARY KEY CLUSTERED 
(
    SocrataDataMartRawJsonImportTableTypeID ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_SocrataDataMartRawJsonImportTableType_SocrataDataMartRawJsonImportTableTypeName] UNIQUE NONCLUSTERED 
(
    SocrataDataMartRawJsonImportTableTypeName ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO





CREATE TABLE dbo.SocrataDataMartRawJsonImport
(
    SocrataDataMartRawJsonImportID [int] IDENTITY(1,1) NOT NULL,
    CreateDate datetime not null,
    SocrataDataMartRawJsonImportTableTypeID int not null,
    RawJsonString varchar(max) not null
 CONSTRAINT [PK_SocrataDataMartRawJsonImport_SocrataDataMartRawJsonImportID] PRIMARY KEY CLUSTERED 
(
    SocrataDataMartRawJsonImportID ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

-- FK to SocrataDataMartRawJsonImportTableTypeID
ALTER TABLE [dbo].SocrataDataMartRawJsonImport  WITH CHECK ADD  CONSTRAINT [FK_SocrataDataMartRawJsonImport_SocrataDataMartRawJsonImportTableType_SocrataDataMartRawJsonImportTableTypeID] FOREIGN KEY(SocrataDataMartRawJsonImportTableTypeID)
REFERENCES [dbo].SocrataDataMartRawJsonImportTableType (SocrataDataMartRawJsonImportTableTypeID)
GO

-- Changing my mind about trying to adjust ProgramCodes. Re-introducing leading zeros.



update ProgramIndex
set ProgramIndexCode = '00' + ProgramIndexCode
GO

alter table ProgramIndex
add Subactivity varchar(max) null