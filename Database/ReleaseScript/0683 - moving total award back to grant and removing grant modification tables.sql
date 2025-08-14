




alter table dbo.[Grant]
add TotalAwardAmount money null
go

update dbo.[Grant] set TotalAwardAmount = 0;

select GrantID, sum(GrantModificationAmount) as TotalAwardAmount into #tempGrantTotals from dbo.GrantModification group by GrantID
update tg set tg.TotalAwardAmount = tgt.TotalAwardAmount from dbo.[Grant] as tg join #tempGrantTotals tgt on tg.GrantID = tgt.GrantID


alter table dbo.[Grant]
alter column TotalAwardAmount money not null



alter table dbo.GrantAllocation
add GrantID int null constraint FK_GrantAllocation_Grant_GrantID references dbo.[Grant](GrantID);
go

update ga set ga.GrantID = gm.GrantID from dbo.GrantAllocation as ga join dbo.GrantModification gm on ga.GrantModificationID = gm.GrantModificationID

alter table dbo.GrantAllocation
alter column GrantID int not null;

alter table dbo.GrantAllocation drop constraint FK_GrantAllocation_GrantModification_GrantModificationID
alter table dbo.GrantAllocation drop column GrantModificationID

drop table dbo.GrantModificationGrantModificationPurpose

drop table dbo.GrantModificationNoteInternal
drop table dbo.GrantModificationFileResource

drop table dbo.GrantModification
drop table dbo.GrantModificationPurpose
drop table dbo.GrantModificationStatus


delete from dbo.FieldDefinitionData where FieldDefinitionID in (347, 348, 349, 350, 351, 352, 353, 354, 355)
