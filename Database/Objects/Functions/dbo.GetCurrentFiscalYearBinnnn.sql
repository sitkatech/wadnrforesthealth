
IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.fCurrentFiscalYearBiennium'))
    drop function dbo.fCurrentFiscalYearBiennium
go

/*
Returns the current Fiscal Year Biennium
*/

create function dbo.fCurrentFiscalYearBiennium()
returns int
begin
    return (select top 1 CurrentBienniumFiscalYear  from dbo.CurrentBiennium)
end
go

/*
select dbo.fCurrentFiscalYearBiennium()
*/