alter table dbo.Invoice ADD  InvoiceMatchAmountTypeID int

go

update dbo.Invoice
set InvoiceMatchAmountTypeID = 1 


go

alter table dbo.Invoice alter column InvoiceMatchAmountTypeID int not null



ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_InvoiceMatchAmountType_InvoiceMatchAmountTypeID] FOREIGN KEY([InvoiceMatchAmountTypeID])
REFERENCES [dbo].[InvoiceMatchAmountType] ([InvoiceMatchAmountTypeID])
GO

ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_InvoiceMatchAmountType_InvoiceMatchAmountTypeID]
GO

alter table dbo.Invoice ADD  MatchAmount money
