/*
select * from ProjectImage

select * from ProjectImageUpdate
*/

alter table dbo.ProjectImage
alter column ProjectImageTimingID int null

go

alter table dbo.ProjectImageUpdate
alter column ProjectImageTimingID int null

go