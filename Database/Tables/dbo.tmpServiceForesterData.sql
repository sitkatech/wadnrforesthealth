SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tmpServiceForesterData](
	[ForesterRoleID] [int] NULL,
	[ForesterWorkUnitName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RegionName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ForesterWorkUnitLocation] [geometry] NULL,
	[ForesterFullName] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ForesterFirstName] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ForesterLastName] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ForesterPhone] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ForesterEmail] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PersonID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
