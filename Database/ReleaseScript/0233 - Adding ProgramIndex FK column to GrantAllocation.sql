alter table dbo.GrantAllocation add 
	ProgramIndexID int null
	CONSTRAINT [FK_GrantAllocation_ProgramIndex_ProgramIndexID] references dbo.[ProgramIndex](ProgramIndexID)

