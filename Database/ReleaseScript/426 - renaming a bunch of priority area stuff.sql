



CREATE TABLE [dbo].[ProjectPriorityLandscape](
	[ProjectPriorityLandscapeID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[PriorityLandscapeID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectPriorityLandscape_ProjectPriorityLandscapeID] PRIMARY KEY CLUSTERED 
(
	[ProjectPriorityLandscapeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ProjectPriorityLandscape_ProjectID_PriorityLandscapeID] UNIQUE NONCLUSTERED 
(
	[ProjectID] ASC,
	[PriorityLandscapeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ProjectPriorityLandscape]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPriorityLandscape_PriorityLandscape_PriorityLandscapeID] FOREIGN KEY([PriorityLandscapeID])
REFERENCES [dbo].[PriorityLandscape] ([PriorityLandscapeID])
GO

ALTER TABLE [dbo].[ProjectPriorityLandscape] CHECK CONSTRAINT [FK_ProjectPriorityLandscape_PriorityLandscape_PriorityLandscapeID]
GO

ALTER TABLE [dbo].[ProjectPriorityLandscape]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPriorityLandscape_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO

ALTER TABLE [dbo].[ProjectPriorityLandscape] CHECK CONSTRAINT [FK_ProjectPriorityLandscape_Project_ProjectID]
GO



CREATE TABLE [dbo].[ProjectPriorityLandscapeUpdate](
	[ProjectPriorityLandscapeUpdateID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectUpdateBatchID] [int] NOT NULL,
	[PriorityLandscapeID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectPriorityLandscapeUpdate_ProjectPriorityLandscapeUpdateID] PRIMARY KEY CLUSTERED 
(
	[ProjectPriorityLandscapeUpdateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ProjectPriorityLandscapeUpdate_ProjectUpdateBatchID_PriorityLandscapeID] UNIQUE NONCLUSTERED 
(
	[ProjectUpdateBatchID] ASC,
	[PriorityLandscapeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ProjectPriorityLandscapeUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPriorityLandscapeUpdate_PriorityLandscape_PriorityLandscapeID] FOREIGN KEY([PriorityLandscapeID])
REFERENCES [dbo].[PriorityLandscape] ([PriorityLandscapeID])
GO

ALTER TABLE [dbo].[ProjectPriorityLandscapeUpdate] CHECK CONSTRAINT [FK_ProjectPriorityLandscapeUpdate_PriorityLandscape_PriorityLandscapeID]
GO

ALTER TABLE [dbo].[ProjectPriorityLandscapeUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPriorityLandscapeUpdate_ProjectUpdateBatch_ProjectUpdateBatchID] FOREIGN KEY([ProjectUpdateBatchID])
REFERENCES [dbo].[ProjectUpdateBatch] ([ProjectUpdateBatchID])
GO

ALTER TABLE [dbo].[ProjectPriorityLandscapeUpdate] CHECK CONSTRAINT [FK_ProjectPriorityLandscapeUpdate_ProjectUpdateBatch_ProjectUpdateBatchID]
GO



drop table dbo.ProjectPriorityArea

drop table dbo.ProjectPriorityAreaUpdate


drop table dbo.PriorityArea


alter table dbo.Project add NoPriorityLandscapesExplanation varchar(4000)


go 

update dbo.Project

set NoPriorityLandscapesExplanation = NoPriorityAreasExplanation

alter table dbo.Project drop column NoPriorityAreasExplanation

alter table dbo.ProjectUpdateBatch add NoPriorityLandscapesExplanation varchar(4000)

go

update dbo.ProjectUpdateBatch

set NoPriorityLandscapesExplanation = NoPriorityAreasExplanation

alter table dbo.ProjectUpdateBatch drop column NoPriorityAreasExplanation