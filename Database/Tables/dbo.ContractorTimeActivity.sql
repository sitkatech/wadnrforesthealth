SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContractorTimeActivity](
	[ContractorTimeActivityID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[FundingSourceID] [int] NOT NULL,
	[ContractorTimeActivityAcreage] [decimal](18, 0) NOT NULL,
	[ContractorTimeActivityHours] [decimal](18, 0) NOT NULL,
	[ContractorTimeActivityRate] [money] NOT NULL,
	[ContractorTimeActivityStartDate] [datetime] NOT NULL,
	[ContractorTimeActivityEndDate] [datetime] NULL,
	[ContractorTimeActivityNotes] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_ContractorTimeActivity_ContractorTimeActivityID] PRIMARY KEY CLUSTERED 
(
	[ContractorTimeActivityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ContractorTimeActivity_ContractorTimeActivityID_TenantID] UNIQUE NONCLUSTERED 
(
	[ContractorTimeActivityID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[ContractorTimeActivity]  WITH CHECK ADD  CONSTRAINT [FK_ContractorTimeActivity_FundingSource_FundingSourceID] FOREIGN KEY([FundingSourceID])
REFERENCES [dbo].[FundingSource] ([FundingSourceID])
GO
ALTER TABLE [dbo].[ContractorTimeActivity] CHECK CONSTRAINT [FK_ContractorTimeActivity_FundingSource_FundingSourceID]
GO
ALTER TABLE [dbo].[ContractorTimeActivity]  WITH CHECK ADD  CONSTRAINT [FK_ContractorTimeActivity_FundingSource_FundingSourceID_TenantID] FOREIGN KEY([FundingSourceID], [TenantID])
REFERENCES [dbo].[FundingSource] ([FundingSourceID], [TenantID])
GO
ALTER TABLE [dbo].[ContractorTimeActivity] CHECK CONSTRAINT [FK_ContractorTimeActivity_FundingSource_FundingSourceID_TenantID]
GO
ALTER TABLE [dbo].[ContractorTimeActivity]  WITH CHECK ADD  CONSTRAINT [FK_ContractorTimeActivity_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[ContractorTimeActivity] CHECK CONSTRAINT [FK_ContractorTimeActivity_Project_ProjectID]
GO
ALTER TABLE [dbo].[ContractorTimeActivity]  WITH CHECK ADD  CONSTRAINT [FK_ContractorTimeActivity_Project_ProjectID_TenantID] FOREIGN KEY([ProjectID], [TenantID])
REFERENCES [dbo].[Project] ([ProjectID], [TenantID])
GO
ALTER TABLE [dbo].[ContractorTimeActivity] CHECK CONSTRAINT [FK_ContractorTimeActivity_Project_ProjectID_TenantID]
GO
ALTER TABLE [dbo].[ContractorTimeActivity]  WITH CHECK ADD  CONSTRAINT [FK_ContractorTimeActivity_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[ContractorTimeActivity] CHECK CONSTRAINT [FK_ContractorTimeActivity_Tenant_TenantID]