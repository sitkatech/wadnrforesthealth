SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantAllocationAwardTravelLineItem](
	[GrantAllocationAwardTravelLineItemID] [int] IDENTITY(1,1) NOT NULL,
	[GrantAllocationAwardID] [int] NOT NULL,
	[GrantAllocationAwardTravelLineItemTarOrMonth] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GrantAllocationAwardTravelLineItemDate] [datetime] NOT NULL,
	[GrantAllocationAwardTravelLineItemTypeID] [int] NOT NULL,
	[GrantAllocationAwardTravelLineItemMiles] [int] NULL,
	[GrantAllocationAwardTravelLineItemMileageRate] [money] NULL,
	[GrantAllocationAwardTravelLineItemAmount] [money] NULL,
	[GrantAllocationAwardTravelLineItemNotes] [varchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PersonID] [int] NULL,
 CONSTRAINT [PK_GrantAllocationAwardTravelLineItem_GrantAllocationAwardTravelLineItemID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationAwardTravelLineItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GrantAllocationAwardTravelLineItem]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationAwardTravelLineItem_GrantAllocationAward_GrantAllocationAwardID] FOREIGN KEY([GrantAllocationAwardID])
REFERENCES [dbo].[GrantAllocationAward] ([GrantAllocationAwardID])
GO
ALTER TABLE [dbo].[GrantAllocationAwardTravelLineItem] CHECK CONSTRAINT [FK_GrantAllocationAwardTravelLineItem_GrantAllocationAward_GrantAllocationAwardID]
GO
ALTER TABLE [dbo].[GrantAllocationAwardTravelLineItem]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationAwardTravelLineItem_GrantAllocationAwardTravelLineItemType_GrantAllocationAwardTravelLineItemTypeID] FOREIGN KEY([GrantAllocationAwardTravelLineItemTypeID])
REFERENCES [dbo].[GrantAllocationAwardTravelLineItemType] ([GrantAllocationAwardTravelLineItemTypeID])
GO
ALTER TABLE [dbo].[GrantAllocationAwardTravelLineItem] CHECK CONSTRAINT [FK_GrantAllocationAwardTravelLineItem_GrantAllocationAwardTravelLineItemType_GrantAllocationAwardTravelLineItemTypeID]
GO
ALTER TABLE [dbo].[GrantAllocationAwardTravelLineItem]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationAwardTravelLineItem_Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[GrantAllocationAwardTravelLineItem] CHECK CONSTRAINT [FK_GrantAllocationAwardTravelLineItem_Person_PersonID]