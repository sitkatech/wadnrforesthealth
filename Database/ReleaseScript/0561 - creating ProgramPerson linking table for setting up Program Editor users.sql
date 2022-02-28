
/*
select * from dbo.Program

select * from dbo.Person
*/

create table dbo.ProgramPerson(
	ProgramPersonID int not null identity(1,1) constraint PK_ProgramPerson_ProgramPersonID primary key,
	ProgramID int not null constraint FK_ProgramPerson_Program_ProgramID foreign key references dbo.Program(ProgramID),
	PersonID int not null constraint FK_ProgramPerson_Person_PersonID foreign key references dbo.Person(PersonID),
	CONSTRAINT AK_ProgramPerson_ProgramID_PersonID UNIQUE(ProgramID, PersonID)  
)