SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectTypeProjectCustomAttributeType](
	[ProjectTypeProjectCustomAttributeTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectTypeID] [int] NOT NULL,
	[ProjectCustomAttributeTypeID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectTypeProjectCustomAttributeType_ProjectTypeProjectCustomAttributeTypeID] PRIMARY KEY CLUSTERED 
(
	[ProjectTypeProjectCustomAttributeTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ProjectTypeProjectCustomAttributeType_ProjectTypeID_ProjectCustomAttributeTypeID] UNIQUE NONCLUSTERED 
(
	[ProjectTypeID] ASC,
	[ProjectCustomAttributeTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectTypeProjectCustomAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTypeProjectCustomAttributeType_ProjectCustomAttributeType_ProjectCustomAttributeTypeID] FOREIGN KEY([ProjectCustomAttributeTypeID])
REFERENCES [dbo].[ProjectCustomAttributeType] ([ProjectCustomAttributeTypeID])
GO
ALTER TABLE [dbo].[ProjectTypeProjectCustomAttributeType] CHECK CONSTRAINT [FK_ProjectTypeProjectCustomAttributeType_ProjectCustomAttributeType_ProjectCustomAttributeTypeID]
GO
ALTER TABLE [dbo].[ProjectTypeProjectCustomAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTypeProjectCustomAttributeType_ProjectType_ProjectTypeID] FOREIGN KEY([ProjectTypeID])
REFERENCES [dbo].[ProjectType] ([ProjectTypeID])
GO
ALTER TABLE [dbo].[ProjectTypeProjectCustomAttributeType] CHECK CONSTRAINT [FK_ProjectTypeProjectCustomAttributeType_ProjectType_ProjectTypeID]