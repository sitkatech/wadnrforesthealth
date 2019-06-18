SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LandownerCostShareLineItemStatus](
	[LandownerCostShareLineItemStatusID] [int] NOT NULL,
	[LandownerCostShareLineItemStatusName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LandownerCostShareLineItemStatusDisplayName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_LandownerCostShareLineItemStatus_LandownerCostShareLineItemStatusID] PRIMARY KEY CLUSTERED 
(
	[LandownerCostShareLineItemStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
