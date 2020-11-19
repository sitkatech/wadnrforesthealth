
insert into dbo.FieldDefinition([FieldDefinitionID], [FieldDefinitionName], [FieldDefinitionDisplayName], [DefaultDefinition]) values (475, N'Program', N'Program', 'Program')

go

insert into dbo.FieldDefinitionData(FieldDefinitionID, FieldDefinitionDataValue, FieldDefinitionLabel)
values (475, N'Program', 'Program')