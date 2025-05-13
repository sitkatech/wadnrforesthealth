

alter table dbo.Treatment drop constraint FK_Treatment_GrantAllocationAwardLandownerCostShareLineItem_GrantAllocationAwardLandownerCostShareLineItemID
alter table dbo.Treatment drop column GrantAllocationAwardLandownerCostShareLineItemID;

alter table dbo.TreatmentUpdate drop constraint FK_TreatmentUpdate_GrantAllocationAwardLandownerCostShareLineItem_GrantAllocationAwardLandownerCostShareLineItemID
alter table dbo.TreatmentUpdate drop column GrantAllocationAwardLandownerCostShareLineItemID;

drop table dbo.GrantAllocationAwardLandownerCostShareLineItem

drop table dbo.LandownerCostShareLineItemStatus

drop table dbo.GrantAllocationAwardContractorInvoice

drop table dbo.GrantAllocationAwardContractorInvoiceType

drop table dbo.GrantAllocationAwardTravelLineItem

drop table dbo.GrantAllocationAwardTravelLineItemType

drop table dbo.GrantAllocationAwardSuppliesLineItem

drop table dbo.GrantAllocationAwardPersonnelAndBenefitsLineItem

drop table dbo.GrantAllocationAward