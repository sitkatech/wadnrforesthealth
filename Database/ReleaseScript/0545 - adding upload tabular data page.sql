insert into dbo.FirmaPageType(FirmaPageTypeID, FirmaPageTypeName, FirmaPageTypeDisplayName, FirmaPageRenderTypeID)
values (66, 'UploadLoaTabularDataExcel', 'Upload Loa Tabular Data', 1)
go


insert into dbo.FirmaPage(FirmaPageTypeID, FirmaPageContent)
select 66,null