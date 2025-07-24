insert into dbo.FirmaPageType(FirmaPageTypeID, FirmaPageTypeName, FirmaPageTypeDisplayName, FirmaPageRenderTypeID)
values
(76, 'ProjectUpdateInstructions', 'Project Update Instructions', 2)

INSERT INTO [dbo].[FirmaPage]
           ([FirmaPageTypeID],[FirmaPageContent])
     VALUES
           (76, '<ol><li>Fill out the fields on the page</li><li>Adjust any optional information in the other pages including location data</li><li>Submit project update</li></ol>')