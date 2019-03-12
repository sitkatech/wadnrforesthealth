

CREATE TABLE [dbo].[InvoiceStatus](
	[InvoiceStatusID] [int] NOT NULL,
	[InvoiceStatusName] [varchar](50) NOT NULL,
	[InvoiceStatusDisplayName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_InvoiceStatus_InvoiceStatusID] PRIMARY KEY CLUSTERED 
(
	[InvoiceStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_InvoiceStatus_InvoiceStatusDisplayName] UNIQUE NONCLUSTERED 
(
	[InvoiceStatusDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_InvoiceStatus_InvoiceStatusName] UNIQUE NONCLUSTERED 
(
	[InvoiceStatusName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

insert into dbo.InvoiceStatus (InvoiceStatusID, InvoiceStatusName, InvoiceStatusDisplayName)
select 1, 'Pending', 'Pending'

insert into dbo.InvoiceStatus (InvoiceStatusID, InvoiceStatusName, InvoiceStatusDisplayName)
select 2, 'Paid', 'Paid'

insert into dbo.InvoiceStatus (InvoiceStatusID, InvoiceStatusName, InvoiceStatusDisplayName)
select 3, 'Canceled', 'Canceled'