
insert into dbo.ProjectProgram(ProjectID, ProgramID)

select distinct p.ProjectID, t.ProgramID from dbo.Project p
join dbo.Treatment t on p.ProjectID = t.ProjectID


insert into dbo.ProjectProgram(ProjectID, ProgramID)
select p.ProjectID, p.ProgramID
from dbo.Project p
left join dbo.ProjectProgram pp
on pp.ProjectID = p.ProjectID
where pp.ProjectProgramID is null and p.ProgramID is not null





ALTER TABLE [dbo].[Project] DROP CONSTRAINT [FK_Project_Program_ProgramID]
GO



alter table dbo.Project drop column ProgramID