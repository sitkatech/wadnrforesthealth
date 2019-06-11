ALTER TABLE [dbo].[Organization] DROP CONSTRAINT [AK_Organization_OrganizationShortName]
GO
alter table dbo.Organization alter column OrganizationShortName varchar(50) not null
ALTER TABLE [dbo].[Organization] ADD  CONSTRAINT [AK_Organization_OrganizationShortName] UNIQUE NONCLUSTERED 
(
	[OrganizationShortName] ASC
)
GO


