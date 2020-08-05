ALTER TABLE dbo.Treatment ADD TreatmentTypeID int


GO



ALTER TABLE [dbo].[Treatment]  WITH CHECK ADD  CONSTRAINT [FK_Treatment_TreatmentType_TreatmentTypeID] FOREIGN KEY([TreatmentTypeID])
REFERENCES [dbo].[TreatmentType] ([TreatmentTypeID])
GO

ALTER TABLE [dbo].[Treatment] CHECK CONSTRAINT [FK_Treatment_TreatmentType_TreatmentTypeID]
GO


ALTER TABLE dbo.Treatment ALTER COLUMN TreatmentTypeID int not null
