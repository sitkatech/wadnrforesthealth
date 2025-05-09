SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectLocationStaging](
	[ProjectLocationStagingID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[PersonID] [int] NOT NULL,
	[FeatureClassName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GeoJson] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SelectedProperty] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ShouldImport] [bit] NOT NULL,
 CONSTRAINT [PK_ProjectLocationStaging_ProjectLocationStagingID] PRIMARY KEY CLUSTERED 
(
	[ProjectLocationStagingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_ProjectLocationStaging_ProjectID_PersonID_FeatureClassName] UNIQUE NONCLUSTERED 
(
	[ProjectID] ASC,
	[PersonID] ASC,
	[FeatureClassName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectLocationStaging]  WITH CHECK ADD  CONSTRAINT [FK_ProjectLocationStaging_Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[ProjectLocationStaging] CHECK CONSTRAINT [FK_ProjectLocationStaging_Person_PersonID]
GO
ALTER TABLE [dbo].[ProjectLocationStaging]  WITH CHECK ADD  CONSTRAINT [FK_ProjectLocationStaging_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[ProjectLocationStaging] CHECK CONSTRAINT [FK_ProjectLocationStaging_Project_ProjectID]