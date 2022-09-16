ALTER TABLE dbo.Program ADD ProgramExampleGeospatialUploadFileResourceID int


ALTER TABLE [dbo].[Program]  WITH CHECK ADD  CONSTRAINT [FK_Program_FileResource_ProgramExampleGeospatialUploadFileResourceID_FileResourceID] FOREIGN KEY([ProgramExampleGeospatialUploadFileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
