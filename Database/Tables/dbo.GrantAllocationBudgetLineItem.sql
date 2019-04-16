SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantAllocationBudgetLineItem](
	[GrantAllocationBudgetLineItemID] [int] IDENTITY(1,1) NOT NULL,
	[GrantAllocationID] [int] NOT NULL,
	[CostTypeID] [int] NOT NULL,
	[GrantAllocationBudgetLineItemAmount] [money] NOT NULL,
	[GrantAllocationBudgetLineItemNote] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_GrantAllocationBudgetLineItem_GrantAllocationBudgetLineItemID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationBudgetLineItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[GrantAllocationBudgetLineItem]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationBudgetLineItem_CostType_CostTypeID] FOREIGN KEY([CostTypeID])
REFERENCES [dbo].[CostType] ([CostTypeID])
GO
ALTER TABLE [dbo].[GrantAllocationBudgetLineItem] CHECK CONSTRAINT [FK_GrantAllocationBudgetLineItem_CostType_CostTypeID]
GO
ALTER TABLE [dbo].[GrantAllocationBudgetLineItem]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationBudgetLineItem_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO
ALTER TABLE [dbo].[GrantAllocationBudgetLineItem] CHECK CONSTRAINT [FK_GrantAllocationBudgetLineItem_GrantAllocation_GrantAllocationID]