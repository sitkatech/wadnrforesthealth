




alter table dbo.[Grant]
add TotalAwardAmount money null
go

update dbo.[Grant] set TotalAwardAmount = 0;

select GrantID, sum(GrantModificationAmount) as TotalAwardAmount into #tempGrantTotals from dbo.GrantModification group by GrantID
update tg set tg.TotalAwardAmount = tgt.TotalAwardAmount from dbo.[Grant] as tg join #tempGrantTotals tgt on tg.GrantID = tgt.GrantID


alter table dbo.[Grant]
alter column TotalAwardAmount money not null
