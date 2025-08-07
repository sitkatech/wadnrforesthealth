

--EXECUTE sp_rename 'Sales.SalesTerritory', 'SalesTerr';
--drop old lookup tables
--drop table dbo.GrantStatus
--drop table dbo.GrantAllocationSource
--drop table dbo.GrantAllocationPriority
--drop table dbo.[tmp2015-19_grant_payments_singlesheet]


execute sp_rename 'dbo.AgreementGrantAllocation.AgreementGrantAllocationID', 'AgreementFundSourceAllocationID', 'COLUMN';
execute sp_rename 'dbo.AgreementGrantAllocation.GrantAllocationID', 'FundSourceAllocationID', 'COLUMN';
execute sp_rename 'dbo.Grant.GrantID', 'FundSourceID', 'COLUMN';
execute sp_rename 'dbo.Grant.GrantName', 'FundSourceName', 'COLUMN';
execute sp_rename 'dbo.Grant.GrantNumber', 'FundSourceNumber', 'COLUMN';
execute sp_rename 'dbo.Grant.GrantStatusID', 'FundSourceStatusID', 'COLUMN';
execute sp_rename 'dbo.Grant.GrantTypeID', 'FundSourceTypeID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocation.GrantAllocationID', 'FundSourceAllocationID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocation.GrantAllocationName', 'FundSourceAllocationName', 'COLUMN';
execute sp_rename 'dbo.GrantAllocation.GrantAllocationPriorityID', 'FundSourceAllocationPriorityID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocation.GrantAllocationSourceID', 'FundSourceAllocationSourceID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocation.GrantID', 'FundSourceID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocation.GrantManagerID', 'FundSourceManagerID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationBudgetLineItem.GrantAllocationBudgetLineItemAmount', 'FundSourceAllocationBudgetLineItemAmount', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationBudgetLineItem.GrantAllocationBudgetLineItemID', 'FundSourceAllocationBudgetLineItemID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationBudgetLineItem.GrantAllocationBudgetLineItemNote', 'FundSourceAllocationBudgetLineItemNote', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationBudgetLineItem.GrantAllocationID', 'FundSourceAllocationID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationChangeLog.GrantAllocationAmountNewValue', 'FundSourceAllocationAmountNewValue', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationChangeLog.GrantAllocationAmountNote', 'FundSourceAllocationAmountNote', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationChangeLog.GrantAllocationAmountOldValue', 'FundSourceAllocationAmountOldValue', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationChangeLog.GrantAllocationChangeLogID', 'FundSourceAllocationChangeLogID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationChangeLog.GrantAllocationID', 'FundSourceAllocationID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationExpenditure.GrantAllocationExpenditureID', 'FundSourceAllocationExpenditureID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationExpenditure.GrantAllocationID', 'FundSourceAllocationID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationExpenditureJsonStage.GrantAllocationExpenditureJsonStageID', 'FundSourceAllocationExpenditureJsonStageID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationFileResource.GrantAllocationFileResourceID', 'FundSourceAllocationFileResourceID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationFileResource.GrantAllocationID', 'FundSourceAllocationID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationLikelyPerson.GrantAllocationID', 'FundSourceAllocationID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationLikelyPerson.GrantAllocationLikelyPersonID', 'FundSourceAllocationLikelyPersonID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationNote.GrantAllocationID', 'FundSourceAllocationID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationNote.GrantAllocationNoteID', 'FundSourceAllocationNoteID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationNote.GrantAllocationNoteText', 'FundSourceAllocationNoteText', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationNoteInternal.GrantAllocationID', 'FundSourceAllocationID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationNoteInternal.GrantAllocationNoteInternalID', 'FundSourceAllocationNoteInternalID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationNoteInternal.GrantAllocationNoteInternalText', 'FundSourceAllocationNoteInternalText', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationPriority.GrantAllocationPriorityColor', 'FundSourceAllocationPriorityColor', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationPriority.GrantAllocationPriorityID', 'FundSourceAllocationPriorityID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationPriority.GrantAllocationPriorityNumber', 'FundSourceAllocationPriorityNumber', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationProgramIndexProjectCode.GrantAllocationID', 'FundSourceAllocationID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationProgramIndexProjectCode.GrantAllocationProgramIndexProjectCodeID', 'FundSourceAllocationProgramIndexProjectCodeID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationProgramManager.GrantAllocationID', 'FundSourceAllocationID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationProgramManager.GrantAllocationProgramManagerID', 'FundSourceAllocationProgramManagerID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationSource.GrantAllocationSourceDisplayName', 'FundSourceAllocationSourceDisplayName', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationSource.GrantAllocationSourceID', 'FundSourceAllocationSourceID', 'COLUMN';
execute sp_rename 'dbo.GrantAllocationSource.GrantAllocationSourceName', 'FundSourceAllocationSourceName', 'COLUMN';
execute sp_rename 'dbo.GrantFileResource.GrantFileResourceID', 'FundSourceFileResourceID', 'COLUMN';
execute sp_rename 'dbo.GrantFileResource.GrantID', 'FundSourceID', 'COLUMN';
execute sp_rename 'dbo.GrantNote.GrantID', 'FundSourceID', 'COLUMN';
execute sp_rename 'dbo.GrantNote.GrantNoteID', 'FundSourceNoteID', 'COLUMN';
execute sp_rename 'dbo.GrantNote.GrantNoteText', 'FundSourceNoteText', 'COLUMN';
execute sp_rename 'dbo.GrantNoteInternal.GrantID', 'FundSourceID', 'COLUMN';
execute sp_rename 'dbo.GrantNoteInternal.GrantNoteInternalID', 'FundSourceNoteInternalID', 'COLUMN';
execute sp_rename 'dbo.GrantNoteInternal.GrantNoteText', 'FundSourceNoteText', 'COLUMN';
execute sp_rename 'dbo.GrantStatus.GrantStatusID', 'FundSourceStatusID', 'COLUMN';
execute sp_rename 'dbo.GrantStatus.GrantStatusName', 'FundSourceStatusName', 'COLUMN';
execute sp_rename 'dbo.GrantType.GrantTypeID', 'FundSourceTypeID', 'COLUMN';
execute sp_rename 'dbo.GrantType.GrantTypeName', 'FundSourceTypeName', 'COLUMN';
execute sp_rename 'dbo.Invoice.GrantID', 'FundSourceID', 'COLUMN';
execute sp_rename 'dbo.LoaStage.GrantNumber', 'FundSourceNumber', 'COLUMN';
execute sp_rename 'dbo.ProjectGrantAllocationRequest.GrantAllocationID', 'FundSourceAllocationID', 'COLUMN';
execute sp_rename 'dbo.ProjectGrantAllocationRequest.ProjectGrantAllocationRequestID', 'ProjectFundSourceAllocationRequestID', 'COLUMN';
execute sp_rename 'dbo.ProjectGrantAllocationRequestUpdate.GrantAllocationID', 'FundSourceAllocationID', 'COLUMN';
execute sp_rename 'dbo.ProjectGrantAllocationRequestUpdate.ProjectGrantAllocationRequestUpdateID', 'ProjectFundSourceAllocationRequestUpdateID', 'COLUMN';




EXECUTE sp_rename 'dbo.Grant', 'FundSource';

EXECUTE sp_rename 'dbo.GrantAllocation', 'FundSourceAllocation';

EXECUTE sp_rename 'dbo.GrantAllocationBudgetLineItem', 'FundSourceAllocationBudgetLineItem';
EXECUTE sp_rename 'dbo.GrantAllocationChangeLog', 'FundSourceAllocationChangeLog';
EXECUTE sp_rename 'dbo.GrantAllocationExpenditure', 'FundSourceAllocationExpenditure';
EXECUTE sp_rename 'dbo.GrantAllocationExpenditureJsonStage', 'FundSourceAllocationExpenditureJsonStage';
EXECUTE sp_rename 'dbo.GrantAllocationFileResource', 'FundSourceAllocationFileResource';
EXECUTE sp_rename 'dbo.GrantAllocationLikelyPerson', 'FundSourceAllocationLikelyPerson';
EXECUTE sp_rename 'dbo.GrantAllocationNote', 'FundSourceAllocationNote';
EXECUTE sp_rename 'dbo.GrantAllocationNoteInternal', 'FundSourceAllocationNoteInternal';
EXECUTE sp_rename 'dbo.GrantAllocationPriority', 'FundSourceAllocationPriority';
EXECUTE sp_rename 'dbo.GrantAllocationProgramIndexProjectCode', 'FundSourceAllocationProgramIndexProjectCode';
EXECUTE sp_rename 'dbo.GrantAllocationProgramManager', 'FundSourceAllocationProgramManager';
EXECUTE sp_rename 'dbo.GrantAllocationSource', 'FundSourceAllocationSource';
EXECUTE sp_rename 'dbo.GrantFileResource', 'FundSourceFileResource';
EXECUTE sp_rename 'dbo.GrantNote', 'FundSourceNote';
EXECUTE sp_rename 'dbo.GrantNoteInternal', 'FundSourceNoteInternal';
EXECUTE sp_rename 'dbo.GrantStatus', 'FundSourceStatus';
EXECUTE sp_rename 'dbo.GrantType', 'FundSourceType';


EXECUTE sp_rename 'dbo.AgreementGrantAllocation', 'AgreementFundSourceAllocation';
EXECUTE sp_rename 'dbo.ProjectGrantAllocationRequest', 'ProjectFundSourceAllocationRequest';
EXECUTE sp_rename 'dbo.ProjectGrantAllocationRequestUpdate', 'ProjectFundSourceAllocationRequestUpdate';



/*

Select s.name As SchemaName, t.name As TableName
From sys.schemas s 
Inner Join sys.tables t On s.schema_id = t.schema_id
where t.name like '%Grant%'
Order By SchemaName, TableName;

--EXECUTE sp_rename 'Sales.SalesTerritory.TerritoryID', 'TerrID', 'COLUMN';
SELECT table_schema, table_name, column_name, REPLACE(column_name,'Grant','FundSource') as new_column_name, 'execute sp_rename ''' + table_schema + '.' + table_name + '.' + column_name + ''', ''' + REPLACE(column_name,'Grant','FundSource') + ''', ''COLUMN'';' as scriptToRun
FROM INFORMATION_SCHEMA.COLUMNS
WHERE COLUMN_NAME like '%Grant%' and table_name not in ('LoaRawSoutheast', 'LoaRawNortheast') and table_name not like 'v%' and table_name not like 'tmp%'
order by table_name
*/