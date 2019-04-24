create table dbo.GrantModification(
	GrantModificationID int identity(1,1) not null,
	GrantModificationName nvarchar(100) not null,
	GrantID int not null,
	GrantModificationStartDate datetime not null,
	GrantModificationEndDate datetime not null,
	GrantModificationStatusID int not null,
	GrantModificationAmount money not null,
	GrantModificationDescription nvarchar(max) null,
	GrantModificationFileResourceID int null
  constraint PK_GrantModification_GrantModificationID primary key clustered
  (
	  GrantModificationID asc
  ) on [PRIMARY]
)
go

create table dbo.GrantModificationStatus(
	GrantModificationStatusID int not null constraint PK_GrantModificationStatus_GrantModificationStatusID primary key clustered,
	GrantModificationStatusName nvarchar(100) not null
)
go

insert dbo.GrantModificationStatus values
	(1, 'Pending'),
	(2, 'Approved')
go

create table dbo.GrantModificationPurpose(
	GrantModificationPurposeID int not null constraint PK_GrantModificationPurpose_GrantModificationPurposeID primary key clustered,
	GrantModificationPurposeName nvarchar(100) not null
)
go

insert into dbo.GrantModificationPurpose values
	(1, 'Change in Performance Period'),
	(2, 'Administrative Changes'),	
	(3, 'Change in Funding'),	
	(4, 'Other')
go		

create table dbo.GrantModificationGrantModificationPurpose
(
	GrantModificationGrantModificationPurposeID int identity(1,1) not null constraint PK_GrantModificationGrantModificationPurpose_GrantModificationGrantModificationPurposeID primary key clustered,
	GrantModificationID int not null constraint FK_GrantModificationGrantModificationPurpose_GrantModification_GrantModificationID foreign key
		references dbo.GrantModification(GrantModificationID),
	GrantModificationPurposeID int not null constraint FK_GrantModificationGrantModificationPurpose_GrantModificationPurpose_GrantModificationPurposeID foreign key
		references dbo.GrantModificationPurpose(GrantModificationPurposeID)
)


	
	
--	GrantModificationID int not null,
--	IsPerformancePeriodChange bit not null,
--	IsAdministrativeChange bit not null,
--	IsFundingChange bit not null,
--	IsOtherChange bit not null
--)

ALTER TABLE [dbo].GrantModification ADD CONSTRAINT [FK_GrantModification_Grant_GrantID] FOREIGN KEY([GrantID])
REFERENCES [dbo].[Grant] ([GrantID])
GO

ALTER TABLE [dbo].GrantModification ADD CONSTRAINT [FK_GrantModification_GrantModificationStatus_GrantModificationStatusID] FOREIGN KEY([GrantModificationStatusID])
REFERENCES [dbo].[GrantModificationStatus] ([GrantModificationStatusID])
GO

ALTER TABLE [dbo].GrantModification ADD CONSTRAINT [FK_GrantModification_FileResource_GrantModificationFileResourceID_FileResourceID] FOREIGN KEY([GrantModificationFileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO