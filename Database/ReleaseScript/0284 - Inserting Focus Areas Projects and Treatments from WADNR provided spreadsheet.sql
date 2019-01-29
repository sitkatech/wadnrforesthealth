ALTER TABLE [dbo].[Project] DROP CONSTRAINT [CK_Project_ApprovalStartDateLessThanEqualToCompletionDate]
GO

ALTER TABLE [dbo].[Project] DROP CONSTRAINT [CK_Project_PlannedDateLessThanEqualToImplementationDate]
GO

ALTER TABLE dbo.Project DROP COLUMN ApprovalStartDate
ALTER TABLE dbo.Project ADD ExpirationDate datetime null

ALTER TABLE dbo.ProjectUpdate DROP COLUMN ApprovalStartDate
ALTER TABLE dbo.ProjectUpdate ADD ExpirationDate datetime null

ALTER TABLE dbo.TreatmentActivity ADD TreatmentActivitySlashAcres decimal(18,0) not null

insert into dbo.TreatmentActivityStatus (TreatmentActivityStatusID, TreatmentActivityStatusName, TreatmentActivityStatusDisplayName)
values (3, 'Cancelled','Cancelled')

