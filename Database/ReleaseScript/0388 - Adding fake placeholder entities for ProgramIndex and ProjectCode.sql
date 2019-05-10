-- Fake placeholder ProgramIndex
insert into ProgramIndex (ProgramIndexCode, ProgramIndexTitle, Biennium, Activity, Program, Subprogram, Subactivity)
values
('000', '000-FAKE Sitka placeholder ProgramIndex - replace with real one', 2017, null, null, null, null)
GO

-- Temp variables for easy access
declare @fakeProgramIndexID int
set @fakeProgramIndexID = (select ProgramIndexID from dbo.ProgramIndex where ProgramIndexCode = '000')
--select @fakeProgramIndexID

-- Set all the null holes to use the Fake ProgramIndex.
update dbo.GrantAllocation
set ProgramIndexID = @fakeProgramIndexID
where ProgramIndexID is null

--select * from dbo.GrantAllocation as ga
--where ga.ProgramIndexID is null

-- Make new GrantAllocationProjectCodeProgramIndex table
----------------------------------------------------------

CREATE TABLE dbo.GrantAllocationProgramIndexProjectCode
(
    GrantAllocationProgramIndexProjectCodeID [int] IDENTITY(1,1) NOT NULL,
    GrantAllocationID [int] NOT NULL,
    ProgramIndexID int not null,
    ProjectCodeID int null
 CONSTRAINT [PK_GrantAllocationProgramIndexProjectCode_GrantAllocationProgramIndexProjectCodeID] PRIMARY KEY CLUSTERED 
(
    GrantAllocationProgramIndexProjectCodeID ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_GrantAllocationProjectCode_GrantAllocationID_ProgramIndexID_ProjectCodeID] UNIQUE NONCLUSTERED 
(
    GrantAllocationID ASC,
    ProgramIndexID ASC,
    ProjectCodeID ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- FK GrantAllocation
ALTER TABLE [dbo].GrantAllocationProgramIndexProjectCode  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationProgramIndexProjectCode_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO

-- FK ProgramIndex
ALTER TABLE [dbo].GrantAllocationProgramIndexProjectCode  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationProgramIndexProjectCode_ProgramIndex_ProgramIndexID] FOREIGN KEY([ProgramIndexID])
REFERENCES [dbo].[ProgramIndex] ([ProgramIndexID])
GO

-- FK ProjectCode
ALTER TABLE [dbo].GrantAllocationProgramIndexProjectCode  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationProgramIndexProjectCode_ProjectCode_ProjectCodeID] FOREIGN KEY([ProjectCodeID])
REFERENCES [dbo].[ProjectCode] ([ProjectCodeID])
GO

-- Fill things in
-----------------

-- Build PI/PC table entries
----------------------------

-- GAPIPC Non-null entries
insert into dbo.GrantAllocationProgramIndexProjectCode
select distinct
ga.GrantAllocationID, pri.ProgramIndexID, pc.ProjectCodeID --,   g.GrantNumber, ga.GrantAllocationName, pri.ProgramIndexCode, pc.ProjectCodeName, * 
from dbo.GrantAllocation as ga
join dbo.[Grant] g on ga.GrantID = g.GrantID
join dbo.ProgramIndex pri on pri.ProgramIndexID = ga.ProgramIndexID
join dbo.GrantAllocationProjectCode gapc on ga.GrantAllocationID = gapc.GrantAllocationID
join dbo.ProjectCode pc on pc.ProjectCodeID = gapc.ProjectCodeID
--where pri.ProgramIndexCode != '000' --and pc.ProjectCodeName != 'FAKE'
--order by  g.GrantNumber, ga.GrantAllocationName, pri.ProgramIndexCode, pc.ProjectCodeName
order by pri.ProgramIndexID, pc.ProjectCodeID
GO

--select * from dbo.GrantAllocationProgramIndexProjectCode
--where ProjectCodeID is null

--select * from dbo.GrantAllocation as ga
--where ga.pro

--select * from dbo.GrantAllocationProjectCode
--where ProjectCodeID is null


-- Allow Project Code to be null
--------------------------------

-- GAPIPC with null ProjectCodeIDs
insert into dbo.GrantAllocationProgramIndexProjectCode
select ga.GrantAllocationID, pri.ProgramIndexID, pc.ProjectCodeID
from dbo.GrantAllocation as ga
join dbo.[Grant] g on ga.GrantID = g.GrantID
left join dbo.ProgramIndex pri on pri.ProgramIndexID = ga.ProgramIndexID
left join dbo.GrantAllocationProjectCode gapc on ga.GrantAllocationID = gapc.GrantAllocationID
left join dbo.ProjectCode pc on pc.ProjectCodeID = gapc.ProjectCodeID
where gapc.GrantAllocationProjectCodeID is null 





-- Notes from past efforts



/*
select * from dbo.ProgramIndexProjectCode

select * from dbo.GrantAllocationProgramIndexProjectCode
insert into dbo.GrantAllocationProgramIndexProjectCode

select gapc.*
from dbo.GrantAllocationProjectCode as gapc

select * from dbo.GrantAllocationProjectCode


-- All, no holes
select distinct
pri.ProgramIndexID, pc.ProjectCodeID --   ga.GrantAllocationID,   g.GrantNumber, ga.GrantAllocationName, pri.ProgramIndexCode, pc.ProjectCodeName, * 
from dbo.GrantAllocation as ga
join dbo.[Grant] g on ga.GrantID = g.GrantID
join dbo.ProgramIndex pri on pri.ProgramIndexID = ga.ProgramIndexID
join dbo.GrantAllocationProjectCode gapc on ga.GrantAllocationID = gapc.GrantAllocationID
join dbo.ProjectCode pc on pc.ProjectCodeID = gapc.ProjectCodeID
--order by  g.GrantNumber, ga.GrantAllocationName, pri.ProgramIndexCode, pc.ProjectCodeName
order by pri.ProgramIndexID, pc.ProjectCodeID


-- Hack attempt to get to PI/PC ID

-- All w/o holes
select 
    ga.GrantAllocationID,
    pipc.ProgramIndexProjectCodeID
from dbo.GrantAllocation as ga
join dbo.[Grant] g on ga.GrantID = g.GrantID
join dbo.ProgramIndex pri on pri.ProgramIndexID = ga.ProgramIndexID
join dbo.GrantAllocationProjectCode gapc on ga.GrantAllocationID = gapc.GrantAllocationID
join dbo.ProjectCode pc on pc.ProjectCodeID = gapc.ProjectCodeID
join dbo.ProgramIndexProjectCode as pipc on pipc.ProgramIndexID = pri.ProgramIndexID and pipc.ProjectCodeID = pc.ProjectCodeID
order by  g.GrantNumber, ga.GrantAllocationName, pri.ProgramIndexCode, pc.ProjectCodeName





select * from dbo.ProgramIndexProjectCode


-- All w/o holes
select g.GrantNumber, ga.GrantAllocationName, pri.ProgramIndexCode, pc.ProjectCodeName, * 
from dbo.GrantAllocation as ga
join dbo.[Grant] g on ga.GrantID = g.GrantID
join dbo.ProgramIndex pri on pri.ProgramIndexID = ga.ProgramIndexID
join dbo.GrantAllocationProjectCode gapc on ga.GrantAllocationID = gapc.GrantAllocationID
join dbo.ProjectCode pc on pc.ProjectCodeID = gapc.ProjectCodeID
order by  g.GrantNumber, ga.GrantAllocationName, pri.ProgramIndexCode, pc.ProjectCodeName
*/



/*

-- All w/o holes
select g.GrantNumber, ga.GrantAllocationName, pri.ProgramIndexCode, pc.ProjectCodeName, * from
dbo.GrantAllocation ga
join dbo.[Grant] g on ga.GrantID = g.GrantID
join dbo.ProgramIndex pri on pri.ProgramIndexID = ga.ProgramIndexID
join dbo.GrantAllocationProjectCode gapc on ga.GrantAllocationID = gapc.GrantAllocationID
join dbo.ProjectCode pc on pc.ProjectCodeID = gapc.ProjectCodeID
order by  g.GrantNumber, ga.GrantAllocationName, pri.ProgramIndexCode, pc.ProjectCodeName




-- Shows holes (if any)

select g.GrantNumber, ga.GrantAllocationName, pri.ProgramIndexCode, pc.ProjectCodeName, * from
dbo.GrantAllocation ga
join dbo.[Grant] g on ga.GrantID = g.GrantID
left join dbo.ProgramIndex pri on pri.ProgramIndexID = ga.ProgramIndexID
left join dbo.GrantAllocationProjectCode gapc on ga.GrantAllocationID = gapc.GrantAllocationID
left join dbo.ProjectCode pc on pc.ProjectCodeID = gapc.ProjectCodeID
where (gapc.GrantAllocationProjectCodeID is null and ga.ProgramIndexID is not null)
or (gapc.GrantAllocationProjectCodeID is not null and ga.ProgramIndexID is null)
or (gapc.GrantAllocationProjectCodeID is null and ga.ProgramIndexID is null)
order by  g.GrantNumber, ga.GrantAllocationName, pri.ProgramIndexCode, pc.ProjectCodeName


select * from GrantAllocation

select * from GrantAllocationProjectCode

*/

