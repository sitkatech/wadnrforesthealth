CREATE TABLE dbo.ActivityType(
	ActivityTypeID int NOT NULL constraint PK_ActivityType_ActivityTypeID primary key,
	ActivityTypeName varchar(50) NOT NULL constraint AK_ActivityType_ActivityTypeName unique,
	ActivityTypeDisplayName varchar(50) NOT NULL constraint AK_ActivityType_ActivityTypeDisplayName unique
)

CREATE TABLE dbo.TreatmentType(
	TreatmentTypeID int NOT NULL constraint PK_TreatmentType_TreatmentTypeID primary key,
	TreatmentTypeName varchar(50) NOT NULL constraint AK_TreatmentType_TreatmentTypeName unique,
	TreatmentTypeDisplayName varchar(50) NOT NULL constraint AK_TreatmentType_TreatmentTypeDisplayName unique
)

Insert into dbo.ActivityType (ActivityTypeID, ActivityTypeName, ActivityTypeDisplayName)
values
(1, 'Travel', 'Travel'),
(2, 'StaffTime', 'Staff Time'),
(3, 'Treatment', 'Treatment'),
(4, 'ContractorTime', 'Contractor Time'),
(5, 'Supplies', 'Supplies')

Create Table dbo.StaffTimeActivity(
	StaffTimeActivityID int not null identity(1,1) constraint PK_StaffTimeActivity_StaffTimeActivityID primary key,
	TenantID int not null constraint FK_StaffTimeActivity_Tenant_TenantID foreign key references dbo.Tenant(TenantID),
	ProjectID int not null constraint FK_StaffTimeActivity_Project_ProjectID foreign key references dbo.Project(ProjectID),
	FundingSourceID int not null constraint FK_StaffTimeActivity_FundingSource_FundingSourceID foreign key references dbo.FundingSource(FundingSourceID),
	StaffTimeActivityHours decimal not null,
	StaffTimeActivityRate money not null,
	StaffTimeActivityStartDate datetime not null,
	StaffTimeActivityEndDate datetime null,
	StaffTimeActivityNotes varchar(max) null,
	Constraint AK_StaffTimeActivity_StaffTimeActivityID_TenantID unique(StaffTimeActivityID, TenantID),
	Constraint FK_StaffTimeActivity_Project_ProjectID_TenantID foreign key (ProjectID, TenantID) references dbo.Project(ProjectID, TenantID),
	Constraint FK_StaffTimeActivity_FundingSource_FundingSourceID_TenantID foreign key (FundingSourceID, TenantID) references dbo.FundingSource(FundingSourceID, TenantID)
)

Create Table dbo.TreatmentActivity(
	TreatmentActivityID int not null identity(1,1) constraint PK_TreatmentActivity_TreatmentActivityID primary key,
	TenantID int not null constraint FK_TreatmentActivity_Tenant_TenantID foreign key references dbo.Tenant(TenantID),
	ProjectID int not null constraint FK_TreatmentActivity_Project_ProjectID foreign key references dbo.Project(ProjectID),
	FundingSourceID int not null constraint FK_TreatmentActivity_FundingSource_FundingSourceID foreign key references dbo.FundingSource(FundingSourceID),
	TreatmentActivityFootprintAcres decimal not null,
	TreatmentActivityBrushControlAcres decimal not null,
	TreatmentActivityThinningAcres decimal not null,
	TreatmentActivityPruningAcres decimal not null,
	TreatmentActivitySlashAcres decimal not null,
	TreatmentActivityPrescribedBurnAcres decimal not null,
	TreatmentActivityAllocatedAmount decimal not null,
	TreatmentActivityTotalCost decimal not null,
	TreatmentActivityGrantCost decimal not null,
	TreatmentActivityStartDate datetime not null,
	TreatmentActivityEndDate datetime null,
	TreatmentActivityNotes varchar(max) null,
	Constraint AK_TreatmentActivity_TreatmentActivityID_TenantID unique(TreatmentActivityID, TenantID),
	Constraint FK_TreatmentActivity_Project_ProjectID_TenantID foreign key (ProjectID, TenantID) references dbo.Project(ProjectID, TenantID),
	Constraint FK_TreatmentActivity_FundingSource_FundingSourceID_TenantID foreign key (FundingSourceID, TenantID) references dbo.FundingSource(FundingSourceID, TenantID)
)