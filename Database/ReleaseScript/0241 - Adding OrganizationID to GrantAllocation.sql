alter table GrantAllocation
add OrganizationID int null
GO

ALTER TABLE [dbo].[GrantAllocation]  WITH CHECK ADD  CONSTRAINT [FK_GrantAllocation_Organization_OrganizationID] FOREIGN KEY(OrganizationID)
REFERENCES [dbo].Organization (OrganizationID)
GO


