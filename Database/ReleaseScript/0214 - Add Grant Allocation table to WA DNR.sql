-- Picking this name at least in part because "grant" is a SQL keyword. 
CREATE TABLE dbo.GrantAllocation
(
    -- Just a few fields to start -- very incomplete
    GrantAllocationID int not NULL identity(1,1) Constraint PK_GrantAllocation_GrantAllocationID primary key,
    TenantID int not null constraint FK_GrantAllocation_Tenant_TenantID references dbo.Tenant(TenantID),
	GrantID int not null constraint FK_GrantAllocation_Grant_GrantID references dbo.[Grant](GrantID),
    StartDate DateTime not NULL,
    EndDate DateTime not NULL,
    AllocationAmount money null
)

insert into FieldDefinition values
(277, N'GrantAllocation', 'Grant Allocation', 'Placeholder definition for Grant Allocation.')
GO

insert into FieldDefinitionData (TenantID, FieldDefinitionID, FieldDefinitionDataValue, FieldDefinitionLabel)
select TenantID, 277, Null, Null
From dbo.Tenant
GO

insert into FirmaPageType (FirmaPageTypeID, FirmaPageTypeName, FirmaPageTypeDisplayName, FirmaPageRenderTypeID)
values
(56, 'FullGrantAllocationList', 'Full Grant Allocation List', 1)


