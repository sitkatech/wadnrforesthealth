
/*
select * from dbo.Program

usfs programs = (2,12,13,14)


SELECT distinct p.ProjectID from dbo.Project as p join dbo.ProjectProgram as pp on p.ProjectID = pp.ProjectID where ProgramID in (2,12,13,14) and p.ProjectName like '(PALS)%'

*/

DROP TABLE IF EXISTS #projectsToDelete 
SELECT distinct p.ProjectID INTO #projectsToDelete from dbo.Project as p join dbo.ProjectProgram as pp on p.ProjectID = pp.ProjectID where ProgramID in (2,12,13,14) and p.ProjectName like '(PALS)%'

--select * from #projectsToDelete


delete po from dbo.ProjectOrganization as po where po.ProjectID in (select * from #projectsToDelete)


delete t from dbo.Treatment as t where t.ProjectID in (select * from #projectsToDelete)

delete tu from dbo.TreatmentUpdate as tu join dbo.ProjectUpdateBatch as pub on tu.ProjectUpdateBatchID = pub.ProjectUpdateBatchID where ProjectID in (select * from #projectsToDelete)

delete plu from dbo.ProjectLocationUpdate as plu where plu.ProjectLocationID in (select ProjectLocationID from dbo.ProjectLocation where ProjectID in (select * from #projectsToDelete))

delete from dbo.ProjectLocation where ProjectID in (select * from #projectsToDelete)

delete from dbo.ProjectRegion where ProjectID in (select * from #projectsToDelete)

delete from dbo.ProjectCounty where ProjectID in (select * from #projectsToDelete)

delete from dbo.ProjectPriorityLandscape where ProjectID in (select * from #projectsToDelete)

delete from dbo.ProjectProgram where ProjectID in (select * from #projectsToDelete)

delete from dbo.ProjectExternalLink where ProjectID in (select * from #projectsToDelete)

delete from dbo.ProjectImage where ProjectID in (select * from #projectsToDelete)

delete from dbo.ProjectClassification where ProjectID in (select * from #projectsToDelete)


delete from dbo.ProjectUpdate where ProjectUpdateBatchID in (select ProjectUpdateBatchID from dbo.ProjectUpdateBatch where ProjectID in (select * from #projectsToDelete))

delete from dbo.ProjectOrganizationUpdate where ProjectUpdateBatchID in (select ProjectUpdateBatchID from dbo.ProjectUpdateBatch where ProjectID in (select * from #projectsToDelete))

delete from dbo.ProjectUpdateHistory where ProjectUpdateBatchID in (select ProjectUpdateBatchID from dbo.ProjectUpdateBatch where ProjectID in (select * from #projectsToDelete))

delete from dbo.ProjectRegionUpdate where ProjectUpdateBatchID in (select ProjectUpdateBatchID from dbo.ProjectUpdateBatch where ProjectID in (select * from #projectsToDelete))

delete from dbo.ProjectUpdateProgram where ProjectUpdateBatchID in (select ProjectUpdateBatchID from dbo.ProjectUpdateBatch where ProjectID in (select * from #projectsToDelete))



delete from dbo.ProjectUpdateBatch where ProjectID in (select * from #projectsToDelete)

delete p from dbo.Project as p where p.ProjectID in (select * from #projectsToDelete)
