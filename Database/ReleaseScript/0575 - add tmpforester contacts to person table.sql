insert into Person(FirstName, LastName, Phone, CreateDate, IsActive, ReceiveSupportEmails)
select distinct FP_FORES_1, FP_FORES_2, PHONE_NO, GETDATE(), 1, 0
from tmpForesterUnit where FP_FORESTE not like '%Vacant%'