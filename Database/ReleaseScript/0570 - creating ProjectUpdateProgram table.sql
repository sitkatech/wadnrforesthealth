


create table ProjectUpdateProgram (
	ProjectUpdateProgramID int not null identity(1,1) constraint PK_ProjectUpdateProgram_ProjectUpdateProgramID primary key,
	ProgramID int not null
	constraint FK_ProjectUpdateProgram_Program_ProgramID foreign key references dbo.Program(ProgramID),
	ProjectUpdateBatchID int not null
	constraint FK_ProjectUpdateProgram_ProjectUpdateBatch_ProjectUpdateBatchID foreign key references dbo.ProjectUpdateBatch(ProjectUpdateBatchID)
)
ALTER TABLE [dbo].[ProjectUpdateProgram] ADD  CONSTRAINT [AK_ProjectUpdateProgram_ProjectUpdateBatchID_ProgramID] UNIQUE NONCLUSTERED 
(
	[ProjectUpdateBatchID] ASC,
	[ProgramID] ASC
)

