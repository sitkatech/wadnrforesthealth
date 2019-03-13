--SUBSTRING(str_col, PATINDEX('%[^0]%', str_col+'.'), LEN(str_col))

select * from dbo.tmpProjectCode

UPDATE dbo.tmpProjectCode
SET project = SUBSTRING(project, PATINDEX('%[^0]%', project+'.'), LEN(project))


select * from dbo.tmpProgramIndex

UPDATE dbo.tmpProgramIndex
SET program_index = SUBSTRING(program_index, PATINDEX('%[^0]%', program_index+'.'), LEN(program_index))
