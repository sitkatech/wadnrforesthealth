
create table dbo.ProjectImportBlacklist
(
	ProjectImportBlacklistID int IDENTITY(1,1) NOT NULL constraint PK_ProjectImportBlacklist_ProjectImportBlacklistID primary key,
	ProgramID int not null CONSTRAINT [FK_ProjectImportBlacklist_Program_ProgramID] FOREIGN KEY([ProgramID]) REFERENCES [dbo].[Program] ([ProgramID]),
	ProjectGisIdentifier varchar(140),
	ProjectName varchar(140)
	
)


ALTER TABLE [dbo].[ProjectImportBlacklist]  WITH CHECK ADD  CONSTRAINT [CK_ProjectImportBlacklist_ProjectGisIdentifierOrProjectName_IsRequired] CHECK  (ProjectGisIdentifier IS NOT NULL OR ProjectName IS NOT NULL)
GO


