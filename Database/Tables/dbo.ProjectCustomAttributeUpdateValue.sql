SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectCustomAttributeUpdateValue](
	[ProjectCustomAttributeUpdateValueID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectCustomAttributeUpdateID] [int] NOT NULL,
	[AttributeValue] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_ProjectCustomAttributeUpdateValue_ProjectCustomAttributeUpdateValueID] PRIMARY KEY CLUSTERED 
(
	[ProjectCustomAttributeUpdateValueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectCustomAttributeUpdateValue]  WITH CHECK ADD  CONSTRAINT [FK_ProjectCustomAttributeUpdateValue_ProjectCustomAttributeUpdate_ProjectCustomAttributeUpdateID] FOREIGN KEY([ProjectCustomAttributeUpdateID])
REFERENCES [dbo].[ProjectCustomAttributeUpdate] ([ProjectCustomAttributeUpdateID])
GO
ALTER TABLE [dbo].[ProjectCustomAttributeUpdateValue] CHECK CONSTRAINT [FK_ProjectCustomAttributeUpdateValue_ProjectCustomAttributeUpdate_ProjectCustomAttributeUpdateID]