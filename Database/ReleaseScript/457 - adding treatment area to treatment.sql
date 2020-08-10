ALTER TABLE dbo.Treatment ADD TreatmentAreaID int


GO



ALTER TABLE [dbo].[Treatment]  WITH CHECK ADD  CONSTRAINT [FK_Treatment_TreatmentArea_TreatmentAreaID] FOREIGN KEY([TreatmentAreaID])
REFERENCES [dbo].[TreatmentArea] ([TreatmentAreaID])
GO

ALTER TABLE [dbo].[Treatment] CHECK CONSTRAINT [FK_Treatment_TreatmentArea_TreatmentAreaID]
GO


ALTER TABLE dbo.Treatment DROP COLUMN TreatmentFeature 

