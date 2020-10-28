--project 12684 and 12720

delete from dbo.ProjectPriorityLandscape where PriorityLandscapeID = 7545 or PriorityLandscapeID = 7534

delete from dbo.PriorityLandscape where PriorityLandscapeID = 7545 or PriorityLandscapeID = 7534


ALTER TABLE [dbo].[ProjectPriorityLandscape] DROP CONSTRAINT [FK_ProjectPriorityLandscape_PriorityLandscape_PriorityLandscapeID]
GO


ALTER TABLE [dbo].[ProjectPriorityLandscapeUpdate] DROP CONSTRAINT [FK_ProjectPriorityLandscapeUpdate_PriorityLandscape_PriorityLandscapeID]
GO





delete from dbo.PriorityLandscape


ALTER TABLE dbo.PriorityLandscape ADD PlanYear int





