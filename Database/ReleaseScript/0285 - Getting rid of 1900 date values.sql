--Getting rid of 1/1/1900 values that came in from empty strings on insert when null wasn't allowed
update dbo.GrantAllocation
set StartDate = NULL
where StartDate = 1/1/1900
go

update dbo.GrantAllocation
set EndDate = NULL
where EndDate = 1/1/1900
go