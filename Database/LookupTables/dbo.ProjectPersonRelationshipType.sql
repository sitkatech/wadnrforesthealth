delete from dbo.ProjectPersonRelationshipType


insert into dbo.ProjectPersonRelationshipType (ProjectPersonRelationshipTypeID,ProjectPersonRelationshipTypeName,ProjectPersonRelationshipTypeDisplayName, IsRequired, FieldDefinitionID)
values
(1, 'PrimaryContact', 'Primary Contact', 1, 275),
(2,'Landowner','Landowner', 0, 273),
(3,'Contractor','Contractor', 0, 272)
