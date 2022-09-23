ALTER TABLE dbo.Project
ADD NoCountiesExplanation varchar(4000) null;

ALTER TABLE dbo.ProjectUpdateBatch
ADD NoCountiesExplanation varchar(4000) null;

CREATE TABLE [dbo].[ProjectCountyUpdate](
	[ProjectCountyUpdateID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectUpdateBatchID] [int] NOT NULL,
	[CountyID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectCountyUpdate_ProjectCountyUpdateID] PRIMARY KEY CLUSTERED ([ProjectCountyUpdateID]),
 CONSTRAINT [AK_ProjectCountyUpdate_ProjectUpdateBatchID_CountyID] UNIQUE NONCLUSTERED ([ProjectUpdateBatchID], [CountyID])
) 
GO

ALTER TABLE [dbo].[ProjectCountyUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectCountyUpdate_County_CountyID] FOREIGN KEY([CountyID])
REFERENCES [dbo].[County] ([CountyID])
GO

ALTER TABLE [dbo].[ProjectCountyUpdate] CHECK CONSTRAINT [FK_ProjectCountyUpdate_County_CountyID]
GO

ALTER TABLE [dbo].[ProjectCountyUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectCountyUpdate_ProjectUpdateBatch_ProjectUpdateBatchID] FOREIGN KEY([ProjectUpdateBatchID])
REFERENCES [dbo].[ProjectUpdateBatch] ([ProjectUpdateBatchID])
GO

ALTER TABLE [dbo].[ProjectCountyUpdate] CHECK CONSTRAINT [FK_ProjectCountyUpdate_ProjectUpdateBatch_ProjectUpdateBatchID]
GO

