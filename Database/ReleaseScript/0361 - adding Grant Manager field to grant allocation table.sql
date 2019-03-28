alter table dbo.GrantAllocation
add GrantManagerID int null constraint FK_GrantAllocation_Person_GrantManagerID_PersonID foreign key references dbo.Person(PersonID)