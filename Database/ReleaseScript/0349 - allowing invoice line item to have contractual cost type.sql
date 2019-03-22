
ALTER TABLE [dbo].[InvoiceLineItem] DROP CONSTRAINT [CK_InvoiceLineItem_CostTypeValues]
GO

ALTER TABLE [dbo].[InvoiceLineItem]  WITH CHECK ADD  CONSTRAINT [CK_InvoiceLineItem_CostTypeValues] CHECK  (([CostTypeID]=(4) OR [CostTypeID]=(3) OR [CostTypeID]=(2) OR [CostTypeID]=(1) OR [CostTypeID]=(5)))
GO

ALTER TABLE [dbo].[InvoiceLineItem] CHECK CONSTRAINT [CK_InvoiceLineItem_CostTypeValues]
GO

