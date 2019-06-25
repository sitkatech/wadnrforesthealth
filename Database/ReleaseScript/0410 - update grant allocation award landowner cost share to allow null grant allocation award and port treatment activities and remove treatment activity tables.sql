alter table dbo.GrantAllocationAwardLandownerCostShareLineItem
alter column GrantAllocationAwardID int null;


insert into dbo.GrantAllocationAwardLandownerCostShareLineItem ([ProjectID]
															   ,[LandownerCostShareLineItemStatusID]
															   ,[GrantAllocationAwardLandownerCostShareLineItemStartDate]
															   ,[GrantAllocationAwardLandownerCostShareLineItemEndDate]
															   ,[GrantAllocationAwardLandownerCostShareLineItemFootprintAcres]
															   ,[GrantAllocationAwardLandownerCostShareLineItemChippingAcres]
															   ,[GrantAllocationAwardLandownerCostShareLineItemPruningAcres]
															   ,[GrantAllocationAwardLandownerCostShareLineItemThinningAcres]
															   ,[GrantAllocationAwardLandownerCostShareLineItemMasticationAcres]
															   ,[GrantAllocationAwardLandownerCostShareLineItemGrazingAcres]
															   ,[GrantAllocationAwardLandownerCostShareLineItemLopAndScatterAcres]
															   ,[GrantAllocationAwardLandownerCostShareLineItemBiomassRemovalAcres]
															   ,[GrantAllocationAwardLandownerCostShareLineItemHandPileAcres]
															   ,[GrantAllocationAwardLandownerCostShareLineItemBroadcastBurnAcres]
															   ,[GrantAllocationAwardLandownerCostShareLineItemHandPileBurnAcres]
															   ,[GrantAllocationAwardLandownerCostShareLineItemMachinePileBurnAcres]
															   ,[GrantAllocationAwardLandownerCostShareLineItemOtherTreatmentAcres]
															   ,[GrantAllocationAwardLandownerCostShareLineItemSlashAcres]
															   ,[GrantAllocationAwardLandownerCostShareLineItemNotes]
															   ,[GrantAllocationAwardLandownerCostShareLineItemAllocatedAmount]
															   ,[GrantAllocationAwardLandownerCostShareLineItemTotalCost]) 
	select [ProjectID]
		  ,[TreatmentActivityStatusID]
		  ,[TreatmentActivityStartDate]
		  ,[TreatmentActivityEndDate]

		  ,[TreatmentActivityFootprintAcres]
		  ,[TreatmentActivityChippingAcres]
		  ,[TreatmentActivityPruningAcres]
		  ,[TreatmentActivityThinningAcres]
		  ,[TreatmentActivityMasticationAcres]
		  ,[TreatmentActivityGrazingAcres]
		  ,[TreatmentActivityLopAndScatterAcres]
		  ,[TreatmentActivityBiomassRemovalAcres]
		  ,[TreatmentActivityHandPileAcres]
		  ,[TreatmentActivityBroadcastBurnAcres]
		  ,[TreatmentActivityHandPileBurnAcres]
		  ,[TreatmentActivityMachinePileBurnAcres]
		  ,[TreatmentActivityOtherTreatmentAcres]
		  ,[TreatmentActivitySlashAcres]
		  ,[TreatmentActivityNotes]
		  ,0
		  ,0
				 from dbo.TreatmentActivity



drop table dbo.TreatmentActivity;
drop table dbo.TreatmentActivityStatus;