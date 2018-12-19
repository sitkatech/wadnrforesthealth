SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectPerson](
	[ProjectPersonID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[PersonID] [int] NOT NULL,
	[ProjectPersonRelationshipTypeID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectPerson_ProjectPersonID] PRIMARY KEY CLUSTERED 
(
	[ProjectPersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ProjectPerson_ProjectPersonID_TenantID] UNIQUE NONCLUSTERED 
(
	[ProjectPersonID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectPerson]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPerson_Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[ProjectPerson] CHECK CONSTRAINT [FK_ProjectPerson_Person_PersonID]
GO
ALTER TABLE [dbo].[ProjectPerson]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPerson_Person_PersonID_TenantID] FOREIGN KEY([PersonID], [TenantID])
REFERENCES [dbo].[Person] ([PersonID], [TenantID])
GO
ALTER TABLE [dbo].[ProjectPerson] CHECK CONSTRAINT [FK_ProjectPerson_Person_PersonID_TenantID]
GO
ALTER TABLE [dbo].[ProjectPerson]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPerson_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[ProjectPerson] CHECK CONSTRAINT [FK_ProjectPerson_Project_ProjectID]
GO
ALTER TABLE [dbo].[ProjectPerson]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPerson_Project_ProjectID_TenantID] FOREIGN KEY([ProjectID], [TenantID])
REFERENCES [dbo].[Project] ([ProjectID], [TenantID])
GO
ALTER TABLE [dbo].[ProjectPerson] CHECK CONSTRAINT [FK_ProjectPerson_Project_ProjectID_TenantID]
GO
ALTER TABLE [dbo].[ProjectPerson]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPerson_ProjectPersonRelationshipType_ProjectPersonRelationshipTypeID] FOREIGN KEY([ProjectPersonRelationshipTypeID])
REFERENCES [dbo].[ProjectPersonRelationshipType] ([ProjectPersonRelationshipTypeID])
GO
ALTER TABLE [dbo].[ProjectPerson] CHECK CONSTRAINT [FK_ProjectPerson_ProjectPersonRelationshipType_ProjectPersonRelationshipTypeID]
GO
ALTER TABLE [dbo].[ProjectPerson]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPerson_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[ProjectPerson] CHECK CONSTRAINT [FK_ProjectPerson_Tenant_TenantID]