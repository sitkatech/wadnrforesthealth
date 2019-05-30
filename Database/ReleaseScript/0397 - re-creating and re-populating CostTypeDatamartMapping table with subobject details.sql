--select * from CostTypeDatamartMapping
--select * from GrantAllocationExpenditure

-- remove grant allocation expenditures
delete from dbo.GrantAllocationExpenditure
--remove old costTypeMappings
drop table dbo.CostTypeDatamartMapping;

--inserting new CostTypes so they can be used later in this script
SET IDENTITY_INSERT dbo.CostType ON;

--If you are adding a new CostType, make sure you update the following check constraints(if needed):
--CK_InvoiceLineItem_CostTypeValues
delete from dbo.CostType

Insert into dbo.CostType (CostTypeID, CostTypeDisplayName, CostTypeName, IsValidInvoiceLineItemCostType)
values
(1, 'Indirect Costs', 'IndirectCosts', 1),
(2, 'Supplies', 'Supplies', 1),
(3, 'Personnel', 'Personnel', 1),
(4, 'Benefits', 'Benefits', 1),
(5, 'Travel', 'Travel', 1),
(6, 'Contractual', 'Contractual', 1),
(7, 'Agreements', 'Agreements', 0),
(8, 'Equipment', 'Equipment', 1),
(9, 'Other', 'Other', 1)

SET IDENTITY_INSERT dbo.CostType OFF;


-- recreate table with identity PK and subObject columns
CREATE TABLE [dbo].[CostTypeDatamartMapping](
	[CostTypeDatamartMappingID] [int] NOT NULL identity(1,1),
	CostTypeID int not null constraint FK_CostTypeDatamartMapping_CostType_CostTypeID references dbo.CostType(CostTypeID),
	DatamartObjectCode [varchar](10) NOT NULL,
	DatamartObjectName varchar(100) NOT NULL,
	DatamartSubObjectCode varchar(10) NOT NULL,
	DatamartSubObjectName varchar(250) NOT NULL
 CONSTRAINT [PK_CostTypeDatamartMapping_CostTypeDatamartMappingID] PRIMARY KEY CLUSTERED 
(
	[CostTypeDatamartMappingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



-- insert updated mappings into our table
insert into dbo.CostTypeDatamartMapping ( CostTypeID, DatamartObjectCode, DatamartObjectName, DatamartSubObjectCode, DatamartSubObjectName)
select	ct.CostTypeID,
		tctm.OBJECT_CODE as DatamartObjectCode,
		tctm.OBJECT_NAME as DatamartObjectName,
		tctm.SUB_OBJECT_CODE as DatamartSubObjectCode,
		tctm.SUB_OBJECT_NAME as DatamartSubObjectName
	from
		 dbo.tmpCostTypeMapping as tctm
	join dbo.CostType as ct on ct.CostTypeDisplayName = tctm.CostTypeDescription




-- re-populate GrantAllocationExpenditures with correct mappings
insert into [GrantAllocationExpenditure] ( GrantAllocationID, CostTypeID, Biennium, FiscalMonth, CalendarYear, CalendarMonth, ExpenditureAmount)
select
	gapc.GrantAllocationID,
	ctdm.CostTypeID as CostTypeID, -- CostTypeID	 
	tgp.BIENNIUM,
	tgp.FM_NO,
	replace(tgp.CAL_YEAR, ',', '') as CalendarYear,
    (select dbo.fGetCalendarMonthIndexFromMonthString(tgp.[MONTH])) as CalendarMonth,
	tgp.EXPEND_ACCRUED
from GrantAllocationProgramIndexProjectCode as gapc
inner join ProgramIndex as pin on gapc.ProgramIndexID = pin.ProgramIndexID
inner join ProjectCode as pc on gapc.ProjectCodeID = pc.ProjectCodeID
inner join [dbo].[tmp2015-19_grant_payments_singlesheet] as tgp 
		on 
		dbo.fRemoveLeadingZeroes(tgp.PROJECT_CODE) = pc.ProjectCodeName 
		and 
		dbo.fRemoveLeadingZeroes(tgp.PROGRAM_INDEX_CODE) = pin.ProgramIndexCode
inner join dbo.CostTypeDatamartMapping as ctdm on ctdm.DatamartObjectCode = tgp.OBJECT_CODE and ctdm.DatamartSubObjectCode =  tgp.SUB_OBJECT_CODE
