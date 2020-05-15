
DECLARE @fileBinary varbinary(max)

IF @@SERVERNAME = 'WALLOWA' or @@SERVERNAME = 'KETTLE'
    SET @fileBinary =  (SELECT bulkcolumn
        FROM OPENROWSET( BULK N'C:\Sitka\WADNRForestHealth\wwwroot\Content\img\wadnr-logo-cropped.png', SINGLE_BLOB ) AS y );
ELSE
    SET @fileBinary =  (SELECT bulkcolumn
        FROM OPENROWSET( BULK N'C:\git\sitkatech\wadnrforesthealth\Source\ProjectFirma.Web\Content\img\wadnr-logo-cropped.png', SINGLE_BLOB ) AS y );
    
insert into dbo.FileResource (FileResourceMimeTypeID, OriginalBaseFilename, OriginalFileExtension, FileResourceGUID, FileResourceData, CreatePersonID, CreateDate)
values
(
    5,
    'Washington_State_Department_of_Natural_Resources_logo',
    '.png',
    NEWID(),
    @fileBinary,
    5233,
    CURRENT_TIMESTAMP
);

update dbo.SystemAttribute
set BannerLogoFileResourceID = SCOPE_IDENTITY()
where SystemAttributeID = 10

/*

select * from SystemAttribute

*/