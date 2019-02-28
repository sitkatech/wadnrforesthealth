--Dropping relationship between Agreement and Grant because now many-to-many
alter table dbo.[Agreement] drop constraint FK_Agreement_Grant_GrantID
go
alter table [dbo].[AgreementGrantAllocation]  drop constraint FK_AgreementGrantAllocation_Agreement_AgreementID_GrantID
go
ALTER TABLE [dbo].[AgreementGrantAllocation] DROP CONSTRAINT [FK_AgreementGrantAllocation_GrantAllocation_GrantAllocationID_GrantID]
GO
ALTER TABLE [dbo].[AgreementGrantAllocation] drop column GrantID
go
ALTER TABLE [dbo].[Agreement] DROP CONSTRAINT [AK_Agreement_AgreementID_GrantID]
GO
alter table dbo.[Agreement] drop column GrantID
go

