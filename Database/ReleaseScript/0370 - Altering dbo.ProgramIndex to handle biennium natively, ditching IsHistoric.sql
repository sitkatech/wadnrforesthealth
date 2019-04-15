
--begin tran

--alter table dbo.ProgramIndex 
--alter column Biennium varchar(255) not null

alter table dbo.ProgramIndex
add Biennium int null
GO

-- All the current entries become the current biennium
update dbo.ProgramIndex
set Biennium = 2019
where Biennium is null and IsHistoric = 0

-- These are wired to all-null ProgramIndexes. They make no sense; unhooking them and killing them.
update GrantAllocation
set ProgramIndexID = null
where ProgramIndexID in (4,5,10)

-- Now we can kill the Historic Program Indexes. These are NOT found in the feed from WA DNR! It would be best to fix them, but we'll see if that proves possible.
delete from dbo.ProgramIndex
where IsHistoric = 1 and Biennium is null


--select * from GrantAllocation where ProgramIndexID in (4,5,10)

--select * from dbo.ProgramIndex where Biennium is null

alter table dbo.ProgramIndex
drop column IsHistoric

-- These next ones are nullable to start; will need to fix this up at a later time, after first sync.
alter table dbo.ProgramIndex
add Activity varchar(200) null

alter table dbo.ProgramIndex
add Program varchar(200) null

alter table dbo.ProgramIndex
add Subprogram varchar(200) null

exec sp_rename 'dbo.ProgramIndex.ProgramIndexAbbrev', 'ProgramIndexCode'

-- Unique constraint
ALTER TABLE dbo.ProgramIndex ADD CONSTRAINT [AK_ProgramIndex_ProgramIndexCode] UNIQUE NONCLUSTERED (ProgramIndexCode ASC)
GO

--select * from dbo.ProgramIndex

--rollback tran
