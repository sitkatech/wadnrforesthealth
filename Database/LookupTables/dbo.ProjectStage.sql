
delete from dbo.ProjectStage

insert dbo.ProjectStage (ProjectStageID, ProjectStageColor, ProjectStageName, ProjectStageDisplayName, SortOrder) values 
--(1, '#dbbdff', 'Proposed', 'Proposed',5),
(2, '#80B2FF', 'Planned', 'Planned', 20),
(3, '#1975FF', 'Implementation', 'Implementation', 30),
(4, '#000066', 'Completed', 'Completed', 50),
(5, '#D6D6D6', 'Cancelled', 'Cancelled', 25)--,
--(6, '#FFE6CC', 'Deferred', 'Deferred', 10),
--(8, '#0047B2', 'PostImplementation', 'Post-Implementation', 40)
