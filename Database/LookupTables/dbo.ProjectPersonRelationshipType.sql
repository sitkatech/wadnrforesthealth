delete from dbo.ProjectPersonRelationshipType

insert into dbo.ProjectPersonRelationshipType (ProjectPersonRelationshipTypeID,ProjectPersonRelationshipTypeName,ProjectPersonRelationshipTypeDisplayName, IsRequired)
values
(1, 'PrimaryContact', 'Primary Contact', 1),
(2,'Landowner','Landowner', 0),
(3,'Contractor','Contractor', 0),
(4,'Partner','Partner', 0)
