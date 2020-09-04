delete from dbo.FundingSource

insert dbo.FundingSource (FundingSourceID, FundingSourceName, FundingSourceDisplayName) 
values 
(1, 'Federal', 'Federal'),
(2, 'State', 'State'),
(3, 'Private', 'Private'),
(4, 'Other', 'Other')