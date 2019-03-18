SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceLineItem](
	[InvoiceLineItemID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceID] [int] NOT NULL,
	[GrantAllocationID] [int] NOT NULL,
	[CostTypeID] [int] NOT NULL,
	[InvoiceLineItemAmount] [money] NOT NULL,
	[InvoiceLineItemNote] [varchar](8000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_InvoiceLineItem_InvoiceLineItemID] PRIMARY KEY CLUSTERED 
(
	[InvoiceLineItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[InvoiceLineItem]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceLineItem_CostType_CostTypeID] FOREIGN KEY([CostTypeID])
REFERENCES [dbo].[CostType] ([CostTypeID])
GO
ALTER TABLE [dbo].[InvoiceLineItem] CHECK CONSTRAINT [FK_InvoiceLineItem_CostType_CostTypeID]
GO
ALTER TABLE [dbo].[InvoiceLineItem]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceLineItem_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO
ALTER TABLE [dbo].[InvoiceLineItem] CHECK CONSTRAINT [FK_InvoiceLineItem_GrantAllocation_GrantAllocationID]
GO
ALTER TABLE [dbo].[InvoiceLineItem]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceLineItem_Invoice_InvoiceID] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[Invoice] ([InvoiceID])
GO
ALTER TABLE [dbo].[InvoiceLineItem] CHECK CONSTRAINT [FK_InvoiceLineItem_Invoice_InvoiceID]
GO
ALTER TABLE [dbo].[InvoiceLineItem]  WITH CHECK ADD  CONSTRAINT [CK_InvoiceLineItem_CostTypeValues] CHECK  (([CostTypeID]=(4) OR [CostTypeID]=(3) OR [CostTypeID]=(2) OR [CostTypeID]=(1)))
GO
ALTER TABLE [dbo].[InvoiceLineItem] CHECK CONSTRAINT [CK_InvoiceLineItem_CostTypeValues]