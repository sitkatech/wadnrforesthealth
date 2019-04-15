SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantAllocation](
	[GrantAllocationID] [int] IDENTITY(1,1) NOT NULL,
	[GrantID] [int] NOT NULL,
	[GrantAllocationName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[AllocationAmount] [money] NULL,
	[ProgramIndexID] [int] NULL,
	[FederalFundCodeID] [int] NULL,
	[OrganizationID] [int] NULL,
	[RegionID] [int] NULL,
	[DivisionID] [int] NULL,
	[GrantManagerID] [int] NULL,
	[GrantAllocationFileResourceID] [int] NULL,
 CONSTRAINT [PK_GrantAllocation_GrantAllocationID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_GrantAllocation_GrantAllocationID_GrantID] UNIQUE NONCLUSTERED 
(
	[GrantAllocationID] ASC,
	[GrantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GrantAllocation]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocation_Division_DivisionID] FOREIGN KEY([DivisionID])
REFERENCES [dbo].[Division] ([DivisionID])
GO
ALTER TABLE [dbo].[GrantAllocation] CHECK CONSTRAINT [FK_GrantAllocation_Division_DivisionID]
GO
ALTER TABLE [dbo].[GrantAllocation]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocation_FederalFundCode_FederalFundCodeID] FOREIGN KEY([FederalFundCodeID])
REFERENCES [dbo].[FederalFundCode] ([FederalFundCodeID])
GO
ALTER TABLE [dbo].[GrantAllocation] CHECK CONSTRAINT [FK_GrantAllocation_FederalFundCode_FederalFundCodeID]
GO
ALTER TABLE [dbo].[GrantAllocation]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocation_FileResource_GrantAllocationFileResourceID_FileResourceID] FOREIGN KEY([GrantAllocationFileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[GrantAllocation] CHECK CONSTRAINT [FK_GrantAllocation_FileResource_GrantAllocationFileResourceID_FileResourceID]
GO
ALTER TABLE [dbo].[GrantAllocation]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocation_Grant_GrantID] FOREIGN KEY([GrantID])
REFERENCES [dbo].[Grant] ([GrantID])
GO
ALTER TABLE [dbo].[GrantAllocation] CHECK CONSTRAINT [FK_GrantAllocation_Grant_GrantID]
GO
ALTER TABLE [dbo].[GrantAllocation]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocation_Organization_OrganizationID] FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[Organization] ([OrganizationID])
GO
ALTER TABLE [dbo].[GrantAllocation] CHECK CONSTRAINT [FK_GrantAllocation_Organization_OrganizationID]
GO
ALTER TABLE [dbo].[GrantAllocation]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocation_Person_GrantManagerID_PersonID] FOREIGN KEY([GrantManagerID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[GrantAllocation] CHECK CONSTRAINT [FK_GrantAllocation_Person_GrantManagerID_PersonID]
GO
ALTER TABLE [dbo].[GrantAllocation]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocation_ProgramIndex_ProgramIndexID] FOREIGN KEY([ProgramIndexID])
REFERENCES [dbo].[ProgramIndex] ([ProgramIndexID])
GO
ALTER TABLE [dbo].[GrantAllocation] CHECK CONSTRAINT [FK_GrantAllocation_ProgramIndex_ProgramIndexID]
GO
ALTER TABLE [dbo].[GrantAllocation]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocation_Region_RegionID] FOREIGN KEY([RegionID])
REFERENCES [dbo].[Region] ([RegionID])
GO
ALTER TABLE [dbo].[GrantAllocation] CHECK CONSTRAINT [FK_GrantAllocation_Region_RegionID]