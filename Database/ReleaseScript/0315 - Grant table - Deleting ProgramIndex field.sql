--Deleting ProgramIndex from Grant table. Should be associated through GrantAllocation table as many-to-one for GrantAllocation-to-Grant
alter table dbo.[Grant]
drop column ProgramIndex