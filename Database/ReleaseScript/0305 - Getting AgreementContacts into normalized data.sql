
-- Adding a constraint that we need to observe first
alter table AgreementPerson
add CONSTRAINT [AK_AgreementPerson_AgreementID_PersonID_AgreementPersonRoleID] UNIQUE NONCLUSTERED 
(
    AgreementID ASC,
    PersonID ASC,
    AgreementPersonRoleID ASC
)
GO




insert into dbo.Person (FirstName, LastName, RoleID, CreateDate, IsActive, ReceiveSupportEmails)

select distinct x.FirstName, x.LastName , 7, GetDate(), 1, 0

from dbo.tmpAgreementContact x
left join dbo.Person p  on p.FirstName = x.FirstName and p.LastName = x.LastName
where p.PersonID is null

go


insert into dbo.AgreementPersonRole(AgreementPersonRoleID,AgreementPersonRoleName, AgreementPersonRoleDisplayName)

select 4, 'Signer', 'Signer'

insert into dbo.AgreementPersonRole( AgreementPersonRoleID, AgreementPersonRoleName, AgreementPersonRoleDisplayName)

select 5, 'TechnicalContact', 'Technical Contact'

go


insert into dbo.AgreementPerson (AgreementID, PersonID, AgreementPersonRoleID)

select 
distinct
a.AgreementID,
p.PersonID,
r.AgreementPersonRoleID
from dbo.tmpAgreementContact tac
join dbo.Agreement a on tac.AgreementNumber = a.AgreementNumber
join dbo.Person p on tac.FirstName = p.FirstName and tac.LastName = p.LastName
join dbo.AgreementPersonRole r on r.AgreementPersonRoleDisplayName = rtrim(ltrim(tac.AgreementRole))