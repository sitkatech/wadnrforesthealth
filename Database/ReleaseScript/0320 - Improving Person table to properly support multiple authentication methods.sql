--begin tran

-- DeploymentEnvironment
-------------------------
create table dbo.DeploymentEnvironment
(
    [DeploymentEnvironmentID] int identity(1,1) not null,
    [DeploymentEnvironmentName] varchar(10) not null
)
GO

-- PK
alter table dbo.DeploymentEnvironment 
add CONSTRAINT [PK_DeploymentEnvironment_DeploymentEnvironmentID] PRIMARY KEY CLUSTERED 
(
    [DeploymentEnvironmentID] ASC
) ON [PRIMARY]
GO

insert into dbo.DeploymentEnvironment(DeploymentEnvironmentName)
values ('Local')
GO

insert into dbo.DeploymentEnvironment(DeploymentEnvironmentName)
values ('QA')
GO

insert into dbo.DeploymentEnvironment(DeploymentEnvironmentName)
values ('Prod')
GO

-- Authentication method
-------------------------

create table dbo.Authenticator
(
    AuthenticatorID int identity(1,1) not null,
    AuthenticatorName varchar(10) not null,
    AuthenticatorFullName  varchar(100) not null
)
GO

-- PK
alter table dbo.Authenticator
add CONSTRAINT [PK_Authenticator_AuthenticatorID] PRIMARY KEY CLUSTERED 
(
    [AuthenticatorID] ASC
) ON [PRIMARY]
GO

insert into dbo.Authenticator(AuthenticatorName, AuthenticatorFullName)
values ('ADFS', 'Washington DNR ADFS Account')
GO

insert into dbo.Authenticator(AuthenticatorName, AuthenticatorFullName)
values ('SAW', 'Secure Access Washington Account')
GO

-- Person / DeploymentEnvironment / Credential
-----------------------------------------
create table dbo.PersonEnvironmentCredential
(
    [PersonEnvironmentCredentialID] int identity(1,1) not null,
    [PersonID] int not null,
    [DeploymentEnvironmentID] int not null,
    [AuthenticatorID] int not null,
    [PersonUniqueIdentifier] varchar(100) null,
)
GO

-- PK
alter table dbo.PersonEnvironmentCredential 
add CONSTRAINT [PK_PersonEnvironmentCredential_PersonEnvironmentCredentialID] PRIMARY KEY CLUSTERED 
(
    PersonEnvironmentCredentialID ASC
) ON [PRIMARY]
GO

-- FK => PersonID
ALTER TABLE [dbo].PersonEnvironmentCredential  WITH CHECK ADD  CONSTRAINT [FK_PersonEnvironmentCredential_Person_PersonID] FOREIGN KEY(PersonID)
REFERENCES [dbo].Person (PersonID)
GO

-- FK => Enviroment
ALTER TABLE [dbo].PersonEnvironmentCredential  WITH CHECK ADD  CONSTRAINT [FK_PersonEnvironmentCredential_DeploymentEnvironment_DeploymentEnvironmentID] FOREIGN KEY(DeploymentEnvironmentID)
REFERENCES [dbo].DeploymentEnvironment (DeploymentEnvironmentID)
GO

-- FK => Authenticator
ALTER TABLE [dbo].PersonEnvironmentCredential  WITH CHECK ADD  CONSTRAINT [FK_PersonEnvironmentCredential_Authenticator_AuthenticatorID] FOREIGN KEY(AuthenticatorID)
REFERENCES [dbo].Authenticator (AuthenticatorID)
GO

-- AK on combination of Person/Env/Authenticator. 
alter table dbo.PersonEnvironmentCredential 
add constraint AK_PersonEnvironmentCredential_PersonID_DeploymentEnvironmentID_AuthenticatorID unique(PersonID, DeploymentEnvironmentID, AuthenticatorID);
GO

-- Create Person / DeploymentEnvironment / Credential records
-------------------------------------------------------------
-- Here we assume **all current PersonUniqueIdentifiers are for Prod, not QA**. 

-- SAW First
insert into dbo.PersonEnvironmentCredential
select p.PersonID,
       (select e.DeploymentEnvironmentID from DeploymentEnvironment as e where e.DeploymentEnvironmentName = 'Prod') as DeploymentEnvironmentID,
       (select a.AuthenticatorID from Authenticator as a where a.AuthenticatorName = 'SAW') as AuthenticatorID,
       p.[PersonUniqueIdentifier]
from dbo.Person as p 
where p.PersonUniqueIdentifier not like '%@dnr.wa%'


-- ADFS Next
insert into dbo.PersonEnvironmentCredential
select p.PersonID,
       (select e.DeploymentEnvironmentID from DeploymentEnvironment as e where e.DeploymentEnvironmentName = 'Prod') as DeploymentEnvironmentID,
       (select a.AuthenticatorID from Authenticator as a where a.AuthenticatorName = 'ADFS') as AuthenticatorID,
       p.[PersonUniqueIdentifier]
from dbo.Person as p 
where p.PersonUniqueIdentifier like '%@dnr.wa%'

DROP INDEX [UQ_Person_PersonUniqueIdentifier] ON [dbo].[Person]
GO

alter table dbo.Person
drop column PersonUniqueIdentifier
GO

--select * from dbo.Person
--select * from PersonEnvironmentCredential

--rollback tran



/*
CREATE TABLE [dbo].[Person]
(
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[PersonUniqueIdentifier] [varchar](100) NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[Email] [varchar](255) NULL,
	[Phone] [varchar](30) NULL,
	[PasswordPdfK2SaltHash] [nvarchar](1000) NULL,
	[RoleID] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[LastActivityDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[OrganizationID] [int] NULL,
	[ReceiveSupportEmails] [bit] NOT NULL,
	[WebServiceAccessToken] [uniqueidentifier] NULL,
	[LoginName] [varchar](128) NULL,
	[MiddleName] [varchar](100) NULL,
	[StatewideVendorNumber] [varchar](100) NULL,
	[Notes] [varchar](500) NULL,
	[PersonAddress] [varchar](255) NULL,
	[AddedByPersonID] [int] NULL,
 CONSTRAINT [PK_Person_PersonID] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Organization_OrganizationID] FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[Organization] ([OrganizationID])
GO

ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_Organization_OrganizationID]
GO

ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Person_AddedByPersonID_PersonID] FOREIGN KEY([AddedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO

ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_Person_AddedByPersonID_PersonID]
GO

ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Role_RoleID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO

ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_Role_RoleID]
GO

*/
