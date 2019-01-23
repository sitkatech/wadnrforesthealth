SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantAllocationNote](
	[GrantAllocationNoteID] [int] IDENTITY(1,1) NOT NULL,
	[GrantAllocationID] [int] NOT NULL,
	[GrantAllocationNoteText] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedByPersonID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[LastUpdatedByPersonID] [int] NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_GrantAllocationNote_GrantAllocationNoteID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationNoteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[GrantAllocationNote]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationNote_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO
ALTER TABLE [dbo].[GrantAllocationNote] CHECK CONSTRAINT [FK_GrantAllocationNote_GrantAllocation_GrantAllocationID]
GO
ALTER TABLE [dbo].[GrantAllocationNote]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationNote_Person_CreatedByPersonID_PersonID] FOREIGN KEY([CreatedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[GrantAllocationNote] CHECK CONSTRAINT [FK_GrantAllocationNote_Person_CreatedByPersonID_PersonID]
GO
ALTER TABLE [dbo].[GrantAllocationNote]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationNote_Person_LastUpdatedByPersonID_PersonID] FOREIGN KEY([LastUpdatedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[GrantAllocationNote] CHECK CONSTRAINT [FK_GrantAllocationNote_Person_LastUpdatedByPersonID_PersonID]