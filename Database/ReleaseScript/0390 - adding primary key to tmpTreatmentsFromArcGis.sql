ALTER TABLE [dbo].[tmpTreatmentsFromArcGis] 
add tmpTreatmentsFromArcGisID [int] IDENTITY(1,1) NOT NULL
GO

/****** Object:  Index [PK_GrantAllocationProjectCode_GrantAllocationProjectCodeID]    Script Date: 4/30/2019 10:26:48 AM ******/
ALTER TABLE [dbo].[tmpTreatmentsFromArcGis] ADD  CONSTRAINT [PK_tmpTreatmentsFromArcGis_tmpTreatmentsFromArcGisID] PRIMARY KEY CLUSTERED 
(
    tmpTreatmentsFromArcGisID ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO