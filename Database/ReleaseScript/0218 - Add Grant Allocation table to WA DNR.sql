-- Picking this name at least in part because "grant" is a SQL keyword. 
CREATE TABLE dbo.GrantAllocation
(
    -- Just a few fields to start -- very incomplete
    GrantAllocationID int not NULL identity(1,1) Constraint PK_GrantAllocation_GrantAllocationID primary key,
    TenantID int not null constraint FK_GrantAllocation_Tenant_TenantID references dbo.Tenant(TenantID),
	GrantID int not null constraint FK_GrantAllocation_Grant_GrantID references dbo.[Grant](GrantID),
	ProgramIndex nvarchar(100) NULL,
	ProjectName nvarchar(100) NULL,
    StartDate DateTime not NULL,
    EndDate DateTime not NULL,
    AllocationAmount money null,
	CostTypeID int null -- This should be a foreign key to CostType lookup table
)

insert into FieldDefinition values
(278, N'GrantAllocation', 'Grant Allocation', 'Placeholder definition for Grant Allocation.')
GO

insert into FieldDefinitionData (TenantID, FieldDefinitionID, FieldDefinitionDataValue, FieldDefinitionLabel)
select TenantID, 278, Null, Null
From dbo.Tenant
GO


