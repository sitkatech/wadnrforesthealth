
-- Fake placeholder ProgramIndex
insert into ProgramIndex (ProgramIndexCode, ProgramIndexTitle, Biennium, Activity, Program, Subprogram, Subactivity)
values
('000', '000-FAKE Sitka placeholder ProgramIndex - replace with real one', 2017, null, null, null, null)
GO

-- Fake placeholder ProjectCode
insert into ProjectCode (ProjectCodeName, ProjectCodeTitle, CreateDate, ProjectStartDate, ProjectEndDate)
values
('FAKE', 'FAKE Sitka placeholder ProjectCode', null, null, null)
GO

-- Temp variables for easy access
declare @fakeProgramIndexID int
set @fakeProgramIndexID = (select ProgramIndexID from dbo.ProgramIndex where ProgramIndexCode = '000')
--select @fakeProgramIndexID

declare @fakeProjectCodeID int
set @fakeProjectCodeID = (select projectCodeID from dbo.projectCode where ProjectCodeName = 'FAKE')
--select @fakeProjectCodeID


-- Set all the null holes to use the Fake ProgramIndex.
update dbo.GrantAllocation
set ProgramIndexID = @fakeProgramIndexID
where GrantAllocationID in 
(
    select ga.GrantAllocationID 
         --ga.ProgramIndexID
    from dbo.GrantAllocation as ga
    join dbo.[Grant] g on ga.GrantID = g.GrantID
    left join dbo.ProgramIndex pri on pri.ProgramIndexID = ga.ProgramIndexID
    left join dbo.GrantAllocationProjectCode gapc on ga.GrantAllocationID = gapc.GrantAllocationID
    left join dbo.ProjectCode pc on pc.ProjectCodeID = gapc.ProjectCodeID
    where (gapc.GrantAllocationProjectCodeID is null and ga.ProgramIndexID is not null)
    or (gapc.GrantAllocationProjectCodeID is not null and ga.ProgramIndexID is null)
    or (gapc.GrantAllocationProjectCodeID is null and ga.ProgramIndexID is null)
    --order by  g.GrantNumber, ga.GrantAllocationName, pri.ProgramIndexCode, pc.ProjectCodeName
)

-- Insert fake pairs for all the missing GrantAllocation ProjectCodes
insert into dbo.GrantAllocationProjectCode
select ga.GrantAllocationID, @fakeProjectCodeID
from dbo.GrantAllocation ga
join dbo.[Grant] g on ga.GrantID = g.GrantID
left join dbo.ProgramIndex pri on pri.ProgramIndexID = ga.ProgramIndexID
left join dbo.GrantAllocationProjectCode gapc on ga.GrantAllocationID = gapc.GrantAllocationID
left join dbo.ProjectCode pc on pc.ProjectCodeID = gapc.ProjectCodeID
where (gapc.GrantAllocationProjectCodeID is null and ga.ProgramIndexID is not null)
or (gapc.GrantAllocationProjectCodeID is not null and ga.ProgramIndexID is null)
or (gapc.GrantAllocationProjectCodeID is null and ga.ProgramIndexID is null)
order by  g.GrantNumber, ga.GrantAllocationName, pri.ProgramIndexCode, pc.ProjectCodeName




/*

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

