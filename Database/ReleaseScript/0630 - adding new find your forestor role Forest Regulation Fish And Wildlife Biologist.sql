SET IDENTITY_INSERT dbo.ForesterRole ON;

Insert into dbo.ForesterRole (ForesterRoleID, ForesterRoleDisplayName, ForesterRoleName, SortOrder)
values (14, 'Forest Regulation Fish & Wildlife Biologist', 'ForestRegulationFishAndWildlifeBiologist', 140)

SET IDENTITY_INSERT dbo.ForesterRole OFF;

insert into dbo.ForesterWorkUnit(ForesterRoleID,PersonID,ForesterWorkUnitName,RegionName,ForesterWorkUnitLocation)
select
	14 as ForesterRoleID,
	(select PersonID from dbo.Person where Email = 'Brent.haverkamp@dnr.wa.gov') as PersonID,
	fwu.ForesterWorkUnitName,
	fwu.RegionName,
	fwu.ForesterWorkUnitLocation
from dbo.ForesterWorkUnit as fwu
where fwu.ForesterWorkUnitID = 55


/*

ForesterWorkUnitID	ForesterRoleID	PersonID	ForesterWorkUnitName	RegionName
55	8	6487	State of Washington	State of Washington

*/