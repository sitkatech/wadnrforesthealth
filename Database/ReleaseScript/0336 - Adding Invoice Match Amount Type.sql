

CREATE TABLE [dbo].[InvoiceMatchAmountType](
	[InvoiceMatchAmountTypeID] [int] NOT NULL,
	[InvoiceMatchAmountTypeName] [varchar](50) NOT NULL,
	[InvoiceMatchAmountTypeDisplayName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_InvoiceMatchAmountType_InvoiceMatchAmountTypeID] PRIMARY KEY CLUSTERED 
(
	[InvoiceMatchAmountTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_InvoiceMatchAmountType_InvoiceMatchAmountTypeDisplayName] UNIQUE NONCLUSTERED 
(
	[InvoiceMatchAmountTypeDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_InvoiceMatchAmountType_InvoiceMatchAmountTypeName] UNIQUE NONCLUSTERED 
(
	[InvoiceMatchAmountTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

insert into dbo.InvoiceMatchAmountType (InvoiceMatchAmountTypeID, InvoiceMatchAmountTypeName, InvoiceMatchAmountTypeDisplayName)
select 1, 'DollarAmount', 'Dollar Amount'