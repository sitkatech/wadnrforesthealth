SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectGrantAllocationExpenditureUpdate](
	[ProjectGrantAllocationExpenditureUpdateID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectUpdateBatchID] [int] NOT NULL,
	[FundingSourceID] [int] NULL,
	[CalendarYear] [int] NOT NULL,
	[ExpenditureAmount] [money] NOT NULL,
	[GrantAllocationID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectGrantAllocationExpenditureUpdate_ProjectGrantAllocationExpenditureUpdateID] PRIMARY KEY CLUSTERED 
(
	[ProjectGrantAllocationExpenditureUpdateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ProjectGrantAllocationExpenditureUpdate_ProjectUpdateBatchID_FundingSourceID_CalendarYear] UNIQUE NONCLUSTERED 
(
	[ProjectUpdateBatchID] ASC,
	[FundingSourceID] ASC,
	[CalendarYear] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectGrantAllocationExpenditureUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectGrantAllocationExpenditureUpdate_FundingSource_FundingSourceID] FOREIGN KEY([FundingSourceID])
REFERENCES [dbo].[FundingSource] ([FundingSourceID])
GO
ALTER TABLE [dbo].[ProjectGrantAllocationExpenditureUpdate] CHECK CONSTRAINT [FK_ProjectGrantAllocationExpenditureUpdate_FundingSource_FundingSourceID]
GO
ALTER TABLE [dbo].[ProjectGrantAllocationExpenditureUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectGrantAllocationExpenditureUpdate_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO
ALTER TABLE [dbo].[ProjectGrantAllocationExpenditureUpdate] CHECK CONSTRAINT [FK_ProjectGrantAllocationExpenditureUpdate_GrantAllocation_GrantAllocationID]
GO
ALTER TABLE [dbo].[ProjectGrantAllocationExpenditureUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectGrantAllocationExpenditureUpdate_ProjectUpdateBatch_ProjectUpdateBatchID] FOREIGN KEY([ProjectUpdateBatchID])
REFERENCES [dbo].[ProjectUpdateBatch] ([ProjectUpdateBatchID])
GO
ALTER TABLE [dbo].[ProjectGrantAllocationExpenditureUpdate] CHECK CONSTRAINT [FK_ProjectGrantAllocationExpenditureUpdate_ProjectUpdateBatch_ProjectUpdateBatchID]