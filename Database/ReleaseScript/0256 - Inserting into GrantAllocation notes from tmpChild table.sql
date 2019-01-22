insert into dbo.GrantAllocationNote(GrantAllocationID,GrantAllocationNoteText, CreatedByPersonID, CreatedDate)


select ga.GrantAllocationID
, tcg.Notes
, 5251 -- Christel
, GETDATE()
 from 
 dbo.[GrantAllocation] ga
 join dbo.tmpChildrenGrantsInGrantsTab tcg on tcg.GrantAllocationID = ga.GrantAllocationID
 where tcg.Notes is not null