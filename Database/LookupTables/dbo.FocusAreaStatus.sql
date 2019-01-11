
delete from dbo.FocusAreaStatus

insert dbo.FocusAreaStatus (FocusAreaStatusID, FocusAreaStatusName, FocusAreaStatusDisplayName) 
values 
(1, 'Planned', 'Planned'),
(2, 'In Progress', 'In Progress'),
(3, 'Completed', 'Completed')
