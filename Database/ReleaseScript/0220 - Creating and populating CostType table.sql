-- CostType Lookup table creation
CREATE TABLE dbo.CostType
(
    CostTypeID int not null identity(1,1) Constraint PK_CostType_CostTypeID primary key,
	CostTypeDescription varchar(255) not null
)
GO
INSERT INTO dbo.CostType (CostTypeDescription) VALUES
	('Indirect Costs'),
	('Supplies'),
	('Personnel and Benefits'),
	('Travel'),
	('Contracts'),
	('Agreements')
