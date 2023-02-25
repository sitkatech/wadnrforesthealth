
CREATE TABLE [dbo].ArcOnlineFinanceApiRawJsonImportTableType(
	[ArcOnlineFinanceApiRawJsonImportTableTypeID] [int] NOT NULL,
	[ArcOnlineFinanceApiRawJsonImportTableTypeName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_ArcOnlineFinanceApiRawJsonImportTableType_ArcOnlineFinanceApiRawJsonImportTableTypeID] PRIMARY KEY CLUSTERED 
(
	[ArcOnlineFinanceApiRawJsonImportTableTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_ArcOnlineFinanceApiRawJsonImportTableType_ArcOnlineFinanceApiRawJsonImportTableTypeName] UNIQUE NONCLUSTERED 
(
	[ArcOnlineFinanceApiRawJsonImportTableTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO




CREATE TABLE [dbo].[ArcOnlineFinanceApiRawJsonImport](
	[ArcOnlineFinanceApiRawJsonImportID] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[ArcOnlineFinanceApiRawJsonImportTableTypeID] [int] NOT NULL,
	[BienniumFiscalYear] [int] NULL,
	[FinanceApiLastLoadDate] [datetime] NULL,
	[RawJsonString] [varchar](max) NOT NULL,
	[JsonImportDate] [datetime] NULL,
	[JsonImportStatusTypeID] [int] NOT NULL,
 CONSTRAINT [PK_ArcOnlineFinanceApiRawJsonImport_ArcOnlineFinanceApiRawJsonImportID] PRIMARY KEY CLUSTERED 
(
	[ArcOnlineFinanceApiRawJsonImportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ArcOnlineFinanceApiRawJsonImport]  WITH CHECK ADD  CONSTRAINT [FK_ArcOnlineFinanceApiRawJsonImport_JsonImportStatusType_JsonImportStatusTypeID] FOREIGN KEY([JsonImportStatusTypeID])
REFERENCES [dbo].[JsonImportStatusType] ([JsonImportStatusTypeID])
GO

ALTER TABLE [dbo].[ArcOnlineFinanceApiRawJsonImport] CHECK CONSTRAINT [FK_ArcOnlineFinanceApiRawJsonImport_JsonImportStatusType_JsonImportStatusTypeID]
GO

ALTER TABLE [dbo].[ArcOnlineFinanceApiRawJsonImport]  WITH CHECK ADD  CONSTRAINT [FK_ArcOnlineFinanceApiRawJsonImport_ArcOnlineFinanceApiRawJsonImportTableType_ArcOnlineFinanceApiRawJsonImportTableTypeID] FOREIGN KEY([ArcOnlineFinanceApiRawJsonImportTableTypeID])
REFERENCES [dbo].[ArcOnlineFinanceApiRawJsonImportTableType] ([ArcOnlineFinanceApiRawJsonImportTableTypeID])
GO

ALTER TABLE [dbo].[ArcOnlineFinanceApiRawJsonImport] CHECK CONSTRAINT [FK_ArcOnlineFinanceApiRawJsonImport_ArcOnlineFinanceApiRawJsonImportTableType_ArcOnlineFinanceApiRawJsonImportTableTypeID]
GO

