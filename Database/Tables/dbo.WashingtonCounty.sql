SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WashingtonCounty](
	[WashingtonCountyID] [int] IDENTITY(1,1) NOT NULL,
	[WashingtonCountyLocation] [geometry] NOT NULL,
	[WashingtonCountyName] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[WashingtonCountyFullName] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_WashingtonCounty_WashingtonCountyID] PRIMARY KEY CLUSTERED 
(
	[WashingtonCountyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
