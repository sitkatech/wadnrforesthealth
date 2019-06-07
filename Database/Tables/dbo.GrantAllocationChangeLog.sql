SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantAllocationChangeLog](
	[GrantAllocationChangeLogID] [int] IDENTITY(1,1) NOT NULL,
	[GrantAllocationID] [int] NOT NULL,
	[GrantAllocationAmountOldValue] [money] NULL,
	[GrantAllocationAmountNewValue] [money] NULL,
	[GrantAllocationAmountNote] [nvarchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ChangePersonID] [int] NOT NULL,
	[ChangeDate] [datetime] NOT NULL,
 CONSTRAINT [PK_GrantAllocationChangeLog_GrantAllocationChangeLogID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationChangeLogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GrantAllocationChangeLog]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationChangeLog_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO
ALTER TABLE [dbo].[GrantAllocationChangeLog] CHECK CONSTRAINT [FK_GrantAllocationChangeLog_GrantAllocation_GrantAllocationID]
GO
ALTER TABLE [dbo].[GrantAllocationChangeLog]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationChangeLog_Person_ChangePersonID_PersonID] FOREIGN KEY([ChangePersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[GrantAllocationChangeLog] CHECK CONSTRAINT [FK_GrantAllocationChangeLog_Person_ChangePersonID_PersonID]