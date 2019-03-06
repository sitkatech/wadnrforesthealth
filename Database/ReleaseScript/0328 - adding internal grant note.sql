

CREATE TABLE [dbo].[GrantNoteInternal](
	[GrantNoteInternalID] [int] IDENTITY(1,1) NOT NULL,
	[GrantID] [int] NOT NULL,
	[GrantNoteText] [varchar](max) NULL,
	[CreatedByPersonID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[LastUpdatedByPersonID] [int] NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_GrantNoteInternal_GrantNoteInternalID] PRIMARY KEY CLUSTERED 
(
	[GrantNoteInternalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[GrantNoteInternal]  WITH CHECK ADD  CONSTRAINT [FK_GrantNoteInternal_Grant_GrantID] FOREIGN KEY([GrantID])
REFERENCES [dbo].[Grant] ([GrantID])
GO

ALTER TABLE [dbo].[GrantNoteInternal] CHECK CONSTRAINT [FK_GrantNoteInternal_Grant_GrantID]
GO

ALTER TABLE [dbo].[GrantNoteInternal]  WITH CHECK ADD  CONSTRAINT [FK_GrantNoteInternal_Person_CreatedByPersonID_PersonID] FOREIGN KEY([CreatedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO

ALTER TABLE [dbo].[GrantNoteInternal] CHECK CONSTRAINT [FK_GrantNoteInternal_Person_CreatedByPersonID_PersonID]
GO

ALTER TABLE [dbo].[GrantNoteInternal]  WITH CHECK ADD  CONSTRAINT [FK_GrantNoteInternal_Person_LastUpdatedByPersonID_PersonID] FOREIGN KEY([LastUpdatedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO

ALTER TABLE [dbo].[GrantNoteInternal] CHECK CONSTRAINT [FK_GrantNoteInternal_Person_LastUpdatedByPersonID_PersonID]
GO


