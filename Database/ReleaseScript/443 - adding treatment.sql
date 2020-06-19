
CREATE TABLE [dbo].[Treatment](
	[TreatmentID] [int] IDENTITY(1,1) NOT NULL,
    [ProjectID] [int] NOT NULL,
    [TreatmentFeature] [geometry]  null,
	[GrantAllocationAwardLandownerCostShareLineItemID] [int] NULL,
	[TreatmentStartDate] [datetime] NULL,
	[TreatmentEndDate] [datetime] NULL,
	[TreatmentFootprintAcres] [decimal](38, 10) NOT NULL,
	[TreatmentChippingAcres] [decimal](38, 10) NOT NULL,
	[TreatmentPruningAcres] [decimal](38, 10) NOT NULL,
	[TreatmentThinningAcres] [decimal](38, 10) NOT NULL,
	[TreatmentMasticationAcres] [decimal](38, 10) NOT NULL,
	[TreatmentGrazingAcres] [decimal](38, 10) NOT NULL,
	[TreatmentLopAndScatterAcres] [decimal](38, 10) NOT NULL,
	[TreatmentBiomassRemovalAcres] [decimal](38, 10) NOT NULL,
	[TreatmentHandPileAcres] [decimal](38, 10) NOT NULL,
	[TreatmentBroadcastBurnAcres] [decimal](38, 10) NOT NULL,
	[TreatmentHandPileBurnAcres] [decimal](38, 10) NOT NULL,
	[TreatmentMachinePileBurnAcres] [decimal](38, 10) NOT NULL,
	[TreatmentOtherTreatmentAcres] [decimal](38, 10) NOT NULL,
	[TreatmentSlashAcres] [decimal](38, 10) NOT NULL,
	[TreatmentNotes] [varchar](2000) NULL,
 CONSTRAINT [PK_Treatment_TreatmentID] PRIMARY KEY CLUSTERED 
(
	[TreatmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Treatment]  WITH CHECK ADD  CONSTRAINT [FK_Treatment_GrantAllocationAwardLandownerCostShareLineItem_GrantAllocationAwardLandownerCostShareLineItemID] FOREIGN KEY([GrantAllocationAwardLandownerCostShareLineItemID])
REFERENCES [dbo].[GrantAllocationAwardLandownerCostShareLineItem] ([GrantAllocationAwardLandownerCostShareLineItemID])
GO

ALTER TABLE [dbo].[Treatment]  WITH CHECK ADD  CONSTRAINT [FK_Treatment_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO

ALTER TABLE [dbo].[Treatment] CHECK CONSTRAINT [FK_Treatment_Project_ProjectID]
GO

