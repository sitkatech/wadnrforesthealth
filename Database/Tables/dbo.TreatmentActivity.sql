SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentActivity](
	[TreatmentActivityID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[FundingSourceID] [int] NOT NULL,
	[TreatmentActivityFootprintAcres] [decimal](18, 0) NOT NULL,
	[TreatmentActivityBrushControlAcres] [decimal](18, 0) NOT NULL,
	[TreatmentActivityThinningAcres] [decimal](18, 0) NOT NULL,
	[TreatmentActivityPruningAcres] [decimal](18, 0) NOT NULL,
	[TreatmentActivitySlashAcres] [decimal](18, 0) NOT NULL,
	[TreatmentActivityPrescribedBurnAcres] [decimal](18, 0) NOT NULL,
	[TreatmentActivityAllocatedAmount] [money] NOT NULL,
	[TreatmentActivityTotalCost] [money] NOT NULL,
	[TreatmentActivityGrantCost] [money] NOT NULL,
	[TreatmentActivityStartDate] [datetime] NOT NULL,
	[TreatmentActivityEndDate] [datetime] NULL,
	[TreatmentActivityNotes] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_TreatmentActivity_TreatmentActivityID] PRIMARY KEY CLUSTERED 
(
	[TreatmentActivityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentActivity_TreatmentActivityID_TenantID] UNIQUE NONCLUSTERED 
(
	[TreatmentActivityID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentActivity]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentActivity_FundingSource_FundingSourceID] FOREIGN KEY([FundingSourceID])
REFERENCES [dbo].[FundingSource] ([FundingSourceID])
GO
ALTER TABLE [dbo].[TreatmentActivity] CHECK CONSTRAINT [FK_TreatmentActivity_FundingSource_FundingSourceID]
GO
ALTER TABLE [dbo].[TreatmentActivity]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentActivity_FundingSource_FundingSourceID_TenantID] FOREIGN KEY([FundingSourceID], [TenantID])
REFERENCES [dbo].[FundingSource] ([FundingSourceID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentActivity] CHECK CONSTRAINT [FK_TreatmentActivity_FundingSource_FundingSourceID_TenantID]
GO
ALTER TABLE [dbo].[TreatmentActivity]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentActivity_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[TreatmentActivity] CHECK CONSTRAINT [FK_TreatmentActivity_Project_ProjectID]
GO
ALTER TABLE [dbo].[TreatmentActivity]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentActivity_Project_ProjectID_TenantID] FOREIGN KEY([ProjectID], [TenantID])
REFERENCES [dbo].[Project] ([ProjectID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentActivity] CHECK CONSTRAINT [FK_TreatmentActivity_Project_ProjectID_TenantID]
GO
ALTER TABLE [dbo].[TreatmentActivity]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentActivity_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[TreatmentActivity] CHECK CONSTRAINT [FK_TreatmentActivity_Tenant_TenantID]