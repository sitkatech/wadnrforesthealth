
alter table dbo.[Grant]
add CFDANumber varchar(10) null
go 

update dbo.[Grant]
set CFDANumber = (select top 1 tcg.[CFDA #] from tmpChildrenGrantsInGrantsTab tcg where tcg.[Grant #] = GrantNumber) 
go

select * from dbo.[Grant]
where CFDANumber is null
