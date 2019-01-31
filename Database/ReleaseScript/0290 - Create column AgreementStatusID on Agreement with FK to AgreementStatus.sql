alter table dbo.Agreement
add AgreementStatusID int null
constraint FK_Agreement_AgreementStatus_AgreementStatusID foreign key (AgreementStatusID) 
references AgreementStatus(AgreementStatusID)
go

update dbo.Agreement 
set AgreementStatusID = 1