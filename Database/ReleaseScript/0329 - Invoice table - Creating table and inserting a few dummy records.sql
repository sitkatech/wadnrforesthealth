CREATE TABLE [dbo].[Invoice](
	[InvoiceID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceIdentifyingName] [varchar](255) NULL,
	[RequestorName] [varchar](255) NOT NULL, --Landowner or Vendor (when fully implementing, use two fields with an exclusive or)
	[InvoiceDate] [DateTime] NOT NULL,
	[PurchaseAuthority] [varchar](255) NULL,
	[InvoiceStatus] [varchar](30) NULL,
	[TotalPaymentAmount] [money] NULL,
	[PreparedByPersonID] [int] NOT NULL,
 CONSTRAINT [PK_Invoice_InvoiceID] PRIMARY KEY CLUSTERED 
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

insert into dbo.Invoice
values
	('Thinning on the Back 40', 'Johnny B. Goode', '1/2/2008', 'Landowner Cost-Share Agreement', 'Pending', 1000, 5282),
	('Fuels reduction work on BlackAcre', 'Little Miss Muffett', '2/15/2010', '113-091', 'Canceled', 5000, 5282)
go

alter table dbo.Invoice
add constraint FK_Invoice_Person_PreparedByPersonID_PersonID foreign key(PreparedByPersonID)
references dbo.Person(PersonID) 
go

insert into dbo.FirmaPageType values
(61, 'FullInvoiceList', 'Full Invoice List', 1)
go

insert into dbo.FirmaPage values
(61, NULL)