

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


execute sp_rename 'dbo.FK_AgreementGrantAllocation_Agreement_AgreementID', 'FK_AgreementFundSourceAllocation_Agreement_AgreementID';
execute sp_rename 'dbo.FK_AgreementGrantAllocation_GrantAllocation_GrantAllocationID', 'FK_AgreementFundSourceAllocation_FundSourceAllocation_FundSourceAllocationID';
execute sp_rename 'dbo.FK_Grant_GrantStatus_GrantStatusID', 'FK_FundSource_FundSourceStatus_FundSourceStatusID';
execute sp_rename 'dbo.FK_Grant_GrantType_GrantTypeID', 'FK_FundSource_FundSourceType_FundSourceTypeID';
execute sp_rename 'dbo.FK_Grant_Organization_OrganizationID', 'FK_FundSource_Organization_OrganizationID';
execute sp_rename 'dbo.FK_GrantAllocation_Division_DivisionID', 'FK_FundSourceAllocation_Division_DivisionID';
execute sp_rename 'dbo.FK_GrantAllocation_DNRUplandRegion_DNRUplandRegionID', 'FK_FundSourceAllocation_DNRUplandRegion_DNRUplandRegionID';
execute sp_rename 'dbo.FK_GrantAllocation_FederalFundCode_FederalFundCodeID', 'FK_FundSourceAllocation_FederalFundCode_FederalFundCodeID';
execute sp_rename 'dbo.FK_GrantAllocation_Grant_GrantID', 'FK_FundSourceAllocation_FundSource_FundSourceID';
execute sp_rename 'dbo.FK_GrantAllocation_GrantAllocationPriority_GrantAllocationPriorityID', 'FK_FundSourceAllocation_FundSourceAllocationPriority_FundSourceAllocationPriorityID';
execute sp_rename 'dbo.FK_GrantAllocation_GrantAllocationSource_GrantAllocationSourceID', 'FK_FundSourceAllocation_FundSourceAllocationSource_FundSourceAllocationSourceID';
execute sp_rename 'dbo.FK_GrantAllocation_Organization_OrganizationID', 'FK_FundSourceAllocation_Organization_OrganizationID';
execute sp_rename 'dbo.FK_GrantAllocation_Person_GrantManagerID_PersonID', 'FK_FundSourceAllocation_Person_FundSourceManagerID_PersonID';
execute sp_rename 'dbo.FK_GrantAllocationBudgetLineItem_CostType_CostTypeID', 'FK_FundSourceAllocationBudgetLineItem_CostType_CostTypeID';
execute sp_rename 'dbo.FK_GrantAllocationBudgetLineItem_GrantAllocation_GrantAllocationID', 'FK_FundSourceAllocationBudgetLineItem_FundSourceAllocation_FundSourceAllocationID';
execute sp_rename 'dbo.FK_GrantAllocationChangeLog_GrantAllocation_GrantAllocationID', 'FK_FundSourceAllocationChangeLog_FundSourceAllocation_FundSourceAllocationID';
execute sp_rename 'dbo.FK_GrantAllocationChangeLog_Person_ChangePersonID_PersonID', 'FK_FundSourceAllocationChangeLog_Person_ChangePersonID_PersonID';
execute sp_rename 'dbo.FK_GrantAllocationExpenditure_CostType_CostTypeID', 'FK_FundSourceAllocationExpenditure_CostType_CostTypeID';
execute sp_rename 'dbo.FK_GrantAllocationExpenditure_GrantAllocation_GrantAllocationID', 'FK_FundSourceAllocationExpenditure_FundSourceAllocation_FundSourceAllocationID';
execute sp_rename 'dbo.FK_GrantAllocationFileResource_FileResource_FileResourceID', 'FK_FundSourceAllocationFileResource_FileResource_FileResourceID';
execute sp_rename 'dbo.FK_GrantAllocationFileResource_GrantAllocation_GrantAllocationID', 'FK_FundSourceAllocationFileResource_FundSourceAllocation_FundSourceAllocationID';
execute sp_rename 'dbo.FK_GrantAllocationLikelyPerson_GrantAllocation_GrantAllocationID', 'FK_FundSourceAllocationLikelyPerson_FundSourceAllocation_FundSourceAllocationID';
execute sp_rename 'dbo.FK_GrantAllocationLikelyPerson_Person_PersonID', 'FK_FundSourceAllocationLikelyPerson_Person_PersonID';
execute sp_rename 'dbo.FK_GrantAllocationNote_GrantAllocation_GrantAllocationID', 'FK_FundSourceAllocationNote_FundSourceAllocation_FundSourceAllocationID';
execute sp_rename 'dbo.FK_GrantAllocationNote_Person_CreatedByPersonID_PersonID', 'FK_FundSourceAllocationNote_Person_CreatedByPersonID_PersonID';
execute sp_rename 'dbo.FK_GrantAllocationNote_Person_LastUpdatedByPersonID_PersonID', 'FK_FundSourceAllocationNote_Person_LastUpdatedByPersonID_PersonID';
execute sp_rename 'dbo.FK_GrantAllocationNoteInternal_GrantAllocation_GrantAllocationID', 'FK_FundSourceAllocationNoteInternal_FundSourceAllocation_FundSourceAllocationID';
execute sp_rename 'dbo.FK_GrantAllocationNoteInternal_Person_CreatedByPersonID_PersonID', 'FK_FundSourceAllocationNoteInternal_Person_CreatedByPersonID_PersonID';
execute sp_rename 'dbo.FK_GrantAllocationNoteInternal_Person_LastUpdatedByPersonID_PersonID', 'FK_FundSourceAllocationNoteInternal_Person_LastUpdatedByPersonID_PersonID';
execute sp_rename 'dbo.FK_GrantAllocationProgramIndexProjectCode_GrantAllocation_GrantAllocationID', 'FK_FundSourceAllocationProgramIndexProjectCode_FundSourceAllocation_FundSourceAllocationID';
execute sp_rename 'dbo.FK_GrantAllocationProgramIndexProjectCode_ProgramIndex_ProgramIndexID', 'FK_FundSourceAllocationProgramIndexProjectCode_ProgramIndex_ProgramIndexID';
execute sp_rename 'dbo.FK_GrantAllocationProgramIndexProjectCode_ProjectCode_ProjectCodeID', 'FK_FundSourceAllocationProgramIndexProjectCode_ProjectCode_ProjectCodeID';
execute sp_rename 'dbo.FK_GrantAllocationProgramManager_GrantAllocation_GrantAllocationID', 'FK_FundSourceAllocationProgramManager_FundSourceAllocation_FundSourceAllocationID';
execute sp_rename 'dbo.FK_GrantAllocationProgramManager_Person_PersonID', 'FK_FundSourceAllocationProgramManager_Person_PersonID';
execute sp_rename 'dbo.FK_GrantFileResource_FileResource_FileResourceID', 'FK_FundSourceFileResource_FileResource_FileResourceID';
execute sp_rename 'dbo.FK_GrantFileResource_Grant_GrantID', 'FK_FundSourceFileResource_FundSource_FundSourceID';
execute sp_rename 'dbo.FK_GrantNote_Grant_GrantID', 'FK_FundSourceNote_FundSource_FundSourceID';
execute sp_rename 'dbo.FK_GrantNote_Person_CreatedByPersonID_PersonID', 'FK_FundSourceNote_Person_CreatedByPersonID_PersonID';
execute sp_rename 'dbo.FK_GrantNote_Person_LastUpdatedByPersonID_PersonID', 'FK_FundSourceNote_Person_LastUpdatedByPersonID_PersonID';
execute sp_rename 'dbo.FK_GrantNoteInternal_Grant_GrantID', 'FK_FundSourceNoteInternal_FundSource_FundSourceID';
execute sp_rename 'dbo.FK_GrantNoteInternal_Person_CreatedByPersonID_PersonID', 'FK_FundSourceNoteInternal_Person_CreatedByPersonID_PersonID';
execute sp_rename 'dbo.FK_GrantNoteInternal_Person_LastUpdatedByPersonID_PersonID', 'FK_FundSourceNoteInternal_Person_LastUpdatedByPersonID_PersonID';
execute sp_rename 'dbo.FK_Invoice_Grant_GrantID', 'FK_Invoice_FundSource_FundSourceID';
execute sp_rename 'dbo.FK_ProjectGrantAllocationRequest_GrantAllocation_GrantAllocationID', 'FK_ProjectFundSourceAllocationRequest_FundSourceAllocation_FundSourceAllocationID';
execute sp_rename 'dbo.FK_ProjectGrantAllocationRequest_Project_ProjectID', 'FK_ProjectFundSourceAllocationRequest_Project_ProjectID';
execute sp_rename 'dbo.FK_ProjectGrantAllocationRequestUpdate_GrantAllocation_GrantAllocationID', 'FK_ProjectFundSourceAllocationRequestUpdate_FundSourceAllocation_FundSourceAllocationID';
execute sp_rename 'dbo.FK_ProjectGrantAllocationRequestUpdate_ProjectUpdateBatch_ProjectUpdateBatchID', 'FK_ProjectFundSourceAllocationRequestUpdate_ProjectUpdateBatch_ProjectUpdateBatchID';
execute sp_rename 'dbo.PK_AgreementGrantAllocation_AgreementGrantAllocationID', 'PK_AgreementFundSourceAllocation_AgreementFundSourceAllocationID';
execute sp_rename 'dbo.PK_Grant_GrantID', 'PK_FundSource_FundSourceID';
execute sp_rename 'dbo.PK_GrantAllocation_GrantAllocationID', 'PK_FundSourceAllocation_FundSourceAllocationID';
execute sp_rename 'dbo.PK_GrantAllocationBudgetLineItem_GrantAllocationBudgetLineItemID', 'PK_FundSourceAllocationBudgetLineItem_FundSourceAllocationBudgetLineItemID';
execute sp_rename 'dbo.PK_GrantAllocationChangeLog_GrantAllocationChangeLogID', 'PK_FundSourceAllocationChangeLog_FundSourceAllocationChangeLogID';
execute sp_rename 'dbo.PK_GrantAllocationExpenditure_GrantAllocationExpenditureID', 'PK_FundSourceAllocationExpenditure_FundSourceAllocationExpenditureID';
execute sp_rename 'dbo.PK_GrantAllocationExpenditureJsonStage_GrantAllocationExpenditureJsonStageID', 'PK_FundSourceAllocationExpenditureJsonStage_FundSourceAllocationExpenditureJsonStageID';
execute sp_rename 'dbo.PK_GrantAllocationFileResource_GrantAllocationFileResourceID', 'PK_FundSourceAllocationFileResource_FundSourceAllocationFileResourceID';
execute sp_rename 'dbo.PK_GrantAllocationLikelyPerson_GrantAllocationLikelyPersonID', 'PK_FundSourceAllocationLikelyPerson_FundSourceAllocationLikelyPersonID';
execute sp_rename 'dbo.PK_GrantAllocationNote_GrantAllocationNoteID', 'PK_FundSourceAllocationNote_FundSourceAllocationNoteID';
execute sp_rename 'dbo.PK_GrantAllocationNoteInternal_GrantAllocationNoteInternalID', 'PK_FundSourceAllocationNoteInternal_FundSourceAllocationNoteInternalID';
execute sp_rename 'dbo.PK_GrantAllocationPriority_GrantAllocationPriorityID', 'PK_FundSourceAllocationPriority_FundSourceAllocationPriorityID';
execute sp_rename 'dbo.PK_GrantAllocationProgramIndexProjectCode_GrantAllocationProgramIndexProjectCodeID', 'PK_FundSourceAllocationProgramIndexProjectCode_FundSourceAllocationProgramIndexProjectCodeID';
execute sp_rename 'dbo.PK_GrantAllocationProgramManager_GrantAllocationProgramManagerID', 'PK_FundSourceAllocationProgramManager_FundSourceAllocationProgramManagerID';
execute sp_rename 'dbo.PK_GrantAllocationSource_GrantAllocationSourceID', 'PK_FundSourceAllocationSource_FundSourceAllocationSourceID';
execute sp_rename 'dbo.PK_GrantFileResource_GrantFileResourceID', 'PK_FundSourceFileResource_FundSourceFileResourceID';
execute sp_rename 'dbo.PK_GrantNote_GrantNoteID', 'PK_FundSourceNote_FundSourceNoteID';
execute sp_rename 'dbo.PK_GrantNoteInternal_GrantNoteInternalID', 'PK_FundSourceNoteInternal_FundSourceNoteInternalID';
execute sp_rename 'dbo.PK_GrantStatus_GrantStatusID', 'PK_FundSourceStatus_FundSourceStatusID';
execute sp_rename 'dbo.PK_GrantType_GrantTypeID', 'PK_FundSourceType_FundSourceTypeID';
execute sp_rename 'dbo.PK_ProjectGrantAllocationRequest_ProjectGrantAllocationRequestID', 'PK_ProjectFundSourceAllocationRequest_ProjectFundSourceAllocationRequestID';
execute sp_rename 'dbo.PK_ProjectGrantAllocationRequestUpdate_ProjectGrantAllocationRequestUpdateID', 'PK_ProjectFundSourceAllocationRequestUpdate_ProjectFundSourceAllocationRequestUpdateID';







/*
EXECUTE sp_rename 'HumanResources.FK_Employee_Person_BusinessEntityID', 'FK_EmployeeID';
SELECT [name],
       SCHEMA_NAME(schema_id) AS schema_name,
       [type_desc], 
	   'execute sp_rename ''' + SCHEMA_NAME(schema_id) + '.' + [name] + ''', ''' + REPLACE([name],'Grant','FundSource') + ''';' as scriptToRun
FROM sys.objects
WHERE name like '%Grant%' and --parent_object_id = (OBJECT_ID('HumanResources.Employee')) AND 
type IN ('C', 'F', 'PK')
order by [name]


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