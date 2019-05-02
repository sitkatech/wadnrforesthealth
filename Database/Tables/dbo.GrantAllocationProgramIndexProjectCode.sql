SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantAllocationProgramIndexProjectCode](
	[GrantAllocationProgramIndexProjectCodeID] [int] IDENTITY(1,1) NOT NULL,
	[GrantAllocationID] [int] NOT NULL,
	[ProgramIndexProjectCodeID] [int] NOT NULL,
 CONSTRAINT [PK_GrantAllocationProgramIndexProjectCode_GrantAllocationProgramIndexProjectCodeID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationProgramIndexProjectCodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_GrantAllocationProjectCode_GrantAllocationID_ProgramIndexProjectCodeID] UNIQUE NONCLUSTERED 
(
	[GrantAllocationID] ASC,
	[ProgramIndexProjectCodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GrantAllocationProgramIndexProjectCode]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationProgramIndexProjectCode_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO
ALTER TABLE [dbo].[GrantAllocationProgramIndexProjectCode] CHECK CONSTRAINT [FK_GrantAllocationProgramIndexProjectCode_GrantAllocation_GrantAllocationID]
GO
ALTER TABLE [dbo].[GrantAllocationProgramIndexProjectCode]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationProgramIndexProjectCode_ProgramIndexProjectCode_ProgramIndexProjectCodeID] FOREIGN KEY([ProgramIndexProjectCodeID])
REFERENCES [dbo].[ProgramIndexProjectCode] ([ProgramIndexProjectCodeID])
GO
ALTER TABLE [dbo].[GrantAllocationProgramIndexProjectCode] CHECK CONSTRAINT [FK_GrantAllocationProgramIndexProjectCode_ProgramIndexProjectCode_ProgramIndexProjectCodeID]