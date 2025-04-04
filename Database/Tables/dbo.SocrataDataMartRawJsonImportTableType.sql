SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SocrataDataMartRawJsonImportTableType](
	[SocrataDataMartRawJsonImportTableTypeID] [int] NOT NULL,
	[SocrataDataMartRawJsonImportTableTypeName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_SocrataDataMartRawJsonImportTableType_SocrataDataMartRawJsonImportTableTypeID] PRIMARY KEY CLUSTERED 
(
	[SocrataDataMartRawJsonImportTableTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_SocrataDataMartRawJsonImportTableType_SocrataDataMartRawJsonImportTableTypeName] UNIQUE NONCLUSTERED 
(
	[SocrataDataMartRawJsonImportTableTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
