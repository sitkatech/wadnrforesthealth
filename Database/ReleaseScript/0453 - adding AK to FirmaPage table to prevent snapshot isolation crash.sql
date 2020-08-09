


ALTER TABLE [dbo].[FirmaPage] ADD  CONSTRAINT [AK_FirmaPage_FirmaPageTypeID] UNIQUE NONCLUSTERED 
(
	[FirmaPageTypeID] ASC
) ON [PRIMARY]
GO


