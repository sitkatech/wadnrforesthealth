--begin tran

-- Removing leading zeros from ProgramIndexCode again. Making compensatory changes to pProgramIndexImport as well.
update dbo.ProgramIndex 
set ProgramIndexCode =  SUBSTRING(ProgramIndexCode, PATINDEX('%[^0]%', ProgramIndexCode+'.'), LEN(ProgramIndexCode))

--select * from dbo.ProgramIndex

--rollback tran