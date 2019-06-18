SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantAllocationAwardSuppliesLineItem](
	[GrantAllocationAwardSuppliesLineItemID] [int] IDENTITY(1,1) NOT NULL,
	[GrantAllocationAwardID] [int] NOT NULL,
	[GrantAllocationAwardSuppliesLineItemDescription] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GrantAllocationAwardSuppliesLineItemTarOrMonth] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GrantAllocationAwardSuppliesLineItemDate] [datetime] NOT NULL,
	[GrantAllocationAwardSuppliesLineItemAmount] [money] NOT NULL,
	[GrantAllocationAwardSuppliesLineItemNotes] [varchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_GrantAllocationAwardSuppliesLineItem_GrantAllocationAwardSuppliesLineItemID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationAwardSuppliesLineItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GrantAllocationAwardSuppliesLineItem]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationAwardSuppliesLineItem_GrantAllocationAward_GrantAllocationAwardID] FOREIGN KEY([GrantAllocationAwardID])
REFERENCES [dbo].[GrantAllocationAward] ([GrantAllocationAwardID])
GO
ALTER TABLE [dbo].[GrantAllocationAwardSuppliesLineItem] CHECK CONSTRAINT [FK_GrantAllocationAwardSuppliesLineItem_GrantAllocationAward_GrantAllocationAwardID]