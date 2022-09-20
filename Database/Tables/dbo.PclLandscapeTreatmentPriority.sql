SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PclLandscapeTreatmentPriority](
	[PclLandscapeTreatmentPriorityID] [int] IDENTITY(1,1) NOT NULL,
	[PriorityLandscapeID] [int] NOT NULL,
	[Feature] [geometry] NOT NULL,
	[Color] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_PclLandscapeTreatmentPriority_PclLandscapeTreatmentPriorityID] PRIMARY KEY CLUSTERED 
(
	[PclLandscapeTreatmentPriorityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[PclLandscapeTreatmentPriority]  WITH CHECK ADD  CONSTRAINT [FK_PclLandscapeTreatmentPriority_PriorityLandscape_PriorityLandscapeID] FOREIGN KEY([PriorityLandscapeID])
REFERENCES [dbo].[PriorityLandscape] ([PriorityLandscapeID])
GO
ALTER TABLE [dbo].[PclLandscapeTreatmentPriority] CHECK CONSTRAINT [FK_PclLandscapeTreatmentPriority_PriorityLandscape_PriorityLandscapeID]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF

GO
CREATE SPATIAL INDEX [SPATIAL_PclLandscapeTreatmentPriority_Feature] ON [dbo].[PclLandscapeTreatmentPriority]
(
	[Feature]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-121, 47, -120, 48), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]