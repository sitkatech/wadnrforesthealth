
-- Since this will be data hotfixed, making these changes re-entrant -- SLG 2/25/2019

if EXISTS(select 1 from dbo.Person where PersonID = 5288 and FirstName = 'Dal' and LastName = 'Marsters')
begin
    -- Delete the damn duplicate Dal
    delete from AuditLog
    where PersonID = 5288

    delete from dbo.Person
    where PersonID = 5288 and FirstName = 'Dal' and LastName = 'Marsters'
end

if not EXISTS(SELECT * FROM sys.indexes WHERE name='UQ_Person_PersonUniqueIdentifier' AND object_id = OBJECT_ID('dbo.Person'))
begin
    -- Prevent duplicate PersonUniqueIdentifier keys (except for null)
    CREATE UNIQUE NONCLUSTERED INDEX UQ_Person_PersonUniqueIdentifier
    ON dbo.Person(PersonUniqueIdentifier)
    WHERE PersonUniqueIdentifier IS NOT NULL;
end


--/*
--alter table dbo.Person
--add constraint UK_Person_PersonUniqueIdentifier unique(PersonUniqueIdentifier);
--*/


--select p.PersonUniqueIdentifier, 
--       count(*)
--from dbo.Person as p
--group by PersonUniqueIdentifier
--having count(*) > 1


--select * from dbo.Person
--where PersonUniqueIdentifier is null

----select * from dbo.Person
--where PersonUniqueIdentifier = 'DP4LM5FL0TM6F-4PQ8VL4PQ-DD7WV4ZZ8D-9VL7MQ6QL'

--select * from Organization
--where OrganizationID in (4702, 4703)