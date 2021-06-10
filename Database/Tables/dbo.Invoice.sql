SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[InvoiceID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceIdentifyingName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RequestorName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[InvoiceDate] [datetime] NOT NULL,
	[PurchaseAuthority] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TotalPaymentAmount] [money] NULL,
	[PreparedByPersonID] [int] NOT NULL,
	[InvoiceApprovalStatusID] [int] NOT NULL,
	[InvoiceApprovalStatusComment] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PurchaseAuthorityIsLandownerCostShareAgreement] [bit] NOT NULL,
	[InvoiceMatchAmountTypeID] [int] NOT NULL,
	[MatchAmount] [money] NULL,
	[InvoiceStatusID] [int] NOT NULL,
	[InvoiceFileResourceID] [int] NULL,
 CONSTRAINT [PK_Invoice_InvoiceID] PRIMARY KEY CLUSTERED 
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_FileResource_InvoiceFileResourceID_FileResourceID] FOREIGN KEY([InvoiceFileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_FileResource_InvoiceFileResourceID_FileResourceID]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_InvoiceApprovalStatus_InvoiceApprovalStatusID] FOREIGN KEY([InvoiceApprovalStatusID])
REFERENCES [dbo].[InvoiceApprovalStatus] ([InvoiceApprovalStatusID])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_InvoiceApprovalStatus_InvoiceApprovalStatusID]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_InvoiceMatchAmountType_InvoiceMatchAmountTypeID] FOREIGN KEY([InvoiceMatchAmountTypeID])
REFERENCES [dbo].[InvoiceMatchAmountType] ([InvoiceMatchAmountTypeID])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_InvoiceMatchAmountType_InvoiceMatchAmountTypeID]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_InvoiceStatus_InvoiceStatusID] FOREIGN KEY([InvoiceStatusID])
REFERENCES [dbo].[InvoiceStatus] ([InvoiceStatusID])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_InvoiceStatus_InvoiceStatusID]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Person_PreparedByPersonID_PersonID] FOREIGN KEY([PreparedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Person_PreparedByPersonID_PersonID]