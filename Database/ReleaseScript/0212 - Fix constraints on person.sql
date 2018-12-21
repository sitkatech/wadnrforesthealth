ALTER TABLE [dbo].[Person] DROP CONSTRAINT [AK_Person_Email_TenantID]
GO

Create Unique Nonclustered Index AK_Person_Email_TenantID
on dbo.Person(Email, TenantID)
where Email is not null