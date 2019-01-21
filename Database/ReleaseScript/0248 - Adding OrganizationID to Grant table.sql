ALTER TABLE dbo.[Grant] ADD OrganizationID int

go

update dbo.[Grant]

set OrganizationID = 4708
from dbo.[Grant] 

go

ALTER TABLE dbo.[Grant] ALTER COLUMN OrganizationID int not null

go

ALTER TABLE [dbo].[Grant]  WITH CHECK ADD  CONSTRAINT [FK_Grant_Organization_OrganizationID] FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[Organization] ([OrganizationID])

ALTER TABLE [dbo].[Grant] CHECK CONSTRAINT [FK_Grant_Organization_OrganizationID]
GO