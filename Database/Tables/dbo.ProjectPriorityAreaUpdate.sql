SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectPriorityAreaUpdate](
	[ProjectPriorityAreaUpdateID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectUpdateBatchID] [int] NOT NULL,
	[PriorityAreaID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectPriorityAreaUpdate_ProjectPriorityAreaUpdateID] PRIMARY KEY CLUSTERED 
(
	[ProjectPriorityAreaUpdateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ProjectPriorityAreaUpdate_ProjectUpdateBatchID_PriorityAreaID] UNIQUE NONCLUSTERED 
(
	[ProjectUpdateBatchID] ASC,
	[PriorityAreaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectPriorityAreaUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPriorityAreaUpdate_PriorityArea_PriorityAreaID] FOREIGN KEY([PriorityAreaID])
REFERENCES [dbo].[PriorityArea] ([PriorityAreaID])
GO
ALTER TABLE [dbo].[ProjectPriorityAreaUpdate] CHECK CONSTRAINT [FK_ProjectPriorityAreaUpdate_PriorityArea_PriorityAreaID]
GO
ALTER TABLE [dbo].[ProjectPriorityAreaUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPriorityAreaUpdate_ProjectUpdateBatch_ProjectUpdateBatchID] FOREIGN KEY([ProjectUpdateBatchID])
REFERENCES [dbo].[ProjectUpdateBatch] ([ProjectUpdateBatchID])
GO
ALTER TABLE [dbo].[ProjectPriorityAreaUpdate] CHECK CONSTRAINT [FK_ProjectPriorityAreaUpdate_ProjectUpdateBatch_ProjectUpdateBatchID]