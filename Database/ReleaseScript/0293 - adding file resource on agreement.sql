ALTER TABLE dbo.Agreement ADD AgreementFileResourceID int


ALTER TABLE [dbo].[Agreement]  WITH CHECK ADD  CONSTRAINT [FK_Agreement_FileResource_AgreementFileResourceID_FileResourceID] FOREIGN KEY([AgreementFileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO

ALTER TABLE [dbo].[Agreement] CHECK CONSTRAINT [FK_Agreement_FileResource_AgreementFileResourceID_FileResourceID]
GO

