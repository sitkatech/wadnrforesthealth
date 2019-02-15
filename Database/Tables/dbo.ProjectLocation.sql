SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectLocation](
	[ProjectLocationID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[ProjectLocationGeometry] [geometry] NOT NULL,
	[ProjectLocationNotes] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProjectLocationTypeID] [int] NOT NULL,
	[ProjectLocationName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_ProjectLocation_ProjectLocationID] PRIMARY KEY CLUSTERED 
(
	[ProjectLocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ProjectLocation_ProjectID_ProjectLocationName] UNIQUE NONCLUSTERED 
(
	[ProjectID] ASC,
	[ProjectLocationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectLocation]  WITH CHECK ADD  CONSTRAINT [FK_ProjectLocation_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[ProjectLocation] CHECK CONSTRAINT [FK_ProjectLocation_Project_ProjectID]
GO
ALTER TABLE [dbo].[ProjectLocation]  WITH CHECK ADD  CONSTRAINT [FK_ProjectLocation_ProjectLocationType_ProjectLocationTypeID] FOREIGN KEY([ProjectLocationTypeID])
REFERENCES [dbo].[ProjectLocationType] ([ProjectLocationTypeID])
GO
ALTER TABLE [dbo].[ProjectLocation] CHECK CONSTRAINT [FK_ProjectLocation_ProjectLocationType_ProjectLocationTypeID]