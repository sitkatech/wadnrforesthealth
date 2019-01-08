create table dbo.FocusAreaStatus (
	FocusAreaStatusID int not null constraint PK_FocusAreaStatus_FocusAreaStatusID primary key,
	FocusAreaStatusName varchar(50) constraint AK_FocusAreaStatus_FocusAreaStatusName unique,
	FocusAreaStatusDisplayName varchar(50) constraint AK_FocusAreaStatus_FocusAreaStatusDisplayName unique
)


CREATE table dbo.FocusArea (
	FocusAreaID int not null identity(1, 1) constraint PK_FocusArea_FocusAreaID primary key,
	TenantID int not null constraint FK_FocusArea_Tenant_TenantID foreign key references dbo.Tenant,
	FocusAreaName varchar(200) not null constraint AK_FocusArea_FocusAreaName unique,
	FocusAreaStatusID int not null constraint FK_FocusArea_FocusAreaStatus_FocusAreaStatusID foreign key references dbo.FocusAreaStatus(FocusAreaStatusID),
	FocusAreaLocation geometry
)


Alter table dbo.Project add FocusAreaID int constraint FK_Project_FocusArea_FocusAreaID foreign key references dbo.FocusArea(FocusAreaID)


insert into dbo.FirmaPageType(FirmaPageTypeID, FirmaPageTypeName, FirmaPageTypeDisplayName, FirmaPageRenderTypeID)
values (55, 'FocusAreasList', 'Focus Areas List', 1)

insert into dbo.FirmaPage(FirmaPageTypeID, TenantID) values(55, 10)