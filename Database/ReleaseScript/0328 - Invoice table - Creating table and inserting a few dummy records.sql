CREATE TABLE [dbo].[Invoice](
	[InvoiceID] [int] IDENTITY(1,1) NOT NULL,
	[RequestorName] [varchar](255) NOT NULL, --Landowner or Vendor
	[InvoiceDate] [DateTime] NOT NULL,
	[PurchaseAuthority] [varchar](255) NULL,
	[Status] [varchar](30) NULL,
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
	('Johnny B. Goode', 1/2/1955, 'Landowner Cost-Share Agreement', 'Pending', 1000, 5282),
	('Little Miss Muffett', 2/15/1955, '113-091', 'Canceled', 5000, 5282)
go