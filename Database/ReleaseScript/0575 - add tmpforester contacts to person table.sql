
insert into dbo.Person(FirstName, LastName, Phone, CreateDate, IsActive, ReceiveSupportEmails)
select distinct FP_FORES_1, FP_FORES_2, PHONE_NO, GETDATE(), 1, 0
from dbo.tmpForesterUnit where FP_FORESTE not like '%Vacant%'
go


update dbo.tmpForesterUnit 
set PersonID = (select PersonID from dbo.Person as p where p.FirstName = FP_FORES_1 and p.LastName = FP_FORES_2)


update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.tmpForesterUnit as fu where fu.FP_UNIT_NM = ForesterWorkUnitName and fu.REGION_NM = RegionName)
where ForesterRoleID = 3



