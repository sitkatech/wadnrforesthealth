-- Changing column order so that RegionAbbrev appears earlier in the table definition. 
-- SQL Server doesn't care, but us human users do. -- SLG

CREATE TABLE dbo.Tmp_Region
	(
	RegionID int NOT NULL,
	RegionAbbrev nvarchar(10) NULL,
	RegionName varchar(100) NOT NULL,
	RegionLocation geometry NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Region SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.Region)
	 EXEC('INSERT INTO dbo.Tmp_Region (RegionID, RegionAbbrev, RegionName, RegionLocation)
		SELECT RegionID, RegionAbbrev, RegionName, RegionLocation FROM dbo.Region WITH (HOLDLOCK TABLOCKX)')
GO
ALTER TABLE dbo.ProjectRegion
	DROP CONSTRAINT FK_ProjectRegion_Region_RegionID
GO
ALTER TABLE dbo.ProjectRegionUpdate
	DROP CONSTRAINT FK_ProjectRegionUpdate_Region_RegionID
GO
ALTER TABLE dbo.PersonStewardRegion
	DROP CONSTRAINT FK_PersonStewardRegion_Region_RegionID
GO
ALTER TABLE dbo.FocusArea
	DROP CONSTRAINT FK_FocusArea_Region_RegionID
GO
ALTER TABLE dbo.GrantAllocation
	DROP CONSTRAINT FK_GrantAllocation_Region_RegionID
GO
ALTER TABLE dbo.Agreement
	DROP CONSTRAINT FK_Agreement_Region_RegionID
GO
DROP TABLE dbo.Region
GO
EXECUTE sp_rename N'dbo.Tmp_Region', N'Region', 'OBJECT' 
GO
ALTER TABLE dbo.Region ADD CONSTRAINT
	PK_Region_RegionID PRIMARY KEY CLUSTERED 
	(
	RegionID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Region ADD CONSTRAINT
	AK_Region_RegionName UNIQUE NONCLUSTERED 
	(
	RegionName
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Agreement ADD CONSTRAINT
	FK_Agreement_Region_RegionID FOREIGN KEY
	(
	RegionID
	) REFERENCES dbo.Region
	(
	RegionID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Agreement SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.GrantAllocation ADD CONSTRAINT
	FK_GrantAllocation_Region_RegionID FOREIGN KEY
	(
	RegionID
	) REFERENCES dbo.Region
	(
	RegionID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.GrantAllocation SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.FocusArea ADD CONSTRAINT
	FK_FocusArea_Region_RegionID FOREIGN KEY
	(
	RegionID
	) REFERENCES dbo.Region
	(
	RegionID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.FocusArea SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.PersonStewardRegion ADD CONSTRAINT
	FK_PersonStewardRegion_Region_RegionID FOREIGN KEY
	(
	RegionID
	) REFERENCES dbo.Region
	(
	RegionID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.PersonStewardRegion SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.ProjectRegionUpdate ADD CONSTRAINT
	FK_ProjectRegionUpdate_Region_RegionID FOREIGN KEY
	(
	RegionID
	) REFERENCES dbo.Region
	(
	RegionID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.ProjectRegionUpdate SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.ProjectRegion ADD CONSTRAINT
	FK_ProjectRegion_Region_RegionID FOREIGN KEY
	(
	RegionID
	) REFERENCES dbo.Region
	(
	RegionID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.ProjectRegion SET (LOCK_ESCALATION = TABLE)
GO
