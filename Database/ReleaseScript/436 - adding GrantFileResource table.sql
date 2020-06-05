
CREATE TABLE [dbo].[GrantFileResource](
	[GrantFileResourceID] [int] IDENTITY(1,1) NOT NULL,
	[GrantID] [int] NOT NULL,
	[FileResourceID] [int] NOT NULL
 CONSTRAINT [PK_GrantFileResource_GrantFileResourceID] PRIMARY KEY CLUSTERED 
(
	[GrantFileResourceID] ASC
), CONSTRAINT [AK_GrantFileResource_GrantID_FileResourceID] UNIQUE NONCLUSTERED 
(
	[GrantID] ASC,
	[FileResourceID] ASC
)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GrantFileResource]  WITH CHECK ADD  CONSTRAINT [FK_GrantFileResource_FileResource_FileResourceID] FOREIGN KEY([FileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO

ALTER TABLE [dbo].[GrantFileResource] CHECK CONSTRAINT [FK_GrantFileResource_FileResource_FileResourceID]
GO

ALTER TABLE [dbo].[GrantFileResource]  WITH CHECK ADD  CONSTRAINT [FK_GrantFileResource_Grant_GrantID] FOREIGN KEY([GrantID])
REFERENCES [dbo].[Grant] ([GrantID])
GO

ALTER TABLE [dbo].[GrantFileResource] CHECK CONSTRAINT [FK_GrantFileResource_Grant_GrantID]
GO


INSERT INTO dbo.GrantFileResource (GrantID, FileResourceID)
select GrantID, GrantFileResourceID from dbo.[Grant] where GrantFileResourceID is not null

go

ALTER TABLE dbo.[Grant]
DROP CONSTRAINT FK_Grant_FileResource_GrantFileResourceID_FileResourceID; 

go

ALTER TABLE dbo.[Grant]
DROP COLUMN GrantFileResourceID;
go