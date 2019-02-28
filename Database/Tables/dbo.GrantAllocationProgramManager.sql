SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantAllocationProgramManager](
	[GrantAllocationProgramManagerID] [int] IDENTITY(1,1) NOT NULL,
	[GrantAllocationID] [int] NOT NULL,
	[PersonID] [int] NOT NULL,
 CONSTRAINT [PK_GrantAllocationProgramManager_GrantAllocationProgramManagerID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationProgramManagerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GrantAllocationProgramManager]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationProgramManager_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO
ALTER TABLE [dbo].[GrantAllocationProgramManager] CHECK CONSTRAINT [FK_GrantAllocationProgramManager_GrantAllocation_GrantAllocationID]
GO
ALTER TABLE [dbo].[GrantAllocationProgramManager]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationProgramManager_Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[GrantAllocationProgramManager] CHECK CONSTRAINT [FK_GrantAllocationProgramManager_Person_PersonID]