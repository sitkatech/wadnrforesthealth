

CREATE TABLE [dbo].[TreatmentArea](
	[TreatmentAreaID] [int] IDENTITY(1,1) NOT NULL,
	[TreatmentAreaFeature] [geometry] NOT NULL,
    [TemporaryTreatmentCacheID] int null
	
 CONSTRAINT [PK_TreatmentArea_TreatmentAreaID] PRIMARY KEY CLUSTERED 
(
	[TreatmentAreaID] ASC
)

)


GO


