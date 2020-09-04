
CREATE TABLE [dbo].[GisDefaultMapping](
	[GisDefaultMappingID] [int] IDENTITY(1,1) NOT NULL,
	[GisUploadSourceOrganizationID] [int] NOT NULL,
	[FieldDefinitionID] [int] NOT NULL,
	[GisDefaultMappingColumnName] [varchar](300) NOT NULL,
 CONSTRAINT [PK_GisDefaultMapping_GisDefaultMappingD] PRIMARY KEY CLUSTERED 
(
	[GisDefaultMappingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GisDefaultMapping]  WITH CHECK ADD  CONSTRAINT [FK_GisDefaultMapping_FieldDefinition_FieldDefinitionID] FOREIGN KEY([FieldDefinitionID])
REFERENCES [dbo].[FieldDefinition] ([FieldDefinitionID])
GO



ALTER TABLE [dbo].[GisDefaultMapping]  WITH CHECK ADD  CONSTRAINT [FK_GisDefaultMapping_GisUploadSourceOrganization_GisUploadSourceOrganizationID] FOREIGN KEY([GisUploadSourceOrganizationID])
REFERENCES [dbo].[GisUploadSourceOrganization] ([GisUploadSourceOrganizationID])
GO

