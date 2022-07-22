insert into Person(FirstName, LastName, Phone, CreateDate, IsActive, ReceiveSupportEmails)
select distinct FP_FORES_1, FP_FORES_2, PHONE_NO, GETDATE(), 1, 0
from tmpForesterUnit where FP_FORESTE not like '%Vacant%'


insert into ForesterWorkUnitPerson(ForesterWorkUnitID, PersonID)
select distinct fwu.ForesterWorkUnitID, p.PersonID 
from ForesterWorkUnit as fwu 
left join tmpForesterUnit as tfu on fwu.RegionName  = tfu.Region_NM 
left join person as p on p.FirstName = tfu.FP_FORES_1 and p.LastName=tfu.FP_FORES_2
where fwu.foresterRoleID = 3 and tfu.FP_FORESTE not like '%Vacant%'
