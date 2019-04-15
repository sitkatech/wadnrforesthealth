alter table dbo.ProjectCode
add CreateDate datetime null
GO

alter table dbo.ProjectCode
add ProjectStartDate datetime null
GO

alter table dbo.ProjectCode
add ProjectEndDate datetime null
GO

exec sp_rename "dbo.ProjectCode.ProjectCodeAbbrev", "ProjectCodeName"
GO

-- It was well intentioned to remove leading zeros, but it just makes merges
-- harder.
update dbo.ProjectCode
set ProjectCodeName = '0' + ProjectCodeName

--select * from dbo.ProjectCode

