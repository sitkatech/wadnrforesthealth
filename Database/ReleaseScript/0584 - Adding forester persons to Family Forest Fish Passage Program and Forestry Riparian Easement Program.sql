


insert into dbo.Person(FirstName, LastName, Phone, Email, CreateDate, IsActive, ReceiveSupportEmails)
values( 'Chris', 'Dwight', '(360) 490-0020', 'Christpher.dwight@dnr.wa.gov', GETDATE(), 1, 0)
go

update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person as p where p.FirstName = 'Chris' and p.LastName = 'Dwight')
where ForesterRoleID = 8





insert into dbo.Person(FirstName, LastName, Phone, Email, CreateDate, IsActive, ReceiveSupportEmails)
values( 'Dan', 'Pomerenk', '(360) 480-4621', 'Daniel.pomerenk@dnr.wa.gov', GETDATE(), 1, 0)
go

update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person as p where p.FirstName = 'Dan' and p.LastName = 'Pomerenk')
where ForesterRoleID = 9


insert into dbo.Person(FirstName, LastName, Phone, Email, CreateDate, IsActive, ReceiveSupportEmails)
values( 'KelliAnne', 'Ricks', '(360) 480-9702', 'Kellianne.ricks@dnr.wa.gov', GETDATE(), 1, 0)
go

update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person as p where p.FirstName = 'KelliAnne' and p.LastName = 'Ricks')
where ForesterRoleID = 10




update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person as p where p.FirstName = 'MATTHEW' and p.LastName = 'PROVENCHER')
where ForesterRoleID = 11


insert into dbo.Person(FirstName, LastName, Phone, Email, CreateDate, IsActive, ReceiveSupportEmails)
values( 'Jess', 'Lloyd', '(360) 561-8050', 'jessica.lloyd@dnr.wa.gov', GETDATE(), 1, 0)
go

update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person as p where p.FirstName = 'Jess' and p.LastName = 'Lloyd')
where ForesterRoleID = 12


insert into dbo.Person(FirstName, LastName, Phone, Email, CreateDate, IsActive, ReceiveSupportEmails)
values( 'Tami', 'Miketa', '(360) 902-1415', 'tamara.miketa@dnr.wa.gov', GETDATE(), 1, 0)
go

update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person as p where p.FirstName = 'Tami' and p.LastName = 'Miketa')
where ForesterRoleID = 13


/*

Select * from dbo.Person as p where p.LastName in ('Ricks', 'Provencher', 'Lloyd', 'Miketa')

select * from dbo.ForesterRole




*/