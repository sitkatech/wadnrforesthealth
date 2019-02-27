--begin tran

delete from dbo.AgreementGrantAllocation

alter table AgreementGrantAllocation add GrantID int not null
GO

alter table dbo.grantallocation add constraint AK_GrantAllocation_GrantAllocationID_GrantID unique (GrantAllocationID, GrantID)
alter table AgreementGrantAllocation add constraint FK_AgreementGrantAllocation_GrantAllocation_GrantAllocationID_GrantID FOREIGN KEY(GrantAllocationID, GrantID) REFERENCES [dbo].[GrantAllocation] (GrantAllocationID, GrantID)

alter table dbo.Agreement add constraint AK_Agreement_AgreementID_GrantID unique (AgreementID, GrantID)
alter table dbo.AgreementGrantAllocation add constraint FK_AgreementGrantAllocation_Agreement_AgreementID_GrantID FOREIGN KEY(AgreementID, GrantID) REFERENCES [dbo].Agreement (AgreementID, GrantID)


drop table dbo.AgreementProjectCode
alter table dbo.[Grant] drop column ProjectCode





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