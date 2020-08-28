CREATE TABLE dbo.GrantModificationFileResource
(
    GrantModificationFileResourceID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_GrantModificationFileResource_GrantModificationFileResourceID PRIMARY KEY,
    GrantModificationID int NOT NULL CONSTRAINT FK_GrantModificationFileResource_GrantModification_GrantModificationID FOREIGN KEY REFERENCES dbo.GrantModification(GrantModificationID),
    FileResourceID int NOT NULL CONSTRAINT FK_GrantModificationFileResource_FileResource_FileResourceID FOREIGN KEY REFERENCES dbo.FileResource(FileResourceID) ON DELETE CASCADE,
    DisplayName varchar(200) NOT NULL,
	Description varchar(1000) NULL,
	CONSTRAINT AK_GrantModificationFileResource_FileResourceID UNIQUE(FileResourceID)
);

INSERT INTO dbo.GrantModificationFileResource
	       (GrantModificationID, FileResourceID)
	SELECT GrantModificationID, GrantModificationFileResourceID
	FROM dbo.GrantModification
		WHERE GrantModificationFileResourceID IS NOT NULL;

ALTER TABLE dbo.GrantModification
    DROP CONSTRAINT FK_GrantModification_FileResource_GrantModificationFileResourceID_FileResourceID;

ALTER TABLE dbo.GrantModification
    DROP COLUMN GrantModificationFileResourceID;

ALTER TABLE dbo.GrantModification REBUILD;

CREATE TABLE dbo.InteractionEventFileResource
(
    InteractionEventFileResourceID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_InteractionEventFileResource_InteractionEventFileResourceID PRIMARY KEY,
    InteractionEventID int NOT NULL CONSTRAINT FK_InteractionEventFileResource_InteractionEvent_InteractionEventID FOREIGN KEY REFERENCES dbo.InteractionEvent(InteractionEventID),
    FileResourceID int NOT NULL CONSTRAINT FK_InteractionEventFileResource_FileResource_FileResourceID FOREIGN KEY REFERENCES dbo.FileResource(FileResourceID) ON DELETE CASCADE,
    DisplayName varchar(200) NOT NULL,
	Description varchar(1000) NULL,
	CONSTRAINT AK_InteractionEventFileResource_FileResourceID UNIQUE(FileResourceID)
);
