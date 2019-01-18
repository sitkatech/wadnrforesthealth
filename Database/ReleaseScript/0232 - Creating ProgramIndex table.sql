-- CostType Lookup table creation
CREATE TABLE dbo.ProgramIndex
(
    ProgramIndexID int not null identity(1,1) Constraint PK_ProgramIndex_ProgramIndexID primary key,
	ProgramIndexAbbrev varchar(255) not null
)
GO


insert into dbo.FieldDefinition values
(282, N'ProgramIndex', 'Program Index', 'Placeholder definition for Program Index.')


insert into dbo.FieldDefinitionData (FieldDefinitionID, FieldDefinitionDataValue, FieldDefinitionLabel) VALUES
(282, Null, Null)


INSERT INTO dbo.ProgramIndex (ProgramIndexAbbrev)
select distinct [Prog Index] 
from tmpChildrenGrantsInGrantsTab 
where [Prog Index] <> ''