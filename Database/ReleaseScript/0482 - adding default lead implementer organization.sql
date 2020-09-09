ALTER TABLE dbo.GisUploadSourceOrganization add DefaultLeadImplementerOrganizationID int

ALTER TABLE dbo.GisUploadSourceOrganization add RelationshipTypeForDefaultOrganizationID int

go

update dbo.GisUploadSourceOrganization
set RelationshipTypeForDefaultOrganizationID = 33

update dbo.GisUploadSourceOrganization
set DefaultLeadImplementerOrganizationID = 4704
where GisUploadSourceOrganizationID = 1

update dbo.GisUploadSourceOrganization
set DefaultLeadImplementerOrganizationID = 4708
where GisUploadSourceOrganizationID = 2

update dbo.GisUploadSourceOrganization
set DefaultLeadImplementerOrganizationID = 4704
where GisUploadSourceOrganizationID = 3

update dbo.GisUploadSourceOrganization
set DefaultLeadImplementerOrganizationID = 5797
where GisUploadSourceOrganizationID = 5

update dbo.GisUploadSourceOrganization
set DefaultLeadImplementerOrganizationID = 4704
where GisUploadSourceOrganizationID = 6

go 


ALTER TABLE dbo.GisUploadSourceOrganization ALTER COLUMN DefaultLeadImplementerOrganizationID int not null



ALTER TABLE [dbo].GisUploadSourceOrganization  WITH CHECK ADD  CONSTRAINT [FK_GisUploadSourceOrganization_Organization_DefaultLeadImplementerOrganizationID_OrganizationID] FOREIGN KEY([DefaultLeadImplementerOrganizationID])
REFERENCES [dbo].Organization (OrganizationID)
GO


ALTER TABLE dbo.GisUploadSourceOrganization ALTER COLUMN RelationshipTypeForDefaultOrganizationID int not null



ALTER TABLE [dbo].GisUploadSourceOrganization  WITH CHECK ADD  CONSTRAINT [FK_GisUploadSourceOrganization_RelationshipType_RelationshipTypeForDefaultOrganizationID_RelationshipTypeID] FOREIGN KEY([RelationshipTypeForDefaultOrganizationID])
REFERENCES [dbo].RelationshipType (RelationshipTypeID)
GO




