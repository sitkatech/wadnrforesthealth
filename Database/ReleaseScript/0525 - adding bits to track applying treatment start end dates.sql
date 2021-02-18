
ALTER TABLE dbo.GisUploadSourceOrganization ADD  ApplyStartDateToTreatments bit

ALTER TABLE dbo.GisUploadSourceOrganization ADD  ApplyEndDateToTreatments bit

go

update dbo.GisUploadSourceOrganization
set ApplyEndDateToTreatments = 0,
    ApplyStartDateToTreatments = 0
from dbo.GisUploadSourceOrganization

go

ALTER TABLE dbo.GisUploadSourceOrganization ALTER COLUMN  ApplyEndDateToTreatments bit not null

ALTER TABLE dbo.GisUploadSourceOrganization ALTER COLUMN  ApplyStartDateToTreatments bit not null

--story number 1768
--1=DNR State Trust Lands (no project start/end yes treatment start/end)
--2=USFS (no project start/end yes treatment start/end)
--3=LOA (yes project start/end no treatment start/end)
--5=WDFW (both yes) (changed after 1768)
--6=Stewardship (yes project start/end no treatment start/end)
--7=State Parks (both yes)
--8=usfws (both yes) (nothing documented in 1768)
--9=silviculture (both yes)

update dbo.GisUploadSourceOrganization
set ApplyStartDateToProject = 0
from dbo.GisUploadSourceOrganization 
where GisUploadSourceOrganizationID in (1,2)

update dbo.GisUploadSourceOrganization
set ApplyStartDateToProject = 1
from dbo.GisUploadSourceOrganization 
where GisUploadSourceOrganizationID in (8,9)



update dbo.GisUploadSourceOrganization
set ApplyEndDateToTreatments = 1
from dbo.GisUploadSourceOrganization 
where GisUploadSourceOrganizationID in (1,2,5,7,8,9)

update dbo.GisUploadSourceOrganization
set ApplyStartDateToTreatments = 1
from dbo.GisUploadSourceOrganization 
where GisUploadSourceOrganizationID in (1,2,5,7,8,9)