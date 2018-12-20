--begin tran

-- Picking this name at least in part because "grant" is a SQL keyword. 
CREATE TABLE dbo.GrantAllocation
(
    -- Just a few fields to start -- very incomplete
    GrantAllocationID int not NULL identity(1,1) Constraint PK_GrantAllocation_GrantAllocationID primary key,
	TenantID int not null constraint FK_GrantAllocation_Tenant_TenantID references dbo.Tenant(TenantID),
    GrantNumber varchar(30) null,
    StartDate DateTime not NULL,
    EndDate DateTime not NULL
)

insert into FieldDefinition values
(276, N'GrantAllocation', 'Grant Allocation', 'Placeholder definition for Grant Allocation.')
GO

insert into FieldDefinitionData (TenantID, FieldDefinitionID, FieldDefinitionDataValue, FieldDefinitionLabel)
select TenantID, 276, Null, Null
From dbo.Tenant
GO


--rollback tran

