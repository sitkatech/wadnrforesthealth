SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectPriorityArea](
	[ProjectPriorityAreaID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[PriorityAreaID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectPriorityArea_ProjectPriorityAreaID] PRIMARY KEY CLUSTERED 
(
	[ProjectPriorityAreaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ProjectPriorityArea_ProjectID_PriorityAreaID] UNIQUE NONCLUSTERED 
(
	[ProjectID] ASC,
	[PriorityAreaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectPriorityArea]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPriorityArea_PriorityArea_PriorityAreaID] FOREIGN KEY([PriorityAreaID])
REFERENCES [dbo].[PriorityArea] ([PriorityAreaID])
GO
ALTER TABLE [dbo].[ProjectPriorityArea] CHECK CONSTRAINT [FK_ProjectPriorityArea_PriorityArea_PriorityAreaID]
GO
ALTER TABLE [dbo].[ProjectPriorityArea]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPriorityArea_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[ProjectPriorityArea] CHECK CONSTRAINT [FK_ProjectPriorityArea_Project_ProjectID]