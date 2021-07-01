SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantAllocationAward](
	[GrantAllocationAwardID] [int] IDENTITY(1,1) NOT NULL,
	[GrantAllocationID] [int] NOT NULL,
	[FocusAreaID] [int] NOT NULL,
	[GrantAllocationAwardName] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GrantAllocationAwardExpirationDate] [datetime] NOT NULL,
	[ContractorInvoiceContractor] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GrantAllocationAwardCalendarStartYear] [int] NOT NULL,
 CONSTRAINT [PK_GrantAllocationAward_GrantAllocationAwardID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationAwardID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GrantAllocationAward]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationAward_FocusArea_FocusAreaID] FOREIGN KEY([FocusAreaID])
REFERENCES [dbo].[FocusArea] ([FocusAreaID])
GO
ALTER TABLE [dbo].[GrantAllocationAward] CHECK CONSTRAINT [FK_GrantAllocationAward_FocusArea_FocusAreaID]
GO
ALTER TABLE [dbo].[GrantAllocationAward]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationAward_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO
ALTER TABLE [dbo].[GrantAllocationAward] CHECK CONSTRAINT [FK_GrantAllocationAward_GrantAllocation_GrantAllocationID]