

CREATE TABLE [dbo].[GisMetadataAttribute](
	[GisMetadataAttributeID] [int] IDENTITY(1,1) NOT NULL,
	[GisMetadataAttributeName] [varchar](500) NOT NULL,
    [GisMetadataAttributeType] [varchar](100)  NULL,
	
 CONSTRAINT [PK_GisMetadataAttribute_GisMetadataAttributeID] PRIMARY KEY CLUSTERED 
(
	[GisMetadataAttributeID] ASC
))

go

CREATE TABLE [dbo].[GisUploadAttemptGisMetadataAttribute](
	[GisUploadAttemptGisMetadataAttributeID] [int] IDENTITY(1,1) NOT NULL,
	[GisUploadAttemptID] [int] NOT NULL,
	[GisMetadataAttributeID] [int] NOT NULL,
    [SortOrder] [int] NOT NULL
 CONSTRAINT [PK_GisUploadAttemptGisMetadataAttribute_GisUploadAttemptGisMetadataAttributeID] PRIMARY KEY CLUSTERED 
(
	[GisUploadAttemptGisMetadataAttributeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 
GO

ALTER TABLE [dbo].[GisUploadAttemptGisMetadataAttribute]  WITH CHECK ADD  CONSTRAINT [FK_GisUploadAttemptGisMetadataAttribute_GisUploadAttempt_GisUploadAttemptID] FOREIGN KEY([GisUploadAttemptID])
REFERENCES [dbo].[GisUploadAttempt] ([GisUploadAttemptID])
GO


ALTER TABLE [dbo].[GisUploadAttemptGisMetadataAttribute]  WITH CHECK ADD  CONSTRAINT [FK_GisUploadAttemptGisMetadataAttribute_GisMetadataAttribute_GisMetadataAttributeID] FOREIGN KEY([GisMetadataAttributeID])
REFERENCES [dbo].[GisMetadataAttribute] ([GisMetadataAttributeID])
GO


CREATE TABLE [dbo].[GisFeature](
	[GisFeatureID] [int] IDENTITY(1,1) NOT NULL,
	[GisUploadAttemptID] [int] NOT NULL,
	[GisFeatureGeometry] geometry NOT NULL,
    [GisImportFeatureKey] int not null,
    [IsValid] as GisFeatureGeometry.STIsValid()

 CONSTRAINT [PK_GisFeature_GisFeatureID] PRIMARY KEY CLUSTERED 
(
	[GisFeatureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 
GO

ALTER TABLE [dbo].[GisFeature]  WITH CHECK ADD  CONSTRAINT [FK_GisFeature_GisUploadAttempt_GisUploadAttemptID] FOREIGN KEY([GisUploadAttemptID])
REFERENCES [dbo].[GisUploadAttempt] ([GisUploadAttemptID])
GO

CREATE TABLE [dbo].[GisFeatureMetadataAttribute](
	[GisFeatureMetadataAttributeID] [int] IDENTITY(1,1) NOT NULL,
	[GisFeatureID] [int] NOT NULL,
	[GisMetadataAttributeID] [int] NOT NULL,
    [GisFeatureMetadataAttributeValue] varchar(max) NULL

 CONSTRAINT [PK_GisFeatureMetadataAttribute_GisFeatureMetadataAttributeID] PRIMARY KEY CLUSTERED 
(
	[GisFeatureMetadataAttributeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[GisFeatureMetadataAttribute]  WITH CHECK ADD  CONSTRAINT [FK_GisFeatureMetadataAttribute_GisFeature_GisFeatureID] FOREIGN KEY([GisFeatureID])
REFERENCES [dbo].[GisFeature] ([GisFeatureID])
GO



ALTER TABLE [dbo].[GisFeatureMetadataAttribute]  WITH CHECK ADD  CONSTRAINT [FK_GisFeatureMetadataAttribute_GisMetadataAttribute_GisMetadataAttributeID] FOREIGN KEY([GisMetadataAttributeID])
REFERENCES [dbo].[GisMetadataAttribute] ([GisMetadataAttributeID])
GO






