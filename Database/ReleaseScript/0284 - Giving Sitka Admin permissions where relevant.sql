

declare @SitkaOrgID int
set @SitkaOrgID = 4702

update Person
set OrganizationID = @SitkaOrgID
where FirstName = 'Stewart' and LastName = 'Loving-Gibbard'

update Person
set OrganizationID = @SitkaOrgID
where FirstName = 'Michael' and LastName = 'Jolliffe'

update Person
set OrganizationID = @SitkaOrgID
where FirstName = 'John' and LastName = 'Vivio'

update Person
set OrganizationID = @SitkaOrgID
where FirstName = 'Dal' and LastName = 'Marsters'

-- That's all the people I can think of that weren't already added on our side, at least
-- as of now. -- SLG 01/29/2019.

-- Set all Sitka to SitkaAdmins (at least for now)
update Person
set RoleID = 8
where OrganizationID = @SitkaOrgID

--select * from Person
--where
--RoleID != 8

--select * from Organization

