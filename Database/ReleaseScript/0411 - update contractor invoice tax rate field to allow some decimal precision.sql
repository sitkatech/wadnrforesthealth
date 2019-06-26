alter table dbo.GrantAllocationAwardContractorInvoice
alter column GrantAllocationAwardContractorInvoiceTaxRate decimal(38,10);

alter table dbo.GrantAllocationAwardContractorInvoice
alter column GrantAllocationAwardContractorInvoiceForemanHours decimal(38,10);

alter table dbo.GrantAllocationAwardContractorInvoice
alter column GrantAllocationAwardContractorInvoiceLaborHours decimal(38,10);

alter table dbo.GrantAllocationAwardContractorInvoice
alter column GrantAllocationAwardContractorInvoiceGrappleHours decimal(38,10);

alter table dbo.GrantAllocationAwardContractorInvoice
alter column GrantAllocationAwardContractorInvoiceMasticationHours decimal(38,10);

alter table dbo.GrantAllocationAwardContractorInvoice
alter column GrantAllocationAwardContractorInvoiceAcresReported decimal(38,10);


alter table dbo.GrantAllocationAwardLandownerCostShareLineItem
alter column [GrantAllocationAwardLandownerCostShareLineItemFootprintAcres] [decimal](38,10) NOT NULL
	
alter table dbo.GrantAllocationAwardLandownerCostShareLineItem
alter column [GrantAllocationAwardLandownerCostShareLineItemChippingAcres] [decimal](38,10) NOT NULL;
	
alter table dbo.GrantAllocationAwardLandownerCostShareLineItem
alter column [GrantAllocationAwardLandownerCostShareLineItemPruningAcres] [decimal](38,10) NOT NULL;
	
alter table dbo.GrantAllocationAwardLandownerCostShareLineItem
alter column [GrantAllocationAwardLandownerCostShareLineItemThinningAcres] [decimal](38,10) NOT NULL;
	
alter table dbo.GrantAllocationAwardLandownerCostShareLineItem
alter column [GrantAllocationAwardLandownerCostShareLineItemMasticationAcres] [decimal](38,10) NOT NULL;
	
alter table dbo.GrantAllocationAwardLandownerCostShareLineItem
alter column [GrantAllocationAwardLandownerCostShareLineItemGrazingAcres] [decimal](38,10) NOT NULL;
	
alter table dbo.GrantAllocationAwardLandownerCostShareLineItem
alter column [GrantAllocationAwardLandownerCostShareLineItemLopAndScatterAcres] [decimal](38,10) NOT NULL;
	
alter table dbo.GrantAllocationAwardLandownerCostShareLineItem
alter column [GrantAllocationAwardLandownerCostShareLineItemBiomassRemovalAcres] [decimal](38,10) NOT NULL;
	
alter table dbo.GrantAllocationAwardLandownerCostShareLineItem
alter column [GrantAllocationAwardLandownerCostShareLineItemHandPileAcres] [decimal](38,10) NOT NULL;
	
alter table dbo.GrantAllocationAwardLandownerCostShareLineItem
alter column [GrantAllocationAwardLandownerCostShareLineItemBroadcastBurnAcres] [decimal](38,10) NOT NULL;
	
alter table dbo.GrantAllocationAwardLandownerCostShareLineItem
alter column [GrantAllocationAwardLandownerCostShareLineItemHandPileBurnAcres] [decimal](38,10) NOT NULL;
	
alter table dbo.GrantAllocationAwardLandownerCostShareLineItem
alter column [GrantAllocationAwardLandownerCostShareLineItemMachinePileBurnAcres] [decimal](38,10) NOT NULL;
	
alter table dbo.GrantAllocationAwardLandownerCostShareLineItem
alter column [GrantAllocationAwardLandownerCostShareLineItemOtherTreatmentAcres] [decimal](38,10) NOT NULL;
	
alter table dbo.GrantAllocationAwardLandownerCostShareLineItem
alter column [GrantAllocationAwardLandownerCostShareLineItemSlashAcres] [decimal](38,10) NOT NULL;

