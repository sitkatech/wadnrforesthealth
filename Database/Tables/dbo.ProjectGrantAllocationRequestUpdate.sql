SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectGrantAllocationRequestUpdate](
	[ProjectGrantAllocationRequestUpdateID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectUpdateBatchID] [int] NOT NULL,
	[GrantAllocationID] [int] NOT NULL,
	[TotalAmount] [money] NULL,
	[MatchAmount] [money] NULL,
	[PayAmount] [money] NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[ImportedFromTabularData] [bit] NOT NULL,
 CONSTRAINT [PK_ProjectGrantAllocationRequestUpdate_ProjectGrantAllocationRequestUpdateID] PRIMARY KEY CLUSTERED 
(
	[ProjectGrantAllocationRequestUpdateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_ProjectGrantAllocationRequestUpdate_ProjectUpdateBatchID_GrantAllocationID] UNIQUE NONCLUSTERED 
(
	[ProjectUpdateBatchID] ASC,
	[GrantAllocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectGrantAllocationRequestUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectGrantAllocationRequestUpdate_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO
ALTER TABLE [dbo].[ProjectGrantAllocationRequestUpdate] CHECK CONSTRAINT [FK_ProjectGrantAllocationRequestUpdate_GrantAllocation_GrantAllocationID]
GO
ALTER TABLE [dbo].[ProjectGrantAllocationRequestUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectGrantAllocationRequestUpdate_ProjectUpdateBatch_ProjectUpdateBatchID] FOREIGN KEY([ProjectUpdateBatchID])
REFERENCES [dbo].[ProjectUpdateBatch] ([ProjectUpdateBatchID])
GO
ALTER TABLE [dbo].[ProjectGrantAllocationRequestUpdate] CHECK CONSTRAINT [FK_ProjectGrantAllocationRequestUpdate_ProjectUpdateBatch_ProjectUpdateBatchID]