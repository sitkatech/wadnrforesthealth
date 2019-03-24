-- We're using email no need for PersonEnvironmentCredential any longer
drop table dbo.PersonEnvironmentCredential
-- This column is unused, removing it
alter table dbo.Person drop column loginname