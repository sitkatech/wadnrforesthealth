ALTER TABLE [dbo].[ProjectTypePerformanceMeasure] DROP CONSTRAINT [FK_TaxonomyLeafPerformanceMeasure_PerformanceMeasure_PerformanceMeasureID]
GO

ALTER TABLE [dbo].[ProjectTypePerformanceMeasure]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTypePerformanceMeasure_PerformanceMeasure_PerformanceMeasureID] FOREIGN KEY([PerformanceMeasureID])
REFERENCES [dbo].[PerformanceMeasure] ([PerformanceMeasureID])
GO

ALTER TABLE [dbo].[ProjectTypePerformanceMeasure] CHECK CONSTRAINT [FK_ProjectTypePerformanceMeasure_PerformanceMeasure_PerformanceMeasureID]
GO

exec sp_rename 'dbo.ProjectTypePerformanceMeasure.IsPrimaryTaxonomyLeaf', 'IsPrimaryProjectType';
GO