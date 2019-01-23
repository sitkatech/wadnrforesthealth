--begin tran

alter table dbo.GrantAllocation
	with check add constraint [FK_GrantAllocation_Person_ProgramManagerPersonID_PersonID] foreign key(ProgramManagerPersonID) 
	references dbo.Person(PersonID)
GO

--USE [WADNRForestHealthDB]
--GO

--ALTER TABLE [dbo].[SupportRequestLog]  WITH CHECK ADD  CONSTRAINT [FK_SupportRequestLog_Person_RequestPersonID_PersonID] FOREIGN KEY([RequestPersonID])
--REFERENCES [dbo].[Person] ([PersonID])
--GO

--ALTER TABLE [dbo].[SupportRequestLog] CHECK CONSTRAINT [FK_SupportRequestLog_Person_RequestPersonID_PersonID]
--GO



--rollback tran
