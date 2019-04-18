

CREATE TABLE [dbo].[GrantModificationNoteInternal](
	[GrantModificationNoteInternalID] [int] IDENTITY(1,1) NOT NULL,
	[GrantModificationID] [int] NOT NULL,
	[GrantModificationNoteInternalText] [varchar](max) NULL,
	[CreatedByPersonID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[LastUpdatedByPersonID] [int] NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_GrantModificationNoteInternal_GrantModificationNoteInternalID] PRIMARY KEY CLUSTERED 
(
	[GrantModificationNoteInternalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[GrantModificationNoteInternal]  WITH CHECK ADD  CONSTRAINT [FK_GrantModificationNoteInternal_GrantModification_GrantModificationID] FOREIGN KEY([GrantModificationID])
REFERENCES [dbo].[GrantModification] ([GrantModificationID])
GO

ALTER TABLE [dbo].[GrantModificationNoteInternal] CHECK CONSTRAINT [FK_GrantModificationNoteInternal_GrantModification_GrantModificationID]
GO

ALTER TABLE [dbo].[GrantModificationNoteInternal]  WITH CHECK ADD  CONSTRAINT [FK_GrantModificationNoteInternal_Person_CreatedByPersonID_PersonID] FOREIGN KEY([CreatedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO

ALTER TABLE [dbo].[GrantModificationNoteInternal] CHECK CONSTRAINT [FK_GrantModificationNoteInternal_Person_CreatedByPersonID_PersonID]
GO

ALTER TABLE [dbo].[GrantModificationNoteInternal]  WITH CHECK ADD  CONSTRAINT [FK_GrantModificationNoteInternal_Person_LastUpdatedByPersonID_PersonID] FOREIGN KEY([LastUpdatedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO

ALTER TABLE [dbo].[GrantModificationNoteInternal] CHECK CONSTRAINT [FK_GrantModificationNoteInternal_Person_LastUpdatedByPersonID_PersonID]
GO

