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
	[Status] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TotalPaymentAmount] [money] NULL,
	[PreparedByPersonID] [int] NOT NULL,
 CONSTRAINT [PK_Invoice_InvoiceID] PRIMARY KEY CLUSTERED 
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Person_PreparedByPersonID_PersonID] FOREIGN KEY([PreparedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Person_PreparedByPersonID_PersonID]