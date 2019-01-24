SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agreement](
	[AgreementID] [int] IDENTITY(1,1) NOT NULL,
	[TmpAgreementID] [int] NULL,
	[AgreementTypeID] [int] NULL,
	[AgreementNumber] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProjectCodeID] [int] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[AgreementAmount] [money] NULL,
	[ExpendedAmount] [money] NULL,
	[BalanceAmount] [money] NULL,
	[RegionID] [int] NULL,
	[FirstBillDueOn] [datetime] NULL,
	[Notes] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Agreement_AgreementID] PRIMARY KEY CLUSTERED 
(
	[AgreementID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
CREATE UNIQUE NONCLUSTERED INDEX [UNIQUE_INDEX_Agreement_AgreementNumber] ON [dbo].[Agreement]
(
	[AgreementNumber] ASC
)
WHERE ([AgreementNumber] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Agreement]  WITH CHECK ADD  CONSTRAINT [FK_Agreement_AgreementType_AgreementTypeID] FOREIGN KEY([AgreementTypeID])
REFERENCES [dbo].[AgreementType] ([AgreementTypeID])
GO
ALTER TABLE [dbo].[Agreement] CHECK CONSTRAINT [FK_Agreement_AgreementType_AgreementTypeID]
GO
ALTER TABLE [dbo].[Agreement]  WITH CHECK ADD  CONSTRAINT [FK_Agreement_ProjectCode_ProjectCodeID] FOREIGN KEY([ProjectCodeID])
REFERENCES [dbo].[ProjectCode] ([ProjectCodeID])
GO
ALTER TABLE [dbo].[Agreement] CHECK CONSTRAINT [FK_Agreement_ProjectCode_ProjectCodeID]
GO
ALTER TABLE [dbo].[Agreement]  WITH CHECK ADD  CONSTRAINT [FK_Agreement_Region_RegionID] FOREIGN KEY([RegionID])
REFERENCES [dbo].[Region] ([RegionID])
GO
ALTER TABLE [dbo].[Agreement] CHECK CONSTRAINT [FK_Agreement_Region_RegionID]
GO
ALTER TABLE [dbo].[Agreement]  WITH CHECK ADD  CONSTRAINT [FK_Agreement_TmpAgreement_TmpAgreementID] FOREIGN KEY([TmpAgreementID])
REFERENCES [dbo].[tmpAgreement] ([TmpAgreementID])
GO
ALTER TABLE [dbo].[Agreement] CHECK CONSTRAINT [FK_Agreement_TmpAgreement_TmpAgreementID]