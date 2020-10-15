
--ORG 4708 is USFS

IF OBJECT_ID('tempdb..#usfsProjectsToUpdate') IS NOT NULL
    DROP TABLE #usfsProjectsToUpdate

select 
    p.ProjectID,
    p.CompletionDate
into #usfsProjectsToUpdate
from
    dbo.Project as p
    join dbo.ProjectOrganization as po on p.ProjectID = po.ProjectID
where
    po.OrganizationID = 4708 and p.CompletionDate is not null


update t
set t.TreatmentEndDate = ptu.CompletionDate
from
    dbo.Treatment as t
    join #usfsProjectsToUpdate as ptu on t.ProjectID = ptu.ProjectID

update p
set p.CompletionDate = null, p.PlannedDate = null
from
    dbo.Project as p
    join #usfsProjectsToUpdate as ptu on p.ProjectID = ptu.ProjectID


    select * from dbo.Project
