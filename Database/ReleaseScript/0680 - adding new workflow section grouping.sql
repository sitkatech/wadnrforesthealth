insert into dbo.ProjectWorkflowSectionGrouping (ProjectWorkflowSectionGroupingID, ProjectWorkflowSectionGroupingName, ProjectWorkflowSectionGroupingDisplayName, SortOrder)
values
(6, 'ProjectSetup', 'Project Setup', 15)



insert into dbo.FirmaPageType(FirmaPageTypeID, FirmaPageTypeName, FirmaPageTypeDisplayName, FirmaPageRenderTypeID)
values
(75, 'ProjectCreateInstructions', 'Project Create Instructions', 2)

INSERT INTO [dbo].[FirmaPage]
           ([FirmaPageTypeID],[FirmaPageContent])
     VALUES
           (75, '<ol><li>Fill out the fields on the page</li><li>Adjust any optional information in the other pages including location data</li><li>Submit project</li></ol>')


