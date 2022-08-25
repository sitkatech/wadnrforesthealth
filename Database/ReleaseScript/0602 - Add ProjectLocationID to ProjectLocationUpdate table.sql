ALTER TABLE dbo.ProjectLocationUpdate
ADD [ProjectLocationID] [int] NULL
GO
ALTER TABLE [dbo].[ProjectLocationUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectLocationUpdate_ProjectLocation_ProjectLocationID] FOREIGN KEY([ProjectLocationID])
REFERENCES [dbo].[ProjectLocation] ([ProjectLocationID])
GO
ALTER TABLE [dbo].[ProjectLocationUpdate] CHECK CONSTRAINT [FK_ProjectLocationUpdate_ProjectLocation_ProjectLocationID]

UPDATE PLU
	SET ProjectLocationID = PL.ProjectLocationID
FROM dbo.ProjectLocationUpdate PLU
JOIN dbo.ProjectUpdateBatch PUB ON PLU.ProjectUpdateBatchID = PUB.ProjectUpdateBatchID
LEFT JOIN dbo.ProjectLocation PL ON PUB.ProjectID = PL.ProjectID AND CAST(PLU.ProjectLocationUpdateGeometry AS VARCHAR(MAX)) = CAST(PL.ProjectLocationGeometry AS VARCHAR(MAX))