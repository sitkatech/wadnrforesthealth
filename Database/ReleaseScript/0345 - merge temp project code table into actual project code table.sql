--select * from dbo.ProjectCode as pc

--select * from dbo.ProjectCode as pc
--full outer join dbo.tmpProjectCode as tpc on pc.ProjectCodeAbbrev = tpc.project

--select * from dbo.tmpProjectCode

--ALTER TABLE dbo.doc_exa ADD column_b VARCHAR(20) NULL, column_c INT NULL ;  

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



--SELECT tpc.project, tpc.title FROM dbo.tmpProjectCode as tpc left join dbo.ProjectCode as pc on tpc.project = pc.ProjectCodeAbbrev where pc.ProjectCodeAbbrev is null




--select * from dbo.ProgramIndex as pix
--select * from dbo.tmpProgramIndex as tpix


--select * from dbo.ProgramIndex as pix
--full outer join dbo.tmpProgramIndex as tpix on pix.ProgramIndexAbbrev = tpix.program_index
----where tpix.fy_biennium = '2019'
--order by pix.ProgramIndexID