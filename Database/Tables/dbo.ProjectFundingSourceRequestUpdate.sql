SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectFundingSourceRequestUpdate](
	[ProjectFundingSourceRequestUpdateID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectUpdateBatchID] [int] NOT NULL,
	[FundingSourceID] [int] NOT NULL,
	[SecuredAmount] [money] NULL,
	[UnsecuredAmount] [money] NULL,
	[GrantAllocationID] [int] NULL,
 CONSTRAINT [PK_ProjectFundingSourceRequestUpdate_ProjectFundingSourceRequestUpdateID] PRIMARY KEY CLUSTERED 
(
	[ProjectFundingSourceRequestUpdateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ProjectFundingSourceRequestUpdate_ProjectUpdateBatchID_FundingSourceID] UNIQUE NONCLUSTERED 
(
	[ProjectUpdateBatchID] ASC,
	[FundingSourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectFundingSourceRequestUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectFundingSourceRequestUpdate_FundingSource_FundingSourceID] FOREIGN KEY([FundingSourceID])
REFERENCES [dbo].[FundingSource] ([FundingSourceID])
GO
ALTER TABLE [dbo].[ProjectFundingSourceRequestUpdate] CHECK CONSTRAINT [FK_ProjectFundingSourceRequestUpdate_FundingSource_FundingSourceID]
GO
ALTER TABLE [dbo].[ProjectFundingSourceRequestUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectFundingSourceRequestUpdate_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO
ALTER TABLE [dbo].[ProjectFundingSourceRequestUpdate] CHECK CONSTRAINT [FK_ProjectFundingSourceRequestUpdate_GrantAllocation_GrantAllocationID]
GO
ALTER TABLE [dbo].[ProjectFundingSourceRequestUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectFundingSourceRequestUpdate_ProjectUpdateBatch_ProjectUpdateBatchID] FOREIGN KEY([ProjectUpdateBatchID])
REFERENCES [dbo].[ProjectUpdateBatch] ([ProjectUpdateBatchID])
GO
ALTER TABLE [dbo].[ProjectFundingSourceRequestUpdate] CHECK CONSTRAINT [FK_ProjectFundingSourceRequestUpdate_ProjectUpdateBatch_ProjectUpdateBatchID]
GO
ALTER TABLE [dbo].[ProjectFundingSourceRequestUpdate]  WITH CHECK ADD  CONSTRAINT [CK_ProjectFundingSourceRequestUpdate_SecuredUnsecuredAmountBothCannotBeZero] CHECK  (([SecuredAmount]<>(0) OR [UnsecuredAmount]<>(0)))
GO
ALTER TABLE [dbo].[ProjectFundingSourceRequestUpdate] CHECK CONSTRAINT [CK_ProjectFundingSourceRequestUpdate_SecuredUnsecuredAmountBothCannotBeZero]