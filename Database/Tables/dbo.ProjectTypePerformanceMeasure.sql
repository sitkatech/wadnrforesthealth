SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectTypePerformanceMeasure](
	[ProjectTypePerformanceMeasureID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectTypeID] [int] NOT NULL,
	[PerformanceMeasureID] [int] NOT NULL,
	[IsPrimaryProjectType] [bit] NOT NULL,
 CONSTRAINT [PK_ProjectTypePerformanceMeasure_ProjectTypePerformanceMeasureID] PRIMARY KEY CLUSTERED 
(
	[ProjectTypePerformanceMeasureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_ProjectTypePerformanceMeasure_ProjectTypeID_PerformanceMeasureID] UNIQUE NONCLUSTERED 
(
	[ProjectTypeID] ASC,
	[PerformanceMeasureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectTypePerformanceMeasure]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTypePerformanceMeasure_PerformanceMeasure_PerformanceMeasureID] FOREIGN KEY([PerformanceMeasureID])
REFERENCES [dbo].[PerformanceMeasure] ([PerformanceMeasureID])
GO
ALTER TABLE [dbo].[ProjectTypePerformanceMeasure] CHECK CONSTRAINT [FK_ProjectTypePerformanceMeasure_PerformanceMeasure_PerformanceMeasureID]
GO
ALTER TABLE [dbo].[ProjectTypePerformanceMeasure]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTypePerformanceMeasure_ProjectType_ProjectTypeID] FOREIGN KEY([ProjectTypeID])
REFERENCES [dbo].[ProjectType] ([ProjectTypeID])
GO
ALTER TABLE [dbo].[ProjectTypePerformanceMeasure] CHECK CONSTRAINT [FK_ProjectTypePerformanceMeasure_ProjectType_ProjectTypeID]