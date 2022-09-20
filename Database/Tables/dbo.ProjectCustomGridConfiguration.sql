SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectCustomGridConfiguration](
	[ProjectCustomGridConfigurationID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectCustomGridTypeID] [int] NOT NULL,
	[ProjectCustomGridColumnID] [int] NOT NULL,
	[ProjectCustomAttributeTypeID] [int] NULL,
	[IsEnabled] [bit] NOT NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_ProjectCustomGridConfiguration_ProjectCustomGridConfigurationID] PRIMARY KEY CLUSTERED 
(
	[ProjectCustomGridConfigurationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectCustomGridConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_ProjectCustomGridConfiguration_ProjectCustomAttributeType_ProjectCustomAttributeTypeID] FOREIGN KEY([ProjectCustomAttributeTypeID])
REFERENCES [dbo].[ProjectCustomAttributeType] ([ProjectCustomAttributeTypeID])
GO
ALTER TABLE [dbo].[ProjectCustomGridConfiguration] CHECK CONSTRAINT [FK_ProjectCustomGridConfiguration_ProjectCustomAttributeType_ProjectCustomAttributeTypeID]
GO
ALTER TABLE [dbo].[ProjectCustomGridConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_ProjectCustomGridConfiguration_ProjectCustomGridColumn_ProjectCustomGridColumnID] FOREIGN KEY([ProjectCustomGridColumnID])
REFERENCES [dbo].[ProjectCustomGridColumn] ([ProjectCustomGridColumnID])
GO
ALTER TABLE [dbo].[ProjectCustomGridConfiguration] CHECK CONSTRAINT [FK_ProjectCustomGridConfiguration_ProjectCustomGridColumn_ProjectCustomGridColumnID]
GO
ALTER TABLE [dbo].[ProjectCustomGridConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_ProjectCustomGridConfiguration_ProjectCustomGridType_ProjectCustomGridTypeID] FOREIGN KEY([ProjectCustomGridTypeID])
REFERENCES [dbo].[ProjectCustomGridType] ([ProjectCustomGridTypeID])
GO
ALTER TABLE [dbo].[ProjectCustomGridConfiguration] CHECK CONSTRAINT [FK_ProjectCustomGridConfiguration_ProjectCustomGridType_ProjectCustomGridTypeID]
GO
ALTER TABLE [dbo].[ProjectCustomGridConfiguration]  WITH CHECK ADD  CONSTRAINT [CK_ProjectCustomGridConfiguration_SortOrder_OnlyIf_IsEnabled] CHECK  (([IsEnabled]=(1) AND [SortOrder] IS NOT NULL OR [IsEnabled]=(0) AND [SortOrder] IS NULL))
GO
ALTER TABLE [dbo].[ProjectCustomGridConfiguration] CHECK CONSTRAINT [CK_ProjectCustomGridConfiguration_SortOrder_OnlyIf_IsEnabled]