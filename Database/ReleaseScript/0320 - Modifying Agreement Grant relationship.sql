--Dropping relationship between Agreement and Grant because now many-to-many
alter table dbo.[Agreement]
drop constraint FK_Agreement_Grant_GrantID

alter table [dbo].[AgreementGrantAllocation]  
drop constraint FK_AgreementGrantAllocation_Agreement_AgreementID_GrantID

alter table dbo.[Agreement]
drop column GrantID