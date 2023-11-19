BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
EXECUTE sp_rename N'dbo.ProjectPersonRelationshipType.IsRestrictedToAdminAndProjectSteward', N'Tmp_IsRestrictedToAdminAndProjectStewardAndCanViewLandownerInfo', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.ProjectPersonRelationshipType.Tmp_IsRestrictedToAdminAndProjectStewardAndCanViewLandownerInfo', N'IsRestrictedToAdminAndProjectStewardAndCanViewLandownerInfo', 'COLUMN' 
GO
ALTER TABLE dbo.ProjectPersonRelationshipType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
