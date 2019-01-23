--begin tran
create table dbo.FederalFundCode
(
	FederalFundCodeID int not null identity(1,1) constraint PK_FederalFundCode_FederalFundCodeID primary key,
	FederalFundCodeAbbrev varchar(10) null
)
go

insert into dbo.FederalFundCode
	select distinct [Federal Fund Code]
from tmpChildrenGrantsInGrantsTab
where [Federal Fund Code] <> ''

select * from dbo.FederalFundCode
--rollback tran