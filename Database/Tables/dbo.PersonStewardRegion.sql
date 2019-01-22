SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonStewardRegion](
	[PersonStewardRegionID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[RegionID] [int] NOT NULL,
 CONSTRAINT [PK_PersonStewardRegion_PersonStewardRegionID] PRIMARY KEY CLUSTERED 
(
	[PersonStewardRegionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PersonStewardRegion]  WITH CHECK ADD  CONSTRAINT [FK_PersonStewardRegion_Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[PersonStewardRegion] CHECK CONSTRAINT [FK_PersonStewardRegion_Person_PersonID]
GO
ALTER TABLE [dbo].[PersonStewardRegion]  WITH CHECK ADD  CONSTRAINT [FK_PersonStewardRegion_Region_RegionID] FOREIGN KEY([RegionID])
REFERENCES [dbo].[Region] ([RegionID])
GO
ALTER TABLE [dbo].[PersonStewardRegion] CHECK CONSTRAINT [FK_PersonStewardRegion_Region_RegionID]