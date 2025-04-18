



insert into dbo.Person(FirstName, LastName, Email, Phone, CreateDate, IsActive, OrganizationID, ReceiveSupportEmails)
  values ('Kirk', 'Troberg', 'kirk.troberg@dnr.wa.gov', '(360) 972-4428', getdate(), 1, 4704, 0)

  insert into dbo.Person(FirstName, LastName, Email, Phone, CreateDate, IsActive, OrganizationID, ReceiveSupportEmails)
  values ('Jesse', 'Duvall', 'jesse.duvall@dnr.wa.gov', '(360) 972-4135', getdate(), 1, 4704, 0)


  	delete from dbo.ForesterWorkUnit where ForesterRoleID = 6

insert into dbo.ForesterWorkUnit (ForesterRoleID, PersonID, ForesterWorkUnitName, RegionName, ForesterWorkUnitLocation) 
select ForesterRoleID, p.PersonID, ucf.ForesterWorkUnitName, ucf.RegionName, ucf.ForesterWorkUnitLocation
FROM dbo.[tmpForesterWorkUnitUpdatesCoordinator] as ucf join dbo.Person as p on ucf.cr_email = p.Email

--select * from dbo.tmpForesterWorkUnitUpdatesCoordinator

drop table dbo.tmpForesterWorkUnitUpdatesCoordinator
drop table dbo.tmpForesterWorkUnitUpdatesUrbanTech