SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomPage](
	[CustomPageID] [int] IDENTITY(1,1) NOT NULL,
	[CustomPageDisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CustomPageVanityUrl] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CustomPageDisplayTypeID] [int] NOT NULL,
	[CustomPageContent] [dbo].[html] NULL,
	[CustomPageNavigationSectionID] [int] NOT NULL,
 CONSTRAINT [PK_CustomPage_CustomPageID] PRIMARY KEY CLUSTERED 
(
	[CustomPageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[CustomPage]  WITH CHECK ADD  CONSTRAINT [FK_CustomPage_CustomPageDisplayType_CustomPageDisplayTypeID] FOREIGN KEY([CustomPageDisplayTypeID])
REFERENCES [dbo].[CustomPageDisplayType] ([CustomPageDisplayTypeID])
GO
ALTER TABLE [dbo].[CustomPage] CHECK CONSTRAINT [FK_CustomPage_CustomPageDisplayType_CustomPageDisplayTypeID]
GO
ALTER TABLE [dbo].[CustomPage]  WITH CHECK ADD  CONSTRAINT [FK_CustomPage_CustomPageNavigationSection_CustomPageNavigationSectionID] FOREIGN KEY([CustomPageNavigationSectionID])
REFERENCES [dbo].[CustomPageNavigationSection] ([CustomPageNavigationSectionID])
GO
ALTER TABLE [dbo].[CustomPage] CHECK CONSTRAINT [FK_CustomPage_CustomPageNavigationSection_CustomPageNavigationSectionID]