insert into dbo.GrantAllocation(GrantID, ProjectName, StartDate, EndDate, AllocationAmount, ProgramIndex)
select 
	gt.GrantID,
	gta.Grant_Project_Name, 
	gta.Start_Date,
	gta.End_Date,
	gta.Total_Award,
	gta.Program_Index_PI
from dbo.tmpGrantAllocationsFlatFile as gta
join dbo.[Grant] gt on gta.Funding_Source_or_Grant_Number = gt.GrantNumber


select * from dbo.tmpGrantAllocationsFlatFile

