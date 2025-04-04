


  insert into dbo.Person(FirstName, LastName, Email, Phone, CreateDate, IsActive, OrganizationID, ReceiveSupportEmails)
  values ('Celesta', 'Collacchi', 'celesta.collacchi@dnr.wa.gov', '(564) 250-1415', getdate(), 1, 4704, 0)


  update dbo.tmpForesterWorkUnitUpdatesUrbanTech set ForesterWorkUnitName = 'East', RegionName = 'East' where RegionName = 'E'
  update dbo.tmpForesterWorkUnitUpdatesUrbanTech set ForesterWorkUnitName = 'Northwest', RegionName = 'Northwest' where RegionName = 'NW'
    update dbo.tmpForesterWorkUnitUpdatesUrbanTech set ForesterWorkUnitName = 'Southwest', RegionName = 'SouthWest' where RegionName = 'SW'


	delete from dbo.ForesterWorkUnit where ForesterRoleID = 5

insert into dbo.ForesterWorkUnit (ForesterRoleID, PersonID, ForesterWorkUnitName, RegionName, ForesterWorkUnitLocation) 
select ForesterRoleID, p.PersonID, ucf.ForesterWorkUnitName, ucf.RegionName, ucf.ForesterWorkUnitLocation
FROM dbo.tmpForesterWorkUnitUpdatesUrbanTech as ucf join dbo.Person as p on ucf.Email = p.Email

