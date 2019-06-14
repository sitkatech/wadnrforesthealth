alter table dbo.GrantAllocationAward
add 
	IndirectCostAllocationTotal money null,
	SuppliesAllocationTotal money null,
	PersonnelAndBenefitsAllocationTotal money null,
	PersonnelAndBenefitsForester varchar(255),
	TravelAllocationTotal money null,
	TravelForester varchar(255),
	LandownerCostShareAllocationTotal money null,
	LandownerCostShareTargetFootprintAcreage int null,
	LandownerCostShareTargetTotalAcreage int null,
	LandownerCostShareForester varchar(255),
	ContractorInvoicesAllocationTotal money null,
	ContractorInvoicesContractor varchar(255),
	ContractorInvoicesTargetTotalAcreage int null;



create table dbo.GrantAllocationAwardSuppliesLineItem (
	GrantAllocationAwardSuppliesLineItemID int not null identity(1,1) constraint PK_GrantAllocationAwardSuppliesLineItem_GrantAllocationAwardSuppliesLineItemID primary key,
	GrantAllocationAwardID int not null constraint FK_GrantAllocationAwardSuppliesLineItem_GrantAllocationAward_GrantAllocationAwardID foreign key references dbo.GrantAllocationAward(GrantAllocationAwardID),
	GrantAllocationAwardSuppliesLineItemDescription varchar(255) not null,
	GrantAllocationAwardSuppliesLineItemTarOrMonth varchar(255) not null,
	GrantAllocationAwardSuppliesLineItemDate datetime not null,
	GrantAllocationAwardSuppliesLineItemAmount money not null,
	GrantAllocationAwardSuppliesLineItemNotes varchar(2000)
)


create table dbo.GrantAllocationAwardPersonnelAndBenefitsLineItem (
	GrantAllocationAwardPersonnelAndBenefitsLineItemID int not null identity(1,1) constraint PK_GrantAllocationAwardPersonnelAndBenefitsLineItem_GrantAllocationAwardPersonnelAndBenefitsLineItemID primary key,
	GrantAllocationAwardID int not null constraint FK_GrantAllocationAwardPersonnelAndBenefitsLineItem_GrantAllocationAward_GrantAllocationAwardID foreign key references dbo.GrantAllocationAward(GrantAllocationAwardID),
	GrantAllocationAwardPersonnelAndBenefitsLineItemDescription varchar(255) not null,
	GrantAllocationAwardPersonnelAndBenefitsLineItemTarOrMonth varchar(255) not null,
	GrantAllocationAwardPersonnelAndBenefitsLineItemDate datetime not null,
	GrantAllocationAwardPersonnelAndBenefitsLineItemTarHours int not null,
	GrantAllocationAwardPersonnelAndBenefitsLineItemHourlyRate money not null,
	GrantAllocationAwardPersonnelAndBenefitsLineItemFringeRate money not null,
	GrantAllocationAwardPersonnelAndBenefitsLineItemNotes varchar(2000)
)

create table dbo.GrantAllocationAwardTravelLineItemType (
	GrantAllocationAwardTravelLineItemTypeID int not null constraint PK_GrantAllocationAwardTravelLineItemType_GrantAllocationAwardTravelLineItemTypeID primary key,
	GrantAllocationAwardTravelLineItemTypeName varchar(255),
	GrantAllocationAwardTravelLineItemTypeDisplayName varchar(255)
)

insert into dbo.GrantAllocationAwardTravelLineItemType (GrantAllocationAwardTravelLineItemTypeID, GrantAllocationAwardTravelLineItemTypeName, GrantAllocationAwardTravelLineItemTypeDisplayName) values
(1, 'Transportation', 'Transportation'),
(2, 'Other', 'Other');


create table dbo.GrantAllocationAwardTravelLineItem (
	GrantAllocationAwardTravelLineItemID int not null identity(1,1) constraint PK_GrantAllocationAwardTravelLineItem_GrantAllocationAwardTravelLineItemID primary key,
	GrantAllocationAwardID int not null constraint FK_GrantAllocationAwardTravelLineItem_GrantAllocationAward_GrantAllocationAwardID foreign key references dbo.GrantAllocationAward(GrantAllocationAwardID),
	GrantAllocationAwardTravelLineItemDescription varchar(255) not null,
	GrantAllocationAwardTravelLineItemTarOrMonth varchar(255) not null,
	GrantAllocationAwardTravelLineItemDate datetime not null,
	GrantAllocationAwardTravelLineItemTypeID int not null constraint FK_GrantAllocationAwardTravelLineItem_GrantAllocationAwardTravelLineItemType_GrantAllocationAwardTravelLineItemTypeID foreign key references dbo.GrantAllocationAwardTravelLineItemType(GrantAllocationAwardTravelLineItemTypeID),
	GrantAllocationAwardTravelLineItemMiles int,
	GrantAllocationAwardTravelLineItemMileageRate money,
	GrantAllocationAwardTravelLineItemAmount money,
	GrantAllocationAwardTravelLineItemNotes varchar(2000)
)


create table dbo.GrantAllocationAwardLandownerCostShareLineItemStatus (
	GrantAllocationAwardLandownerCostShareLineItemStatusID int not null constraint PK_GrantAllocationAwardLandownerCostShareLineItemStatus_GrantAllocationAwardLandownerCostShareLineItemStatusID primary key,
	GrantAllocationAwardLandownerCostShareLineItemStatusName varchar(255),
	GrantAllocationAwardLandownerCostShareLineItemStatusDisplayName varchar(255)
)

insert into dbo.GrantAllocationAwardLandownerCostShareLineItemStatus (GrantAllocationAwardLandownerCostShareLineItemStatusID, GrantAllocationAwardLandownerCostShareLineItemStatusName, GrantAllocationAwardLandownerCostShareLineItemStatusDisplayName) values
(1, 'Planned', 'Planned'),
(2, 'Completed', 'Completed'),
(3, 'Cancelled', 'Cancelled');


create table dbo.GrantAllocationAwardLandownerCostShareLineItem (
	GrantAllocationAwardLandownerCostShareLineItemID int not null identity(1,1) constraint PK_GrantAllocationAwardLandownerCostShareLineItem_GrantAllocationAwardLandownerCostShareLineItemID primary key,
	GrantAllocationAwardID int not null constraint FK_GrantAllocationAwardLandownerCostShareLineItem_GrantAllocationAward_GrantAllocationAwardID foreign key references dbo.GrantAllocationAward(GrantAllocationAwardID),
	[ProjectID] int NOT NULL constraint FK_GrantAllocationAwardLandownerCostShareLineItem_Project_ProjectID foreign key references dbo.Project(ProjectID),
	--the name for this contraint is TOO long, and code gen doesn't like it
	[GrantAllocationAwardLandownerCostShareLineItemStatusID] [int] NOT NULL constraint FK_GrantAllocationAwardLandownerCostShareLineItem_GrantAllocationAwardLandownerCostShareLineItemStatus_GrantAllocationAwardLandownerCostShareLineItemStatusID foreign key references dbo.GrantAllocationAwardLandownerCostShareLineItemStatus(GrantAllocationAwardLandownerCostShareLineItemStatusID),
	[GrantAllocationAwardLandownerCostShareLineItemStartDate] [datetime] NULL,
	[GrantAllocationAwardLandownerCostShareLineItemEndDate] [datetime] NULL,
	[GrantAllocationAwardLandownerCostShareLineItemFootprintAcres] decimal NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemChippingAcres] decimal NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemPruningAcres] decimal NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemThinningAcres] decimal NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemMasticationAcres] decimal NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemGrazingAcres] decimal NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemLopAndScatterAcres] decimal NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemBiomassRemovalAcres] decimal NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemHandPileAcres] decimal NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemBroadcastBurnAcres] decimal NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemHandPileBurnAcres] decimal NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemMachinePileBurnAcres] decimal NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemOtherTreatmentAcres] decimal NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemSlashAcres] decimal NOT NULL,
	[GrantAllocationAwardLandownerCostShareLineItemNotes] varchar(2000)
)


create table dbo.GrantAllocationAwardContractorInvoiceType (
	GrantAllocationAwardContractorInvoiceTypeID int not null constraint PK_GrantAllocationAwardContractorInvoiceType_GrantAllocationAwardContractorInvoiceTypeID primary key,
	GrantAllocationAwardContractorInvoiceTypeName varchar(255),
	GrantAllocationAwardContractorInvoiceDisplayName varchar(255)
)

insert into dbo.GrantAllocationAwardContractorInvoiceType (GrantAllocationAwardContractorInvoiceTypeID, GrantAllocationAwardContractorInvoiceTypeName, GrantAllocationAwardContractorInvoiceDisplayName) values
(1, 'Hourly', 'Hourly'),
(2, 'Other', 'Other');



create table dbo.GrantAllocationAwardContractorInvoice (
	GrantAllocationAwardContractorInvoiceID int not null identity(1,1) constraint PK_GrantAllocationAwardContractorInvoice_GrantAllocationAwardContractorInvoiceID primary key,
	GrantAllocationAwardID int not null constraint FK_GrantAllocationAwardContractorInvoice_GrantAllocationAward_GrantAllocationAwardID foreign key references dbo.GrantAllocationAward(GrantAllocationAwardID),
	GrantAllocationAwardContractorInvoiceDescription varchar(255) not null,
	GrantAllocationAwardContractorInvoiceNumber varchar(255) not null,
	GrantAllocationAwardContractorInvoiceDate datetime not null,
	GrantAllocationAwardContractorInvoiceType int not null constraint FK_GrantAllocationAwardContractorInvoice_GrantAllocationAwardContractorInvoiceType_GrantAllocationAwardContractorInvoiceTypeID foreign key references dbo.GrantAllocationAwardContractorInvoiceType(GrantAllocationAwardContractorInvoiceTypeID),
	GrantAllocationAwardContractorInvoiceForemanHours decimal,
	GrantAllocationAwardContractorInvoiceForemanRate money,
	GrantAllocationAwardContractorInvoiceLaborHours decimal,
	GrantAllocationAwardContractorInvoiceLaborRate money,
	GrantAllocationAwardContractorInvoiceGrappleHours decimal,
	GrantAllocationAwardContractorInvoiceGrappleRate money,
	GrantAllocationAwardContractorInvoiceMasticationHours decimal,
	GrantAllocationAwardContractorInvoiceMasticationRate money,
	GrantAllocationAwardContractorInvoiceTotal money,
	GrantAllocationAwardContractorInvoiceTaxRate decimal,
	GrantAllocationAwardContractorInvoiceAcresReported decimal,
	GrantAllocationAwardContractorInvoiceNotes varchar(2000),
	GrantAllocationAwardContractorInvoiceFileResourceID int null constraint FK_GrantAllocationAwardContractorInvoice_FileResource_GrantAllocationAwardContractorInvoiceFileResourceID_FileResourceID foreign key references dbo.FileResource(FileResourceID)
)