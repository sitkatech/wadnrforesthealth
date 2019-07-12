IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.pClearGrantAllocationExpenditureTables'))
    DROP PROCEDURE dbo.pClearGrantAllocationExpenditureTables
GO
CREATE PROCEDURE dbo.pClearGrantAllocationExpenditureTables
(
	@bienniumFiscalYear int
)
AS
begin

    delete from dbo.GrantAllocationExpenditureJsonStage
	where Biennium = @bienniumFiscalYear
    --DBCC CHECKIDENT ('dbo.GrantAllocationExpenditureJsonStage', RESEED, 0);

    delete from dbo.GrantAllocationExpenditure
	where Biennium = @bienniumFiscalYear
    --DBCC CHECKIDENT ('dbo.GrantAllocationExpenditure', RESEED, 0);
	
end
go



/*

set statistics time on

exec pClearGrantAllocationExpenditureTables 2013

set statistics time off

select * from [dbo].[SocrataDataMartRawJsonImport]
select * from dbo.GrantAllocationExpenditureJsonStage
select * from dbo.GrantAllocationExpenditure

*/