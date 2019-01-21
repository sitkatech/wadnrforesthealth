ALTER TABLE dbo.[Grant] ADD GrantTypeID int

go

update dbo.[Grant]

set GrantTypeID = 2
from dbo.[Grant] where GrantID in (select GrantID from dbo.GrantAllocation)


update dbo.[Grant]

set GrantTypeID = 1
from dbo.[Grant] where GrantID not in (select GrantID from dbo.GrantAllocation)

go



ALTER TABLE [dbo].[Grant]  WITH CHECK ADD  CONSTRAINT [FK_Grant_GrantType_GrantTypeID] FOREIGN KEY([GrantTypeID])
REFERENCES [dbo].[GrantType] ([GrantTypeID])

ALTER TABLE [dbo].[Grant] CHECK CONSTRAINT [FK_Grant_GrantType_GrantTypeID]
GO