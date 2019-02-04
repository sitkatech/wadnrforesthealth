create table dbo.AgreementPersonRole (
	AgreementPersonRoleID int not null constraint PK_AgreementPersonRole_AgreementPersonRoleID primary key,
	AgreementPersonRoleName varchar(50) constraint AK_AgreementPersonRole_AgreementPersonRoleName unique,
	AgreementPersonRoleDisplayName varchar(50) constraint AK_AgreementPersonRole_AgreementPersonRoleDisplayName unique
)


CREATE table dbo.AgreementPerson(
	AgreementPersonID int not null identity(1,1) constraint PK_AgreementPerson_AgreementPersonID primary key,
	AgreementID int not null constraint FK_AgreementPerson_Agreement_AgreementID foreign key references dbo.Agreement(AgreementID),
	PersonID int not null constraint FK_AgreementPerson_Person_PersonID foreign key references dbo.Person(PersonID),
	AgreementPersonRoleID int not null constraint FK_AgreementPerson_AgreementPersonRole_AgreementPersonRoleID foreign key references dbo.AgreementPersonRole(AgreementPersonRoleID)

)