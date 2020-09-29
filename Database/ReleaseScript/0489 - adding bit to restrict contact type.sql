alter table dbo.ProjectPersonRelationshipType ADD IsRestrictedToAdminAndProjectSteward bit

go

update dbo.ProjectPersonRelationshipType
set IsRestrictedToAdminAndProjectSteward = 0

go

alter table dbo.ProjectPersonRelationshipType ALTER COLUMN IsRestrictedToAdminAndProjectSteward bit not null
