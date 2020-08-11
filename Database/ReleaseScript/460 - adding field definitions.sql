INSERT [dbo].[FieldDefinition] ([FieldDefinitionID], [FieldDefinitionName], [FieldDefinitionDisplayName], [DefaultDefinition]) 
VALUES 

(465, N'ProjectIdentifier', 'Project Identifier', N'<p>Project Identifier for mapping to GIS source data</p>'),
(466, N'PlannedDate', 'Planned Date', N'<p>The date work is planned to start for a Project</p>'),
(467, N'TreatedAcres', 'Treated Acres', N'<p>The amount of acres completed under a treatment</p>')


go

INSERT [dbo].[FieldDefinitionData] ([FieldDefinitionID], FieldDefinitionDataValue, FieldDefinitionLabel ) 
VALUES 
(465, N'<p>Project Identifier for mapping to GIS source data</p>', 'Project Identifier'),
(466, N'<p>The date work is planned to start for a Project</p>', 'Planned Date'),
(467, N'<p>The amount of acres completed under a treatment</p>',  'Treated Acres')