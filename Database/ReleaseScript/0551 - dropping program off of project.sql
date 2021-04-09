
insert into dbo.ProjectProgram(ProjectID, ProgramID)

select distinct p.ProjectID, t.ProgramID from dbo.Project p
join dbo.Treatment t on p.ProjectID = t.ProjectID





ALTER TABLE [dbo].[Project] DROP CONSTRAINT [FK_Project_Program_ProgramID]
GO



alter table dbo.Project drop column ProgramID