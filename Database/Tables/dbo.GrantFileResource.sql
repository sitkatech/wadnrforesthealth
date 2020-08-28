SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrantFileResource](
	[GrantFileResourceID] [int] IDENTITY(1,1) NOT NULL,
	[GrantID] [int] NOT NULL,
	[FileResourceID] [int] NOT NULL,
	[DisplayName] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_GrantFileResource_GrantFileResourceID] PRIMARY KEY CLUSTERED 
(
	[GrantFileResourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_GrantFileResource_GrantID_FileResourceID] UNIQUE NONCLUSTERED 
(
	[GrantID] ASC,
	[FileResourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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