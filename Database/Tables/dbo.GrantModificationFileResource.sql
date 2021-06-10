SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantModificationFileResource](
	[GrantModificationFileResourceID] [int] IDENTITY(1,1) NOT NULL,
	[GrantModificationID] [int] NOT NULL,
	[FileResourceID] [int] NOT NULL,
	[DisplayName] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_GrantModificationFileResource_GrantModificationFileResourceID] PRIMARY KEY CLUSTERED 
(
	[GrantModificationFileResourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_GrantModificationFileResource_FileResourceID] UNIQUE NONCLUSTERED 
(
	[FileResourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GrantModificationFileResource]  WITH CHECK ADD  CONSTRAINT [FK_GrantModificationFileResource_FileResource_FileResourceID] FOREIGN KEY([FileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GrantModificationFileResource] CHECK CONSTRAINT [FK_GrantModificationFileResource_FileResource_FileResourceID]
GO
ALTER TABLE [dbo].[GrantModificationFileResource]  WITH CHECK ADD  CONSTRAINT [FK_GrantModificationFileResource_GrantModification_GrantModificationID] FOREIGN KEY([GrantModificationID])
REFERENCES [dbo].[GrantModification] ([GrantModificationID])
GO
ALTER TABLE [dbo].[GrantModificationFileResource] CHECK CONSTRAINT [FK_GrantModificationFileResource_GrantModification_GrantModificationID]