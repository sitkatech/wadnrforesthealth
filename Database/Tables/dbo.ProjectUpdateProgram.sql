SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectUpdateProgram](
	[ProjectUpdateProgramID] [int] IDENTITY(1,1) NOT NULL,
	[ProgramID] [int] NOT NULL,
	[ProjectUpdateID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectUpdateProgram_ProjectUpdateProgramID] PRIMARY KEY CLUSTERED 
(
	[ProjectUpdateProgramID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_ProjectUpdateProgram_ProjectUpdateID_ProgramID] UNIQUE NONCLUSTERED 
(
	[ProjectUpdateID] ASC,
	[ProgramID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectUpdateProgram]  WITH CHECK ADD  CONSTRAINT [FK_ProjectUpdateProgram_Program_ProgramID] FOREIGN KEY([ProgramID])
REFERENCES [dbo].[Program] ([ProgramID])
GO
ALTER TABLE [dbo].[ProjectUpdateProgram] CHECK CONSTRAINT [FK_ProjectUpdateProgram_Program_ProgramID]
GO
ALTER TABLE [dbo].[ProjectUpdateProgram]  WITH CHECK ADD  CONSTRAINT [FK_ProjectUpdateProgram_ProjectUpdate_ProjectUpdateID] FOREIGN KEY([ProjectUpdateID])
REFERENCES [dbo].[ProjectUpdate] ([ProjectUpdateID])
GO
ALTER TABLE [dbo].[ProjectUpdateProgram] CHECK CONSTRAINT [FK_ProjectUpdateProgram_ProjectUpdate_ProjectUpdateID]