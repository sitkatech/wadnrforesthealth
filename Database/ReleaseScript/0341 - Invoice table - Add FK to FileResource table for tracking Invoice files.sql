alter table dbo.Invoice add 
	InvoiceFileResourceID int null
	CONSTRAINT FK_Invoice_FileResource_InvoiceFileResourceID_FileResourceID FOREIGN KEY (InvoiceFileResourceID) 
    REFERENCES dbo.FileResource (FileResourceID)