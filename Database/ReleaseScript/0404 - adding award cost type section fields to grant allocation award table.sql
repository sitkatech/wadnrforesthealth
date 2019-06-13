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