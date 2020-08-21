delete from dbo.ProjectGrantAllocationRequestFundingSource

insert dbo.ProjectGrantAllocationRequestFundingSource (ProjectGrantAllocationRequestFundingSourceID, ProjectGrantAllocationRequestFundingSourceName, ProjectGrantAllocationRequestFundingSourceDisplayName) 
values 
(1, 'Federal', 'Federal'),
(2, 'State', 'State'),
(3, 'Private', 'Private'),
(4, 'Other', 'Other')