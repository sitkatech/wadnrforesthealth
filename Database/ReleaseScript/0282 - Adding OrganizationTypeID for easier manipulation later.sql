--ADD OrgTypeID column to temp table for easier insertion later
alter table dbo.tmpWADNROrganizations
add OrganizationTypeID int null
go

--Add tribe as an Organization Type
insert into dbo.OrganizationType(OrganizationTypeName, OrganizationTypeAbbreviation, LegendColor, ShowOnProjectMaps, IsDefaultOrganizationType, IsFundingType) VALUES
('Tribe', 'TRI', '#ffffff', 0, 0, 1)
go

--Add OrgTypeIDs to temp table
update tmp
set tmp.OrganizationTypeID = ot.OrganizationTypeID
from dbo.tmpWADNROrganizations tmp
join dbo.OrganizationType ot on tmp.OrganizationType = ot.OrganizationTypeName 
go

--Insert from temp table into Organization table
insert into dbo.Organization(OrganizationName, OrganizationShortName, IsActive, OrganizationTypeID)
select [Name], [ShortName], 1, [OrganizationTypeID]
from dbo.tmpWADNROrganizations
