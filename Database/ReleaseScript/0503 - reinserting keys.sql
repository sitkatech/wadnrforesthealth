




ALTER TABLE [dbo].[ProjectPriorityLandscapeUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPriorityLandscapeUpdate_PriorityLandscape_PriorityLandscapeID] FOREIGN KEY([PriorityLandscapeID])
REFERENCES [dbo].[PriorityLandscape] ([PriorityLandscapeID])
GO

ALTER TABLE [dbo].[ProjectPriorityLandscapeUpdate] CHECK CONSTRAINT [FK_ProjectPriorityLandscapeUpdate_PriorityLandscape_PriorityLandscapeID]
GO

ALTER TABLE [dbo].[ProjectPriorityLandscape]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPriorityLandscape_PriorityLandscape_PriorityLandscapeID] FOREIGN KEY([PriorityLandscapeID])
REFERENCES [dbo].[PriorityLandscape] ([PriorityLandscapeID])
GO

ALTER TABLE [dbo].[ProjectPriorityLandscape] CHECK CONSTRAINT [FK_ProjectPriorityLandscape_PriorityLandscape_PriorityLandscapeID]
GO

