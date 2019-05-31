SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantAllocationExpenditure](
	[GrantAllocationExpenditureID] [int] IDENTITY(1,1) NOT NULL,
	[GrantAllocationID] [int] NOT NULL,
	[CostTypeID] [int] NULL,
	[Biennium] [int] NOT NULL,
	[FiscalMonth] [int] NOT NULL,
	[CalendarYear] [int] NOT NULL,
	[CalendarMonth] [int] NOT NULL,
	[ExpenditureAmount] [money] NOT NULL,
 CONSTRAINT [PK_GrantAllocationExpenditure_GrantAllocationExpenditureID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationExpenditureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GrantAllocationExpenditure]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationExpenditure_CostType_CostTypeID] FOREIGN KEY([CostTypeID])
REFERENCES [dbo].[CostType] ([CostTypeID])
GO
ALTER TABLE [dbo].[GrantAllocationExpenditure] CHECK CONSTRAINT [FK_GrantAllocationExpenditure_CostType_CostTypeID]
GO
ALTER TABLE [dbo].[GrantAllocationExpenditure]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationExpenditure_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO
ALTER TABLE [dbo].[GrantAllocationExpenditure] CHECK CONSTRAINT [FK_GrantAllocationExpenditure_GrantAllocation_GrantAllocationID]