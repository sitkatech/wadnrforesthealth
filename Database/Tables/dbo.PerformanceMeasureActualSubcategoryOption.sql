SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PerformanceMeasureActualSubcategoryOption](
	[PerformanceMeasureActualSubcategoryOptionID] [int] IDENTITY(1,1) NOT NULL,
	[PerformanceMeasureActualID] [int] NOT NULL,
	[PerformanceMeasureSubcategoryOptionID] [int] NOT NULL,
	[PerformanceMeasureID] [int] NOT NULL,
	[PerformanceMeasureSubcategoryID] [int] NOT NULL,
 CONSTRAINT [PK_PerformanceMeasureActualSubcategoryOption_PerformanceMeasureActualSubcategoryOptionID] PRIMARY KEY CLUSTERED 
(
	[PerformanceMeasureActualSubcategoryOptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PerformanceMeasureActualSubcategoryOption]  WITH CHECK ADD  CONSTRAINT [FK_PerformanceMeasureActualSubcategoryOption_PerformanceMeasure_PerformanceMeasureID] FOREIGN KEY([PerformanceMeasureID])
REFERENCES [dbo].[PerformanceMeasure] ([PerformanceMeasureID])
GO
ALTER TABLE [dbo].[PerformanceMeasureActualSubcategoryOption] CHECK CONSTRAINT [FK_PerformanceMeasureActualSubcategoryOption_PerformanceMeasure_PerformanceMeasureID]
GO
ALTER TABLE [dbo].[PerformanceMeasureActualSubcategoryOption]  WITH CHECK ADD  CONSTRAINT [FK_PerformanceMeasureActualSubcategoryOption_PerformanceMeasureActual_PerformanceMeasureActualID] FOREIGN KEY([PerformanceMeasureActualID])
REFERENCES [dbo].[PerformanceMeasureActual] ([PerformanceMeasureActualID])
GO
ALTER TABLE [dbo].[PerformanceMeasureActualSubcategoryOption] CHECK CONSTRAINT [FK_PerformanceMeasureActualSubcategoryOption_PerformanceMeasureActual_PerformanceMeasureActualID]
GO
ALTER TABLE [dbo].[PerformanceMeasureActualSubcategoryOption]  WITH CHECK ADD  CONSTRAINT [FK_PerformanceMeasureActualSubcategoryOption_PerformanceMeasureActual_PerformanceMeasureActualID_PerformanceMeasureID] FOREIGN KEY([PerformanceMeasureActualID], [PerformanceMeasureID])
REFERENCES [dbo].[PerformanceMeasureActual] ([PerformanceMeasureActualID], [PerformanceMeasureID])
GO
ALTER TABLE [dbo].[PerformanceMeasureActualSubcategoryOption] CHECK CONSTRAINT [FK_PerformanceMeasureActualSubcategoryOption_PerformanceMeasureActual_PerformanceMeasureActualID_PerformanceMeasureID]
GO
ALTER TABLE [dbo].[PerformanceMeasureActualSubcategoryOption]  WITH CHECK ADD  CONSTRAINT [FK_PerformanceMeasureActualSubcategoryOption_PerformanceMeasureSubcategory_PerformanceMeasureSubcategoryID] FOREIGN KEY([PerformanceMeasureSubcategoryID])
REFERENCES [dbo].[PerformanceMeasureSubcategory] ([PerformanceMeasureSubcategoryID])
GO
ALTER TABLE [dbo].[PerformanceMeasureActualSubcategoryOption] CHECK CONSTRAINT [FK_PerformanceMeasureActualSubcategoryOption_PerformanceMeasureSubcategory_PerformanceMeasureSubcategoryID]
GO
ALTER TABLE [dbo].[PerformanceMeasureActualSubcategoryOption]  WITH CHECK ADD  CONSTRAINT [FK_PerformanceMeasureActualSubcategoryOption_PerformanceMeasureSubcategory_PerformanceMeasureSubcategoryID_PerformanceMeasureID] FOREIGN KEY([PerformanceMeasureSubcategoryID], [PerformanceMeasureID])
REFERENCES [dbo].[PerformanceMeasureSubcategory] ([PerformanceMeasureSubcategoryID], [PerformanceMeasureID])
GO
ALTER TABLE [dbo].[PerformanceMeasureActualSubcategoryOption] CHECK CONSTRAINT [FK_PerformanceMeasureActualSubcategoryOption_PerformanceMeasureSubcategory_PerformanceMeasureSubcategoryID_PerformanceMeasureID]
GO
ALTER TABLE [dbo].[PerformanceMeasureActualSubcategoryOption]  WITH CHECK ADD  CONSTRAINT [FK_PerformanceMeasureActualSubcategoryOption_PerformanceMeasureSubcategoryOption_PerformanceMeasureSubcategoryOptionID] FOREIGN KEY([PerformanceMeasureSubcategoryOptionID])
REFERENCES [dbo].[PerformanceMeasureSubcategoryOption] ([PerformanceMeasureSubcategoryOptionID])
GO
ALTER TABLE [dbo].[PerformanceMeasureActualSubcategoryOption] CHECK CONSTRAINT [FK_PerformanceMeasureActualSubcategoryOption_PerformanceMeasureSubcategoryOption_PerformanceMeasureSubcategoryOptionID]
GO
ALTER TABLE [dbo].[PerformanceMeasureActualSubcategoryOption]  WITH CHECK ADD  CONSTRAINT [FK_PerformanceMeasureActualSubcategoryOption_PerformanceMeasureSubcategoryOption_PerformanceMeasureSubcategoryOptionID_Performan] FOREIGN KEY([PerformanceMeasureSubcategoryOptionID], [PerformanceMeasureSubcategoryID])
REFERENCES [dbo].[PerformanceMeasureSubcategoryOption] ([PerformanceMeasureSubcategoryOptionID], [PerformanceMeasureSubcategoryID])
GO
ALTER TABLE [dbo].[PerformanceMeasureActualSubcategoryOption] CHECK CONSTRAINT [FK_PerformanceMeasureActualSubcategoryOption_PerformanceMeasureSubcategoryOption_PerformanceMeasureSubcategoryOptionID_Performan]