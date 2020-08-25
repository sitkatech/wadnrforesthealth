delete from dbo.ProjectFundingSource

insert dbo.ProjectFundingSource (ProjectFundingSourceID, ProjectFundingSourceName, ProjectFundingSourceDisplayName) 
values 
(1, 'Federal', 'Federal'),
(2, 'State', 'State'),
(3, 'Private', 'Private'),
(4, 'Other', 'Other')