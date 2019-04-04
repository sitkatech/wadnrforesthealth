SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectGrantAllocationExpenditure](
	[ProjectGrantAllocationExpenditureID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[FundingSourceID] [int] NULL,
	[CalendarYear] [int] NOT NULL,
	[ExpenditureAmount] [money] NOT NULL,
	[GrantAllocationID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectGrantAllocationExpenditure_ProjectGrantAllocationExpenditureID] PRIMARY KEY CLUSTERED 
(
	[ProjectGrantAllocationExpenditureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ProjectGrantAllocationExpenditure_ProjectID_GrantAllocationID_CalendarYear] UNIQUE NONCLUSTERED 
(
	[ProjectID] ASC,
	[GrantAllocationID] ASC,
	[CalendarYear] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectGrantAllocationExpenditure]  WITH CHECK ADD  CONSTRAINT [FK_ProjectGrantAllocationExpenditure_FundingSource_FundingSourceID] FOREIGN KEY([FundingSourceID])
REFERENCES [dbo].[FundingSource] ([FundingSourceID])
GO
ALTER TABLE [dbo].[ProjectGrantAllocationExpenditure] CHECK CONSTRAINT [FK_ProjectGrantAllocationExpenditure_FundingSource_FundingSourceID]
GO
ALTER TABLE [dbo].[ProjectGrantAllocationExpenditure]  WITH CHECK ADD  CONSTRAINT [FK_ProjectGrantAllocationExpenditure_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO
ALTER TABLE [dbo].[ProjectGrantAllocationExpenditure] CHECK CONSTRAINT [FK_ProjectGrantAllocationExpenditure_GrantAllocation_GrantAllocationID]
GO
ALTER TABLE [dbo].[ProjectGrantAllocationExpenditure]  WITH CHECK ADD  CONSTRAINT [FK_ProjectGrantAllocationExpenditure_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[ProjectGrantAllocationExpenditure] CHECK CONSTRAINT [FK_ProjectGrantAllocationExpenditure_Project_ProjectID]