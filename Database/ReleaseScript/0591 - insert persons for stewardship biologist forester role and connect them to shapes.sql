


insert into dbo.Person(Email, FirstName, LastName, Phone, CreateDate, IsActive, ReceiveSupportEmails)
values ('ken.bevis@dnr.wa.gov', 'Ken', 'Bevis', '(360) 489-4802', GETDATE(), 1,	0)
go



update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person where Email = 'ken.bevis@dnr.wa.gov')
where ForesterRoleID = 4