ALTER TABLE dbo.[Grant] ADD ShortName varchar(64)

go

exec sp_rename 'dbo.Grant.GrantTitle', 'GrantName', 'COLUMN';  