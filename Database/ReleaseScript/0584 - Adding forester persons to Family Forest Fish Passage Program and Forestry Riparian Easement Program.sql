


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

