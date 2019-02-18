--Adding 'Expired', 'Terminated' to AgreementStatus table. Updating 'Complete' to 'Completed'
insert into dbo.AgreementStatus (AgreementStatusName) values
('Expired'),
('Terminated')
go

update dbo.AgreementStatus
set AgreementStatusName = 'Completed'
where AgreementStatusName = 'Complete'