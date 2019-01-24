SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentActivity](
	[TreatmentActivityID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[TreatmentActivityStartDate] [datetime] NULL,
	[TreatmentActivityEndDate] [datetime] NULL,
	[TreatmentActivityStatusID] [int] NOT NULL,
	[TreatmentActivityContactID] [int] NULL,
	[TreatmentActivityFootprintAcres] [decimal](18, 0) NOT NULL,
	[TreatmentActivityChippingAcres] [decimal](18, 0) NOT NULL,
	[TreatmentActivityPruningAcres] [decimal](18, 0) NOT NULL,
	[TreatmentActivityThinningAcres] [decimal](18, 0) NOT NULL,
	[TreatmentActivityMasticationAcres] [decimal](18, 0) NOT NULL,
	[TreatmentActivityGrazingAcres] [decimal](18, 0) NOT NULL,
	[TreatmentActivityLopAndScatterAcres] [decimal](18, 0) NOT NULL,
	[TreatmentActivityBiomassRemovalAcres] [decimal](18, 0) NOT NULL,
	[TreatmentActivityHandPileAcres] [decimal](18, 0) NOT NULL,
	[TreatmentActivityBroadcastBurnAcres] [decimal](18, 0) NOT NULL,
	[TreatmentActivityHandPileBurnAcres] [decimal](18, 0) NOT NULL,
	[TreatmentActivityMachinePileBurnAcres] [decimal](18, 0) NOT NULL,
	[TreatmentActivityOtherTreatmentAcres] [decimal](18, 0) NOT NULL,
	[TreatmentActivityNotes] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProgramIndexID] [int] NULL,
	[ProjectCodeID] [int] NULL,
 CONSTRAINT [PK_TreatmentActivity_TreatmentActivityID] PRIMARY KEY CLUSTERED 
(
	[TreatmentActivityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentActivity]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentActivity_Person_TreatmentActivityContactID_PersonID] FOREIGN KEY([TreatmentActivityContactID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[TreatmentActivity] CHECK CONSTRAINT [FK_TreatmentActivity_Person_TreatmentActivityContactID_PersonID]
GO
ALTER TABLE [dbo].[TreatmentActivity]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentActivity_ProgramIndex_ProgramIndexID] FOREIGN KEY([ProgramIndexID])
REFERENCES [dbo].[ProgramIndex] ([ProgramIndexID])
GO
ALTER TABLE [dbo].[TreatmentActivity] CHECK CONSTRAINT [FK_TreatmentActivity_ProgramIndex_ProgramIndexID]
GO
ALTER TABLE [dbo].[TreatmentActivity]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentActivity_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[TreatmentActivity] CHECK CONSTRAINT [FK_TreatmentActivity_Project_ProjectID]
GO
ALTER TABLE [dbo].[TreatmentActivity]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentActivity_ProjectCode_ProjectCodeID] FOREIGN KEY([ProjectCodeID])
REFERENCES [dbo].[ProjectCode] ([ProjectCodeID])
GO
ALTER TABLE [dbo].[TreatmentActivity] CHECK CONSTRAINT [FK_TreatmentActivity_ProjectCode_ProjectCodeID]
GO
ALTER TABLE [dbo].[TreatmentActivity]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentActivity_TreatmentActivityStatus_TreamentActivityStatusID] FOREIGN KEY([TreatmentActivityStatusID])
REFERENCES [dbo].[TreatmentActivityStatus] ([TreatmentActivityStatusID])
GO
ALTER TABLE [dbo].[TreatmentActivity] CHECK CONSTRAINT [FK_TreatmentActivity_TreatmentActivityStatus_TreamentActivityStatusID]