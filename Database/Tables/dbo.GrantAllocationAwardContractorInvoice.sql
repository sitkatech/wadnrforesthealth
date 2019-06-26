SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantAllocationAwardContractorInvoice](
	[GrantAllocationAwardContractorInvoiceID] [int] IDENTITY(1,1) NOT NULL,
	[GrantAllocationAwardID] [int] NOT NULL,
	[GrantAllocationAwardContractorInvoiceDescription] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GrantAllocationAwardContractorInvoiceNumber] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GrantAllocationAwardContractorInvoiceDate] [datetime] NOT NULL,
	[GrantAllocationAwardContractorInvoiceTypeID] [int] NOT NULL,
	[GrantAllocationAwardContractorInvoiceForemanHours] [decimal](38, 10) NULL,
	[GrantAllocationAwardContractorInvoiceForemanRate] [money] NULL,
	[GrantAllocationAwardContractorInvoiceLaborHours] [decimal](38, 10) NULL,
	[GrantAllocationAwardContractorInvoiceLaborRate] [money] NULL,
	[GrantAllocationAwardContractorInvoiceGrappleHours] [decimal](38, 10) NULL,
	[GrantAllocationAwardContractorInvoiceGrappleRate] [money] NULL,
	[GrantAllocationAwardContractorInvoiceMasticationHours] [decimal](38, 10) NULL,
	[GrantAllocationAwardContractorInvoiceMasticationRate] [money] NULL,
	[GrantAllocationAwardContractorInvoiceTotal] [money] NULL,
	[GrantAllocationAwardContractorInvoiceTaxRate] [decimal](38, 10) NULL,
	[GrantAllocationAwardContractorInvoiceAcresReported] [decimal](38, 10) NULL,
	[GrantAllocationAwardContractorInvoiceNotes] [varchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GrantAllocationAwardContractorInvoiceFileResourceID] [int] NULL,
 CONSTRAINT [PK_GrantAllocationAwardContractorInvoice_GrantAllocationAwardContractorInvoiceID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationAwardContractorInvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GrantAllocationAwardContractorInvoice]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationAwardContractorInvoice_FileResource_GrantAllocationAwardContractorInvoiceFileResourceID_FileResourceID] FOREIGN KEY([GrantAllocationAwardContractorInvoiceFileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[GrantAllocationAwardContractorInvoice] CHECK CONSTRAINT [FK_GrantAllocationAwardContractorInvoice_FileResource_GrantAllocationAwardContractorInvoiceFileResourceID_FileResourceID]
GO
ALTER TABLE [dbo].[GrantAllocationAwardContractorInvoice]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationAwardContractorInvoice_GrantAllocationAward_GrantAllocationAwardID] FOREIGN KEY([GrantAllocationAwardID])
REFERENCES [dbo].[GrantAllocationAward] ([GrantAllocationAwardID])
GO
ALTER TABLE [dbo].[GrantAllocationAwardContractorInvoice] CHECK CONSTRAINT [FK_GrantAllocationAwardContractorInvoice_GrantAllocationAward_GrantAllocationAwardID]
GO
ALTER TABLE [dbo].[GrantAllocationAwardContractorInvoice]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationAwardContractorInvoice_GrantAllocationAwardContractorInvoiceType_GrantAllocationAwardContractorInvoiceTypeID] FOREIGN KEY([GrantAllocationAwardContractorInvoiceTypeID])
REFERENCES [dbo].[GrantAllocationAwardContractorInvoiceType] ([GrantAllocationAwardContractorInvoiceTypeID])
GO
ALTER TABLE [dbo].[GrantAllocationAwardContractorInvoice] CHECK CONSTRAINT [FK_GrantAllocationAwardContractorInvoice_GrantAllocationAwardContractorInvoiceType_GrantAllocationAwardContractorInvoiceTypeID]