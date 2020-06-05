
CREATE TABLE [dbo].[GrantAllocationFileResource](
	[GrantAllocationFileResourceID] [int] IDENTITY(1,1) NOT NULL,
	[GrantAllocationID] [int] NOT NULL,
	[FileResourceID] [int] NOT NULL
 CONSTRAINT [PK_GrantAllocationFileResource_GrantAllocationFileResourceID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationFileResourceID] ASC
), CONSTRAINT [AK_GrantAllocationFileResource_GrantAllocationID_FileResourceID] UNIQUE NONCLUSTERED 
(
	[GrantAllocationID] ASC,
	[FileResourceID] ASC
)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GrantAllocationFileResource]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationFileResource_FileResource_FileResourceID] FOREIGN KEY([FileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO

ALTER TABLE [dbo].[GrantAllocationFileResource] CHECK CONSTRAINT [FK_GrantAllocationFileResource_FileResource_FileResourceID]
GO

ALTER TABLE [dbo].[GrantAllocationFileResource]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationFileResource_GrantAllocation_GrantAllocationID] FOREIGN KEY([GrantAllocationID])
REFERENCES [dbo].[GrantAllocation] ([GrantAllocationID])
GO

ALTER TABLE [dbo].[GrantAllocationFileResource] CHECK CONSTRAINT [FK_GrantAllocationFileResource_GrantAllocation_GrantAllocationID]
GO


INSERT INTO dbo.GrantAllocationFileResource (GrantAllocationID, FileResourceID)
select GrantAllocationID, GrantAllocationFileResourceID from dbo.[GrantAllocation] where GrantAllocationFileResourceID is not null

go

ALTER TABLE dbo.[GrantAllocation]
DROP CONSTRAINT FK_GrantAllocation_FileResource_GrantAllocationFileResourceID_FileResourceID; 

go



ALTER TABLE dbo.GrantAllocation
DROP COLUMN GrantAllocationFileResourceID;
go