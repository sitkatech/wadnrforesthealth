SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectCustomAttributeValue](
	[ProjectCustomAttributeValueID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectCustomAttributeID] [int] NOT NULL,
	[AttributeValue] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_ProjectCustomAttributeValue_ProjectCustomAttributeValueID] PRIMARY KEY CLUSTERED 
(
	[ProjectCustomAttributeValueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectCustomAttributeValue]  WITH CHECK ADD  CONSTRAINT [FK_ProjectCustomAttributeValue_ProjectCustomAttribute_ProjectCustomAttributeID] FOREIGN KEY([ProjectCustomAttributeID])
REFERENCES [dbo].[ProjectCustomAttribute] ([ProjectCustomAttributeID])
GO
ALTER TABLE [dbo].[ProjectCustomAttributeValue] CHECK CONSTRAINT [FK_ProjectCustomAttributeValue_ProjectCustomAttribute_ProjectCustomAttributeID]