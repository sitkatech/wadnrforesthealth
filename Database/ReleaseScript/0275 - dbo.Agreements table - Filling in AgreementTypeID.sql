--begin tran
--delete from dbo.Agreement

/*
select * from dbo.tmpAgreement
select * from dbo.Agreement
select * from dbo.Region
select * from AgreementType
*/



-- Update from AgreementTypeAbbrev
update dbo.Agreement
set AgreementTypeID = agt.AgreementTypeID
from dbo.tmpAgreement as ta
inner join AgreementType as agt on ta.AgreementType = AgreementTypeAbbrev
where ta.TmpAgreementID = dbo.Agreement.TmpAgreementID

-- ALSO update from the AgreementType; we get data from the excel spreadsheet using BOTH formats
update dbo.Agreement
set AgreementTypeID = agt.AgreementTypeID
from dbo.tmpAgreement as ta
inner join AgreementType as agt on ta.AgreementType = AgreementTypeName
where ta.TmpAgreementID = dbo.Agreement.TmpAgreementID
      and
      Agreement.AgreementTypeID is null


/*

-- Four left that no AgreementType of any kind was provided for:
select * from dbo.Agreement
where AgreementTypeID is null

select * 
from tmpAgreement
where TmpAgreementID in (105, 106, 77, 78)
*/
