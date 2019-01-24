
/****** Object:  Index [PK_TaxonomyLeafPerformanceMeasure_TaxonomyLeafPerformanceMeasureID]    Script Date: 1/24/2019 10:42:13 AM ******/
ALTER TABLE [dbo].[TaxonomyLeafPerformanceMeasure] DROP CONSTRAINT [PK_TaxonomyLeafPerformanceMeasure_TaxonomyLeafPerformanceMeasureID] WITH ( ONLINE = OFF )
GO

exec sp_rename 'dbo.TaxonomyLeafPerformanceMeasure', 'ProjectTypePerformanceMeasure';
GO

exec sp_rename 'dbo.ProjectTypePerformanceMeasure.TaxonomyLeafPerformanceMeasureID', 'ProjectTypePerformanceMeasureID';
GO

exec sp_rename 'dbo.ProjectTypePerformanceMeasure.TaxonomyLeafID', 'ProjectTypeID';
GO


ALTER TABLE [dbo].[ProjectTypePerformanceMeasure]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTypePerformanceMeasure_ProjectType_ProjectTypeID] FOREIGN KEY([ProjectTypeID])
REFERENCES [dbo].[ProjectType] ([ProjectTypeID])
GO

ALTER TABLE [dbo].[ProjectTypePerformanceMeasure] CHECK CONSTRAINT [FK_ProjectTypePerformanceMeasure_ProjectType_ProjectTypeID]
GO

ALTER TABLE [dbo].[ProjectTypePerformanceMeasure] ADD  CONSTRAINT [AK_ProjectTypePerformanceMeasure_ProjectTypeID_PerformanceMeasureID] UNIQUE NONCLUSTERED 
(
	[ProjectTypeID] ASC,
	[PerformanceMeasureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


/****** Object:  Index [PK_ProjectTypePerformanceMeasure_ProjectTypePerformanceMeasureID]    Script Date: 1/24/2019 10:42:13 AM ******/
ALTER TABLE [dbo].[ProjectTypePerformanceMeasure] ADD  CONSTRAINT [PK_ProjectTypePerformanceMeasure_ProjectTypePerformanceMeasureID] PRIMARY KEY CLUSTERED 
(
	[ProjectTypePerformanceMeasureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO