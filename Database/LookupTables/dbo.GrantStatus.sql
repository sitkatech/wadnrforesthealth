
delete from dbo.GrantStatus

SET IDENTITY_INSERT dbo.GrantStatus ON
Insert into dbo.GrantStatus (GrantStatusID, GrantStatusName)
values


(1, 'Active'),
(2, 'Pending'),
(3, 'Planned'),
(4, 'Closeout')

SET IDENTITY_INSERT dbo.GrantStatus OFF

