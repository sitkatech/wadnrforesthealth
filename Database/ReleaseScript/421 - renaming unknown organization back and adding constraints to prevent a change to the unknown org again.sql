--select * from dbo.AuditLog
--where TableName = 'Organization'

--select * from dbo.Person where PersonID = 5427


--select * from dbo.Person where OrganizationID = 4703



alter table dbo.Organization
add IsEditable bit;
go

update dbo.Organization
set IsEditable = 1;

alter table dbo.Organization
alter column IsEditable bit not null;
go

update dbo.Organization
set OrganizationName = '(Unknown or Unspecified Organization)', OrganizationTypeID = 1065, OrganizationShortName = 'N/A', IsEditable = 0
where OrganizationID = 4703
go
/*

CREATE TRIGGER sampleTrigger
    ON database1.dbo.table1
    FOR DELETE
AS
    DELETE FROM database2.dbo.table2
    WHERE bar = 4 AND ID IN(SELECT deleted.id FROM deleted)
GO

*/


/*

CREATE TRIGGER preventDeleteOfUnknownOrganization ON dbo.Organization
FOR DELETE AS
    
    if exists (select d.OrganizationID from deleted as d where d.isEditable = 0)
    begin
         RAISERROR('Attempted to delete Organization with IsEditable set to false!', 16, 1)
         rollback tran
         return
    end
GO

CREATE TRIGGER preventUpdateOfUnknownOrganization ON dbo.Organization
FOR UPDATE AS
    
    if exists (select d.OrganizationID from deleted as d where d.isEditable = 0)
    begin
         RAISERROR('Attempted to update Organization with IsEditable set to false!', 16, 1)
         rollback tran
         return
    end
GO




INSERT INTO [dbo].[Organization]
           ([OrganizationName]
           ,[OrganizationShortName]
           ,[IsActive]
           ,[IsEditable]
           ,OrganizationTypeID)
     VALUES
           ('TEST ORG'
           , 'TEST'   
           ,1
           ,0
           ,1065
           )
GO

delete from dbo.Organization where OrganizationName = 'TEST ORG'

update dbo.Organization set OrganizationShortName = 'THIS IS BAD' where OrganizationName = 'TEST ORG'

*/