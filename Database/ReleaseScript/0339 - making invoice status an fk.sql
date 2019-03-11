alter table dbo.Invoice ADD  InvoiceStatusID int

go

update dbo.Invoice
set InvoiceStatusID = 1 where InvoiceStatus = 'Pending'

update dbo.Invoice
set InvoiceStatusID = 2 where InvoiceStatus = 'Paid'

update dbo.Invoice
set InvoiceStatusID = 3 where InvoiceStatus = 'Canceled'

go

alter table dbo.Invoice alter column InvoiceStatusID int not null



ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_InvoiceStatus_InvoiceStatusID] FOREIGN KEY([InvoiceStatusID])
REFERENCES [dbo].[InvoiceStatus] ([InvoiceStatusID])
GO

ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_InvoiceStatus_InvoiceStatusID]
GO

alter table dbo.Invoice drop column InvoiceStatus