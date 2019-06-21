--Drop the AllocationTotals
alter table dbo.GrantAllocationAward
drop column GrantAllocationAwardAmount, IndirectCostAllocationTotal, SuppliesAllocationTotal, PersonnelAndBenefitsAllocationTotal, TravelAllocationTotal, LandownerCostShareAllocationTotal, ContractorInvoiceAllocationTotal


--Drop the ForesterFields
alter table dbo.GrantAllocationAward
drop column PersonnelAndBenefitsForester,TravelForester,LandownerCostShareForester

alter table dbo.GrantAllocationAward
add GrantAllocationAwardCalendarStartYear int 