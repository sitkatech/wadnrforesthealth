ALTER TABLE [dbo].[Project] DROP CONSTRAINT [FK_Project_TaxonomyLeaf_TaxonomyLeafID]
ALTER TABLE [dbo].[TaxonomyLeafPerformanceMeasure] DROP CONSTRAINT [FK_TaxonomyLeafPerformanceMeasure_TaxonomyLeaf_TaxonomyLeafID]
ALTER TABLE [dbo].[TaxonomyLeafPerformanceMeasure] DROP CONSTRAINT [AK_TaxonomyLeafPerformanceMeasure_TaxonomyLeafID_PerformanceMeasureID]
ALTER TABLE [dbo].[TaxonomyLeaf] DROP CONSTRAINT [PK_TaxonomyLeaf_TaxonomyLeafID] WITH ( ONLINE = OFF )
GO

exec sp_rename 'dbo.TaxonomyLeaf', 'ProjectType';
GO

exec sp_rename 'dbo.ProjectType.TaxonomyLeafID', 'ProjectTypeID';
GO

exec sp_rename 'dbo.Project.TaxonomyLeafID', 'ProjectTypeID';
GO

/****** Object:  Index [PK_TaxonomyLeaf_TaxonomyLeafID]    Script Date: 1/24/2019 10:37:28 AM ******/
ALTER TABLE [dbo].[ProjectType] ADD  CONSTRAINT [PK_ProjectType_ProjectTypeID] PRIMARY KEY CLUSTERED 
(
	[ProjectTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_ProjectType_ProjectTypeID] FOREIGN KEY([ProjectTypeID])
REFERENCES [dbo].[ProjectType] ([ProjectTypeID])
GO

ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_ProjectType_ProjectTypeID]
GO

