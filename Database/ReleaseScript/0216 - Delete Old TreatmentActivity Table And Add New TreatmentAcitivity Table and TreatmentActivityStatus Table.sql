drop table dbo.TreatmentActivity;

create table dbo.TreatmentActivityStatus(
	TreatmentActivityStatusID int not null constraint PK_TreatmentActivityStatus_TreatmentActivityStatusID primary key,
	TreatmentActivityStatusName varchar(100) not null,
	TreatmentActivityStatusDisplayName varchar(100) not null
	);

insert into dbo.TreatmentActivityStatus values (1,'Planned','Planned');
insert into dbo.TreatmentActivityStatus values (2,'Completed','Completed');

Create Table dbo.TreatmentActivity(
	TreatmentActivityID int not null identity(1,1) constraint PK_TreatmentActivity_TreatmentActivityID primary key,
	TenantID int not null constraint FK_TreatmentActivity_Tenant_TenantID foreign key references dbo.Tenant(TenantID),
	ProjectID int not null constraint FK_TreatmentActivity_Project_ProjectID foreign key references dbo.Project(ProjectID),
	TreatmentActivityStartDate datetime not null,
	TreatmentActivityEndDate datetime null,
	TreatmentActivityProgramIndex varchar(100) null,
	TreatmentActivityProjectCode varchar(100) null,
	TreatmentActivityStatusID int not null constraint FK_TreatmentActivity_TreatmentActivityStatus_TreamentActivityStatusID foreign key references dbo.TreatmentActivityStatus(TreatmentActivityStatusID),
	TreatmentActivityContactID int null constraint FK_TreatmentActivity_Person_TreatmentActivityContactID_PersonID foreign key references dbo.Person(PersonID),
	TreatmentActivityFootprintAcres decimal not null,
	TreatmentActivityChippingAcres decimal not null,
	TreatmentActivityPruningAcres decimal not null,
	TreatmentActivityThinningAcres decimal not null,
	TreatmentActivityMasticationAcres decimal not null,
	TreatmentActivityGrazingAcres decimal not null,
	TreatmentActivityLopAndScatterAcres decimal not null,
	TreatmentActivityBiomassRemovalAcres decimal not null,
	TreatmentActivityHandPileAcres decimal not null,
	TreatmentActivityBroadcastBurnAcres decimal not null,
	TreatmentActivityHandPileBurnAcres decimal not null,
	TreatmentActivityMachinePileBurnAcres decimal not null,
	TreatmentActivityOtherTreatmentAcres decimal not null,
	TreatmentActivityNotes varchar(max) null
	)