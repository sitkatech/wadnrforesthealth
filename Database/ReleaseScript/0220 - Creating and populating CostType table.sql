-- CostType Lookup table creation
CREATE TABLE dbo.CostType
(
    CostTypeID int not null identity(1,1) Constraint PK_CostType_CostTypeID primary key,
	CostTypeDescription varchar(255) not null
)
GO
go
insert into dbo.FieldDefinition values
(278, N'CostType', 'CostType', 'Placeholder definition for CostType.')

insert into dbo.FieldDefinitionData (TenantID, FieldDefinitionID, FieldDefinitionDataValue, FieldDefinitionLabel)
select t.TenantID, 278, Null, Null
From dbo.Tenant as t

INSERT INTO dbo.CostType (CostTypeDescription) VALUES
	('Indirect Costs'),
	('Supplies'),
	('Personnel and Benefits'),
	('Travel'),
	('Contracts'),
	('Agreements')
