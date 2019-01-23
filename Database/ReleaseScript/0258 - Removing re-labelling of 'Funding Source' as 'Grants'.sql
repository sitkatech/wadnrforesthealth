-- Remove re-labelling of 'Funding Source' as 'Grants'.
-- Wadnr now has proper tables for Grants and Grant Allocation that
-- replace this.
update FieldDefinitionData
set FieldDefinitionLabel = null
where FieldDefinitionDataID = 1472