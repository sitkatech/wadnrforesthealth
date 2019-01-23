ALTER TABLE dbo.[Grant] ADD GrantTitle varchar(64)

go




update dbo.[Grant]
set GrantTitle = 'Non-Federal Wildland Urban Interface (WUI)'
from dbo.[Grant] where GrantID = 35

go

update dbo.[Grant]
set GrantTitle = Title
from dbo.[Grant]
join dbo.tmpGrantsFlatFile on Grant_Number = GrantNumber

go

delete from dbo.[Grant] where GrantTitle is null

go

ALTER TABLE dbo.[Grant] ALTER COLUMN GrantTitle varchar(64) not null

