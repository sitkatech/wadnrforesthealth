alter table dbo.Project
add ProgramID int null constraint FK_Project_Program_ProgramID foreign key references dbo.Program(ProgramID);
go

--select * from dbo.Project
update dbo.Project
set ProgramID = 2
go



