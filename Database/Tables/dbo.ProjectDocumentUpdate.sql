SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectDocumentUpdate](
	[ProjectDocumentUpdateID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectUpdateBatchID] [int] NOT NULL,
	[FileResourceID] [int] NOT NULL,
	[DisplayName] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProjectDocumentTypeID] [int] NULL,
 CONSTRAINT [PK_ProjectDocumentUpdate_ProjectDocumentUpdateID] PRIMARY KEY CLUSTERED 
(
	[ProjectDocumentUpdateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_ProjectDocumentUpdate_DisplayName_ProjectUpdateBatchID] UNIQUE NONCLUSTERED 
(
	[DisplayName] ASC,
	[ProjectUpdateBatchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_ProjectDocumentUpdate_ProjectUpdateBatchID_FileResourceID] UNIQUE NONCLUSTERED 
(
	[ProjectUpdateBatchID] ASC,
	[FileResourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectDocumentUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectDocumentUpdate_FileResource_FileResourceID] FOREIGN KEY([FileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[ProjectDocumentUpdate] CHECK CONSTRAINT [FK_ProjectDocumentUpdate_FileResource_FileResourceID]
GO
ALTER TABLE [dbo].[ProjectDocumentUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectDocumentUpdate_ProjectDocumentType_ProjectDocumentTypeID] FOREIGN KEY([ProjectDocumentTypeID])
REFERENCES [dbo].[ProjectDocumentType] ([ProjectDocumentTypeID])
GO
ALTER TABLE [dbo].[ProjectDocumentUpdate] CHECK CONSTRAINT [FK_ProjectDocumentUpdate_ProjectDocumentType_ProjectDocumentTypeID]
GO
ALTER TABLE [dbo].[ProjectDocumentUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectDocumentUpdate_ProjectUpdateBatch_ProjectUpdateBatchID] FOREIGN KEY([ProjectUpdateBatchID])
REFERENCES [dbo].[ProjectUpdateBatch] ([ProjectUpdateBatchID])
GO
ALTER TABLE [dbo].[ProjectDocumentUpdate] CHECK CONSTRAINT [FK_ProjectDocumentUpdate_ProjectUpdateBatch_ProjectUpdateBatchID]