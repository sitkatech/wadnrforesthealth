
-- The equivalent of this AND the JSON bulk load has been hotfixed already into production.

-- Also, dbo.pVendorImportJson should be working now, and can be used as a starting point for future loads.

--IF OBJECT_ID('dbo.Vendor', 'U') IS NOT NULL
--begin
--		alter table dbo.Person drop constraint FK_Person_Vendor_VendorID;
--		alter table dbo.Organization drop constraint FK_Organization_Vendor_VendorID;
--	  DROP TABLE dbo.Vendor;
--  end 

--create table dbo.Vendor (
--	VendorID int not null constraint PK_Vendor_VendorID primary key identity(1,1),
--	VendorName varchar(100) not null,
--	StatewideVendorNumber varchar(20) not null,
--	StatewideVendorNumberSuffix varchar(10) not null,
--	VendorType varchar(3),
--	BillingAgency varchar(200),
--	BillingSubAgency varchar(200),
--	BillingFund varchar(200),
--	BillingFundBreakout varchar(200),
--	VendorAddressLine1 varchar(200),
--	VendorAddressLine2 varchar(200), 
--	VendorAddressLine3 varchar(200), 
--	VendorCity varchar(200), 
--	VendorState varchar(200),
--	VendorZip varchar(200), 
--	Remarks varchar(200), 
--	VendorPhone varchar(200), 
--	VendorStatus varchar(200),
--	TaxpayerIdNumber varchar(200), 
--	Email varchar(200),
--	CONSTRAINT AK_Vendor_StatewideVendorNumber_StatewideVendorNumberSuffix UNIQUE (StatewideVendorNumber, StatewideVendorNumberSuffix)
--);

--insert dbo.Vendor (VendorName, StatewideVendorNumber, StatewideVendorNumberSuffix) 
--values 
--('Atg Daily Deposit', 'SWV0006858', '06'),
--('Atg Document Services', 'SWV0006858', '07'),
--('Atg Gse Transfer', 'SWV0006858', '09'),
--('Brent Murphy Inc', 'SWV0005422', '00'),
--('Brent Murphy Inc', 'SWV0005422', '01'),
--('Clark County Clerk', 'SWV0003051', '22'),
--('Dairy Fresh Farms Inc', 'SWV0005671', '00'),
--('Dept of Commerce', 'SWV0000116', '00'),
--('Dept of Commerce', 'SWV0000116', '01'),
--( 'DOC Petty Cash 001', 'SWV0003872', '59')

--C:\git\sitkatech\wadnrforesthealth\Database\SourceJsonFiles
--exec pVendorImportJson @VendorListJsonFileName = 'c:\temp\partialVendorList.json'

alter table dbo.Person drop column StatewideVendorNumber;

alter table dbo.Person 
	add VendorID int null 
		constraint FK_Person_Vendor_VendorID foreign key references dbo.Vendor(VendorID);

alter table dbo.Organization
	add VendorID int null 
		constraint FK_Organization_Vendor_VendorID foreign key references dbo.Vendor(VendorID);


