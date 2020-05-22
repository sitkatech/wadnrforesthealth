

delete from dbo.ProjectOrganization where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004'))

delete from dbo.ProjectRegion where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004'))


delete from dbo.GrantAllocationAwardLandownerCostShareLineItem where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004'))

delete from dbo.ProjectPerson where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004'))

delete from dbo.ProjectLocation where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004'))

delete from dbo.ProjectClassification where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004'))

delete from dbo.ProjectGrantAllocationRequest where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004'))


delete from dbo.ProjectDocument where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004'))


delete from dbo.ProjectUpdate where
ProjectUpdateBatchID in 
(select ProjectUpdateBatchID from dbo.ProjectUpdateBatch where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004')))

delete from dbo.ProjectOrganizationUpdate where
ProjectUpdateBatchID in 
(select ProjectUpdateBatchID from dbo.ProjectUpdateBatch where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004')))

delete from dbo.ProjectUpdateHistory where
ProjectUpdateBatchID in 
(select ProjectUpdateBatchID from dbo.ProjectUpdateBatch where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004')))

delete from dbo.ProjectRegionUpdate where
ProjectUpdateBatchID in 
(select ProjectUpdateBatchID from dbo.ProjectUpdateBatch where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004')))

delete from dbo.ProjectGrantAllocationRequestUpdate where
ProjectUpdateBatchID in 
(select ProjectUpdateBatchID from dbo.ProjectUpdateBatch where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004')))

delete from dbo.ProjectPriorityAreaUpdate where
ProjectUpdateBatchID in 
(select ProjectUpdateBatchID from dbo.ProjectUpdateBatch where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004')))

delete from dbo.ProjectLocationUpdate where
ProjectUpdateBatchID in 
(select ProjectUpdateBatchID from dbo.ProjectUpdateBatch where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004')))

delete from dbo.ProjectPersonUpdate where
ProjectUpdateBatchID in 
(select ProjectUpdateBatchID from dbo.ProjectUpdateBatch where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004')))


delete from dbo.ProjectUpdateBatch where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004'))

delete from dbo.ProjectGrantAllocationExpenditure where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004'))

delete from dbo.NotificationProject where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004'))

delete from dbo.ProjectPriorityArea where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004'))

delete from dbo.ProjectLocationStaging where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004'))

delete from dbo.ProjectNote where ProjectID not in (select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004'))


delete from dbo.Project where ProjectID not in(select P.ProjectID from dbo.Project p where p.FhtProjectNumber in ('FHT-2020-003', 'FHT-2020-002', 'FHT-2020-004'))
