SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectImportBlacklist](
	[ProjectImportBlacklistID] [int] IDENTITY(1,1) NOT NULL,
	[ProgramID] [int] NOT NULL,
	[ProjectGisIdentifier] [varchar](140) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProjectName] [varchar](140) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_ProjectImportBlacklist_ProjectImportBlacklistID] PRIMARY KEY CLUSTERED 
(
	[ProjectImportBlacklistID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectImportBlacklist]  WITH CHECK ADD  CONSTRAINT [FK_ProjectImportBlacklist_Program_ProgramID] FOREIGN KEY([ProgramID])
REFERENCES [dbo].[Program] ([ProgramID])
GO
ALTER TABLE [dbo].[ProjectImportBlacklist] CHECK CONSTRAINT [FK_ProjectImportBlacklist_Program_ProgramID]
GO
ALTER TABLE [dbo].[ProjectImportBlacklist]  WITH CHECK ADD  CONSTRAINT [CK_ProjectImportBlacklist_ProjectGisIdentifierOrProjectName_IsRequired] CHECK  (([ProjectGisIdentifier] IS NOT NULL OR [ProjectName] IS NOT NULL))
GO
ALTER TABLE [dbo].[ProjectImportBlacklist] CHECK CONSTRAINT [CK_ProjectImportBlacklist_ProjectGisIdentifierOrProjectName_IsRequired]