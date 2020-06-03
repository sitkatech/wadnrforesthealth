/*
select * from ProjectImage

select * from ProjectImageUpdate
*/

alter table ProjectImage
alter column ProjectImageTimingID int null

go

alter table ProjectImageUpdate
alter column ProjectImageTimingID int null

go