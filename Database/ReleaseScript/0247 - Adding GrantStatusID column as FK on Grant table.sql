ALTER TABLE dbo.[Grant] ADD GrantStatusID int

go

update dbo.[Grant]

set GrantStatusID = 1
from dbo.[Grant] 

go

ALTER TABLE dbo.[Grant] ALTER COLUMN GrantStatusID int not null

go

ALTER TABLE [dbo].[Grant]  WITH CHECK ADD  CONSTRAINT [FK_Grant_GrantStatus_GrantStatusID] FOREIGN KEY([GrantStatusID])
REFERENCES [dbo].[GrantStatus] ([GrantStatusID])

ALTER TABLE [dbo].[Grant] CHECK CONSTRAINT [FK_Grant_GrantStatus_GrantStatusID]
GO