

create table dbo.PersonRole
(
	PersonRoleID int not null identity(1,1) constraint PK_PersonRole_PersonRoleID primary key,
	PersonID int not null constraint FK_PersonRole_Person_PersonID foreign key references dbo.Person(PersonID),
	RoleID int not null constraint FK_PersonRole_Role_RoleID foreign key references dbo.[Role](RoleID),
	CONSTRAINT AK_PersonRole_PersonID_RoleID UNIQUE(PersonID, RoleID)
);



/*
select * from dbo.Person

select * from dbo.[Role]

*/

insert into dbo.PersonRole(PersonID, RoleID) 
select
	PersonID, RoleID
from dbo.Person




ALTER TABLE [dbo].[Person] DROP CONSTRAINT [FK_Person_Role_RoleID]
GO

alter table dbo.Person drop column RoleID
