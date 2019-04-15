

--begin tran


--select * from dbo.ProgramIndex

-- Re-doing these AKs to consider Biennium 
alter table dbo.ProgramIndex
drop constraint AK_ProgramIndex_ProgramIndexAbbrev
GO

alter table dbo.ProgramIndex
drop constraint AK_ProgramIndex_ProgramIndexCode
GO


-- These are wired to all-null ProgramIndexes. They make no sense; unhooking them and killing them.
update GrantAllocation
set ProgramIndexID = null
where ProgramIndexID in (4,5,10)

delete from ProgramIndex where ProgramIndexTitle is null and Biennium is null

alter table dbo.ProgramIndex 
alter column ProgramIndexTitle varchar(255) not null


-- All the current entries become the current biennium
update dbo.ProgramIndex
set Biennium = 2019
where Biennium is null

alter table dbo.ProgramIndex 
alter column Biennium varchar(255) not null


-- Wishful thinking:
/*
alter table dbo.ProgramIndex 
alter column Activity varchar(255) not null

alter table dbo.ProgramIndex 
alter column Program varchar(255) not null

alter table dbo.ProgramIndex 
alter column Subprogram varchar(255) not null

alter table dbo.ProgramIndex 
alter column Subactivity varchar(255) not null
*/

-- And once those gibberish ProgramIndexes are dead, we can turn on these constraints
ALTER TABLE [dbo].[ProgramIndex] ADD  CONSTRAINT [AK_ProgramIndex_ProgramIndexCode_Biennium] UNIQUE NONCLUSTERED 
(
    ProgramIndexCode ASC,
    Biennium ASC
)
GO

ALTER TABLE [dbo].[ProgramIndex] ADD  CONSTRAINT [AK_ProgramIndex_ProgramIndexTitle_Biennium] UNIQUE NONCLUSTERED 
(
    ProgramIndexTitle ASC,
    Biennium ASC
)
GO



--rollback tran



/*
delete from ProgramIndex where ProgramIndexTitle is null and Biennium is null

select * from ProgramIndex where ProgramIndexTitle is null




*/

/*
select * from ProgramIndex where ProgramIndexCode in ('00216', '0021B','0025N')


select * from GrantAllocation as ga where ga.ProgramIndexID in (4,5,10)
select * from [Grant] where GrantID in (2,15,24)

update GrantAllocation
set ProgramIndexID = null
where ProgramIndexID in (4,5,10)
*/

