--select * from dbo.tmpServiceForesterData



insert into dbo.Person(FirstName, LastName, Email, Phone, CreateDate, IsActive, ReceiveSupportEmails)
select 
	sf.ForesterFirstName as FirstName,
	sf.ForesterLastName as LastName,
	sf.ForesterEmail as Email,
	sf.ForesterPhone as Phone, 
	GETDATE() as CreateDate, 
	1 as IsActive,
	0 as ReceiveSupportEmails
from
	dbo.tmpServiceForesterData as sf
where
	PersonID is null and ForesterEmail is not null and  ForesterEmail != ''
go



insert into dbo.ForesterWorkUnit(ForesterRoleID, ForesterWorkUnitName, RegionName, ForesterWorkUnitLocation, PersonID)
select
	1 as ForesterRoleID,
	sf.ForesterWorkUnitName as ForesterWorkUnitName,
	sf.RegionName as RegionName,
	sf.ForesterWorkUnitLocation as ForesterWorkUnitLocation,
	(select PersonID from dbo.Person where FirstName = sf.ForesterFirstName and LastName = sf.ForesterLastName and Email = sf.ForesterEmail) as PersonID
from
	dbo.tmpServiceForesterData as sf
where 
	PersonID is null



insert into dbo.ForesterWorkUnit(ForesterRoleID, ForesterWorkUnitName, RegionName, ForesterWorkUnitLocation, PersonID)
select
	1 as ForesterRoleID,
	sf.ForesterWorkUnitName as ForesterWorkUnitName,
	sf.RegionName as RegionName,
	sf.ForesterWorkUnitLocation as ForesterWorkUnitLocation,
	sf.PersonID as PersonID
from
	dbo.tmpServiceForesterData as sf
where 
	PersonID is not null



delete from dbo.ForesterWorkUnit where ForesterRoleID = 5 and PersonID is null
