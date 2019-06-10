if exists (select * from dbo.sysobjects where id = object_id('dbo.vSocrataDataMartRawJsonImportIndex'))
    drop view dbo.vSocrataDataMartRawJsonImportIndex
go

create view dbo.vSocrataDataMartRawJsonImportIndex
as
select 
    rj.SocrataDataMartRawJsonImportID,
    rj.CreateDate,
    rj.SocrataDataMartRawJsonImportTableTypeID,
    tt.SocrataDataMartRawJsonImportTableTypeName,
    DATALENGTH(rj.RawJsonString) as RawJsonStringLength
from dbo.SocrataDataMartRawJsonImport as rj
inner join SocrataDataMartRawJsonImportTableType as tt on rj.SocrataDataMartRawJsonImportTableTypeID = tt.SocrataDataMartRawJsonImportTableTypeID

/*

select * from dbo.SocrataDataMartRawJsonImport
select * from dbo.vSocrataDataMartRawJsonImportIndex

*/