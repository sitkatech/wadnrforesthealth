Update dbo.FieldDefinition
set FieldDefinitionDisplayName = 'adlgjhqlatnjqljbnkhadbgkljqbetgklqjbkuabdgkqnb etkhabdgkhbqketjb qkhbgkaidbgklqjet,njadbgkouqbtekqjbnek,jbgkbthquebtkljbad,kgjbqkbeobhqetyg'
where FieldDefinitionDisplayName = 'Primary Contact';

-- a field definitions
insert into dbo.FieldDefinition values (270, N'Contact', 'Contact', 'A person who is associated with a project who may or may not have an account in the system.')
insert into dbo.FieldDefinition values (271, N'ContactRelationshipType', 'Contact Relationship Type', N'<p>A categorization of a relationship between an organization and a contact, e.g. Landowner, Contractor.</p>')
insert into dbo.FieldDefinition values (272, N'Contractor', 'Contractor', 'Placeholder definition for Contractor.')
insert into dbo.FieldDefinition values (273, N'Landowner', 'Landowner', 'Placeholder definition for Landowner.')
insert into dbo.FieldDefinition values (274, N'Partner', 'Partner', 'Placeholder definition for Partner.')
insert into dbo.FieldDefinition values (275, N'PrimaryContact', 'Primary Contact', 'Placeholder definition for Primary Contact.')

Insert into FieldDefinitionData ([TenantID], [FieldDefinitionID], [FieldDefinitionDataValue], [FieldDefinitionLabel])
Select TenantID, 270, Null, Null from Tenant

Insert into FieldDefinitionData ([TenantID], [FieldDefinitionID], [FieldDefinitionDataValue], [FieldDefinitionLabel])
Select TenantID, 271, Null, Null from Tenant

Insert into FieldDefinitionData ([TenantID], [FieldDefinitionID], [FieldDefinitionDataValue], [FieldDefinitionLabel])
Select TenantID, 272, Null, Null from Tenant

Insert into FieldDefinitionData ([TenantID], [FieldDefinitionID], [FieldDefinitionDataValue], [FieldDefinitionLabel])
Select TenantID, 273, Null, Null from Tenant

Insert into FieldDefinitionData ([TenantID], [FieldDefinitionID], [FieldDefinitionDataValue], [FieldDefinitionLabel])
Select TenantID, 274, Null, Null from Tenant

Insert into FieldDefinitionData ([TenantID], [FieldDefinitionID], [FieldDefinitionDataValue], [FieldDefinitionLabel])
Select TenantID, 275, Null, Null from Tenant
GO

-- das lookup
Create Table dbo.ProjectPersonRelationshipType(
ProjectPersonRelationshipTypeID int not null constraint PK_ProjectPersonRelationshipType_ProjectPersonRelationshipTypeID primary key,
ProjectPersonRelationshipTypeName varchar(25) not null constraint AK_ProjectPersonRelationshipType_ProjectPersonRelationshipTypeName unique,
ProjectPersonRelationshipTypeDisplayName varchar(25) not null constraint AK_ProjectPersonRelationshipType_ProjectPersonRelationshipTypeDisplayName unique,
FieldDefinitionID int not null constraint FK_ProjectPersonRelationshipType_ProjectPersonRelationshipType foreign key references dbo.FieldDefinition(FieldDefinitionID),
IsRequired bit not null
)

insert into dbo.ProjectPersonRelationshipType (ProjectPersonRelationshipTypeID,ProjectPersonRelationshipTypeName,ProjectPersonRelationshipTypeDisplayName, IsRequired, FieldDefinitionID)
values
(1, 'PrimaryContact', 'Primary Contact', 1, 275),
(2,'Landowner','Landowner', 0, 273),
(3,'Contractor','Contractor', 0, 272),
(4,'Partner','Partner', 0,274)


-- das many many
CREATE TABLE dbo.ProjectPerson(
	ProjectPersonID int IDENTITY(1,1) NOT NULL,
	TenantID int NOT NULL,
	ProjectID int NOT NULL,
	PersonID int NOT NULL,
	ProjectPersonRelationshipTypeID int NOT NULL
 CONSTRAINT PK_ProjectPerson_ProjectPersonID PRIMARY KEY CLUSTERED 
(
	ProjectPersonID ASC
),
 CONSTRAINT AK_ProjectPerson_ProjectPersonID_TenantID UNIQUE NONCLUSTERED 
(
	ProjectPersonID ASC,
	TenantID ASC
)
)
GO

ALTER TABLE dbo.ProjectPerson  WITH CHECK ADD  CONSTRAINT FK_ProjectPerson_Person_PersonID FOREIGN KEY(PersonID)
REFERENCES dbo.Person (PersonID)
GO

ALTER TABLE dbo.ProjectPerson  WITH CHECK ADD  CONSTRAINT FK_ProjectPerson_Person_PersonID_TenantID FOREIGN KEY(PersonID, TenantID)
REFERENCES dbo.Person (PersonID, TenantID)
GO

ALTER TABLE dbo.ProjectPerson  WITH CHECK ADD  CONSTRAINT FK_ProjectPerson_Project_ProjectID FOREIGN KEY(ProjectID)
REFERENCES dbo.Project (ProjectID)
GO

ALTER TABLE dbo.ProjectPerson  WITH CHECK ADD  CONSTRAINT FK_ProjectPerson_Project_ProjectID_TenantID FOREIGN KEY(ProjectID, TenantID)
REFERENCES dbo.Project (ProjectID, TenantID)
GO

ALTER TABLE dbo.ProjectPerson  WITH CHECK ADD  CONSTRAINT FK_ProjectPerson_ProjectPersonRelationshipType_ProjectPersonRelationshipTypeID FOREIGN KEY(ProjectPersonRelationshipTypeID)
REFERENCES dbo.ProjectPersonRelationshipType (ProjectPersonRelationshipTypeID)
GO

ALTER TABLE dbo.ProjectPerson  WITH CHECK ADD  CONSTRAINT FK_ProjectPerson_Tenant_TenantID FOREIGN KEY(TenantID)
REFERENCES dbo.Tenant (TenantID)
GO