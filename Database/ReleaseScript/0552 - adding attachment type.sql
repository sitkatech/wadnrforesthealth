ALTER TABLE dbo.Program ADD ProgramFileResourceID int

ALTER TABLE dbo.Program ADD ProgramNotes varchar(max)



ALTER TABLE [dbo].[Program]  WITH CHECK ADD  CONSTRAINT [FK_Program_FileResource_ProgramFileResourceID_FileResourceID] FOREIGN KEY([ProgramFileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO


