SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectPersonUpdate](
	[ProjectPersonUpdateID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[ProjectUpdateBatchID] [int] NOT NULL,
	[PersonID] [int] NOT NULL,
	[ProjectPersonRelationshipTypeID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectPersonUpdate_ProjectPersonUpdateID] PRIMARY KEY CLUSTERED 
(
	[ProjectPersonUpdateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ProjectPersonUpdate_ProjectPersonUpdateID_TenantID] UNIQUE NONCLUSTERED 
(
	[ProjectPersonUpdateID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectPersonUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPersonUpdate_Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[ProjectPersonUpdate] CHECK CONSTRAINT [FK_ProjectPersonUpdate_Person_PersonID]
GO
ALTER TABLE [dbo].[ProjectPersonUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPersonUpdate_Person_PersonID_TenantID] FOREIGN KEY([PersonID], [TenantID])
REFERENCES [dbo].[Person] ([PersonID], [TenantID])
GO
ALTER TABLE [dbo].[ProjectPersonUpdate] CHECK CONSTRAINT [FK_ProjectPersonUpdate_Person_PersonID_TenantID]
GO
ALTER TABLE [dbo].[ProjectPersonUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPersonUpdate_ProjectPersonRelationshipType_ProjectPersonRelationshipTypeID] FOREIGN KEY([ProjectPersonRelationshipTypeID])
REFERENCES [dbo].[ProjectPersonRelationshipType] ([ProjectPersonRelationshipTypeID])
GO
ALTER TABLE [dbo].[ProjectPersonUpdate] CHECK CONSTRAINT [FK_ProjectPersonUpdate_ProjectPersonRelationshipType_ProjectPersonRelationshipTypeID]
GO
ALTER TABLE [dbo].[ProjectPersonUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPersonUpdate_ProjectUpdateBatch_ProjectUpdateBatchID] FOREIGN KEY([ProjectUpdateBatchID])
REFERENCES [dbo].[ProjectUpdateBatch] ([ProjectUpdateBatchID])
GO
ALTER TABLE [dbo].[ProjectPersonUpdate] CHECK CONSTRAINT [FK_ProjectPersonUpdate_ProjectUpdateBatch_ProjectUpdateBatchID]
GO
ALTER TABLE [dbo].[ProjectPersonUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPersonUpdate_ProjectUpdateBatch_ProjectUpdateBatchID_TenantID] FOREIGN KEY([ProjectUpdateBatchID], [TenantID])
REFERENCES [dbo].[ProjectUpdateBatch] ([ProjectUpdateBatchID], [TenantID])
GO
ALTER TABLE [dbo].[ProjectPersonUpdate] CHECK CONSTRAINT [FK_ProjectPersonUpdate_ProjectUpdateBatch_ProjectUpdateBatchID_TenantID]
GO
ALTER TABLE [dbo].[ProjectPersonUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPersonUpdate_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[ProjectPersonUpdate] CHECK CONSTRAINT [FK_ProjectPersonUpdate_Tenant_TenantID]