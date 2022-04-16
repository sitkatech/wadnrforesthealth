SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PclVectorRanked](
	[PclVectorRankedID] [int] IDENTITY(1,1) NOT NULL,
	[PriorityLandscapeID] [int] NOT NULL,
	[Feature] [geometry] NOT NULL,
	[Color] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_PclVectorRanked_PclVectorRankedID] PRIMARY KEY CLUSTERED 
(
	[PclVectorRankedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[PclVectorRanked]  WITH CHECK ADD  CONSTRAINT [FK_PclVectorRanked_PriorityLandscape_PriorityLandscapeID] FOREIGN KEY([PriorityLandscapeID])
REFERENCES [dbo].[PriorityLandscape] ([PriorityLandscapeID])
GO
ALTER TABLE [dbo].[PclVectorRanked] CHECK CONSTRAINT [FK_PclVectorRanked_PriorityLandscape_PriorityLandscapeID]