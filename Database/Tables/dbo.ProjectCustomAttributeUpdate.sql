SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectCustomAttributeUpdate](
	[ProjectCustomAttributeUpdateID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectUpdateBatchID] [int] NOT NULL,
	[ProjectCustomAttributeTypeID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectCustomAttributeUpdate_ProjectCustomAttributeUpdateID] PRIMARY KEY CLUSTERED 
(
	[ProjectCustomAttributeUpdateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectCustomAttributeUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectCustomAttributeUpdate_ProjectCustomAttributeType_ProjectCustomAttributeTypeID] FOREIGN KEY([ProjectCustomAttributeTypeID])
REFERENCES [dbo].[ProjectCustomAttributeType] ([ProjectCustomAttributeTypeID])
GO
ALTER TABLE [dbo].[ProjectCustomAttributeUpdate] CHECK CONSTRAINT [FK_ProjectCustomAttributeUpdate_ProjectCustomAttributeType_ProjectCustomAttributeTypeID]
GO
ALTER TABLE [dbo].[ProjectCustomAttributeUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectCustomAttributeUpdate_ProjectUpdateBatch_ProjectUpdateBatchID] FOREIGN KEY([ProjectUpdateBatchID])
REFERENCES [dbo].[ProjectUpdateBatch] ([ProjectUpdateBatchID])
GO
ALTER TABLE [dbo].[ProjectCustomAttributeUpdate] CHECK CONSTRAINT [FK_ProjectCustomAttributeUpdate_ProjectUpdateBatch_ProjectUpdateBatchID]