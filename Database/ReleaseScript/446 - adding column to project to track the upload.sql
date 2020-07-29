ALTER TABLE dbo.Project ADD CreateGisUploadAttemptID int

GO



ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_GisUploadAttempt_CreateGisUploadAttemptID_GisUploadAttemptID] FOREIGN KEY([CreateGisUploadAttemptID])
REFERENCES [dbo].GisUploadAttempt (GisUploadAttemptID)
GO


ALTER TABLE dbo.Project ADD LastUpdateGisUploadAttemptID int

GO



ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_GisUploadAttempt_LastUpdateGisUploadAttemptID_GisUploadAttemptID] FOREIGN KEY([LastUpdateGisUploadAttemptID])
REFERENCES [dbo].GisUploadAttempt (GisUploadAttemptID)
GO
