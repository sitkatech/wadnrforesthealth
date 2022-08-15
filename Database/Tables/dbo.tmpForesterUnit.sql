SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tmpForesterUnit](
	[OBJECTID_1] [int] NULL,
	[OBJECTID] [int] NULL,
	[FP_UNIT_NM] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[JURISDICT_] [smallint] NULL,
	[REGION_NM] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[JOB_POSN_N] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FP_FORESTE] [varchar](38) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FP_FORES_1] [varchar](13) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FP_FORES_2] [varchar](24) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PHONE_NO] [varchar](14) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Shape_Length] [float] NULL,
	[Shape_Area] [float] NULL,
	[GEOM] [geometry] NULL,
	[PersonID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
