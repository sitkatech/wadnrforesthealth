--begin tran

-- Socrata-datamart-app-token

alter table dbo.SystemAttribute
add SocrataAppToken varchar(200) null
GO

update dbo.SystemAttribute
set SocrataAppToken =  '8ZsE5VWbIdVO9ihhG96247h51'
GO

alter table dbo.SystemAttribute
alter column SocrataAppToken varchar(200) not null
GO

--select * from dbo.SystemAttribute

--rollback tran

