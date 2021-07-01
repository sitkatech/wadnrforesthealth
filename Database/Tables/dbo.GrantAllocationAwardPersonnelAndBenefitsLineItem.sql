SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantAllocationAwardPersonnelAndBenefitsLineItem](
	[GrantAllocationAwardPersonnelAndBenefitsLineItemID] [int] IDENTITY(1,1) NOT NULL,
	[GrantAllocationAwardID] [int] NOT NULL,
	[GrantAllocationAwardPersonnelAndBenefitsLineItemTarOrMonth] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GrantAllocationAwardPersonnelAndBenefitsLineItemDate] [datetime] NOT NULL,
	[GrantAllocationAwardPersonnelAndBenefitsLineItemTarHours] [int] NOT NULL,
	[GrantAllocationAwardPersonnelAndBenefitsLineItemHourlyRate] [money] NOT NULL,
	[GrantAllocationAwardPersonnelAndBenefitsLineItemFringeRate] [money] NOT NULL,
	[GrantAllocationAwardPersonnelAndBenefitsLineItemNotes] [varchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PersonID] [int] NULL,
 CONSTRAINT [PK_GrantAllocationAwardPersonnelAndBenefitsLineItem_GrantAllocationAwardPersonnelAndBenefitsLineItemID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationAwardPersonnelAndBenefitsLineItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GrantAllocationAwardPersonnelAndBenefitsLineItem]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationAwardPersonnelAndBenefitsLineItem_GrantAllocationAward_GrantAllocationAwardID] FOREIGN KEY([GrantAllocationAwardID])
REFERENCES [dbo].[GrantAllocationAward] ([GrantAllocationAwardID])
GO
ALTER TABLE [dbo].[GrantAllocationAwardPersonnelAndBenefitsLineItem] CHECK CONSTRAINT [FK_GrantAllocationAwardPersonnelAndBenefitsLineItem_GrantAllocationAward_GrantAllocationAwardID]
GO
ALTER TABLE [dbo].[GrantAllocationAwardPersonnelAndBenefitsLineItem]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationAwardPersonnelAndBenefitsLineItem_Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[GrantAllocationAwardPersonnelAndBenefitsLineItem] CHECK CONSTRAINT [FK_GrantAllocationAwardPersonnelAndBenefitsLineItem_Person_PersonID]