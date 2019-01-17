SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GeospatialAreaType](
	[GeospatialAreaTypeID] [int] IDENTITY(1,1) NOT NULL,
	[GeospatialAreaTypeName] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GeospatialAreaTypeNamePluralized] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GeospatialAreaIntroContent] [dbo].[html] NULL,
	[GeospatialAreaTypeDefinition] [dbo].[html] NULL,
	[MapServiceUrl] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GeospatialAreaLayerName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_GeospatialAreaType_GeospatialAreaTypeID] PRIMARY KEY CLUSTERED 
(
	[GeospatialAreaTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
