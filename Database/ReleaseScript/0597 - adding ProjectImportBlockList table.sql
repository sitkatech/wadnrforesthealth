
create table dbo.ProjectImportBlockList
(
	ProjectImportBlockListID int IDENTITY(1,1) NOT NULL constraint PK_ProjectImportBlockList_ProjectImportBlockListID primary key,
	ProgramID int not null CONSTRAINT [FK_ProjectImportBlockList_Program_ProgramID] FOREIGN KEY([ProgramID]) REFERENCES [dbo].[Program] ([ProgramID]),
	ProjectGisIdentifier varchar(140),
	ProjectName varchar(140)
	
)


ALTER TABLE [dbo].[ProjectImportBlockList]  WITH CHECK ADD  CONSTRAINT [CK_ProjectImportBlockList_ProjectGisIdentifierOrProjectName_IsRequired] CHECK  (ProjectGisIdentifier IS NOT NULL OR ProjectName IS NOT NULL)
GO


