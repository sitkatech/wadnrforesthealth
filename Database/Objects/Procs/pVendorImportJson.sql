IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.pVendorImportJson'))
	DROP PROCEDURE dbo.pVendorImportJson
GO

CREATE PROCEDURE dbo.pVendorImportJson
(
	@VendorListJsonFileName varchar(500)
)
AS  
begin  

delete from dbo.Vendor;

DECLARE @sqlCommand nvarchar(max)

-- In order to have the filename be dynamic, the whole SQL expression must be a dynamic string. OPENROWSET won't take it 
-- as a @VendorListJsonFileName with regular SQL - a pity.

set @sqlCommand = 
N'
insert into dbo.Vendor (VendorName, StatewideVendorNumber, StatewideVendorNumberSuffix)
SELECT vendorList.vendor_name,
		vendorList.vendor_num,
		vendorList.vendor_num_suffix
 FROM OPENROWSET (BULK ''' + @VendorListJsonFileName + ''', SINGLE_CLOB) as j
 CROSS APPLY OPENJSON(BulkColumn)
 WITH(	vendor_num varchar(30),
		vendor_num_suffix varchar(5),
		vendor_type varchar(3),
		billing_agency varchar(200),
		billing_subagency varchar(200),
		billing_fund varchar(200),
		billing_fund_breakout varchar(200),
		vendor_name varchar(200), 
		vendor_addr_1 varchar(200),
		vendor_addr_2 varchar(200), 
		vendor_addr_3 varchar(200), 
		vendor_city varchar(200), 
		vendor_state varchar(200),
		vendor_zip varchar(200), 
		remarks varchar(200), 
		vendor_phone varchar(200), 
		vendor_status varchar(200),
		taxpayer_id_num varchar(200), 
		last_process_date varchar(200),
		email_addr varchar(200)	
	) AS vendorList
	';
	
	EXEC sp_executesql @sqlCommand

end
go



/*

exec pVendorImportJson @VendorListJsonFileName = 'C:\temp\partialVendorList.json'
select * from dbo.Vendor;

*/