SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProgramIndexProjectCode](
	[ProgramIndexProjectCodeID] [int] IDENTITY(1,1) NOT NULL,
	[ProgramIndexID] [int] NOT NULL,
	[ProjectCodeID] [int] NOT NULL,
 CONSTRAINT [PK_ProgramIndexProjectCode_ProgramIndexProjectCodeID] PRIMARY KEY CLUSTERED 
(
	[ProgramIndexProjectCodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ProgramIndexProjectCode_ProgramIndexID_ProjectCodeID] UNIQUE NONCLUSTERED 
(
	[ProgramIndexID] ASC,
	[ProjectCodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProgramIndexProjectCode]  WITH CHECK ADD  CONSTRAINT [FK_ProgramIndexProjectCode_ProgramIndex_ProgramIndexID] FOREIGN KEY([ProgramIndexID])
REFERENCES [dbo].[ProgramIndex] ([ProgramIndexID])
GO
ALTER TABLE [dbo].[ProgramIndexProjectCode] CHECK CONSTRAINT [FK_ProgramIndexProjectCode_ProgramIndex_ProgramIndexID]
GO
ALTER TABLE [dbo].[ProgramIndexProjectCode]  WITH CHECK ADD  CONSTRAINT [FK_ProgramIndexProjectCode_ProjectCode_ProjectCodeID] FOREIGN KEY([ProjectCodeID])
REFERENCES [dbo].[ProjectCode] ([ProjectCodeID])
GO
ALTER TABLE [dbo].[ProgramIndexProjectCode] CHECK CONSTRAINT [FK_ProgramIndexProjectCode_ProjectCode_ProjectCodeID]