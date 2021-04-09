

update dbo.Treatment
set ProgramID = p.ProgramID
,   ImportedFromGis = 1
from dbo.Treatment t
join dbo.Project p on t.ProjectID = p.ProjectID
where p.ProgramID is not null and p.CreateGisUploadAttemptID is not null

update dbo.ProjectLocation
set ProgramID = p.ProgramID
,   ImportedFromGisUpload = 1
from dbo.ProjectLocation t
join dbo.Project p on t.ProjectID = p.ProjectID
where p.ProgramID is not null and p.CreateGisUploadAttemptID is not null


-- silviculture into fuels
select p.ProjectGisIdentifier, t.ProjectID as OriginalProjectID, secP.ProjectID as NewProjectID, t.TreatmentID
into #silvicultureIntoFuels
from 
dbo.Project p 
join dbo.Project secP on secP.ProjectGisIdentifier = p.ProjectGisIdentifier
join dbo.Treatment t on t.ProjectID = p.ProjectID
where p.ProgramID = 12 and secP.ProgramID = 13

update dbo.Treatment
set ProjectID = x.NewProjectID
from #silvicultureIntoFuels x
join dbo.Treatment t on x.TreatmentID = t.TreatmentID


select p.ProjectGisIdentifier, t.ProjectID as OriginalProjectID, secP.ProjectID as NewProjectID, t.ProjectLocationID
into #silvicultureIntoFuelsPL
from 
dbo.Project p 
join dbo.Project secP on secP.ProjectGisIdentifier = p.ProjectGisIdentifier
join dbo.ProjectLocation t on t.ProjectID = p.ProjectID
where p.ProgramID = 12 and secP.ProgramID = 13

update dbo.ProjectLocation
set ProjectID = x.NewProjectID
from #silvicultureIntoFuelsPL x
join dbo.ProjectLocation t on x.ProjectLocationID = t.ProjectLocationID



-- silviculture into harvest
select p.ProjectGisIdentifier, t.ProjectID as OriginalProjectID, secP.ProjectID as NewProjectID, t.TreatmentID
into #silviculturetoHarvest
from 
dbo.Project p 
join dbo.Project secP on secP.ProjectGisIdentifier = p.ProjectGisIdentifier
join dbo.Treatment t on t.ProjectID = p.ProjectID
where p.ProgramID = 12 and secP.ProgramID = 14

update dbo.Treatment
set ProjectID = x.NewProjectID
from #silviculturetoHarvest x
join dbo.Treatment t on x.TreatmentID = t.TreatmentID




select p.ProjectGisIdentifier, t.ProjectID as OriginalProjectID, secP.ProjectID as NewProjectID, t.ProjectLocationID
into #silvicultureIntoHarvestPL
from 
dbo.Project p 
join dbo.Project secP on secP.ProjectGisIdentifier = p.ProjectGisIdentifier
join dbo.ProjectLocation t on t.ProjectID = p.ProjectID
where p.ProgramID = 12 and secP.ProgramID = 14

update dbo.ProjectLocation
set ProjectID = x.NewProjectID
from #silvicultureIntoHarvestPL x
join dbo.ProjectLocation t on x.ProjectLocationID = t.ProjectLocationID


-- fuels into harvest
select p.ProjectGisIdentifier, t.ProjectID as OriginalProjectID, secP.ProjectID as NewProjectID, t.TreatmentID
into #fuelsToHarvest
from 
dbo.Project p 
join dbo.Project secP on secP.ProjectGisIdentifier = p.ProjectGisIdentifier
join dbo.Treatment t on t.ProjectID = p.ProjectID
where p.ProgramID = 13 and secP.ProgramID = 14

update dbo.Treatment
set ProjectID = x.NewProjectID
from #fuelsToHarvest x
join dbo.Treatment t on x.TreatmentID = t.TreatmentID


select p.ProjectGisIdentifier, t.ProjectID as OriginalProjectID, secP.ProjectID as NewProjectID, t.ProjectLocationID
into #fuelsToHarvestPL
from 
dbo.Project p 
join dbo.Project secP on secP.ProjectGisIdentifier = p.ProjectGisIdentifier
join dbo.ProjectLocation t on t.ProjectID = p.ProjectID
where p.ProgramID = 13 and secP.ProgramID = 14

update dbo.ProjectLocation
set ProjectID = x.NewProjectID
from #fuelsToHarvestPL x
join dbo.ProjectLocation t on x.ProjectLocationID = t.ProjectLocationID



delete from dbo.ProjectOrganization where
ProjectID in (

 select ProjectID from dbo.Project where ProgramID = 12 and ProjectGisIdentifier in (select ProjectGisIdentifier from dbo.Project p where p.ProgramID in (13,14)))


 delete from dbo.ProjectRegion where
ProjectID in (

 select ProjectID from dbo.Project where ProgramID = 12 and ProjectGisIdentifier in (select ProjectGisIdentifier from dbo.Project p where p.ProgramID in (13,14)))

  delete from dbo.ProjectPriorityLandscape where
ProjectID in (

 select ProjectID from dbo.Project where ProgramID = 12 and ProjectGisIdentifier in (select ProjectGisIdentifier from dbo.Project p where p.ProgramID in (13,14)))


    delete from dbo.ProjectExternalLink where
ProjectID in (

 select ProjectID from dbo.Project where ProgramID = 12 and ProjectGisIdentifier in (select ProjectGisIdentifier from dbo.Project p where p.ProgramID in (13,14)))

 
 delete from dbo.ProjectCustomAttributeValue where ProjectCustomAttributeID in (
 select ProjectCustomAttributeID from dbo.ProjectCustomAttribute where
ProjectID in (

 select ProjectID from dbo.Project where ProgramID = 12 and ProjectGisIdentifier in (select ProjectGisIdentifier from dbo.Project p where p.ProgramID in (13,14)))
 
 )
 
 
 delete from dbo.ProjectCustomAttribute where
ProjectID in (

 select ProjectID from dbo.Project where ProgramID = 12 and ProjectGisIdentifier in (select ProjectGisIdentifier from dbo.Project p where p.ProgramID in (13,14)))





delete from dbo.Project where ProgramID = 12 and ProjectGisIdentifier in (select ProjectGisIdentifier from dbo.Project p where p.ProgramID in (13,14))


delete from dbo.ProjectOrganization where
ProjectID in (

 select ProjectID from dbo.Project where ProgramID = 13 and ProjectGisIdentifier in (select ProjectGisIdentifier from dbo.Project p where p.ProgramID in (14)))


 delete from dbo.ProjectRegion where
ProjectID in (

 select ProjectID from dbo.Project where ProgramID = 13 and ProjectGisIdentifier in (select ProjectGisIdentifier from dbo.Project p where p.ProgramID in (14)))

  delete from dbo.ProjectPriorityLandscape where
ProjectID in (

 select ProjectID from dbo.Project where ProgramID = 13 and ProjectGisIdentifier in (select ProjectGisIdentifier from dbo.Project p where p.ProgramID in (14)))


   delete from dbo.ProjectExternalLink where
ProjectID in (

 select ProjectID from dbo.Project where ProgramID = 13 and ProjectGisIdentifier in (select ProjectGisIdentifier from dbo.Project p where p.ProgramID in (14)))

 
 delete from dbo.ProjectCustomAttributeValue where ProjectCustomAttributeID in (
 select ProjectCustomAttributeID from dbo.ProjectCustomAttribute where
ProjectID in (

 select ProjectID from dbo.Project where ProgramID = 13 and ProjectGisIdentifier in (select ProjectGisIdentifier from dbo.Project p where p.ProgramID in (14)))
 
 )
 
 
 delete from dbo.ProjectCustomAttribute where
ProjectID in (

 select ProjectID from dbo.Project where ProgramID = 13 and ProjectGisIdentifier in (select ProjectGisIdentifier from dbo.Project p where p.ProgramID in (14)))

delete from dbo.Project where ProgramID = 13 and ProjectGisIdentifier in (select ProjectGisIdentifier from dbo.Project p where p.ProgramID in (14))

