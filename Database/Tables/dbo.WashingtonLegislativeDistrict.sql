SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WashingtonLegislativeDistrict](
	[WashingtonLegislativeDistrictID] [int] IDENTITY(1,1) NOT NULL,
	[WashingtonLegislativeDistrictLocation] [geometry] NOT NULL,
	[WashingtonLegislativeDistrictNumber] [int] NOT NULL,
	[WashingtonLegislativeDistrictName] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_WashingtonLegislativeDistrict_WashingtonLegislativeDistrictID] PRIMARY KEY CLUSTERED 
(
	[WashingtonLegislativeDistrictID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
