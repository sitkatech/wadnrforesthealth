create table dbo.Vendor (
	VendorID int not null constraint PK_Vendor_VendorID primary key identity(1,1),
	VendorName varchar(100) not null,
	StatewideVendorNumber varchar(20) not null,
	StatewideVendorNumberSuffix varchar(10) not null,
	CONSTRAINT AK_Vendor_StatewideVendorNumber_StatewideVendorNumberSuffix UNIQUE (StatewideVendorNumber, StatewideVendorNumberSuffix)
);

insert dbo.Vendor (VendorName, StatewideVendorNumber, StatewideVendorNumberSuffix) 
values 
('Atg Daily Deposit', 'SWV0006858', '06'),
('Atg Document Services', 'SWV0006858', '07'),
('Atg Gse Transfer', 'SWV0006858', '09'),
('Brent Murphy Inc', 'SWV0005422', '00'),
('Brent Murphy Inc', 'SWV0005422', '01'),
('Clark County Clerk', 'SWV0003051', '22'),
('Dairy Fresh Farms Inc', 'SWV0005671', '00'),
('Dept of Commerce', 'SWV0000116', '00'),
('Dept of Commerce', 'SWV0000116', '01'),
( 'DOC Petty Cash 001', 'SWV0003872', '59')



alter table dbo.Person drop column StatewideVendorNumber;

alter table dbo.Person 
	add VendorID int null 
		constraint FK_Person_Vendor_VendorID foreign key references dbo.Vendor(VendorID);

alter table dbo.Organization
	add VendorID int null 
		constraint FK_Organization_Vendor_VendorID foreign key references dbo.Vendor(VendorID);