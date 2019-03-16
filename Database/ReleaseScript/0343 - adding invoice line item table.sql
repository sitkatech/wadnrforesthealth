
CREATE TABLE [dbo].[InvoiceLineItem](
	[InvoiceLineItemID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceID] [int] NOT NULL,
	[GrantID] [int] NOT NULL,
    [CostTypeID] [int] NOT NULL,
    [InvoiceLineItemAmount] [money] NOT NULL,
    [InvoiceLineItemNote] varchar(8000) NULL,
 CONSTRAINT [PK_InvoiceLineItem_InvoiceLineItemID] PRIMARY KEY CLUSTERED 
(
	[InvoiceLineItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)
GO

ALTER TABLE [dbo].[InvoiceLineItem]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceLineItem_Grant_GrantID] FOREIGN KEY([GrantID])
REFERENCES [dbo].[Grant] ([GrantID])
GO

ALTER TABLE [dbo].[InvoiceLineItem] CHECK CONSTRAINT [FK_InvoiceLineItem_Grant_GrantID]
GO


ALTER TABLE [dbo].[InvoiceLineItem]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceLineItem_Invoice_InvoiceID] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[Invoice] ([InvoiceID])
GO

ALTER TABLE [dbo].[InvoiceLineItem] CHECK CONSTRAINT [FK_InvoiceLineItem_Invoice_InvoiceID]
GO

ALTER TABLE [dbo].[InvoiceLineItem]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceLineItem_CostType_CostTypeID] FOREIGN KEY([CostTypeID])
REFERENCES [dbo].[CostType] ([CostTypeID])
GO

ALTER TABLE [dbo].[InvoiceLineItem] CHECK CONSTRAINT [FK_InvoiceLineItem_CostType_CostTypeID]
GO

ALTER TABLE [dbo].[InvoiceLineItem]
ADD CONSTRAINT CK_InvoiceLineItem_CostTypeValues CHECK (CostTypeID in (1,2,3,4))
GO