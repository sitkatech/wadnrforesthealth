
create table dbo.GrantAllocationSource (
	GrantAllocationSourceID int not null, 
	GrantAllocationSourceName varchar(200) not null,
	GrantAllocationSourceDisplayName varchar(200) not null,
	SortOrder int not null,
	constraint [PK_GrantAllocationSource_GrantAllocationSourceID] PRIMARY KEY CLUSTERED 
	(
		GrantAllocationSourceID ASC
	)
)
go

alter table dbo.GrantAllocation
add Priority int null
go

alter table dbo.GrantAllocation
add HasFundFSPs bit null
go

alter table dbo.GrantAllocation
add GrantAllocationSourceID int null
go

ALTER TABLE [dbo].GrantAllocation WITH CHECK ADD  CONSTRAINT [FK_GrantAllocation_GrantAllocationSource_GrantAllocationSourceID] FOREIGN KEY(GrantAllocationSourceID)
REFERENCES [dbo].GrantAllocationSource (GrantAllocationSourceID)
GO

create table dbo.GrantAllocationLikelyPerson
(
	GrantAllocationLikelyUserID int identity(1, 1) not null,
	GrantAllocationID int not null,
	PersonID int not null,

	constraint [PK_GrantAllocationLikelyUser_GrantAllocationLikelyUserID] PRIMARY KEY CLUSTERED 
	(
		GrantAllocationLikelyUserID ASC
	)
)

ALTER TABLE [dbo].GrantAllocationLikelyPerson WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationLikelyPerson_GrantAllocation_GrantAllocationID] FOREIGN KEY(GrantAllocationID)
REFERENCES [dbo].GrantAllocation (GrantAllocationID)
GO

ALTER TABLE [dbo].GrantAllocationLikelyPerson WITH CHECK ADD  CONSTRAINT [FK_GrantAllocationLikelyPerson_Person_PersonID] FOREIGN KEY(PersonID)
REFERENCES [dbo].Person (PersonID)
GO

ALTER TABLE [dbo].GrantAllocationLikelyPerson ADD  CONSTRAINT [AK_GrantAllocationLikelyPerson_GrantAllocationID_PersonID] UNIQUE NONCLUSTERED 
(
	[GrantAllocationID] ASC,
	[PersonID] ASC
) ON [PRIMARY]
GO

