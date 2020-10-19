--begin tran

alter table dbo.ProjectType
add LimitVisibilityToAdmin bit null
GO

update dbo.ProjectType
set LimitVisibilityToAdmin = 0
GO

update dbo.ProjectType
set LimitVisibilityToAdmin = 1
where ProjectTypeID = 2222
GO

alter table dbo.ProjectType
alter column LimitVisibilityToAdmin bit not null
GO

--rollback tran