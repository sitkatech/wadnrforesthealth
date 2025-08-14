
delete from dbo.FundSourceStatus

SET IDENTITY_INSERT dbo.FundSourceStatus ON
Insert into dbo.FundSourceStatus (FundSourceStatusID, FundSourceStatusName)
values


(1, 'Active'),
(2, 'Pending'),
(3, 'Planned'),
(4, 'Closeout')

SET IDENTITY_INSERT dbo.FundSourceStatus OFF

