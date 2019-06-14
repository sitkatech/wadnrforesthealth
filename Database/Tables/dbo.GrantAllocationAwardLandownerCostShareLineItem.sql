SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantAllocationAwardLandownerCostShareLineItem](
	[GrantAllocationAwardLandownerCostShareLineItemID] [int] IDENTITY(1,1) NOT NULL,
	[GrantAllocationAwardID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemStatusID] [int] NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemStartDate] [datetime] NULL,
	[GrantAllocationAwardLandownerCostShareLineItemEndDate] [datetime] NULL,
	[GrantAllocationAwardLandownerCostShareLineItemFootprintAcres] [decimal](18, 0) NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemChippingAcres] [decimal](18, 0) NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemPruningAcres] [decimal](18, 0) NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemThinningAcres] [decimal](18, 0) NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemMasticationAcres] [decimal](18, 0) NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemGrazingAcres] [decimal](18, 0) NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemLopAndScatterAcres] [decimal](18, 0) NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemBiomassRemovalAcres] [decimal](18, 0) NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemHandPileAcres] [decimal](18, 0) NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemBroadcastBurnAcres] [decimal](18, 0) NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemHandPileBurnAcres] [decimal](18, 0) NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemMachinePileBurnAcres] [decimal](18, 0) NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemOtherTreatmentAcres] [decimal](18, 0) NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemSlashAcres] [decimal](18, 0) NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemNotes] [varchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_GrantAllocationAwardLandownerCostShareLineItem_GrantAllocationAwardLandownerCostShareLineItemID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationAwardLandownerCostShareLineItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GrantAllocationAwardLandownerCostShareLineItem]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationAwardLandownerCostShareLineItem_GrantAllocationAward_GrantAllocationAwardID] FOREIGN KEY([GrantAllocationAwardID])
REFERENCES [dbo].[GrantAllocationAward] ([GrantAllocationAwardID])
GO
ALTER TABLE [dbo].[GrantAllocationAwardLandownerCostShareLineItem] CHECK CONSTRAINT [FK_GrantAllocationAwardLandownerCostShareLineItem_GrantAllocationAward_GrantAllocationAwardID]
GO
ALTER TABLE [dbo].[GrantAllocationAwardLandownerCostShareLineItem]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationAwardLandownerCostShareLineItem_GrantAllocationAwardLandownerCostShareLineItemStatus_GrantAllocationAwardLando] FOREIGN KEY([GrantAllocationAwardLandownerCostShareLineItemStatusID])
REFERENCES [dbo].[GrantAllocationAwardLandownerCostShareLineItemStatus] ([GrantAllocationAwardLandownerCostShareLineItemStatusID])
GO
ALTER TABLE [dbo].[GrantAllocationAwardLandownerCostShareLineItem] CHECK CONSTRAINT [FK_GrantAllocationAwardLandownerCostShareLineItem_GrantAllocationAwardLandownerCostShareLineItemStatus_GrantAllocationAwardLando]
GO
ALTER TABLE [dbo].[GrantAllocationAwardLandownerCostShareLineItem]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationAwardLandownerCostShareLineItem_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[GrantAllocationAwardLandownerCostShareLineItem] CHECK CONSTRAINT [FK_GrantAllocationAwardLandownerCostShareLineItem_Project_ProjectID]