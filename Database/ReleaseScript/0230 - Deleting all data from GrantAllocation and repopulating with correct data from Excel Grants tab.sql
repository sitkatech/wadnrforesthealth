/* 
	This drops all data from GrantAllocation (which was incorrectly inserting from the MasterSummary Excel tab,
	and replaces it with data from the Grants tab.
*/
delete from dbo.GrantAllocation

insert into dbo.GrantAllocation(GrantID, ProjectName, StartDate, EndDate, AllocationAmount, ProgramIndex)
select 
	gt.[GrantID], --Grant #
	tcg.Title, -- Title
	tcg.[Start Date], --Start Date
	tcg.[End Date], --End Date
	isnull(cast(REPLACE(tcg.[Funds Awarded], '"','') as money),0) as 'Funds Awarded', --Funds Awarded
	tcg.[Prog Index] --Prog Index
from dbo.tmpChildrenGrantsInGrantsTab as tcg
join dbo.[Grant] gt on tcg.[Grant #] = gt.GrantNumber