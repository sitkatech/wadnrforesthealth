SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectDocumentType](
	[ProjectDocumentTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectDocumentTypeName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProjectDocumentTypeDescription] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProjectDocumentParentTypeID] [int] NULL,
 CONSTRAINT [PK_ProjectDocumentType_ProjectDocumentTypeID] PRIMARY KEY CLUSTERED 
(
	[ProjectDocumentTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_ProjectDocumentType_ProjectDocumentParentTypeID_ProjectDocumentTypeID] UNIQUE NONCLUSTERED 
(
	[ProjectDocumentParentTypeID] ASC,
	[ProjectDocumentTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectDocumentType]  WITH CHECK ADD  CONSTRAINT [FK_ProjectDocumentType_ProjectDocumentType_ProjectDocumentParentTypeID_ProjectDocumentTypeID] FOREIGN KEY([ProjectDocumentParentTypeID])
REFERENCES [dbo].[ProjectDocumentType] ([ProjectDocumentTypeID])
GO
ALTER TABLE [dbo].[ProjectDocumentType] CHECK CONSTRAINT [FK_ProjectDocumentType_ProjectDocumentType_ProjectDocumentParentTypeID_ProjectDocumentTypeID]