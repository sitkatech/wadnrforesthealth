



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
(7, 'Regulation Assistance Forester', 'RegulationAssistanceForester')

SET IDENTITY_INSERT dbo.ForesterRole OFF;




create table dbo.ForesterWorkUnit(
	ForesterWorkUnitID int not null identity(1,1) constraint PK_ForesterWorkUnit_ForesterWorkUnitID primary key,
	ForesterRoleID int not null constraint FK_ForesterWorkUnit_ForesterRole_ForesterRoleID foreign key references dbo.ForesterRole(ForesterRoleID),
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



create table dbo.ForesterWorkUnitPerson(
	ForesterWorkUnitPersonID int not null identity(1,1) constraint PK_ForesterWorkUnitPerson_ForesterWorkUnitPersonID primary key,
	ForesterWorkUnitID int not null constraint FK_ForesterWorkUnitPerson_ForesterWorkUnit_ForesterWorkUnitID foreign key references dbo.ForesterWorkUnit(ForesterWorkUnitID),
	PersonID int not null constraint FK_ForesterWorkUnitPerson_Person_PersonID foreign key references dbo.Person(PersonID)
)




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



