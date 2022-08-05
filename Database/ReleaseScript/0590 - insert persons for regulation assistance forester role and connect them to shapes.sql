


insert into dbo.Person(Email, FirstName, LastName, Phone, CreateDate, IsActive, ReceiveSupportEmails)
values ('Tom.chandler@dnr.wa.gov', 'Tom', 'Chandler', null, GETDATE(), 1,	0)

insert into dbo.Person(Email, FirstName, LastName, Phone, CreateDate, IsActive, ReceiveSupportEmails)
values ('Hollis.crapo@dnr.wa.gov', 'Hollis', 'Crapo', null, GETDATE(), 1,	0)

insert into dbo.Person(Email, FirstName, LastName, Phone, CreateDate, IsActive, ReceiveSupportEmails)
values ('todd.olson@dnr.wa.gov', 'Todd', 'Olson', null, GETDATE(), 1,	0)

insert into dbo.Person(Email, FirstName, LastName, Phone, CreateDate, IsActive, ReceiveSupportEmails)
values ('Noah.nequette@dnr.wa.gov', 'Noah', 'Nequette', null, GETDATE(), 1,	0)

insert into dbo.Person(Email, FirstName, LastName, Phone, CreateDate, IsActive, ReceiveSupportEmails)
values ('Roslyn.henricks@dnr.wa.gov', 'Roslyn', 'Henricks', null, GETDATE(), 1,	0)
go



update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person where Email = 'Tom.chandler@dnr.wa.gov')
where ForesterRoleID = 7 and ForesterWorkUnitName = 'Southwest WA'


update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person where Email = 'Hollis.crapo@dnr.wa.gov')
where ForesterRoleID = 7 and ForesterWorkUnitName = 'Northwest WA'

update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person where Email = 'todd.olson@dnr.wa.gov')
where ForesterRoleID = 7 and ForesterWorkUnitName = 'Olympic Peninsula'

update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person where Email = 'Noah.nequette@dnr.wa.gov')
where ForesterRoleID = 7 and ForesterWorkUnitName = 'Eastern WA'

update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person where Email = 'Roslyn.henricks@dnr.wa.gov')
where ForesterRoleID = 7 and ForesterWorkUnitName = 'South Puget Sound '




