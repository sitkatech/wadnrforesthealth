SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tmpGrantsFlatFile](
	[Grant_Number] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CFDA] [float] NULL,
	[Title] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Program_Manager] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Prog_Index] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Project_Codes] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Funding_Increase] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Federal_Fund_Code] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Funds_Awarded] [float] NULL,
	[Matching_Funds] [int] NULL,
	[Grant_Total] [int] NULL,
	[DNR_Match_Amount] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Non_DNR_Other_Match] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Landowner_Match] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Match_PI_Alpha_Code] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Expend_Method] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Start_Date] [datetime2](7) NULL,
	[End_Date] [datetime2](7) NULL,
	[Notes] [nvarchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
