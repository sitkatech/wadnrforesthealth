--begin tran
delete from dbo.Agreement

/*
select * from dbo.tmpAgreement
select * from dbo.Agreement
*/



insert into dbo.Agreement (TmpAgreementID, 
                           AgreementNumber, 
                           StartDate, 
                           EndDate, 
                           AgreementAmount, 
                           ExpendedAmount, 
                           BalanceAmount, 
                           /*FirstBillDueOn,*/ 
                           Notes)
select ta.TmpAgreementID, 
       ta.AgreementNumber, 
       TRY_PARSE(ta.StartDate as DATETIME), 
       TRY_PARSE(ta.EndDate as DATETIME), 
       TRY_PARSE(ta.AgreementAmount AS MONEY), 
       TRY_PARSE(ta.Expended as MONEY), 
       TRY_PARSE(ta.Balance as MONEY), 
       /*ta.[1ST_BILL_DUE_ON], */ 
       ta.Notes
from dbo.tmpAgreement as ta
-- Don't add AgreementNumbers that are duplicated in the original spreadsheet. Omit for now to force the question of what to do about
-- duplicate AgreementNumbers.
where ta.AgreementNumber in
(
    select ta.AgreementNumber
           --count(*) as DupeCount
    from dbo.tmpAgreement as ta
    group by ta.AgreementNumber
    having count(*) = 1
)

select * from dbo.Agreement

--rollback tran