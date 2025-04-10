SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PerformanceMeasureSubcategory](
	[PerformanceMeasureSubcategoryID] [int] IDENTITY(1,1) NOT NULL,
	[PerformanceMeasureID] [int] NOT NULL,
	[PerformanceMeasureSubcategoryDisplayName] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ChartConfigurationJson] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GoogleChartTypeID] [int] NULL,
 CONSTRAINT [PK_PerformanceMeasureSubcategory_PerformanceMeasureSubcategoryID] PRIMARY KEY CLUSTERED 
(
	[PerformanceMeasureSubcategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_PerformanceMeasureSubcategory_PerformanceMeasureSubcategoryID_PerformanceMeasureID] UNIQUE NONCLUSTERED 
(
	[PerformanceMeasureSubcategoryID] ASC,
	[PerformanceMeasureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[PerformanceMeasureSubcategory]  WITH CHECK ADD  CONSTRAINT [FK_PerformanceMeasureSubcategory_GoogleChartType_GoogleChartTypeID] FOREIGN KEY([GoogleChartTypeID])
REFERENCES [dbo].[GoogleChartType] ([GoogleChartTypeID])
GO
ALTER TABLE [dbo].[PerformanceMeasureSubcategory] CHECK CONSTRAINT [FK_PerformanceMeasureSubcategory_GoogleChartType_GoogleChartTypeID]
GO
ALTER TABLE [dbo].[PerformanceMeasureSubcategory]  WITH CHECK ADD  CONSTRAINT [FK_PerformanceMeasureSubcategory_PerformanceMeasure_PerformanceMeasureID] FOREIGN KEY([PerformanceMeasureID])
REFERENCES [dbo].[PerformanceMeasure] ([PerformanceMeasureID])
GO
ALTER TABLE [dbo].[PerformanceMeasureSubcategory] CHECK CONSTRAINT [FK_PerformanceMeasureSubcategory_PerformanceMeasure_PerformanceMeasureID]