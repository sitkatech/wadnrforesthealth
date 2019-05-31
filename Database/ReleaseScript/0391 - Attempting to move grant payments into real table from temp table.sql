/*
select 
	gapc.GrantAllocationProgramIndexProjectCodeID,
	gapc.GrantAllocationID,
    tgp.*
from GrantAllocationProgramIndexProjectCode as gapc
inner join ProgramIndex as pin on gapc.ProgramIndexID = pin.ProgramIndexID
inner join ProjectCode as pc on gapc.ProjectCodeID = pc.ProjectCodeID
inner join [dbo].[tmp2015-19_grant_payments_singlesheet] as tgp 
		on 
		dbo.fRemoveLeadingZeroes(tgp.PROJECT_CODE) = pc.ProjectCodeName 
		and 
		dbo.fRemoveLeadingZeroes(tgp.PROGRAM_INDEX_CODE) = pin.ProgramIndexCode 
*/


-- Introduce this now, since we don't normally do functions until after scripts run.
IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.fGetCalendarMonthIndexFromMonthString'))
    drop function dbo.fGetCalendarMonthIndexFromMonthString
go

/*
Returns the current Fiscal Year Biennium
*/

create function dbo.fGetCalendarMonthIndexFromMonthString
(
   @monthString NVARCHAR(MAX)
)
returns int
begin
    return (SELECT MONTH(@monthString + ' 1 1901'))
end
go




--begin tran
-- We need our own version of Expenditure that isn't tied to Project, for one, and has CostType as well.
CREATE TABLE [dbo].[GrantAllocationExpenditure]
(
    [GrantAllocationExpenditureID] [int] IDENTITY(1,1) NOT NULL,
    [GrantAllocationID] [int] NOT NULL,
    [CostTypeID] int null, -- Probably needs to be non-null, but starting here.
    [Biennium] int not null,
    [FiscalMonth] int not null,
    [CalendarYear] [int] NOT NULL,
    [CalendarMonth] int not null,
    [ExpenditureAmount] [money] NOT NULL
 CONSTRAINT [PK_GrantAllocationExpenditure_GrantAllocationExpenditureID] PRIMARY KEY CLUSTERED 
(
    [GrantAllocationExpenditureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GrantAllocationExpenditure]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationExpenditure_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO

ALTER TABLE [dbo].[GrantAllocationExpenditure]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationExpenditure_CostType_CostTypeID] FOREIGN KEY([CostTypeID])
REFERENCES [dbo].CostType ([CostTypeID])
GO

--rollback tran

--select * from dbo.CostType
--select *
--from [dbo].[tmp2015-19_grant_payments_singlesheet] as tgp 


--insert into [GrantAllocationExpenditure] ( GrantAllocationID, CostTypeID, Biennium, FiscalMonth, CalendarYear, CalendarMonth, ExpenditureAmount)
--select
--	gapc.GrantAllocationID,
--	null as CostTypeID, -- CostTypeID	 
--	tgp.BIENNIUM,
--	tgp.FM_NO,
--	replace(tgp.CAL_YEAR, ',', '') as CalendarYear,
--    (select dbo.fGetCalendarMonthIndexFromMonthString(tgp.[MONTH])) as CalendarMonth,
--	tgp.EXPEND_ACCRUED
--from GrantAllocationProgramIndexProjectCode as gapc
--inner join ProgramIndex as pin on gapc.ProgramIndexID = pin.ProgramIndexID
--inner join ProjectCode as pc on gapc.ProjectCodeID = pc.ProjectCodeID
--inner join [dbo].[tmp2015-19_grant_payments_singlesheet] as tgp 
--		on 
--		dbo.fRemoveLeadingZeroes(tgp.PROJECT_CODE) = pc.ProjectCodeName 
--		and 
--		dbo.fRemoveLeadingZeroes(tgp.PROGRAM_INDEX_CODE) = pin.ProgramIndexCode 


/*


select distinct
  tgp.[MONTH] 
 ,(select dbo.fGetCalendarMonthIndexFromMonthString(tgp.[MONTH])) as CalendarMonth
from [dbo].[tmp2015-19_grant_payments_singlesheet] as tgp
--where tgp.[MONTH] = 'January'



--select * from dbo.ProjectCode
select * from dbo.ProgramIndex

select * from dbo.ProjectGrantAllocationExpenditure
select * from dbo.ProjectGrantAllocationRequest

select * from dbo.Project

select * from GrantAllocation
select * from GrantAllocation
*/