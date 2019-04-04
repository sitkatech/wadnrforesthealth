SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectFundingSourceRequest](
	[ProjectFundingSourceRequestID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[FundingSourceID] [int] NULL,
	[SecuredAmount] [money] NULL,
	[UnsecuredAmount] [money] NULL,
	[GrantAllocationID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectFundingSourceRequest_ProjectFundingSourceRequestID] PRIMARY KEY CLUSTERED 
(
	[ProjectFundingSourceRequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ProjectFundingSourceRequest_ProjectID_FundingSourceID] UNIQUE NONCLUSTERED 
(
	[ProjectID] ASC,
	[FundingSourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectFundingSourceRequest]  WITH CHECK ADD  CONSTRAINT [FK_ProjectFundingSourceRequest_FundingSource_FundingSourceID] FOREIGN KEY([FundingSourceID])
REFERENCES [dbo].[FundingSource] ([FundingSourceID])
GO
ALTER TABLE [dbo].[ProjectFundingSourceRequest] CHECK CONSTRAINT [FK_ProjectFundingSourceRequest_FundingSource_FundingSourceID]
GO
ALTER TABLE [dbo].[ProjectFundingSourceRequest]  WITH CHECK ADD  CONSTRAINT [FK_ProjectFundingSourceRequest_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO
ALTER TABLE [dbo].[ProjectFundingSourceRequest] CHECK CONSTRAINT [FK_ProjectFundingSourceRequest_GrantAllocation_GrantAllocationID]
GO
ALTER TABLE [dbo].[ProjectFundingSourceRequest]  WITH CHECK ADD  CONSTRAINT [FK_ProjectFundingSourceRequest_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[ProjectFundingSourceRequest] CHECK CONSTRAINT [FK_ProjectFundingSourceRequest_Project_ProjectID]
GO
ALTER TABLE [dbo].[ProjectFundingSourceRequest]  WITH CHECK ADD  CONSTRAINT [CK_ProjectFundingSourceRequest_SecuredUnsecuredAmountBothCannotBeZero] CHECK  (([SecuredAmount]<>(0) OR [UnsecuredAmount]<>(0)))
GO
ALTER TABLE [dbo].[ProjectFundingSourceRequest] CHECK CONSTRAINT [CK_ProjectFundingSourceRequest_SecuredUnsecuredAmountBothCannotBeZero]