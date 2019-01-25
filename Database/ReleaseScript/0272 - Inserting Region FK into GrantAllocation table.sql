--begin tran
--Working with flat file FuzzyMatchingOut_ForImport.csv imported into dbo.[tmpFuzzyMatchingRegionsToGrants]
--DIV is not a geographic region
delete from dbo.[tmpFuzzyMatchingRegionsToGrants]
where Region = 'div'

--Adding RegionAbbrev to the Region table
alter table dbo.Region
add RegionAbbrev nvarchar(10) null
go

update dbo.Region
set RegionAbbrev =  
case 
	when RegionName = 'Pacific Cascade' then 'PC'
	when RegionName = 'Northwest' then 'NW'
	when RegionName = 'Southeast' then 'SE'
	when RegionName = 'Northeast' then 'NE'
end
from dbo.Region
go

--Adding RegionID FK value to the GrantAllocation table
update dbo.GrantAllocation
set RegionID = r.RegionID
from dbo.tmpFuzzyMatchingRegionsToGrants as tfz
inner join dbo.Region as r on tfz.REGION = r.RegionAbbrev
inner join dbo.GrantAllocation as ga on tfz.GRANTS = ga.ProjectName
