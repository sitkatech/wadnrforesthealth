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

--Insert WADNR orgs from temp table into Organization table
insert into dbo.Organization(OrganizationName, OrganizationShortName, IsActive, OrganizationTypeID)
select [Name], [ShortName], 1, [OrganizationTypeID]
from dbo.tmpWADNROrganizations

-- remove duplicate orgs by OrganizationName
delete dbo.Organization
from dbo.Organization o
join 
(
	select min(o.OrganizationID) as OrganizationIDToKeep, o.OrganizationName
	from dbo.Organization o
	group by o.OrganizationName
	having count(*) > 1
) x on o.OrganizationName = x.OrganizationName and o.OrganizationID != x.OrganizationIDToKeep

-- remove duplicate orgs by OrganizationShortName
delete dbo.Organization
from dbo.Organization o
join 
(
	select min(o.OrganizationID) as OrganizationIDToKeep, o.OrganizationShortName
	from dbo.Organization o
	group by o.OrganizationShortName
	having count(*) > 1
) x on o.OrganizationShortName = x.OrganizationShortName and o.OrganizationID != x.OrganizationIDToKeep


alter table dbo.Organization add constraint AK_Organization_OrganizationName UNIQUE (OrganizationName)
alter table dbo.Organization add constraint AK_Organization_OrganizationShortName UNIQUE (OrganizationShortName)


----select * from dbo.Person
----select * from dbo.ProjectOrganization
----select * from dbo.FundingSource
----select * from dbo.Organization
