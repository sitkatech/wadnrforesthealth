ALTER TABLE dbo.ProjectCustomAttributeType ADD ApplyToAllProjectTypes bit
go

update dbo.ProjectCustomAttributeType
set ApplyToAllProjectTypes = 1
from dbo.ProjectCustomAttributeType
go

ALTER TABLE dbo.ProjectCustomAttributeType ALTER COLUMN ApplyToAllProjectTypes bit not null
go