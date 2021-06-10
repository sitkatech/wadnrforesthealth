SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantAllocationProgramIndexProjectCode](
	[GrantAllocationProgramIndexProjectCodeID] [int] IDENTITY(1,1) NOT NULL,
	[GrantAllocationID] [int] NOT NULL,
	[ProgramIndexID] [int] NOT NULL,
	[ProjectCodeID] [int] NULL,
 CONSTRAINT [PK_GrantAllocationProgramIndexProjectCode_GrantAllocationProgramIndexProjectCodeID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationProgramIndexProjectCodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_GrantAllocationProgramIndexProjectCode_GrantAllocationID_ProgramIndexID_ProjectCodeID] UNIQUE NONCLUSTERED 
(
	[GrantAllocationID] ASC,
	[ProgramIndexID] ASC,
	[ProjectCodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GrantAllocationProgramIndexProjectCode]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationProgramIndexProjectCode_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO
ALTER TABLE [dbo].[GrantAllocationProgramIndexProjectCode] CHECK CONSTRAINT [FK_GrantAllocationProgramIndexProjectCode_GrantAllocation_GrantAllocationID]
GO
ALTER TABLE [dbo].[GrantAllocationProgramIndexProjectCode]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationProgramIndexProjectCode_ProgramIndex_ProgramIndexID] FOREIGN KEY([ProgramIndexID])
REFERENCES [dbo].[ProgramIndex] ([ProgramIndexID])
GO
ALTER TABLE [dbo].[GrantAllocationProgramIndexProjectCode] CHECK CONSTRAINT [FK_GrantAllocationProgramIndexProjectCode_ProgramIndex_ProgramIndexID]
GO
ALTER TABLE [dbo].[GrantAllocationProgramIndexProjectCode]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationProgramIndexProjectCode_ProjectCode_ProjectCodeID] FOREIGN KEY([ProjectCodeID])
REFERENCES [dbo].[ProjectCode] ([ProjectCodeID])
GO
ALTER TABLE [dbo].[GrantAllocationProgramIndexProjectCode] CHECK CONSTRAINT [FK_GrantAllocationProgramIndexProjectCode_ProjectCode_ProjectCodeID]