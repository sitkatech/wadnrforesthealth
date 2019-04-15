--select * from dbo.ProgramIndex

alter table dbo.ProgramIndex
drop column IsHistoric

-- These next ones are nullable to start; will need to fix this up at a later time, after first sync.
alter table dbo.ProgramIndex
add Activity varchar(200) null

alter table dbo.ProgramIndex
add Biennium int null

alter table dbo.ProgramIndex
add Program varchar(200) null

alter table dbo.ProgramIndex
add Subprogram varchar(200) null

exec sp_rename 'dbo.ProgramIndex.ProgramIndexAbbrev', 'ProgramIndexCode'

-- Unique constraint
ALTER TABLE dbo.ProgramIndex ADD CONSTRAINT [AK_ProgramIndex_ProgramIndexCode] UNIQUE NONCLUSTERED (ProgramIndexCode ASC)
GO

--select * from dbo.ProgramIndex
