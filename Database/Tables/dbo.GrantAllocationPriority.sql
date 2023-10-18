SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantAllocationPriority](
	[GrantAllocationPriorityID] [int] NOT NULL,
	[GrantAllocationPriorityNumber] [int] NOT NULL,
	[GrantAllocationPriorityColor] [varchar](8) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_GrantAllocationSource_GrantAllocationPriorityID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationPriorityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
