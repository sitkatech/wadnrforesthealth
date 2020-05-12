
IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.fGetFiscalYearBienniumForDate'))
    drop function dbo.fGetFiscalYearBienniumForDate
go

/*
Returns the Fiscal Year Biennium for a given date
*/

create function dbo.fGetFiscalYearBienniumForDate(@dateToCheck DATETIME)
returns int
begin
    declare @yearToCheck int
    set @yearToCheck = (SELECT YEAR(@dateToCheck))

    declare @monthToCheck int
    set @monthToCheck = (SELECT MONTH(@dateToCheck))

    declare @dayNumberToCheck int
    set @dayNumberToCheck = (SELECT DAY(@dateToCheck))

    --return (select top 1 CurrentBienniumFiscalYear  from dbo.CurrentBiennium)
    declare @isOddYear bit
    set @isOddYear = (@yearToCheck % 2)

    declare @afterJulyFirst bit
    set @afterJulyFirst = (case when @monthToCheck >= 7 then 1 else 0 end) --asdfadfsad

    if ((@isOddYear = 1) and (@afterJulyFirst = 1))
    begin
        return @yearToCheck;
    end

    if ((@isOddYear = 1) and (@afterJulyFirst = 0))
    begin
        return @yearToCheck - 2;
    end

    -- Even year, after July 1st
    if ((@isOddYear = 0) and (@afterJulyFirst = 1))
    begin
        return 99999;
    end

    return 999999999123213;

end
go

/*

select dbo.fGetFiscalYearBienniumForDate(GETDATE())
SELECT 1 FROM dbo.Project

SELECT dbo.fGetFiscalYearBienniumForDate(GETDATE())

SELECT dbo.fGetFiscalYearBienniumForDate('1/1/2017')

SELECT dbo.fGetFiscalYearBienniumForDate('1/1/2018')

("1/1/2018"))  == 2017, 




*/