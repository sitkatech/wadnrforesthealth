--begin tran



--insert into ProgramIndex (ProgramIndexAbbrev, ProgramIndexTitle, IsHistoric)
--values 
--('216', '216-TRAINING 2014 CPG', 1),
--('21B', '21B-TRAINING 2015 CPG', 1),
--('25N', '25N-2011 CPG WIND AND WHITE SALMON RIVER', 1)



alter table dbo.ProgramIndex
add Biennium int null
GO

-- This is a hack, the ProgramIndexTitle will not be right for these 3 records quite yet.
-- We will handle this by re-exporting the whole table as it looks after a sync, in a 
-- later change script  (SLG)
update dbo.ProgramIndex 
set Biennium = 2017,
    ProgramIndexTitle = ProgramIndexAbbrev
where IsHistoric = 1

-- All the current entries become the current biennium
update dbo.ProgramIndex
set Biennium = 2019
where Biennium is null and IsHistoric = 0



/*
ProgramIndexID	ProgramIndexCode	ProgramIndexTitle	Biennium	Activity	Program	Subprogram	Subactivity
603	00216	216-TRAINING 2014 CPG	2017	01	020	01	06
608	0021B	21B-TRAINING 2015 CPG	2017	01	020	01	07
653	0025N	25N-2011 CPG WIND AND WHITE SALMON RIVER	2017	03	020	05	06
*/

-- These are wired to all-null ProgramIndexes. They make no sense; unhooking them and killing them.
--update GrantAllocation
--set ProgramIndexID = null
--where ProgramIndexID in (4,5,10)

-- Now we can kill the Historic Program Indexes. These are NOT found in the feed from WA DNR! It would be best to fix them, but we'll see if that proves possible.
--delete from dbo.ProgramIndex
--where IsHistoric = 1 and Biennium is null




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
