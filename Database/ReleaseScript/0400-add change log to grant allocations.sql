--begin tran
CREATE TABLE dbo.GrantAllocationChangeLog
	(
	GrantAllocationChangeLogID int identity(1,1) NOT NULL,
	GrantAllocationID int NOT NULL,
	GrantAllocationAmountOldValue money NULL,
	GrantAllocationAmountNewValue money NULL,
	GrantAllocationAmountNote nvarchar(2000) NULL,
	ChangePersonID int NOT NULL,
	ChangeDate datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.GrantAllocationChangeLog ADD CONSTRAINT
	PK_GrantAllocationChangeLog_GrantAllocationChangeLogID PRIMARY KEY CLUSTERED 
	(
	GrantAllocationChangeLogID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

ALTER TABLE dbo.GrantAllocationChangeLog ADD CONSTRAINT
	FK_GrantAllocationChangeLog_GrantAllocation_GrantAllocationID Foreign KEY 
	(
	GrantAllocationID
	) references dbo.GrantAllocation (GrantAllocationID)

ALTER TABLE dbo.GrantAllocationChangeLog ADD CONSTRAINT
    --FK_GrantAllocationChangeLog_ChangePersonID_Person_PersonID
	FK_GrantAllocationChangeLog_Person_ChangePersonID_PersonID Foreign KEY 
	(
	ChangePersonID
	) references dbo.Person (PersonID)
--rollback tran
