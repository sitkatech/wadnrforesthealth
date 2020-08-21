SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectGrantAllocationRequestFundingSource](
	[ProjectGrantAllocationRequestFundingSourceID] [int] NOT NULL,
	[ProjectGrantAllocationRequestFundingSourceName] [varchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProjectGrantAllocationRequestFundingSourceDisplayName] [varchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_ProjectGrantAllocationRequestFundingSource_ProjectGrantAllocationRequestFundingSourceID] PRIMARY KEY CLUSTERED 
(
	[ProjectGrantAllocationRequestFundingSourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
