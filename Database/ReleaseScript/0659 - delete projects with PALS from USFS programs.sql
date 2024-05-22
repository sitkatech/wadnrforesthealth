
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

delete plu from dbo.ProjectLocationUpdate as plu join dbo.ProjectLocation as pl on plu.ProjectLocationID = pl.ProjectLocationID where pl.ProjectID in (select * from #projectsToDelete)

delete from dbo.ProjectLocation where ProjectID in (select * from #projectsToDelete)

delete from dbo.ProjectRegion where ProjectID in (select * from #projectsToDelete)

delete from dbo.ProjectCounty where ProjectID in (select * from #projectsToDelete)

delete from dbo.ProjectPriorityLandscape where ProjectID in (select * from #projectsToDelete)

delete from dbo.ProjectProgram where ProjectID in (select * from #projectsToDelete)

delete from dbo.ProjectExternalLink where ProjectID in (select * from #projectsToDelete)

delete from dbo.ProjectImage where ProjectID in (select * from #projectsToDelete)

delete from dbo.ProjectClassification where ProjectID in (select * from #projectsToDelete)


delete pu from dbo.ProjectUpdate as pu join dbo.ProjectUpdateBatch as pub on pu.ProjectUpdateBatchID = pub.ProjectUpdateBatchID where ProjectID in (select * from #projectsToDelete)

delete pou from dbo.ProjectOrganizationUpdate as pou join dbo.ProjectUpdateBatch as pub on pou.ProjectUpdateBatchID = pub.ProjectUpdateBatchID where ProjectID in (select * from #projectsToDelete)

delete puh from dbo.ProjectUpdateHistory as puh join dbo.ProjectUpdateBatch as pub on puh.ProjectUpdateBatchID = pub.ProjectUpdateBatchID where ProjectID in (select * from #projectsToDelete)

delete pru from dbo.ProjectRegionUpdate as pru join dbo.ProjectUpdateBatch as pub on pru.ProjectUpdateBatchID = pub.ProjectUpdateBatchID where ProjectID in (select * from #projectsToDelete)

delete pup from dbo.ProjectUpdateProgram as pup join dbo.ProjectUpdateBatch as pub on pup.ProjectUpdateBatchID = pub.ProjectUpdateBatchID where ProjectID in (select * from #projectsToDelete)

delete pfsu from dbo.ProjectFundingSourceUpdate as pfsu join dbo.ProjectUpdateBatch as pub on pfsu.ProjectUpdateBatchID = pub.ProjectUpdateBatchID where ProjectID in (select * from #projectsToDelete)

delete ppu from dbo.ProjectPersonUpdate as ppu join dbo.ProjectUpdateBatch as pub on ppu.ProjectUpdateBatchID = pub.ProjectUpdateBatchID where ProjectID in (select * from #projectsToDelete)

delete ppu from dbo.ProjectPriorityLandscapeUpdate as ppu join dbo.ProjectUpdateBatch as pub on ppu.ProjectUpdateBatchID = pub.ProjectUpdateBatchID where ProjectID in (select * from #projectsToDelete)

delete ppu from dbo.ProjectExternalLinkUpdate as ppu join dbo.ProjectUpdateBatch as pub on ppu.ProjectUpdateBatchID = pub.ProjectUpdateBatchID where ProjectID in (select * from #projectsToDelete)


delete from dbo.ProjectUpdateBatch where ProjectID in (select * from #projectsToDelete)


delete from dbo.ProjectTag where ProjectID in (select * from #projectsToDelete)

delete from dbo.AgreementProject where ProjectID in (select * from #projectsToDelete)
delete from dbo.ProjectFundingSource where ProjectID in (select * from #projectsToDelete)
delete from dbo.ProjectGrantAllocationRequest where ProjectID in (select * from #projectsToDelete)
delete from dbo.ProjectPerson where ProjectID in (select * from #projectsToDelete)
delete from dbo.NotificationProject where ProjectID in (select * from #projectsToDelete)
delete from dbo.ProjectDocument where ProjectID in (select * from #projectsToDelete)

delete p from dbo.Project as p where p.ProjectID in (select * from #projectsToDelete)
