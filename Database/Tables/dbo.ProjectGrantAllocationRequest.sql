SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectGrantAllocationRequest](
	[ProjectGrantAllocationRequestID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[GrantAllocationID] [int] NOT NULL,
	[TotalAmount] [money] NULL,
	[MatchAmount] [money] NULL,
	[PayAmount] [money] NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[ImportedFromTabularData] [bit] NOT NULL,
 CONSTRAINT [PK_ProjectGrantAllocationRequest_ProjectGrantAllocationRequestID] PRIMARY KEY CLUSTERED 
(
	[ProjectGrantAllocationRequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_ProjectGrantAllocationRequest_ProjectID_GrantAllocationID] UNIQUE NONCLUSTERED 
(
	[ProjectID] ASC,
	[GrantAllocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectGrantAllocationRequest]  WITH CHECK ADD  CONSTRAINT [FK_ProjectGrantAllocationRequest_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO
ALTER TABLE [dbo].[ProjectGrantAllocationRequest] CHECK CONSTRAINT [FK_ProjectGrantAllocationRequest_GrantAllocation_GrantAllocationID]
GO
ALTER TABLE [dbo].[ProjectGrantAllocationRequest]  WITH CHECK ADD  CONSTRAINT [FK_ProjectGrantAllocationRequest_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[ProjectGrantAllocationRequest] CHECK CONSTRAINT [FK_ProjectGrantAllocationRequest_Project_ProjectID]