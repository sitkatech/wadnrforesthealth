
CREATE TABLE [dbo].[GrantAllocationNoteInternal](
	[GrantAllocationNoteInternalID] [int] IDENTITY(1,1) NOT NULL,
	[GrantAllocationID] [int] NOT NULL,
	[GrantAllocationNoteInternalText] [varchar](max) NULL,
	[CreatedByPersonID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[LastUpdatedByPersonID] [int] NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_GrantAllocationNoteInternal_GrantAllocationNoteInternalID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationNoteInternalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[GrantAllocationNoteInternal]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationNoteInternal_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO

ALTER TABLE [dbo].[GrantAllocationNoteInternal] CHECK CONSTRAINT [FK_GrantAllocationNoteInternal_GrantAllocation_GrantAllocationID]
GO

ALTER TABLE [dbo].[GrantAllocationNoteInternal]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationNoteInternal_Person_CreatedByPersonID_PersonID] FOREIGN KEY([CreatedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO

ALTER TABLE [dbo].[GrantAllocationNoteInternal] CHECK CONSTRAINT [FK_GrantAllocationNoteInternal_Person_CreatedByPersonID_PersonID]
GO

ALTER TABLE [dbo].[GrantAllocationNoteInternal]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationNoteInternal_Person_LastUpdatedByPersonID_PersonID] FOREIGN KEY([LastUpdatedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO

ALTER TABLE [dbo].[GrantAllocationNoteInternal] CHECK CONSTRAINT [FK_GrantAllocationNoteInternal_Person_LastUpdatedByPersonID_PersonID]
GO

