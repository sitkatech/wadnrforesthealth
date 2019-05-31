IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.pClearGrantAllocationExpenditureTables'))
    DROP PROCEDURE dbo.pClearGrantAllocationExpenditureTables
GO
CREATE PROCEDURE dbo.pClearGrantAllocationExpenditureTables
AS
begin

    delete from dbo.GrantAllocationExpenditureJsonStage
    DBCC CHECKIDENT ('dbo.GrantAllocationExpenditureJsonStage', RESEED, 0);

    delete from dbo.GrantAllocationExpenditure
    DBCC CHECKIDENT ('dbo.GrantAllocationExpenditure', RESEED, 0);

end
go



/*

set statistics time on

exec pClearGrantAllocationExpenditureTables 

set statistics time off

*/