

create table dbo.InvoicePaymentRequest(
	InvoicePaymentRequestID int not null identity(1,1) constraint PK_InvoicePaymentRequest_InvoicePaymentRequestID primary key,
	ProjectID int not null constraint FK_InvoicePaymentRequest_Project_ProjectID foreign key references dbo.Project(ProjectID),
	VendorID int null constraint FK_InvoicePaymentRequest_Vendor_VendorID foreign key references dbo.Vendor(VendorID),
	PreparedByPersonID int null constraint FK_InvoicePaymentRequest_Person_PreparedByPersonID_PersonID foreign key references dbo.Person(PersonID),
	PurchaseAuthority varchar(255) NULL,
	PurchaseAuthorityIsLandownerCostShareAgreement bit not null,
	Duns varchar(20),
	InvoicePaymentRequestDate datetime not null,
	Notes varchar(max),
	ApprovedByPersonID int null constraint FK_InvoicePaymentRequest_Person_ApprovedByPersonID_PersonID foreign key references dbo.Person(PersonID),
	ApprovedDate datetime null
);



create table dbo.OrganizationCode(
	OrganizationCodeID int not null constraint PK_OrganizationCode_OrganizationCodeID primary key,
	OrganizationCodeName varchar(250),
	OrganizationCodeValue varchar(20)
)

insert dbo.OrganizationCode (OrganizationCodeID, OrganizationCodeName, OrganizationCodeValue) 
values 
('1', 'Forest Resilience Division', '5900'),
('2', 'NE region', '2300'),
('3', 'SE region', '0100'),
('4', 'NW region', '1900'),
('5', 'SPS region', '0900'),
('6', 'OLY region', '0200'),
('7', 'PC region', '0400')

delete from dbo.InvoiceLineItem
delete from dbo.Invoice



ALTER TABLE dbo.Invoice
DROP CONSTRAINT FK_Invoice_Person_PreparedByPersonID_PersonID;

alter table dbo.Invoice
drop column RequestorName, PurchaseAuthority, PreparedByPersonID, PurchaseAuthorityIsLandownerCostShareAgreement


alter table dbo.Invoice
add 
	InvoicePaymentRequestID int not null constraint FK_Invoice_InvoicePaymentRequest_InvoicePaymentRequestID foreign key references dbo.InvoicePaymentRequest(InvoicePaymentRequestID),
	GrantID int null constraint FK_Invoice_Grant_GrantID foreign key references dbo.[Grant](GrantID),
	ProgramIndexID int null constraint FK_Invoice_ProgramIndex_ProgramIndexID foreign key references dbo.ProgramIndex(ProgramIndexID),
	ProjectCodeID int null constraint FK_Invoice_ProjectCode_ProjectCodeID foreign key references dbo.ProjectCode(ProjectCodeID),
	OrganizationCodeID int null constraint FK_Invoice_OrganizationCode_OrganizationCodeID foreign key references dbo.OrganizationCode(OrganizationCodeID),
	InvoiceNumber varchar(50),
	Fund varchar(255),
	Appn varchar(255),
	SubObject varchar(255);




