--begin tran

set identity_insert dbo.Authenticator ON
insert into dbo.Authenticator (AuthenticatorID, AuthenticatorName, AuthenticatorFullName)
values (3, 'SAW-TEST', 'Secure Access Washington Account (TEST)')
set identity_insert Authenticator OFF
GO

update  dbo.Authenticator
set AuthenticatorName = 'SAW-PROD',
    AuthenticatorFullName = 'Secure Access Washington Account (PROD)'
where AuthenticatorName = 'SAW'

alter table dbo.PersonEnvironmentCredential
drop AK_PersonEnvironmentCredential_PersonID_DeploymentEnvironmentID_AuthenticatorID

alter table dbo.PersonEnvironmentCredential
drop FK_PersonEnvironmentCredential_DeploymentEnvironment_DeploymentEnvironmentID

alter table dbo.PersonEnvironmentCredential
alter column PersonUniqueIdentifier varchar(100) not null

--select * from PersonEnvironmentCredential

update dbo.PersonEnvironmentCredential
set AuthenticatorID = 3
where AuthenticatorID = 2 and DeploymentEnvironmentID =  2
GO

delete from dbo.PersonEnvironmentCredential
where AuthenticatorID = 1 and DeploymentEnvironmentID = 2

--select * from DeploymentEnvironment

alter table dbo.PersonEnvironmentCredential
drop column DeploymentEnvironmentID
GO

drop table dbo.DeploymentEnvironment
GO

-- This is non-trivial in this environment. Why? Not sure.
--EXEC sp_rename 'dbo.PersonEnvironmentCredential', 'PersonAuthenticatorCredential';



select * from dbo.PersonEnvironmentCredential

alter table dbo.PersonEnvironmentCredential
add constraint AK_PersonEnvironmentCredential_AuthenticatorID_PersonUniqueIdentifier unique (AuthenticatorID, PersonUniqueIdentifier)
GO

--rollback tran

--select * from PersonEnvironmentCredential
--select * from Person