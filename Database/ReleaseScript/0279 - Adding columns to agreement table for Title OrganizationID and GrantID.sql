--ALTER TABLE dbo.Agreement ADD AgreementTitle varchar(256)

--go

--update dbo.Agreement
--set AgreementTitle = 'This is the agreement Title'
--from dbo.Agreement

--go 


--ALTER TABLE dbo.Agreement ALTER COLUMN AgreementTitle varchar(256) not null



--ALTER TABLE dbo.Agreement ADD OrganizationID int

--go

--update dbo.Agreement
--set OrganizationID = 4708
--from dbo.Agreement

--go 

--ALTER TABLE dbo.Agreement ALTER COLUMN OrganizationID int not null


--ALTER TABLE dbo.Agreement ADD GrantID int

--go

--update dbo.Agreement
--set GrantID = 1
--from dbo.Agreement

--go 

--ALTER TABLE dbo.Agreement ALTER COLUMN GrantID int not null

--ALTER TABLE [dbo].[Agreement]  WITH CHECK ADD  CONSTRAINT [FK_Agreement_Organization_OrganizationID] FOREIGN KEY([OrganizationID])
--REFERENCES [dbo].[Organization] ([OrganizationID])
--GO

--ALTER TABLE [dbo].[Agreement] CHECK CONSTRAINT [FK_Agreement_Organization_OrganizationID]
--GO



--ALTER TABLE [dbo].[Agreement]  WITH CHECK ADD  CONSTRAINT [FK_Agreement_Grant_GrantID] FOREIGN KEY([GrantID])
--REFERENCES [dbo].[Grant] ([GrantID])
--GO

--ALTER TABLE [dbo].[Agreement] CHECK CONSTRAINT [FK_Agreement_Grant_GrantID]
--GO