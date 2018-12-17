alter table dbo.Person
alter column PersonGuid uniqueidentifier null

alter table dbo.Person
add MiddleName varchar(100) null

alter table dbo.Person
add StatewideVendorNumber varchar(100) null

alter table dbo.Person
add Notes varchar(500) null

alter table dbo.Person
add PersonAddress varchar(255) null

alter table dbo.Person
alter column Email varchar(255) null

alter table dbo.Person
alter column LoginName varchar(128) null

alter table dbo.Person
alter column OrganizationID int null

insert into dbo.FieldDefinition values (269, N'StatewideVendorNumber', 'Statewide Vendor Number', 'A number assigned by the State to vendors.')

Insert into FieldDefinitionData ([TenantID], [FieldDefinitionID], [FieldDefinitionDataValue], [FieldDefinitionLabel])
Select TenantID, 269, Null, Null from Tenant