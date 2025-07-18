delete from dbo.ProjectUpdateSection

insert into dbo.ProjectUpdateSection (ProjectUpdateSectionID, ProjectUpdateSectionName, ProjectUpdateSectionDisplayName, SortOrder, HasCompletionStatus, ProjectWorkflowSectionGroupingID)
values
(2, 'Basics', 'Basics', 20, 1, 6),
(3, 'LocationSimple', 'Location - Simple', 30, 1, 6),
(4, 'LocationDetailed', 'Location - Detailed', 40, 0, 2),
--(6, 'PerformanceMeasures', 'Performance Measures', 60, 1, 3),
(7, 'ExpectedFunding', 'Expected Funding', 70, 0, 5),
--(8, 'Expenditures', 'Expenditures', 80, 1, 4),
(9, 'Photos', 'Photos', 100, 0, 5),
(10, 'ExternalLinks', 'External Links', 125, 0, 5),
(11, 'NotesAndDocuments', 'Documents and Notes', 120, 0, 5),
(12, 'Organizations', 'Organizations', 25, 1, 5),
(13, 'Contacts', 'Contacts', 24, 1, 5),
(14, 'DNRUplandRegions', 'DNR Upland Regions', 50, 1, 2),
(15, 'PriorityLandscapes', 'Priority Landscapes', 45, 1, 2),
--(16, 'ProjectAttributes', 'Project Attributes', 22, 1, 1),
(17, 'Treatments', 'Treatments', 90, 0, 2),
(18, 'Counties', 'Counties', 80, 1, 2)
