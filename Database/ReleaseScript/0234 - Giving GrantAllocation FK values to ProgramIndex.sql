-- Inserting foreign key values to ProgramIndex table, dropping column of free-standing Program Index strings afterward
update dbo.GrantAllocation set [ProgramIndexID] = 
	(
		select distinct ProgramIndexID
		from dbo.ProgramIndex [pi]
		where [pi].ProgramIndexAbbrev = ProgramIndex
	)
go
alter table dbo.GrantAllocation drop column ProgramIndex
