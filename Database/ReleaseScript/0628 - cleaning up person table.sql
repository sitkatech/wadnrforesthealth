



delete from dbo.PersonAllowedAuthenticator where PersonID in (6354, 6356, 5543,6274, 5292)

update dbo.ProjectPerson set PersonID = 5582 where PersonID = 6274

delete from dbo.PersonRole where PersonID in (6354, 6356, 5543,6274, 5292)

delete from dbo.Person where PersonID in (6354, 6356, 5543,6274, 5292)

update dbo.Person set FirstName = 'Dylan' where PersonID = 5582

update dbo.Person set FirstName = 'Amy', LastName = 'Ramsey' where PersonID = 5450



update dbo.ForesterWorkUnit set PersonID = (select PersonID from dbo.Person where LastName = 'Fales' and Email = 'Emily.Fales@dnr.wa.gov')
where ForesterWorkUnitName like '%Madrone%' and ForesterRoleID = 1

delete from dbo.GrantAllocationProgramManager
where PersonID in (select PersonID from dbo.Person where LastName like '%999%')

delete from dbo.PersonRole
where PersonID in (select PersonID from dbo.Person where LastName like '%999%')

delete from dbo.Person
where LastName like '%999%'



