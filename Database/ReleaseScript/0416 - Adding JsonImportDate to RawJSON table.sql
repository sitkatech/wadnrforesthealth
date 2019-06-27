
ALTER TABLE dbo.SocrataDataMartRawJsonImport
	DROP CONSTRAINT FK_SocrataDataMartRawJsonImport_JsonImportStatusType_JsonImportStatusTypeID
GO
ALTER TABLE dbo.JsonImportStatusType SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.SocrataDataMartRawJsonImport
	DROP CONSTRAINT FK_SocrataDataMartRawJsonImport_SocrataDataMartRawJsonImportTableType_SocrataDataMartRawJsonImportTableTypeID
GO
ALTER TABLE dbo.SocrataDataMartRawJsonImportTableType SET (LOCK_ESCALATION = TABLE)
GO
CREATE TABLE dbo.Tmp_SocrataDataMartRawJsonImport
	(
	SocrataDataMartRawJsonImportID int NOT NULL IDENTITY (1, 1),
	CreateDate datetime NOT NULL,
	SocrataDataMartRawJsonImportTableTypeID int NOT NULL,
	FinanceApiLastLoadDate datetime NULL,
	RawJsonString varchar(MAX) NOT NULL,
	JsonImportDate datetime NULL,
	JsonImportStatusTypeID int NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SocrataDataMartRawJsonImport SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_SocrataDataMartRawJsonImport ON
GO
IF EXISTS(SELECT * FROM dbo.SocrataDataMartRawJsonImport)
	 EXEC('INSERT INTO dbo.Tmp_SocrataDataMartRawJsonImport (SocrataDataMartRawJsonImportID, CreateDate, SocrataDataMartRawJsonImportTableTypeID, FinanceApiLastLoadDate, RawJsonString, JsonImportStatusTypeID)
		SELECT SocrataDataMartRawJsonImportID, CreateDate, SocrataDataMartRawJsonImportTableTypeID, FinanceApiLastLoadDate, RawJsonString, JsonImportStatusTypeID FROM dbo.SocrataDataMartRawJsonImport WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SocrataDataMartRawJsonImport OFF
GO
DROP TABLE dbo.SocrataDataMartRawJsonImport
GO
EXECUTE sp_rename N'dbo.Tmp_SocrataDataMartRawJsonImport', N'SocrataDataMartRawJsonImport', 'OBJECT' 
GO
ALTER TABLE dbo.SocrataDataMartRawJsonImport ADD CONSTRAINT
	PK_SocrataDataMartRawJsonImport_SocrataDataMartRawJsonImportID PRIMARY KEY CLUSTERED 
	(
	SocrataDataMartRawJsonImportID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SocrataDataMartRawJsonImport ADD CONSTRAINT
	FK_SocrataDataMartRawJsonImport_SocrataDataMartRawJsonImportTableType_SocrataDataMartRawJsonImportTableTypeID FOREIGN KEY
	(
	SocrataDataMartRawJsonImportTableTypeID
	) REFERENCES dbo.SocrataDataMartRawJsonImportTableType
	(
	SocrataDataMartRawJsonImportTableTypeID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SocrataDataMartRawJsonImport ADD CONSTRAINT
	FK_SocrataDataMartRawJsonImport_JsonImportStatusType_JsonImportStatusTypeID FOREIGN KEY
	(
	JsonImportStatusTypeID
	) REFERENCES dbo.JsonImportStatusType
	(
	JsonImportStatusTypeID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
