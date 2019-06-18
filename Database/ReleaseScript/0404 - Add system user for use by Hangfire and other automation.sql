
--begin tran
insert into Person(FirstName, LastName, RoleID, CreateDate, IsActive, OrganizationID, ReceiveSupportEmails, IsProgramManager)
values
('System', 'User', 8, GETDATE(), 1, 4702, 0, 0) 
--rollback tran
