--select *
--from tmpGrantsFlatFile

/*
	Replacing Grant_Number records that start with year but as two digit, rather than four digit year.
*/

update tmpGrantsFlatFile
set Grant_Number = '2016-DG-11062765-717'
where Grant_Number = '16-DG-11062765-717'


update tmpGrantsFlatFile
set Grant_Number = '2016-DG-11062765-710'
where Grant_Number = '16-DG-11062765-710'


update tmpGrantsFlatFile
set Grant_Number = '2016-DG-11062765-718'
where Grant_Number = '16-DG-11062765-718'

update tmpGrantsFlatFile
set Grant_Number = '2016-DG-11062765-731'
where Grant_Number = '16-DG-11062765-731'


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

