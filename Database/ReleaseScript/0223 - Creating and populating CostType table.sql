-- CostType Lookup table creation
CREATE TABLE dbo.CostType
(
    CostTypeID int not null identity(1,1) Constraint PK_CostType_CostTypeID primary key,
	CostTypeDescription varchar(255) not null
)
GO


insert into dbo.FieldDefinition values
(279, N'CostType', 'CostType', 'Placeholder definition for CostType.')


insert into dbo.FieldDefinitionData (FieldDefinitionID, FieldDefinitionDataValue, FieldDefinitionLabel) VALUES
(279, Null, Null)


INSERT INTO dbo.CostType (CostTypeDescription) VALUES
	('Indirect Costs'),
	('Supplies'),
	('Personnel and Benefits'),
	('Travel'),
	('Contracts'),
	('Agreements')

