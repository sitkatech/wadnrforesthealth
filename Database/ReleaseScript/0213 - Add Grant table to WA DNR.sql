--begin tran

-- Picking this name at least in part because "grant" is a SQL keyword. 
CREATE TABLE dbo.GrantAllocation
(
    -- Just a few fields to start -- very incomplete
    GrantAllocationID int not NULL,
    GrantNumber varchar(30),
    StartDate DateTime not NULL,
    EndDate DateTime not NULL,
    CONSTRAINT PK_GrantAllocation_GrantAllocationID PRIMARY KEY CLUSTERED 
    (
        GrantAllocationID ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
)

insert into FieldDefinition values
(276, N'GrantAllocation', 'Grant Allocation', 'Placeholder definition for Grant Allocation.')
GO

insert into FieldDefinitionData (TenantID, FieldDefinitionID, FieldDefinitionDataValue, FieldDefinitionLabel)
select TenantID, 276, Null, Null
From dbo.Tenant
GO


--rollback tran

