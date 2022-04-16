SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PclBoundaryLine](
	[PclBoundaryLineID] [int] IDENTITY(1,1) NOT NULL,
	[PriorityLandscapeID] [int] NOT NULL,
	[Feature] [geometry] NOT NULL,
 CONSTRAINT [PK_PclBoundaryLine_PclBoundaryLineID] PRIMARY KEY CLUSTERED 
(
	[PclBoundaryLineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[PclBoundaryLine]  WITH CHECK ADD  CONSTRAINT [FK_PclBoundaryLine_PriorityLandscape_PriorityLandscapeID] FOREIGN KEY([PriorityLandscapeID])
REFERENCES [dbo].[PriorityLandscape] ([PriorityLandscapeID])
GO
ALTER TABLE [dbo].[PclBoundaryLine] CHECK CONSTRAINT [FK_PclBoundaryLine_PriorityLandscape_PriorityLandscapeID]