
select * from dbo.tmpAgreement
select * from dbo.Agreement

-- Need a function for bogus dates? Parse?

insert into dbo.Agreement (TmpAgreementID, AgreementNumber, StartDate, EndDate, AgreementAmount, ExpendedAmount, BalanceAmount, /*FirstBillDueOn,*/ Notes)
select ta.TmpAgreementID, ta.AgreementNumber, ta.StartDate, ta.EndDate, ta.AgreementAmount, ta.Expended, ta.Balance, /*ta.[1ST_BILL_DUE_ON], */ ta.Notes
from dbo.tmpAgreement as ta
