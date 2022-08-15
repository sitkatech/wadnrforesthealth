



create table dbo.ForesterRole (
	ForesterRoleID int not null identity(1,1) constraint PK_ForesterRole_ForesterRoleID primary key, 
	ForesterRoleDisplayName varchar(100) not null, 
	ForesterRoleName varchar(100) not null
);


SET IDENTITY_INSERT dbo.ForesterRole ON;

Insert into dbo.ForesterRole (ForesterRoleID, ForesterRoleDisplayName, ForesterRoleName)
values
(1, 'Service Forester', 'ServiceForester'),
(2, 'Service Forestry Specialist', 'ServiceForestrySpecialist'),
(3, 'Forest Practices Forester', 'ForestPracticesForester'),
(4, 'Stewardship Biologist', 'StewardshipBiologist'),
(5, 'Urban Forestry Technician', 'UrbanForestryTechnician'),
(6, 'Community Wildfire Preparedness Specialist', 'CommunityWildfirePreparednessSpecialist'),
(7, 'Regulation Assistance Forester', 'RegulationAssistanceForester'),
(8, 'Family Forest Fish Passage Program', 'FamilyForestFishPassageProgram'),
(9, 'Forestry Riparian Easement Program', 'ForestryRiparianEasementProgram'),
(10, 'Rivers and Habitat Open Space Program Manager', 'RiversAndHabitatOpenSpaceProgramManager'),
(11, 'Service Forestry Program Manager', 'ServiceForestryProgramManager'),
(12, 'UCF Statewide Specialist', 'UcfStatewideSpecialist'),
(13, 'Small Forest Landowner Office Program Manager', 'SmallForestLandownerOfficeProgramManager')

SET IDENTITY_INSERT dbo.ForesterRole OFF;




create table dbo.ForesterWorkUnit(
	ForesterWorkUnitID int not null identity(1,1) constraint PK_ForesterWorkUnit_ForesterWorkUnitID primary key,
	ForesterRoleID int not null constraint FK_ForesterWorkUnit_ForesterRole_ForesterRoleID foreign key references dbo.ForesterRole(ForesterRoleID),
	PersonID int null constraint FK_ForesterWorkUnit_Person_PersonID foreign key references dbo.Person(PersonID),
	ForesterWorkUnitName varchar(100) not null,
	RegionName varchar(100),
	ForesterWorkUnitLocation geometry not null

);


insert into dbo.ForesterWorkUnit(ForesterRoleID, ForesterWorkUnitName, RegionName, ForesterWorkUnitLocation)
select
	3 as ForesterRoleID,
	fu.FP_UNIT_NM as ForesterWorkUnitName,
	fu.REGION_NM as RegionName,
	fu.GEOM as ForesterWorkUnitLocation
from
	dbo.tmpForesterUnit as fu



--create table dbo.ForesterWorkUnitPerson(
--	ForesterWorkUnitPersonID int not null identity(1,1) constraint PK_ForesterWorkUnitPerson_ForesterWorkUnitPersonID primary key,
--	ForesterWorkUnitID int not null constraint FK_ForesterWorkUnitPerson_ForesterWorkUnit_ForesterWorkUnitID foreign key references dbo.ForesterWorkUnit(ForesterWorkUnitID),
--	PersonID int not null constraint FK_ForesterWorkUnitPerson_Person_PersonID foreign key references dbo.Person(PersonID)
--)

--ALTER TABLE [dbo].ForesterWorkUnitPerson ADD  CONSTRAINT [AK_ForesterWorkUnitPerson_ForesterWorkUnitID_PersonID] UNIQUE NONCLUSTERED 
--(
--	[ForesterWorkUnitID] ASC,
--	[PersonID] ASC
--)



insert into dbo.ForesterWorkUnit(ForesterRoleID, ForesterWorkUnitName, RegionName, ForesterWorkUnitLocation)
select
	4 as ForesterRoleID,
	ur.DNRUplandRegionName as ForesterWorkUnitName,
	ur.DNRUplandRegionName as RegionName,
	ur.DNRUplandRegionLocation as ForesterWorkUnitLocation
from
	dbo.DNRUplandRegion as ur

insert into dbo.ForesterWorkUnit(ForesterRoleID, ForesterWorkUnitName, RegionName, ForesterWorkUnitLocation)
select
	5 as ForesterRoleID,
	ur.DNRUplandRegionName as ForesterWorkUnitName,
	ur.DNRUplandRegionName as RegionName,
	ur.DNRUplandRegionLocation as ForesterWorkUnitLocation
from
	dbo.DNRUplandRegion as ur


insert into dbo.FirmaPageType(FirmaPageTypeID, FirmaPageTypeName, FirmaPageTypeDisplayName, FirmaPageRenderTypeID)
values 
(67, 'ManageFindYourForester', 'Manage Find Your Forester', 1),
(68, 'FindYourForester', 'Find Your Forester', 1)
go

insert into dbo.FirmaPage(FirmaPageTypeID, FirmaPageContent)
values (67, null),(68, null)



/*


select * from dbo.ForesterWorkUnit




*/