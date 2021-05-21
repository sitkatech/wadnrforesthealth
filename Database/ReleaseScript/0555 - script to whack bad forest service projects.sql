

--SNOQUERA LANDSCAPE ANALYSIS is a project which has edits on it. May not want to delete


select p.* into #onesToDelete from dbo.Project p
where 
 p.creategisuploadattemptid is not null
and 
p.ProjectID in (


select pp.ProjectID
from dbo.ProjectProgram pp
where pp.ProgramID = 2

and pp.ProjectID not in (

select pp.ProjectID
from dbo.ProjectProgram pp
where pp.ProgramID != 2)) order by p.FhtProjectNumber


delete from dbo.Treatment where ProjectID in (select ProjectID from #onesToDelete)

delete from dbo.ProjectOrganization where ProjectID in (select ProjectID from #onesToDelete)

delete from dbo.ProjectRegion where ProjectID in (select ProjectID from #onesToDelete)

delete from dbo.ProjectPriorityLandscape where ProjectID in (select ProjectID from #onesToDelete)

delete from dbo.ProjectProgram where ProjectID in (select ProjectID from #onesToDelete)

delete from dbo.ProjectLocation where ProjectID in (select ProjectID from #onesToDelete)

delete from dbo.ProjectExternalLink where ProjectID in (select ProjectID from #onesToDelete)

delete from dbo.Project where ProjectID in (select ProjectID from #onesToDelete)