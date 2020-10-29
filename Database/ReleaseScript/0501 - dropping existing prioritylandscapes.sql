--project 12684 and 12720

delete from dbo.ProjectPriorityLandscape where PriorityLandscapeID = 7545 or PriorityLandscapeID = 7534

delete from dbo.PriorityLandscapeFileResource where PriorityLandscapeID = 7545 or PriorityLandscapeID = 7534

delete from dbo.PriorityLandscape where PriorityLandscapeID = 7545 or PriorityLandscapeID = 7534


ALTER TABLE [dbo].[ProjectPriorityLandscape] DROP CONSTRAINT [FK_ProjectPriorityLandscape_PriorityLandscape_PriorityLandscapeID]
GO


ALTER TABLE [dbo].[ProjectPriorityLandscapeUpdate] DROP CONSTRAINT [FK_ProjectPriorityLandscapeUpdate_PriorityLandscape_PriorityLandscapeID]
GO

ALTER TABLE [dbo].[PriorityLandscapeFileResource] DROP CONSTRAINT [FK_PriorityLandscapeFileResource_FileResource_FileResourceID]
GO



delete from dbo.PriorityLandscape


ALTER TABLE dbo.PriorityLandscape ADD PlanYear int









ALTER TABLE [dbo].[PriorityLandscapeFileResource]  WITH CHECK ADD  CONSTRAINT [FK_PriorityLandscapeFileResource_FileResource_FileResourceID] FOREIGN KEY([FileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PriorityLandscapeFileResource] CHECK CONSTRAINT [FK_PriorityLandscapeFileResource_FileResource_FileResourceID]
GO

