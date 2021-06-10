SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantAllocationFileResource](
	[GrantAllocationFileResourceID] [int] IDENTITY(1,1) NOT NULL,
	[GrantAllocationID] [int] NOT NULL,
	[FileResourceID] [int] NOT NULL,
	[DisplayName] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_GrantAllocationFileResource_GrantAllocationFileResourceID] PRIMARY KEY CLUSTERED 
(
	[GrantAllocationFileResourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_GrantAllocationFileResource_GrantAllocationID_FileResourceID] UNIQUE NONCLUSTERED 
(
	[GrantAllocationID] ASC,
	[FileResourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
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