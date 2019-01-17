SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectCustomAttribute](
	[ProjectCustomAttributeID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[ProjectCustomAttributeTypeID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectCustomAttribute_ProjectCustomAttributeID] PRIMARY KEY CLUSTERED 
(
	[ProjectCustomAttributeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectCustomAttribute]  WITH CHECK ADD  CONSTRAINT [FK_ProjectCustomAttribute_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[ProjectCustomAttribute] CHECK CONSTRAINT [FK_ProjectCustomAttribute_Project_ProjectID]
GO
ALTER TABLE [dbo].[ProjectCustomAttribute]  WITH CHECK ADD  CONSTRAINT [FK_ProjectCustomAttribute_ProjectCustomAttributeType_ProjectCustomAttributeTypeID] FOREIGN KEY([ProjectCustomAttributeTypeID])
REFERENCES [dbo].[ProjectCustomAttributeType] ([ProjectCustomAttributeTypeID])
GO
ALTER TABLE [dbo].[ProjectCustomAttribute] CHECK CONSTRAINT [FK_ProjectCustomAttribute_ProjectCustomAttributeType_ProjectCustomAttributeTypeID]