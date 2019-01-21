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

insert into dbo.[Grant] (GrantNumber, StartDate, EndDate, AwardedFunds)
select 
	gt.Grant_Number, 
	gt.Start_Date,
	gt.End_Date,
	gt.Funds_Awarded
from tmpGrantsFlatFile as gt

