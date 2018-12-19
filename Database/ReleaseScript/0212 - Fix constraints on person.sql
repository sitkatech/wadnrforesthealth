/****** Object:  Index [AK_Person_PersonGuid_TenantID]    Script Date: 12/19/2018 10:58:50 AM ******/
ALTER TABLE [dbo].[Person] DROP CONSTRAINT [AK_Person_PersonGuid_TenantID]
GO

Create Unique Nonclustered Index AK_Person_PersonGuid_TenantID
on dbo.Person(PersonGuid, TenantID)
where PersonGuid is not null

USE [WADNRForestHealthDB]
GO

/****** Object:  Index [AK_Person_Email_TenantID]    Script Date: 12/19/2018 11:01:05 AM ******/
ALTER TABLE [dbo].[Person] DROP CONSTRAINT [AK_Person_Email_TenantID]
GO

Create Unique Nonclustered Index AK_Person_Email_TenantID
on dbo.Person(Email, TenantID)
where Email is not null