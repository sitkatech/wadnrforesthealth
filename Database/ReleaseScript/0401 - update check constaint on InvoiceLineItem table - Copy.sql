ALTER TABLE dbo.InvoiceLineItem DROP CONSTRAINT [CK_InvoiceLineItem_CostTypeValues]
go

ALTER TABLE [dbo].[InvoiceLineItem]  WITH CHECK ADD  CONSTRAINT [CK_InvoiceLineItem_CostTypeValues] CHECK  (([CostTypeID]=(1) OR [CostTypeID]=(2) OR [CostTypeID]=(3) OR [CostTypeID]=(4) OR [CostTypeID]=(5) OR [CostTypeID]=(6) OR [CostTypeID]=(8) OR [CostTypeID]=(9)))
GO

ALTER TABLE [dbo].[InvoiceLineItem] CHECK CONSTRAINT [CK_InvoiceLineItem_CostTypeValues]
GO


