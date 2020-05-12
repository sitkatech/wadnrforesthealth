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

    -- Create temp table from the bulk JSON field
    ---------------------------------------------
/*

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

*/

-- Make sure we found something to import before going any further
------------------------------------------------------------------
/*
declare @socrataTempRowCount as bigint
set @socrataTempRowCount= (select count(*) from #programIndexSocrataTemp)
if @socrataTempRowCount = 0
begin
    RAISERROR ('No rows in incoming Program Index temp table #programIndexSocrataTemp', 16, 1)
    --rollback tran
    --return
end
*/

-- Current and Previous Bienniums
---------------------------------
DROP TABLE IF EXISTS #CurrentAndNextBiennium
GO
-- Current biennium
select dbo.fGetCurrentFiscalYearBiennium() as BienniumYear into #CurrentAndNextBiennium
GO
-- Next biennium
insert into #CurrentAndNextBiennium (BienniumYear) values(dbo.fGetCurrentFiscalYearBiennium() + 2)
GO

-- Bienniums found in Incoming data
-----------------------------------
DROP TABLE IF EXISTS #BienniumsFoundInIncomingData;
GO
select distinct pist.biennium as BienniumYear
into #BienniumsFoundInIncomingData
from #programIndexSocrataTemp as pist
GO

--select * from #CurrentAndNextBiennium
--select * from #BienniumsFoundInIncomingData

-- Make sure expected and incoming Bienniums overlap exactly.
-- If they do not, we likely have a problem and should stop.
--------------------------------------------------------------

DECLARE @bienniumsOverlapExactly bit
set @bienniumsOverlapExactly =
(
  CASE 
    WHEN EXISTS (
      SELECT * FROM #CurrentAndNextBiennium as cpb 
      FULL JOIN #BienniumsFoundInIncomingData as bfid ON cpb.BienniumYear = bfid.BienniumYear
      WHERE cpb.BienniumYear IS NULL OR bfid.BienniumYear IS NULL
      )
    -- Elements in tables are not equal'
    THEN 0
    -- Elements in tables are equal
    ELSE 1
  END
)

if @bienniumsOverlapExactly = 0
begin
    RAISERROR ('Incoming Bienniums do not match expected Bienniums!', 16, 1)
    rollback tran
    return
end

-- Get started doing import
----------------------------

-- Remove leading zeros from incoming ProgramIndexCodes before we start comparing, since we store them in the WADNR tables without leading zeroes.
update #programIndexSocrataTemp
set program_index_code = dbo.fRemoveLeadingZeroes(program_index_code)

-- DELETE
-- Delete ProgramIndexes in our table not found in incoming temp table
delete from dbo.ProgramIndex 
where ProgramIndexID in 
(
    select dbpi.ProgramIndexID
    from dbo.ProgramIndex as dbpi
    full outer join #programIndexSocrataTemp as tpi on 
            tpi.program_index_code = dbpi.ProgramIndexCode 
            and 
            tpi.biennium = dbpi.Biennium
    where 
    (tpi.program_index_code is null and tpi.biennium is null)
    and 
    -- Ignore fake code on the Sitka side. 
    -- (Eventually this needs to go away, but not immediately.)
    (dbpi.ProgramIndexCode != '000' and dbpi.ProgramIndexTitle not like '%FAKE%')
)
and
-- Only delete if we are dealing with current or next biennium
Biennium in (select BienniumYear from #CurrentAndNextBiennium)



-- UPDATE
-- Update values for keys found in both sides
update dbpi
set 
    Activity = tpi.activity,
    --Biennium = tpi.biennium,
    Program = tpi.program,
    Subactivity = tpi.subactivity,
    Subprogram = tpi.subprogram,
    ProgramIndexTitle = tpi.title
from 
    dbo.ProgramIndex as dbpi
    join #programIndexSocrataTemp as tpi on tpi.program_index_code = dbpi.ProgramIndexCode and tpi.biennium = dbpi.Biennium
    where 
    (
        isnull(dbpi.Activity, '') != isnull(tpi.activity, '') or
        --isnull(dbpi.Biennium, '') != isnull(tpi.biennium, '') or
        isnull(dbpi.Program, '') != isnull(tpi.program, '') or
        isnull(dbpi.Subactivity, '') != isnull(tpi.subactivity, '') or
        isnull(dbpi.Subprogram, '') != isnull(tpi.subprogram, '') or
        isnull(dbpi.ProgramIndexTitle, '') != isnull(tpi.title, '')
    )

-- INSERT 
-- Insert values not already found in our table
insert into dbo.ProgramIndex (ProgramIndexCode,
                              ProgramIndexTitle,
                              Activity,
                              Biennium,
                              Program,
                              Subprogram,
                              Subactivity)
select tpi.program_index_code,
       tpi.title,
       tpi.activity,
       tpi.biennium,
       tpi.program,
       tpi.subprogram,
       tpi.subactivity
from dbo.ProgramIndex as dbpi
full outer join #programIndexSocrataTemp as tpi on tpi.program_index_code = dbpi.ProgramIndexCode and tpi.biennium = dbpi.Biennium
where  (dbpi.ProgramIndexCode is null and dbpi.Biennium is null)
       and
       (tpi.program_index_code is not null and tpi.biennium is not null)

end
go



/*
select * from dbo.SocrataDataMartRawJsonImport
select * from dbo.ProgramIndex

set statistics time on

exec pProgramIndexImportJson @SocrataDataMartRawJsonImportID = 49

set statistics time off

*/