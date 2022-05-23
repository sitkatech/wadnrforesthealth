alter table dbo.PriorityLandscape
add PriorityLandscapeAboveMapText varchar(2000)
go
update dbo.PriorityLandscape set PriorityLandscapeAboveMapText = 'This map displays the simple location of forest health projects in this priority landscape along with optional additional layers that users can select to view including detailed project and treatment locations.'
