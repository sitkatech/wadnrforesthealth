alter table dbo.Person
alter column PersonGuid uniqueidentifier null

alter table dbo.Person
add MiddleName varchar(100) null

alter table dbo.Person
add StatewideVendorNumber varchar(100) null

alter table dbo.Person
add Notes varchar(max) null

alter table dbo.Person
alter column Email varchar(255) null