--begin tran

delete from Person where FirstName = 'Sitka' and  LastName = 'Dev'

insert into Person (FirstName, LastName, RoleID, IsActive, OrganizationID, AllowedAuthenticatorID, ReceiveSupportEmails, CreateDate, Email)
values ('Sitka', 'Dev', 7, 1, 4703, 1, 0, GETDATE(), 'sitkadev@dnr.wa.gov')
GO

declare @SitkaDevPersonID int;
set @SitkaDevPersonID = (select PersonID from Person where FirstName = 'Sitka' and  LastName = 'Dev')

insert into PersonEnvironmentCredential(PersonID, DeploymentEnvironmentID, AuthenticatorID, PersonUniqueIdentifier)
values (@sitkaDevPersonID, 2, 1, 'sitkadev@dnr.wa.lcl')

insert into PersonEnvironmentCredential(PersonID, DeploymentEnvironmentID, AuthenticatorID, PersonUniqueIdentifier)
values (@sitkaDevPersonID, 3, 1, 'sitkadev@dnr.wa.lcl')
GO


--select * from Person
--select * from PersonEnvironmentCredential
--select * from DeploymentEnvironment
--select * from Authenticator

--rollback tran