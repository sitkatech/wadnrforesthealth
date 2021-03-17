INSERT [dbo].[FieldDefinition] ([FieldDefinitionID], [FieldDefinitionName], [FieldDefinitionDisplayName], [DefaultDefinition]) 
VALUES 
(476, N'ProjectGrantAllocationRequestMatchAmount', N'MatchAmount', N'<p>Funding that has been acquired for a project.</p>'),
(477, N'ProjectGrantAllocationRequestPayAmount', N'PayAmount', N'<p>Funding that has been acquired for a project.</p>')

go

insert into dbo.FieldDefinitionData(FieldDefinitionID, FieldDefinitionDataValue, FieldDefinitionLabel)
values(476,N'<p>Funding that has been acquired for a project.</p>', 'Match Amount'),
(477,N'<p>Funding that has been acquired for a project.</p>', 'Pay Amount')

go