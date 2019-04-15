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



--select * from dbo.ProgramIndex
--select * from #programIndexSocrataTemp



-- UPDATE (2nd attempt)
-- Update values for keys found in both sides
update dbpi
set 
    Activity = tpi.activity,
    Biennium = tpi.biennium,
    Program = tpi.program,
    Subactivity = tpi.subactivity,
    Subprogram = tpi.subprogram,
    ProgramIndexTitle = tpi.title
from 
    dbo.ProgramIndex as dbpi
    join #programIndexSocrataTemp as tpi on tpi.program_index_code = dbpi.ProgramIndexCode
    where 
    (
        isnull(dbpi.Activity, '') != isnull(tpi.activity, '') or
        isnull(dbpi.Biennium, '') != isnull(tpi.biennium, '') or
        isnull(dbpi.Program, '') != isnull(tpi.program, '') or
        isnull(dbpi.Subactivity, '') != isnull(tpi.subactivity, '') or
        isnull(dbpi.Subprogram, '') != isnull(tpi.subprogram, '') or
        isnull(dbpi.ProgramIndexTitle, '') != isnull(tpi.title, '')
    )

/*
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

exec pProgramIndexImportJson @SocrataDataMartRawJsonImportID = 1

set statistics time off

*/