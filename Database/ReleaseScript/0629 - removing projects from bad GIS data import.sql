


delete from dbo.ProjectOrganization
where ProjectID in (select ProjectID from dbo.Project where CreateGisUploadAttemptID = 116)

delete from dbo.Treatment
where ProjectID in (select ProjectID from dbo.Project where CreateGisUploadAttemptID = 116)

delete from dbo.ProjectLocation
where ProjectID in (select ProjectID from dbo.Project where CreateGisUploadAttemptID = 116)

delete from dbo.ProjectRegion
where ProjectID in (select ProjectID from dbo.Project where CreateGisUploadAttemptID = 116)

delete from dbo.ProjectPriorityLandscape
where ProjectID in (select ProjectID from dbo.Project where CreateGisUploadAttemptID = 116)

delete from dbo.ProjectProgram
where ProjectID in (select ProjectID from dbo.Project where CreateGisUploadAttemptID = 116)

delete from dbo.ProjectExternalLink
where ProjectID in (select ProjectID from dbo.Project where CreateGisUploadAttemptID = 116)

update dbo.ProjectImportBlockList
set ProjectID = null
where ProjectID in (select ProjectID from dbo.Project where CreateGisUploadAttemptID = 116)

delete from dbo.Project
where CreateGisUploadAttemptID = 116



update dbo.GisUploadSourceOrganization
set GisUploadProgramMergeGroupingID = 1
where GisUploadSourceOrganizationID = 2




/*

GisUploadAttemptID to delete = 116
GisUploadSourceOrg id for USFS = 2

select * from dbo.GisUploadSourceOrganization

*/