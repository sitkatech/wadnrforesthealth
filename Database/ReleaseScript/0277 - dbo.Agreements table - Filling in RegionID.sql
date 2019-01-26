--begin tran

/*
select * from dbo.tmpAgreement
select * from dbo.Agreement
select * from dbo.Region

*/




-- Update from AgreementTypeAbbrev
update dbo.Agreement
set RegionID = r.RegionID
from dbo.tmpAgreement as ta
inner join Region as r on r.RegionAbbrev = Region_Div
where ta.TmpAgreementID = dbo.Agreement.TmpAgreementID

--select * from dbo.Agreement

--rollback tran

