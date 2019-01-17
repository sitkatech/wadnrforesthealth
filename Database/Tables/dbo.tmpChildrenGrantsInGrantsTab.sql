SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tmpChildrenGrantsInGrantsTab](
	[ChildGrantID] [int] IDENTITY(1,1) NOT NULL,
	[Parent Child (SITKA)] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Grant #] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CFDA #] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Title] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Program Manager] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Prog Index] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Project Codes] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Funding increase] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Federal Fund Code] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Funds Awarded] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Matching Funds] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Grant Total] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DNR Match Amount] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Non-DNR Other Match] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Landowner Match] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Match PI & Alpha Code] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Expend Method] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Start Date] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[End Date] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Notes] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_tmpChildrenGrantsInGrantsTab_ChildGrantID] PRIMARY KEY CLUSTERED 
(
	[ChildGrantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
