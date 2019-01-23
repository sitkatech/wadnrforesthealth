CREATE TABLE [dbo].[GrantNote](
	[GrantNoteID] [int] IDENTITY(1,1) NOT  NULL,
	[GrantID] [int] NOT NULL,
    [GrantNoteText] varchar(max),
    [CreatedByPersonID] [int] not null,
    [CreatedDate] datetime not null,
    [LastUpdatedByPersonID] int null,
    [LastUpdatedDate] datetime null
 CONSTRAINT [PK_GrantNote_GrantNoteID] PRIMARY KEY CLUSTERED 
(
	GrantNoteID ASC
) ON [PRIMARY]
)


go

ALTER TABLE [dbo].[GrantNote]  WITH CHECK ADD  CONSTRAINT [FK_GrantNote_Grant_GrantID] FOREIGN KEY([GrantID])
REFERENCES [dbo].[Grant] ([GrantID])

ALTER TABLE [dbo].[GrantNote]  WITH CHECK ADD  CONSTRAINT [FK_GrantNote_Person_CreatedByPersonID_PersonID] FOREIGN KEY([CreatedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])

ALTER TABLE [dbo].[GrantNote]  WITH CHECK ADD  CONSTRAINT [FK_GrantNote_Person_LastUpdatedByPersonID_PersonID] FOREIGN KEY([LastUpdatedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])