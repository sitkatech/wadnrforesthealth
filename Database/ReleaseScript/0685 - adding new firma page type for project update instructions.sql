insert into dbo.FirmaPageType(FirmaPageTypeID, FirmaPageTypeName, FirmaPageTypeDisplayName, FirmaPageRenderTypeID)
values
(76, 'ProjectUpdateInstructions', 'Project Update Instructions', 2)

INSERT INTO [dbo].[FirmaPage]
           ([FirmaPageTypeID],[FirmaPageContent])
     VALUES
           (76, '<ol><li>Navigate through the sections of the Project that require updates and make necessary changes</li><li>Click save on each page where changes are made</li><li>Submit Project update</li></ol>')