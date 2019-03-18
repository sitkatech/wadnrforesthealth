--add title field to ProjectCode
alter table dbo.ProjectCode add
ProjectCodeTitle varchar(255) null;

ALTER TABLE dbo.ProjectCode ADD CONSTRAINT AK_ProjectCode_ProjectCodeAbbrev UNIQUE(ProjectCodeAbbrev)

GO

--update existing records with the title field
UPDATE
    dbo.ProjectCode
SET
    dbo.ProjectCode.ProjectCodeTitle = tpc.title
FROM
    dbo.ProjectCode pc
INNER JOIN
    dbo.tmpProjectCode tpc
ON 
    pc.ProjectCodeAbbrev = tpc.project;


--add missing records
INSERT INTO dbo.ProjectCode (ProjectCodeAbbrev, ProjectCodeTitle) 
	SELECT tpc.project, tpc.title 
	FROM dbo.tmpProjectCode as tpc 
	left join dbo.ProjectCode as pc 
	on tpc.project = pc.ProjectCodeAbbrev 
	where pc.ProjectCodeAbbrev is null

--select * from dbo.tmpProjectCode