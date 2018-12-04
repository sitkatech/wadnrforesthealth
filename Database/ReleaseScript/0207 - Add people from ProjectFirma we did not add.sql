declare @scottChambersPersonID int

insert into dbo.Person(TenantID, PersonGuid, FirstName, LastName, Email, Phone, PasswordPdfK2SaltHash, RoleID, CreateDate, UpdateDate, LastActivityDate, IsActive, OrganizationID, ReceiveSupportEmails, WebServiceAccessToken, LoginName)
values
(10, '3E01762E-4B04-4507-81C3-427409279C0D', 'Scott', 'Chambers', 'scott.chambers@dnr.wa.gov', NULL, NULL, 9, '2018-11-28 11:41:33.800', '2018-11-28 12:32:26.057', '2018-11-28 11:42:25.757', 1, 4704, 0, NULL, 'scha490')
select @scottChambersPersonID = SCOPE_IDENTITY()

insert into dbo.Person(TenantID, PersonGuid, FirstName, LastName, Email, Phone, PasswordPdfK2SaltHash, RoleID, CreateDate, UpdateDate, LastActivityDate, IsActive, OrganizationID, ReceiveSupportEmails, WebServiceAccessToken, LoginName)
values
(10, '34A400E3-C4CC-435D-A0ED-3FDB6B024386', 'Jeanne', 'Christensen', 'jeanne.christensen@dnr.wa.gov', NULL, NULL, 2, '2018-11-28 11:49:48.767', '2018-11-28 15:37:43.657', '2018-11-28 15:42:08.883', 1, 4704, 0, NULL, 'jchs490')


insert into PersonStewardGeoSpatialArea(TenantID, PersonID, GeoSpatialAreaID)
values
(10, @scottChambersPersonID, 7519)