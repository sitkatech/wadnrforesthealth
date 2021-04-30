

ALTER TABLE dbo.Person ADD CreatedAsPartOfBulkImport bit

ALTER TABLE dbo.Person ADD CreateGisUploadAttemptID int

ALTER TABLE dbo.ProjectPerson add CreatedAsPartOfBulkImport bit

ALTER TABLE dbo.ProjectPerson ADD CreateGisUploadAttemptID int

ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_GisUploadAttempt_CreateGisUploadAttemptID_GisUploadAttemptID] FOREIGN KEY([CreateGisUploadAttemptID])
REFERENCES [dbo].[GisUploadAttempt] ([GisUploadAttemptID])
GO


ALTER TABLE [dbo].[ProjectPerson]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPerson_GisUploadAttempt_CreateGisUploadAttemptID_GisUploadAttemptID] FOREIGN KEY([CreateGisUploadAttemptID])
REFERENCES [dbo].[GisUploadAttempt] ([GisUploadAttemptID])
GO
