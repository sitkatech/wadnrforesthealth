--begin tran

delete from AgreementGrantAllocation

alter table AgreementGrantAllocation
add GrantID int not null
GO

alter table AgreementGrantAllocation
with check add constraint FK_AgreementGrantAllocation_Grant_GrantID FOREIGN KEY(GrantID)
REFERENCES [dbo].[Grant] (GrantID)
GO

alter table AgreementGrantAllocation
add constraint AK_AgreementGrantAllocation_GrantAllocationID_GrantID unique(GrantAllocationID, GrantID)
GO





/*
update AgreementGrantAllocation
select a.AgreementID,
	   a.GrantID
from Agreement as a 
inner join AgreementGrantAllocation as aga on a.AgreementID = aga.AgreementID
*/




/*
rollback tran


alter table AgreementGrantAllocation
add constraint 
AK_AgreementGrantAllocation_ProjectID_ProjectLocationName unique(AgreementID, GrantID;
*/