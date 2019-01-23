SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FocusArea](
	[FocusAreaID] [int] IDENTITY(1,1) NOT NULL,
	[FocusAreaName] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FocusAreaStatusID] [int] NOT NULL,
	[FocusAreaLocation] [geometry] NULL,
	[RegionID] [int] NOT NULL,
 CONSTRAINT [PK_FocusArea_FocusAreaID] PRIMARY KEY CLUSTERED 
(
	[FocusAreaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_FocusArea_FocusAreaName] UNIQUE NONCLUSTERED 
(
	[FocusAreaName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[FocusArea]  WITH CHECK ADD  CONSTRAINT [FK_FocusArea_FocusAreaStatus_FocusAreaStatusID] FOREIGN KEY([FocusAreaStatusID])
REFERENCES [dbo].[FocusAreaStatus] ([FocusAreaStatusID])
GO
ALTER TABLE [dbo].[FocusArea] CHECK CONSTRAINT [FK_FocusArea_FocusAreaStatus_FocusAreaStatusID]
GO
ALTER TABLE [dbo].[FocusArea]  WITH CHECK ADD  CONSTRAINT [FK_FocusArea_Region_RegionID] FOREIGN KEY([RegionID])
REFERENCES [dbo].[Region] ([RegionID])
GO
ALTER TABLE [dbo].[FocusArea] CHECK CONSTRAINT [FK_FocusArea_Region_RegionID]