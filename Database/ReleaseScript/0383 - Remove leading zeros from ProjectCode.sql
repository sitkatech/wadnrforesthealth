update dbo.ProjectCode
set ProjectCodeName = dbo.fRemoveLeadingZeroes(ProjectCodeName)
