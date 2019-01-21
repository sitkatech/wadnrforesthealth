
insert into dbo.GrantNote(GrantID,GrantNoteText, CreatedByPersonID, CreatedDate)


select g.GrantID
, tcg.Notes
, 5251 -- Christel
, GETDATE()
 from 
 dbo.[Grant] g
 join dbo.tmpGrantsFlatFile tcg on tcg.Grant_Number = GrantNumber
 where tcg.Notes is not null

