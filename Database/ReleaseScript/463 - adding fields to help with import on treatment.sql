ALTER TABLE dbo.Treatment add TreatmentTypeImportedText varchar(200)

ALTER TABLE dbo.Treatment add CreateGisUploadAttemptID int

ALTER TABLE dbo.Treatment add UpdateGisUploadAttemptID int


GO


ALTER TABLE [dbo].[Treatment]  WITH CHECK ADD  CONSTRAINT [FK_Treatment_GisUploadAttempt_CreateGisUploadAttemptID_GisUploadAttemptID] FOREIGN KEY([CreateGisUploadAttemptID])
REFERENCES [dbo].[GisUploadAttempt] ([GisUploadAttemptID])
GO


ALTER TABLE [dbo].[Treatment]  WITH CHECK ADD  CONSTRAINT [FK_Treatment_GisUploadAttempt_UpdateGisUploadAttemptID_GisUploadAttemptID] FOREIGN KEY([UpdateGisUploadAttemptID])
REFERENCES [dbo].[GisUploadAttempt] ([GisUploadAttemptID])
GO
