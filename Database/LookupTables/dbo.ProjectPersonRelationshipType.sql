delete from dbo.ProjectPersonRelationshipType


insert into dbo.ProjectPersonRelationshipType (ProjectPersonRelationshipTypeID,ProjectPersonRelationshipTypeName,ProjectPersonRelationshipTypeDisplayName, IsRequired, FieldDefinitionID, IsRestrictedToAdminAndProjectSteward, SortOrder)
values
(1, 'PrimaryContact', 'Primary Contact', 0, 275,0, 10),
(2,'PrivateLandowner','Private Landowner', 0, 273,1, 30),
(3,'Contractor','Contractor', 0, 272,0, 20),
(4,'Coordinator','Coordinator', 0, 507,0, 40)
