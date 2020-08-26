--select * from dbo.ProjectGrantAllocationRequest

create table dbo.FundingSource
(
    FundingSourceID int not null constraint PK_FundingSource_FundingSourceID primary key,
    FundingSourceName varchar(150) not null,
    FundingSourceDisplayName varchar(150) not null
)

insert dbo.FundingSource (FundingSourceID, FundingSourceName, FundingSourceDisplayName) 
values 
(1, 'Federal', 'Federal'),
(2, 'State', 'State'),
(3, 'Private', 'Private'),
(4, 'Other', 'Other')



--alter table dbo.Project
--add FundingSourceID int null constraint FK_Project_FundingSource_FundingSourceID foreign key references dbo.FundingSource(FundingSourceID);

alter table dbo.Project
add ProjectFundingSourceNotes varchar(4000)

create table dbo.ProjectFundingSource
(
    ProjectFundingSourceID int not null identity(1,1) constraint PK_ProjectFundingSource_ProjectFundingSourceID primary key,
    ProjectID int not null constraint FK_ProjectFundingSource_Project_ProjectID foreign key references dbo.Project(ProjectID),
    FundingSourceID int not null constraint FK_ProjectFundingSource_FundingSource_FundingSourceID foreign key references dbo.FundingSource(FundingSourceID)
)


alter table dbo.ProjectUpdate
add ProjectFundingSourceNotes varchar(4000)

create table dbo.ProjectFundingSourceUpdate
(
    ProjectFundingSourceUpdateID int not null identity(1,1) constraint PK_ProjectFundingSourceUpdate_ProjectFundingSourceUpdateID primary key,
    ProjectUpdateBatchID int not null constraint FK_ProjectFundingSourceUpdate_ProjectUpdateBatch_ProjectUpdateBatchID foreign key references dbo.ProjectUpdateBatch(ProjectUpdateBatchID),
    FundingSourceID int not null constraint FK_ProjectFundingSourceUpdate_FundingSource_FundingSourceID foreign key references dbo.FundingSource(FundingSourceID)
)
