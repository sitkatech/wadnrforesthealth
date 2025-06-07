delete from dbo.ProjectCreateSection

insert into dbo.ProjectCreateSection (ProjectCreateSectionID, ProjectCreateSectionName, ProjectCreateSectionDisplayName, SortOrder, HasCompletionStatus, ProjectWorkflowSectionGroupingID)
values
(2, 'Basics', 'Basics', 20, 1, 6),
(3, 'LocationSimple', 'Location - Simple', 30, 1, 6),
(4, 'LocationDetailed', 'Location - Detailed', 40, 0, 2),
--(6, 'ExpectedPerformanceMeasures', 'Expected Performance Measures', 60, 1, 3),
--(7, 'ReportedPerformanceMeasures', 'Reported Performance Measures', 70, 1, 3),
(8, 'ExpectedFunding', 'Expected Funding', 80, 0, 5),
--(9, 'ReportedExpenditures', 'Reported Expenditures', 90, 1, 4),
(11, 'Classifications', 'Classifications', 90, 0, 5),
(13, 'Photos', 'Photos', 100, 0, 5),
(14, 'NotesAndDocuments', 'Documents and Notes', 110, 0, 5),
(15, 'Organizations', 'Organizations', 70, 0, 5),
(16, 'Contacts', 'Contacts', 65, 0, 5),
(17, 'DNRUplandRegions', 'DNR Upland Regions', 50, 1, 2),
(18, 'PriorityLandscapes', 'Priority Landscapes', 45, 1, 2),
--(19, 'ProjectAttributes', 'Project Attributes', 22, 1, 1),
(20, 'Treatments', 'Treatments', 60, 0, 2),
(21, 'Counties', 'Counties', 55, 1, 2)
