delete from dbo.InvoiceStatus

Insert into dbo.InvoiceStatus (InvoiceStatusID, InvoiceStatusName, InvoiceStatusDisplayName)
values
(1, 'Pending', 'Pending'),
(2, 'Paid', 'Paid'),
(3, 'Canceled', 'Canceled')