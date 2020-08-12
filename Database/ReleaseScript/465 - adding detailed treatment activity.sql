

CREATE TABLE [dbo].[TreatmentDetailedActivityType](
	[TreatmentDetailedActivityTypeID] [int] NOT NULL,
	[TreatmentDetailedActivityTypeName] [varchar](50) NOT NULL,
	[TreatmentDetailedActivityTypeDisplayName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TreatmentDetailedActivityType_TreatmentDetailedActivityTypeID] PRIMARY KEY CLUSTERED 
(
	[TreatmentDetailedActivityTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentDetailedActivityType_TreatmentDetailedActivityTypeDisplayName] UNIQUE NONCLUSTERED 
(
	[TreatmentDetailedActivityTypeDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentDetailedActivityType_TreatmentDetailedActivityTypeName] UNIQUE NONCLUSTERED 
(
	[TreatmentDetailedActivityTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE dbo.Treatment ADD TreatmentDetailedActivityTypeID int not null

ALTER TABLE dbo.Treatment add TreatmentDetailedActivityTypeImportedText varchar(200)


go

ALTER TABLE [dbo].[Treatment]  WITH CHECK ADD  CONSTRAINT [FK_Treatment_TreatmentDetailedActivityType_TreatmentDetailedActivityTypeID] FOREIGN KEY([TreatmentDetailedActivityTypeID])
REFERENCES [dbo].[TreatmentDetailedActivityType] ([TreatmentDetailedActivityTypeID])
GO



