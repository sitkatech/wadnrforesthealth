IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.pGrantExpenditureImportJson'))
    DROP PROCEDURE dbo.pGrantExpenditureImportJson
GO

CREATE PROCEDURE dbo.pGrantExpenditureImportJson
(
    @SocrataDataMartRawJsonImportID int null,
    @ClearTableBeforeLoad bit null
)
AS
begin


if (@ClearTableBeforeLoad = 1)
begin
    delete from GrantAllocationExpenditure
end


/*

JSON format:
{
\"Biennium\":\"2019\",
\"FiscalMo\":\"22\",
\"FiscalAdjMo\":\"00\",
\"CalYr\":2019,\
"MoString\":\"April\",
\"SourceSystem\":\"FES\",
\"DocNo\":\"11490119\",
\"DocSuffix\":\"FM\",
\"DocDate\":\"2019-04-16T00:00:00\",
\"InvoiceDesc\":\"- WATECH INVOICE 03/2019\",
\"InvoiceDate\":\"2019-04-01T00:00:00\",
\"InvoiceNo\":\"90112019030311\",
\"GlAcctNo\":\"6505\",
\"ObjCd\":\"E\",
\"ObjName\":\"GOODS AND SERVICES\",
\"SubObjCd\":\"EB\",
\"SubObjName\":\"COMMUNICATIONS/TELECOMMUNICATIONS\",
\"SubSubObjCd\":\"B030\",
\"SubSubObjName\":\"STATE PROVIDED TELECOMMUNICATION SERVICE\",
\"ApprnCd\":\"965\",
\"ApprnName\":\"SALARIES AND EXPENSES\",
\"FundCd\":\"190\",
\"FundName\":\"FOREST FIRE PROTECTION ASSESSMENT\",
\"OrgCd\":\"2300\",
\"OrgName\":\"NORTHEAST REGION\",
\"ProgIdxCd\":\"00214\",
\"ProgIdxName\":\"214-PREPAREDNESS\",
\"ProgCd\":\"020\",
\"ProgName\":\"RESOURCE PROTECTION AND REGULATION\",
\"SubProgCd\":\"01\",
\"SubProgName\":\"WILDFIRE DIVISION\",
\"ActivityCd\":\"01\",
\"ActivityName\":\"FIRE ADMINISTRATION AND PREPAREDNESS\",
\"SubActivityCd\":\"02\",
\"SubActivityName\":\"214-PREPAREDNESS\",
\"ProjectCd\":\"0KPV\",
\"ProjectName\":\"NEWICC\",
\"VendorNo\":\"SWV0098113\",
\"VendorName\":\"CTS SERVICES\",
\"ExpendAccrued\":446.98
},

*/

-- select * from dbo.SocrataDataMartRawJsonImport
-- drop #GrantExpenditureSocrataTemp

    -- Create temp table from the bulk JSON field
    ---------------------------------------------

    SELECT GrantExpenditureTemp.*
    into #GrantExpenditureSocrataTemp
    --FROM OPENROWSET (BULK '{pathToVendorJsonFile}', SINGLE_CLOB) as j
    from (select rji.RawJsonString from dbo.SocrataDataMartRawJsonImport as rji where rji.SocrataDataMartRawJsonImportID = @SocrataDataMartRawJsonImportID) as j 
    --from (select rji.RawJsonString from dbo.SocrataDataMartRawJsonImport as rji where rji.SocrataDataMartRawJsonImportID = 4) as j 
    CROSS APPLY OPENJSON(RawJsonString)
    WITH
    (
        Biennium int,
        FiscalMo int,
        FiscalAdjMo int,
        CalYr int,
        MoString varchar(20),
        SourceSystem varchar(50),
        DocNo varchar(200),
        DocSuffix varchar(10),
        DocDate datetime,
        InvoiceDesc varchar(max),
        InvoiceDate datetime,
        GlAcctNo int,
        -- Object Code
        ObjCd varchar(20),
        ObjName varchar(max),
        SubObjCd varchar(20),
        SubObjName varchar(max),
        SubSubObjCd varchar(20),
        SubSubObjName varchar(max),
        ApprnCd varchar(20),
        ApprnName varchar(max),
        FundCd varchar(20),
        FundName varchar(max),
        OrgCd varchar(20),
        OrgName varchar(max),
        -- Program Index
        ProgIdxCd varchar(20),
        ProgIdxName varchar(max),
        ProgCd varchar(20),
        ProgName varchar(max),
        SubProgCd varchar(20),
        SubProgName varchar(max),
        ActivityCd varchar(20),
        ActivityName varchar(max),
        SubActivityCd varchar(20),
        SubActivityName varchar(max),
        -- Project Code
        ProjectCd varchar(20),
        ProjectName varchar(max),
        VendorNo varchar(100),
        VendorName varchar(max),
        ExpendAccrued money
    )
    AS GrantExpenditureTemp

    -- select * from  #GrantExpenditureSocrataTemp where CalYr = 2017

---- Remove leading zeros from incoming ProjectCodes before we start comparing, since we store them in the WADNR tables without leading zeroes.
--update #GrantExpenditureSocrataTemp
--set ProjectCd = dbo.fRemoveLeadingZeroes(ProjectCd)


-- HACK 
-- Start with clearing the table initially, just to get the brute force load done.
-- After this works, we can think about incremental updates.
-- SLG 5/23/2019 - 10:30 AM
--delete from GrantAllocationExpenditure


-- Insert GrantExpenditures into 
insert into [GrantAllocationExpenditure] 
    (
        GrantAllocationID,
        CostTypeID,
        Biennium,
        FiscalMonth,
        CalendarYear,
        CalendarMonth,
        ExpenditureAmount
    )
select
    distinct
    gapc.GrantAllocationID,
    ctdm.CostTypeID as CostTypeID, -- CostTypeID 
    tgp.Biennium,
    tgp.FiscalMo,
    tgp.CalYr as CalendarYear,
    (select dbo.fGetCalendarMonthIndexFromMonthString(tgp.MoString)) as CalendarMonth,
    tgp.ExpendAccrued
from GrantAllocationProgramIndexProjectCode as gapc
inner join ProgramIndex as pin on gapc.ProgramIndexID = pin.ProgramIndexID
inner join ProjectCode as pc on gapc.ProjectCodeID = pc.ProjectCodeID
--inner join [dbo].[tmp2015-19_grant_payments_singlesheet] as tgp 
inner join #GrantExpenditureSocrataTemp as tgp
        on 
        dbo.fRemoveLeadingZeroes(tgp.ProjectCd) = pc.ProjectCodeName 
        and 
        dbo.fRemoveLeadingZeroes(tgp.ProgIdxCd) = pin.ProgramIndexCode
inner join dbo.CostTypeDatamartMapping as ctdm on ctdm.DatamartObjectCode = tgp.ObjCd
order by CalendarYear, CalendarMonth, CostTypeID


--select * from #GrantExpenditureSocrataTemp as tgp 

--select * from dbo.CostTypeDatamartMapping

--select * from dbo.GrantAllocationExpenditure

--select * from dbo.GrantAllocationExpenditure


---- DELETE
---- Delete ProjectCodes in our table not found in incoming temp table
--delete from dbo.ProjectCode 
--where ProjectCodeID in 
--(
--    select dbpc.ProjectCodeID
--    from dbo.ProjectCode as dbpc
--    full outer join #GrantExpenditureSocrataTemp as tpc on tpc.ProjectCd = dbpc.ProjectCodeName
--    where (tpc.ProjectCd is null)
--)


---- UPDATE
---- Update values for keys found in both sides
--update dbpc
--set 
--    ProjectCodeTitle = tpc.title,
--    CreateDate = tpc.create_date,
--    ProjectStartDate = tpc.project_start_date,
--    ProjectEndDate = tpc.project_end_date
--from 
--    dbo.ProjectCode as dbpc
--    join #ProjectCodeSocrataTemp as tpc on tpc.project_code = dbpc.ProjectCodeName
--    where 
--    (
--        isnull(dbpc.ProjectCodeTitle, '') != isnull(tpc.title, '') or
--        isnull(dbpc.CreateDate, '') != isnull(tpc.create_date, '') or
--        isnull(dbpc.ProjectStartDate, '') != isnull(tpc.project_start_date, '') or
--        isnull(dbpc.ProjectEndDate, '') != isnull(tpc.project_end_date, '')
--    )


---- INSERT 
---- Insert values not already found in our table
--insert into dbo.ProjectCode (ProjectCodeName,
--                             ProjectCodeTitle,
--                             CreateDate,
--                             ProjectStartDate,
--                             ProjectEndDate)
--select tpc.project_code,
--       tpc.title,
--       tpc.create_date,
--       tpc.project_start_date,
--       tpc.project_end_date
--from dbo.ProjectCode as dbpc
--full outer join #ProjectCodeSocrataTemp as tpc on tpc.project_code = dbpc.ProjectCodeName
--where  (dbpc.ProjectCodeName is null)


end
go



/*
select * from dbo.SocrataDataMartRawJsonImport

set statistics time on

exec pGrantExpenditureImportJson @SocrataDataMartRawJsonImportID = 5, @clearTableBeforeLoad = 1

set statistics time off

*/