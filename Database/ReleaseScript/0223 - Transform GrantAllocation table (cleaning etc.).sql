--select *
--from tmpGrantsFlatFile

/*
	Replacing Grant_Number records that start with year but as two digit, rather than four digit year.
*/



--select Grant_Total, 
--	   funds_awarded + Matching_Funds as 'calcedTotal'
--from tmpGrantsFlatFile
--where Grant_Total != ( funds_awarded + Matching_Funds) or (( Grant_Total is not null and (funds_awarded + Matching_Funds) is null))

--select * from [Grant]

--select * from tmpGrantsFlatFile

--select * from GrantAllocation

insert into dbo.GrantAllocation(TenantID, GrantID, ProjectName, StartDate, EndDate, AllocationAmount, ProgramIndex)
select 
    (select TenantID from Tenant) as TenantID,
	gt.GrantID,
	gta.Grant_Project_Name, 
	gta.Start_Date,
	gta.End_Date,
	gta.Total_Award,
	gta.Program_Index_PI
from dbo.tmpGrantAllocationsFlatFile as gta
join dbo.[Grant] gt on gta.Funding_Source_or_Grant_Number = gt.GrantNumber


select * from dbo.tmpGrantAllocationsFlatFile

