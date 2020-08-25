--select * from dbo.ProjectGrantAllocationRequest

create table dbo.ProjectFundingSource
(
    ProjectFundingSourceID int not null constraint PK_ProjectFundingSource_ProjectFundingSourceID primary key,
    ProjectFundingSourceName varchar(150) not null,
    ProjectFundingSourceDisplayName varchar(150) not null
)

insert dbo.ProjectFundingSource (ProjectFundingSourceID, ProjectFundingSourceName, ProjectFundingSourceDisplayName) 
values 
(1, 'Federal', 'Federal'),
(2, 'State', 'State'),
(3, 'Private', 'Private'),
(4, 'Other', 'Other')



alter table dbo.Project
add ProjectFundingSourceID int null constraint FK_Project_ProjectFundingSource_ProjectFundingSourceID foreign key references dbo.ProjectFundingSource(ProjectFundingSourceID);

alter table dbo.Project
add ProjectFundingSourceNotes varchar(4000)

