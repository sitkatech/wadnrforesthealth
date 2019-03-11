delete from dbo.InvoiceApprovalStatus

insert dbo.InvoiceApprovalStatus (InvoiceApprovalStatusID, InvoiceApprovalStatusName) 
values 
(1, 'Not Set'),
(2, 'Approved'),
(3, 'Denied')