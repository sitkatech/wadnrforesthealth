exec sp_rename 'dbo.ProjectType.TaxonomyLeafName', 'ProjectTypeName';
GO
exec sp_rename 'dbo.ProjectType.TaxonomyLeafDescription', 'ProjectTypeDescription';
GO
exec sp_rename 'dbo.ProjectType.TaxonomyLeafCode', 'ProjectTypeCode';
GO
exec sp_rename 'dbo.ProjectType.TaxonomyLeafSortOrder', 'ProjectTypeSortOrder';
GO

ALTER TABLE [dbo].[ProjectType] DROP CONSTRAINT [FK_TaxonomyLeaf_TaxonomyBranch_TaxonomyBranchID]
GO

ALTER TABLE [dbo].[ProjectType]  WITH CHECK ADD  CONSTRAINT [FK_ProjectType_TaxonomyBranch_TaxonomyBranchID] FOREIGN KEY([TaxonomyBranchID])
REFERENCES [dbo].[TaxonomyBranch] ([TaxonomyBranchID])
GO

ALTER TABLE [dbo].[ProjectType] CHECK CONSTRAINT [FK_ProjectType_TaxonomyBranch_TaxonomyBranchID]
GO

