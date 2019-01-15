-- CostType Lookup table creation
CREATE TABLE dbo.CostType
(
    CostTypeID int not null identity(1,1) Constraint PK_CostType_CostTypeID primary key,
    TenantID int not null constraint FK_CostType_Tenant_TenantID references dbo.Tenant(TenantID),
	CostTypeDescription varchar(255) not null
)
GO


insert into dbo.FieldDefinition values
(279, N'CostType', 'CostType', 'Placeholder definition for CostType.')

declare @tenantID int
set @tenantID = (select TenantID from dbo.Tenant)

insert into dbo.FieldDefinitionData (TenantID, FieldDefinitionID, FieldDefinitionDataValue, FieldDefinitionLabel) VALUES
(@tenantID, 279, Null, Null)


INSERT INTO dbo.CostType (TenantID, CostTypeDescription) VALUES
	(@tenantID, 'Indirect Costs'),
	(@tenantID, 'Supplies'),
	(@tenantID, 'Personnel and Benefits'),
	(@tenantID, 'Travel'),
	(@tenantID, 'Contracts'),
	(@tenantID, 'Agreements')

