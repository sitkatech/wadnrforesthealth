SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantAllocationAwardLandownerCostShareLineItem](
	[GrantAllocationAwardLandownerCostShareLineItemID] [int] IDENTITY(1,1) NOT NULL,
	[GrantAllocationAwardID] [int] NULL,
	[ProjectID] [int] NOT NULL,
	[LandownerCostShareLineItemStatusID] [int] NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemNotes] [varchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GrantAllocationAwardLandownerCostShareLineItemAllocatedAmount] [money] NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemTotalCost] [money] NOT NULL,
 CONSTRAINT [PK_GrantAllocationAwardLandownerCostShareLineItem_GrantAllocationAwardLandownerCostShareLineItemID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationAwardLandownerCostShareLineItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GrantAllocationAwardLandownerCostShareLineItem]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationAwardLandownerCostShareLineItem_GrantAllocationAward_GrantAllocationAwardID] FOREIGN KEY([GrantAllocationAwardID])
REFERENCES [dbo].[GrantAllocationAward] ([GrantAllocationAwardID])
GO
ALTER TABLE [dbo].[GrantAllocationAwardLandownerCostShareLineItem] CHECK CONSTRAINT [FK_GrantAllocationAwardLandownerCostShareLineItem_GrantAllocationAward_GrantAllocationAwardID]
GO
ALTER TABLE [dbo].[GrantAllocationAwardLandownerCostShareLineItem]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationAwardLandownerCostShareLineItem_LandownerCostShareLineItemStatus_LandownerCostShareLineItemStatusID] FOREIGN KEY([LandownerCostShareLineItemStatusID])
REFERENCES [dbo].[LandownerCostShareLineItemStatus] ([LandownerCostShareLineItemStatusID])
GO
ALTER TABLE [dbo].[GrantAllocationAwardLandownerCostShareLineItem] CHECK CONSTRAINT [FK_GrantAllocationAwardLandownerCostShareLineItem_LandownerCostShareLineItemStatus_LandownerCostShareLineItemStatusID]
GO
ALTER TABLE [dbo].[GrantAllocationAwardLandownerCostShareLineItem]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationAwardLandownerCostShareLineItem_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[GrantAllocationAwardLandownerCostShareLineItem] CHECK CONSTRAINT [FK_GrantAllocationAwardLandownerCostShareLineItem_Project_ProjectID]