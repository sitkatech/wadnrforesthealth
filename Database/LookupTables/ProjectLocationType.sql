delete from dbo.ProjectLocationType;

insert dbo.ProjectLocationType (ProjectLocationTypeID, ProjectLocationTypeName, ProjectLocationTypeDisplayName) 
values 
(1, 'ProjectArea', 'Project Area'),
(2, 'TreatmentActivity', 'Treatment Activity'),
(3, 'ResearchPlot', 'Research Plot'),
(4, 'TestSite', 'Test Site'),
(5, 'Other', 'Other')
