

drop table dbo.GrantAllocationProjectCode

ALTER TABLE [dbo].[GrantAllocation] DROP CONSTRAINT [FK_GrantAllocation_ProgramIndex_ProgramIndexID]
GO
alter table dbo.GrantAllocation drop column ProgramIndexID