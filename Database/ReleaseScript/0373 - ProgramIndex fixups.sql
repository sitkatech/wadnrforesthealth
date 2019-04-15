
-- Changing my mind about trying to adjust ProgramCodes. Re-introducing leading zeros.

-- Changing my mind about trying to adjust ProgramCodes. Re-introducing leading zeros.
update ProgramIndex
set ProgramIndexCode = '00' + ProgramIndexCode
GO

-- Adding neglected column
alter table ProgramIndex
add Subactivity varchar(max) null
