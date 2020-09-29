delete from dbo.ProjectPersonRelationshipType


insert into dbo.ProjectPersonRelationshipType (ProjectPersonRelationshipTypeID,ProjectPersonRelationshipTypeName,ProjectPersonRelationshipTypeDisplayName, IsRequired, FieldDefinitionID, IsRestrictedToAdminAndProjectSteward)
values
(1, 'PrimaryContact', 'Primary Contact', 1, 275,0),
(2,'PrivateLandowner','Private Landowner', 0, 273,1),
(3,'Contractor','Contractor', 0, 272,0)
