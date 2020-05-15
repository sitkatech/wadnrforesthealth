update dbo.SystemAttribute
set BannerLogoFileResourceID = (select FileResourceID from dbo.FileResource where OriginalBaseFilename = 'Washington_State_Department_of_Natural_Resources_logo')
where SystemAttributeID = 10

/*

select * from SystemAttribute

select * from dbo.FileResource where OriginalBaseFilename = 'Washington_State_Department_of_Natural_Resources_logo'

*/