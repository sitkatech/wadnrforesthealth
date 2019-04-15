IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.pProgramIndexImportJson'))
	DROP PROCEDURE dbo.pProgramIndexImportJson
GO

CREATE PROCEDURE dbo.pProgramIndexImportJson
(
    @SocrataDataMartRawJsonImportID int null
)
AS
begin

/*

JSON format:

,{"activity":"05","biennium":"2017","program":"030","program_index_code":"00321","subactivity":"10","subprogram":"03","title":"321-MOTORIZED USE DISCOVER PASS"}

*/


    SELECT programIndexTemp.*
    into #programIndexSocrataTemp
    --FROM OPENROWSET (BULK '{pathToVendorJsonFile}', SINGLE_CLOB) as j
    from (select rji.RawJsonString from dbo.SocrataDataMartRawJsonImport as rji where rji.SocrataDataMartRawJsonImportID = @SocrataDataMartRawJsonImportID) as j 
    CROSS APPLY OPENJSON(RawJsonString)
    WITH
    (
        activity nvarchar(256),
        biennium int,
        program nvarchar(256),
        program_index_code nvarchar(256),
        subactivity nvarchar(256),
        subprogram nvarchar(256),
        title nvarchar(256)
    )
    AS programIndexTemp

-- DELETE
-- Delete ProgramIndexes in our table not found in incoming temp table
delete from dbo.ProgramIndex 
where ProgramIndexID in 
(
    select dbpi.ProgramIndexID
    from dbo.ProgramIndex as dbpi
    full outer join #programIndexSocrataTemp as tpi on tpi.program_index_code = dbpi.ProgramIndexCode
    where (tpi.program_index_code is null)
)







/*










-- UPDATE (2nd attempt)
-- Update values for keys found in both sides
update dbv
set 
    VendorName = tv.vendor_name,
    VendorType = tv.vendor_type,
    BillingAgency = tv.billing_agency,
    BillingSubAgency = tv.billing_subagency,
    BillingFund = tv.billing_fund,
    VendorAddressLine1 = tv.addr1,
    VendorAddressLine2 = tv.addr2,
    VendorAddressLine3 = tv.addr3,
    VendorCity = tv.city,
    VendorState = tv.[state],
    VendorZip = tv.zip_code,
    Remarks = tv.remarks,
    VendorPhone = tv.phone,
    VendorStatus = tv.vendor_status,
    TaxpayerIdNumber = tv.taxpayer_id_num,
    Email = tv.email
from 
    dbo.Vendor as dbv
    join #vendorSocrataTemp as tv on tv.vendor_num = dbv.StatewideVendorNumber and tv.vendor_num_suffix = dbv.StatewideVendorNumberSuffix
--    full outer join #vendorSocrataTemp as tv on tv.vendor_num = dbv.StatewideVendorNumber and tv.vendor_num_suffix = dbv.StatewideVendorNumberSuffix
--where  dbv.StatewideVendorNumber is not null and dbv.StatewideVendorNumberSuffix is not null
--       and
--       tv.vendor_num is not null and tv.vendor_num_suffix is not null
    where 
    (
        isnull(dbv.VendorName, '') != isnull(tv.vendor_name, '') or
        isnull(dbv.VendorType, '') != isnull(tv.vendor_type, '') or
        isnull(dbv.BillingAgency, '') != isnull(tv.billing_agency, '') or
        isnull(dbv.BillingSubAgency, '') != isnull(tv.billing_subagency, '') or
        isnull(dbv.BillingFund, '') != isnull(tv.billing_fund, '') or
        isnull(dbv.VendorAddressLine1, '') != isnull(tv.addr1, '') or
        isnull(dbv.VendorAddressLine2, '') != isnull(tv.addr2, '') or
        isnull(dbv.VendorAddressLine3, '') != isnull(tv.addr3, '') or
        isnull(dbv.VendorCity, '') != isnull(tv.city, '') or
        isnull(dbv.VendorState, '') != isnull(tv.[state], '') or
        isnull(dbv.VendorZip, '') != isnull(tv.zip_code, '') or
        isnull(dbv.Remarks, '') != isnull(tv.remarks, '') or
        isnull(dbv.VendorPhone, '') != isnull(tv.phone, '') or
        isnull(dbv.VendorStatus, '') != isnull(tv.vendor_status, '') or
        isnull(dbv.TaxpayerIdNumber, '') != isnull(tv.taxpayer_id_num, '') or
        isnull(dbv.Email, '') != isnull(tv.email, '')
    )


-- INSERT (2nd attempt)
-- Insert values not already found in our table
insert into dbo.Vendor (VendorName,
                        StatewideVendorNumber,
                        StatewideVendorNumberSuffix,
                        VendorType,
                        BillingAgency,
                        BillingSubAgency,
                        BillingFund,
                        BillingFundBreakout,
                        VendorAddressLine1,
                        VendorAddressLine2,
                        VendorAddressLine3,
                        VendorCity,
                        VendorState,
                        VendorZip,
                        Remarks,
                        VendorPhone,
                        VendorStatus,
                        TaxpayerIdNumber,
                        Email)
select tv.vendor_name,
       tv.vendor_num,
       tv.vendor_num_suffix,
       tv.vendor_type,
       tv.billing_agency,
       tv.billing_subagency,
       tv.billing_fund,
       tv.billing_fund_breakout,
       tv.addr1,
       tv.addr2,
       tv.addr3,
       tv.city,
       tv.[state],
       tv.zip_code,
       tv.remarks,
       tv.phone,
       tv.vendor_status,
       tv.taxpayer_id_num,
       tv.email
from dbo.Vendor as dbv
full outer join #vendorSocrataTemp as tv on tv.vendor_num = dbv.StatewideVendorNumber and tv.vendor_num_suffix = dbv.StatewideVendorNumberSuffix
where  (dbv.StatewideVendorNumber is null and dbv.StatewideVendorNumberSuffix is null)
       and
       (tv.vendor_num is not null and tv.vendor_num_suffix is not null)

*/

end
go



/*
select * from dbo.SocrataDataMartRawJsonImport

set statistics time on

exec pProgramIndexImportJson @SocrataDataMartRawJsonImportID = 2

set statistics time off

*/