

delete from dbo.FirmaPage where FirmaPageTypeID = 61;




ALTER TABLE [dbo].[InvoicePaymentRequest] DROP CONSTRAINT [FK_InvoicePaymentRequest_Person_ApprovedByPersonID_PersonID]
GO



alter table dbo.InvoicePaymentRequest
drop column ApprovedByPersonID, ApprovedDate


EXEC sp_rename 'dbo.Invoice.TotalPaymentAmount', 'PaymentAmount', 'COLUMN';

