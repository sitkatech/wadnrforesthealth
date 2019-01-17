SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonStewardGeospatialArea](
	[PersonStewardGeospatialAreaID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[GeospatialAreaID] [int] NOT NULL,
 CONSTRAINT [PK_PersonStewardGeospatialArea_PersonStewardGeospatialAreaID] PRIMARY KEY CLUSTERED 
(
	[PersonStewardGeospatialAreaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PersonStewardGeospatialArea]  WITH CHECK ADD  CONSTRAINT [FK_PersonStewardGeospatialArea_GeospatialArea_GeospatialAreaID] FOREIGN KEY([GeospatialAreaID])
REFERENCES [dbo].[GeospatialArea] ([GeospatialAreaID])
GO
ALTER TABLE [dbo].[PersonStewardGeospatialArea] CHECK CONSTRAINT [FK_PersonStewardGeospatialArea_GeospatialArea_GeospatialAreaID]
GO
ALTER TABLE [dbo].[PersonStewardGeospatialArea]  WITH CHECK ADD  CONSTRAINT [FK_PersonStewardGeospatialArea_Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[PersonStewardGeospatialArea] CHECK CONSTRAINT [FK_PersonStewardGeospatialArea_Person_PersonID]