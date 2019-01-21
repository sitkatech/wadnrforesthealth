SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectRegionUpdate](
	[ProjectRegionUpdateID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectUpdateBatchID] [int] NOT NULL,
	[RegionID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectRegionUpdate_ProjectRegionUpdateID] PRIMARY KEY CLUSTERED 
(
	[ProjectRegionUpdateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ProjectRegionUpdate_ProjectUpdateBatchID_RegionID] UNIQUE NONCLUSTERED 
(
	[ProjectUpdateBatchID] ASC,
	[RegionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectRegionUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectRegionUpdate_ProjectUpdateBatch_ProjectUpdateBatchID] FOREIGN KEY([ProjectUpdateBatchID])
REFERENCES [dbo].[ProjectUpdateBatch] ([ProjectUpdateBatchID])
GO
ALTER TABLE [dbo].[ProjectRegionUpdate] CHECK CONSTRAINT [FK_ProjectRegionUpdate_ProjectUpdateBatch_ProjectUpdateBatchID]
GO
ALTER TABLE [dbo].[ProjectRegionUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectRegionUpdate_Region_RegionID] FOREIGN KEY([RegionID])
REFERENCES [dbo].[Region] ([RegionID])
GO
ALTER TABLE [dbo].[ProjectRegionUpdate] CHECK CONSTRAINT [FK_ProjectRegionUpdate_Region_RegionID]