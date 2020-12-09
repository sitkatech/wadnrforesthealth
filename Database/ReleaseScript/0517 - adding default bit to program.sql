ALTER TABLE dbo.Program ADD IsDefaultProgramForImportOnly bit 

go

update  dbo.Program
set IsDefaultProgramForImportOnly = 0

go

ALTER TABLE dbo.Program ALTER COLUMN IsDefaultProgramForImportOnly bit not null

ALTER TABLE dbo.Program ALTER COLUMN ProgramName varchar(200)  null

ALTER TABLE dbo.Program ALTER COLUMN ProgramShortName varchar(200)  null

go

update dbo.Program
set IsDefaultProgramForImportOnly = 1,
    ProgramName = null,
    ProgramShortName = null
where ProgramID in (2,4,6,7)
