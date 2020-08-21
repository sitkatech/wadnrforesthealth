--select * from dbo.ProjectGrantAllocationRequest

create table dbo.ProjectGrantAllocationRequestFundingSource
(
    ProjectGrantAllocationRequestFundingSourceID int not null constraint PK_ProjectGrantAllocationRequestFundingSource_ProjectGrantAllocationRequestFundingSourceID primary key,
    ProjectGrantAllocationRequestFundingSourceName varchar(150) not null,
    ProjectGrantAllocationRequestFundingSourceDisplayName varchar(150) not null
)

insert dbo.ProjectGrantAllocationRequestFundingSource (ProjectGrantAllocationRequestFundingSourceID, ProjectGrantAllocationRequestFundingSourceName, ProjectGrantAllocationRequestFundingSourceDisplayName) 
values 
(1, 'Federal', 'Federal'),
(2, 'State', 'State'),
(3, 'Private', 'Private'),
(4, 'Other', 'Other')


create table dbo.ProjectGrantAllocationRequestRequestFundingSource
(
    ProjectGrantAllocationRequestRequestFundingSourceID int not null identity(1,1) constraint PK_ProjectGrantAllocationRequestRequestFundingSource_ProjectGrantAllocationRequestRequestFundingSourceID primary key,
    ProjectGrantAllocationRequestID int not null constraint FK_ProjectGrantAllocationRequestRequestFundingSource_ProjectGrantAllocationRequest_ProjectGrantAllocationRequestID foreign key references dbo.ProjectGrantAllocationRequest(ProjectGrantAllocationRequestID),
    ProjectGrantAllocationRequestFundingSourceID int not null constraint FK_ProjectGrantAllocationRequestRequestFundingSource_ProjectGrantAllocationRequestFundingSource_ProjectGrantAllocationRequestFun foreign key references dbo.ProjectGrantAllocationRequestFundingSource(ProjectGrantAllocationRequestFundingSourceID)
)

alter table dbo.ProjectGrantAllocationRequest
add ProjectGrantAllocationRequestNote varchar(max);

alter table dbo.ProjectGrantAllocationRequestUpdate
add ProjectGrantAllocationRequestUPdateNote varchar(max);


--select * from dbo.ProjectGrantAllocationRequest
--select * from dbo.ProjectGrantAllocationRequestUpdate