-- Getting rid of SecuredAmount and UnsecuredAmount on Project level, as considered not relevant for WADNR budgeting
alter table dbo.ProjectGrantAllocationRequest
drop constraint CK_ProjectFundingSourceRequest_SecuredUnsecuredAmountBothCannotBeZero

alter table dbo.ProjectGrantAllocationRequest
drop column SecuredAmount, UnsecuredAmount
GO

alter table dbo.Project
drop column EstimatedIndirectCost, EstimatedPersonnelAndBenefitsCost, [EstimatedSuppliesCost], [EstimatedTravelCost]

alter table dbo.ProjectGrantAllocationRequestUpdate
add TotalAmount Money null
go

alter table dbo.ProjectGrantAllocationRequestUpdate
drop constraint CK_ProjectFundingSourceRequestUpdate_SecuredUnsecuredAmountBothCannotBeZero

alter table dbo.ProjectGrantAllocationRequestUpdate
drop column SecuredAmount, UnsecuredAmount

alter table dbo.ProjectUpdate
drop column EstimatedIndirectCost, EstimatedPersonnelAndBenefitsCost, [EstimatedSuppliesCost], [EstimatedTravelCost]