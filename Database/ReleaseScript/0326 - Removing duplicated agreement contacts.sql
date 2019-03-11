delete from dbo.AgreementPerson

insert into dbo.AgreementPerson (AgreementID, PersonID, AgreementPersonRoleID)

select 
distinct
a.AgreementID,
p.PersonID,
r.AgreementPersonRoleID
from dbo.tmpAgreementContact tac
join dbo.Person p on tac.FirstName = p.FirstName and tac.LastName = p.LastName
join dbo.AgreementPersonRole r on r.AgreementPersonRoleDisplayName = rtrim(ltrim(tac.AgreementRole))
left join dbo.Agreement a on tac.AgreementNumber = a.AgreementNumber
and tac.Title = a.AgreementTitle
where a.AgreementID is not null


