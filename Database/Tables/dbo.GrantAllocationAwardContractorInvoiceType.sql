SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantAllocationAwardContractorInvoiceType](
	[GrantAllocationAwardContractorInvoiceTypeID] [int] NOT NULL,
	[GrantAllocationAwardContractorInvoiceTypeName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GrantAllocationAwardContractorInvoiceTypeDisplayName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_GrantAllocationAwardContractorInvoiceType_GrantAllocationAwardContractorInvoiceTypeID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationAwardContractorInvoiceTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
