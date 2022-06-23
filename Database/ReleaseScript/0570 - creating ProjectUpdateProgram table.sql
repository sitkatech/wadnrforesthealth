create table ProjectUpdateProgram (
	ProjectUpdateProgramID int not null identity(1,1) constraint PK_ProjectUpdateProgram_ProjectUpdateProgramID primary key,
	ProgramID int not null
	constraint FK_ProjectUpdateProgram_Program_ProgramID foreign key references WADNRForestHealthDB.dbo.Program(ProgramID),
	ProjectUpdateID int not null
	constraint FK_ProjectUpdateProgram_ProjectUpdate_ProjectUpdateID foreign key references WADNRForestHealthDB.dbo.ProjectUpdate(ProjectUpdateID)
)
ALTER TABLE [dbo].[ProjectUpdateProgram] ADD  CONSTRAINT [AK_ProjectUpdateProgram_ProjectUpdateID_ProgramID] UNIQUE NONCLUSTERED 
(
	[ProjectUpdateID] ASC,
	[ProgramID] ASC
)

