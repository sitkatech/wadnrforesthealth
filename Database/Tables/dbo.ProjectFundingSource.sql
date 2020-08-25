SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectFundingSource](
	[ProjectFundingSourceID] [int] NOT NULL,
	[ProjectFundingSourceName] [varchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProjectFundingSourceDisplayName] [varchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_ProjectFundingSource_ProjectFundingSourceID] PRIMARY KEY CLUSTERED 
(
	[ProjectFundingSourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
