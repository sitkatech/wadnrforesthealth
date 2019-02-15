delete from dbo.ProjectLocationType;

insert dbo.ProjectLocationType (ProjectLocationTypeID, ProjectLocationTypeName, ProjectLocationTypeDisplayName, ProjectLocationTypeMapLayerColor) 
values 
(1, 'ProjectArea', 'Project Area', '#2c96c3'),
(2, 'TreatmentActivity', 'Treatment Activity', '#2b7ac3'),
(3, 'ResearchPlot', 'Research Plot', '#2a44c3'),
(4, 'TestSite', 'Test Site', '#3e29c3'),
(5, 'Other', 'Other', '#6928c3')
