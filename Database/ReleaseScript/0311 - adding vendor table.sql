create table dbo.Vendor (
	VendorID int not null constraint PK_Vendor_VendorID primary key,
	VendorName varchar(100) not null,
	StatewideVendorNumber varchar(20) not null,
	VendorSuffix varchar(10) not null
);


insert dbo.Vendor (VendorID, VendorName, StatewideVendorNumber, VendorSuffix) 
values 
(1, 'Atg Daily Deposit', 'SWV0006858', '06'),
(2, 'Atg Document Services', 'SWV0006858', '07'),
(3, 'Atg Gse Transfer', 'SWV0006858', '09'),
(4, 'Brent Murphy Inc', 'SWV0005422', '00'),
(5, 'Brent Murphy Inc', 'SWV0005422', '01'),
(6, 'Clark County Clerk', 'SWV0003051', '22'),
(7, 'Dairy Fresh Farms Inc', 'SWV0005671', '00'),
(8, 'Dept of Commerce', 'SWV0000116', '00'),
(9, 'Dept of Commerce', 'SWV0000116', '01'),
(10, 'DOC Petty Cash 001', 'SWV0003872', '59')