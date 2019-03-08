update dbo.Invoice
set InvoiceApprovalStatusID = 2, InvoiceApprovalStatusComment = 'Approved. All relevant information received.'
where InvoiceID = 1
go

update dbo.Invoice
set InvoiceApprovalStatusID = 3, InvoiceApprovalStatusComment = 'Denied. Invoice requested amount does not match agreement amount.'
where InvoiceID = 2
go