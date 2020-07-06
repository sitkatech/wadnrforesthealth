
CREATE TABLE [dbo].[GisCrossWalkDefault](
	[GisCrossWalkDefaultID] [int] IDENTITY(1,1) NOT NULL,
	[GisUploadSourceOrganizationID] [int] NOT NULL,
	[FieldDefinitionID] [int] not null,
	[GisCrossWalkSourceValue] [varchar](300) NOT NULL,
    [GisCrossWalkMappedValue] [varchar](300) NOT NULL,
 CONSTRAINT [PK_GisCrossWalkDefault_GisCrossWalkDefaultID] PRIMARY KEY CLUSTERED 
(
	[GisCrossWalkDefaultID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 
GO

ALTER TABLE [dbo].[GisCrossWalkDefault]  WITH CHECK ADD  CONSTRAINT [FK_GisCrossWalkDefault_GisUploadSourceOrganization_GisUploadSourceOrganizationID] FOREIGN KEY([GisUploadSourceOrganizationID])
REFERENCES [dbo].GisUploadSourceOrganization (GisUploadSourceOrganizationID)
GO

ALTER TABLE [dbo].[GisCrossWalkDefault]  WITH CHECK ADD  CONSTRAINT [FK_GisCrossWalkDefault_FieldDefinition_FieldDefinitionID] FOREIGN KEY([FieldDefinitionID])
REFERENCES [dbo].FieldDefinition (FieldDefinitionID)
GO

