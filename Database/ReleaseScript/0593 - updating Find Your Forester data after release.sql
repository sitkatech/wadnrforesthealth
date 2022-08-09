


insert into dbo.Person(FirstName, LastName, Email, Phone, CreateDate, IsActive, ReceiveSupportEmails)
values ('Jessica', 'Hawkins', 'Jessica.hawkins@dnr.wa.gov', '(360) 603-8932', GETDATE(), 1,	0)
go

update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person where Email = 'Jessica.hawkins@dnr.wa.gov')
where ForesterRoleID = 3 and ForesterWorkUnitName = 'Islands' and RegionName = 'Northwest Region'



update dbo.Person
set Phone = '(360) 854-8235'
where Email = 'zachary.bastow@dnr.wa.gov'

update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person where Email = 'zachary.bastow@dnr.wa.gov')
where ForesterRoleID = 3 and ForesterWorkUnitName = 'Chuckanut' and RegionName = 'Northwest Region'


insert into dbo.Person(FirstName, LastName, Email, Phone, CreateDate, IsActive, ReceiveSupportEmails)
values ('Kirsten', 'Rowley', 'kirsten.rowley@dnr.wa.gov', '(360) 592-7058', GETDATE(), 1,	0)
go

update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person where Email = 'kirsten.rowley@dnr.wa.gov')
where ForesterRoleID = 3 and ForesterWorkUnitName = 'Skagit' and RegionName = 'Northwest Region'



update dbo.Person
set Phone = '(360) 688-6300'
where Email = 'mackenna.milosevich@dnr.wa.gov'




update dbo.FindYourForesterQuestion
set ForesterRoleID = 7
where FindYourForesterQuestionID in (14, 15, 23)



update dbo.FindYourForesterQuestion
set ForesterRoleID = 1
where FindYourForesterQuestionID = 43




