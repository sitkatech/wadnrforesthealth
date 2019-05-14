
CREATE TABLE [dbo].[CostTypeDatamartMapping](
	[CostTypeDatamartMappingID] [int] NOT NULL,
	CostTypeID int not null constraint FK_CostTypeDatamartMapping_CostType_CostTypeID references dbo.CostType(CostTypeID),
	DatamartObjectCode [varchar](10) NOT NULL,
	DatamartObjectName varchar(100) NOT NULL,
 CONSTRAINT [PK_CostTypeDatamartMapping_CostTypeDatamartMappingID] PRIMARY KEY CLUSTERED 
(
	[CostTypeDatamartMappingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


Insert into dbo.CostTypeDatamartMapping (CostTypeDatamartMappingID, CostTypeID, DatamartObjectCode, DatamartObjectName)
values
(1, 3, 'A', 'SALARIES AND WAGES'),
(2, 3, 'B', 'EMPLOYEE BENEFITS'),
(3, 4, 'G', 'TRAVEL'),
(4, 5, 'N', 'GRANTS, BENEFITS & CLIENT SERVICES')



--insert costTypeID into GrantAllocationExpenditures
/*
--seems right but insert fails. need a way to connect to the correct grantAllocationExpenditure
insert into [GrantAllocationExpenditure] (CostTypeID)
select
	ctdm.CostTypeID
from GrantAllocationProgramIndexProjectCode as gapc
inner join ProgramIndex as pin on gapc.ProgramIndexID = pin.ProgramIndexID
inner join ProjectCode as pc on gapc.ProjectCodeID = pc.ProjectCodeID
inner join [dbo].[tmp2015-19_grant_payments_singlesheet] as tgp 
		on 
		dbo.fRemoveLeadingZeroes(tgp.PROJECT_CODE) = pc.ProjectCodeName 
		and 
		dbo.fRemoveLeadingZeroes(tgp.PROGRAM_INDEX_CODE) = pin.ProgramIndexCode
inner join dbo.CostTypeDatamartMapping as ctdm on ctdm.DatamartObjectCode = tgp.OBJECT_CODE
*/

/*
--too many results
select
	gapc.GrantAllocationID,
	null as CostTypeID, -- CostTypeID	 
	tgp.BIENNIUM,
	tgp.FM_NO,
	replace(tgp.CAL_YEAR, ',', '') as CalendarYear,
    (select dbo.fGetCalendarMonthIndexFromMonthString(tgp.[MONTH])) as CalendarMonth,
	tgp.EXPEND_ACCRUED,
	tgp.OBJECT_CODE,
	ctdm.CostTypeID
from GrantAllocationProgramIndexProjectCode as gapc
inner join ProgramIndex as pin on gapc.ProgramIndexID = pin.ProgramIndexID
inner join ProjectCode as pc on gapc.ProjectCodeID = pc.ProjectCodeID
inner join [dbo].[tmp2015-19_grant_payments_singlesheet] as tgp 
		on 
		dbo.fRemoveLeadingZeroes(tgp.PROJECT_CODE) = pc.ProjectCodeName 
		and 
		dbo.fRemoveLeadingZeroes(tgp.PROGRAM_INDEX_CODE) = pin.ProgramIndexCode
inner join dbo.CostTypeDatamartMapping as ctdm on ctdm.DatamartObjectCode = tgp.OBJECT_CODE
inner join dbo.GrantAllocationExpenditure as gae 
		on
		gae.Biennium = tgp.BIENNIUM
		and
		gae.FiscalMonth = tgp.FM_NO
		and
		gae.ExpenditureAmount = tgp.EXPEND_ACCRUED
*/



/*
select
	gae.GrantAllocationID,
	tgp.OBJECT_CODE,
	tgp.OBJECT_NAME,
	--ctdm.CostTypeID,
	--ctdm.DatamartObjectCode,
	--ct.CostTypeName,
	gae.Biennium,
	gae.FiscalMonth,
	gae.ExpenditureAmount,
	tgp.BIENNIUM,
	tgp.FM_NO
from 
dbo.GrantAllocationExpenditure as gae
inner join GrantAllocationProgramIndexProjectCode as gapc on gae.GrantAllocationID = gapc.GrantAllocationID
inner join ProgramIndex as pin on gapc.ProgramIndexID = pin.ProgramIndexID
inner join ProjectCode as pc on gapc.ProjectCodeID = pc.ProjectCodeID
inner join [dbo].[tmp2015-19_grant_payments_singlesheet] as tgp 
		on 
		dbo.fRemoveLeadingZeroes(tgp.PROJECT_CODE) = pc.ProjectCodeName 
		and 
		dbo.fRemoveLeadingZeroes(tgp.PROGRAM_INDEX_CODE) = pin.ProgramIndexCode
join dbo.CostTypeDatamartMapping as ctdm on tgp.OBJECT_CODE = ctdm.DatamartObjectCode
join dbo.CostType as ct on ct.CostTypeID = ctdm.CostTypeID
*/
