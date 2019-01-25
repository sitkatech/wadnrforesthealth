insert into dbo.FieldDefinition values
(296, N'FederalFundCode', 'Federal Fund Code', 'Placeholder definition for Federal Fund Code description.')

insert into dbo.FieldDefinitionData (FieldDefinitionID, FieldDefinitionDataValue, FieldDefinitionLabel)
select 296, Null, Null
go

insert into dbo.FieldDefinition values
(297, N'AllocationAmount', 'Allocation Amount', 'Placeholder for GrantAllocation Allocation Amount.')

insert into dbo.FieldDefinitionData (FieldDefinitionID, FieldDefinitionDataValue, FieldDefinitionLabel)
select 297, Null, Null
go
