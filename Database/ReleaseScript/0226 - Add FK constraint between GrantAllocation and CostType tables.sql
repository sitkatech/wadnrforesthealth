ALTER TABLE [dbo].[GrantAllocation] 
ADD CONSTRAINT [FK_GrantAllocation_CostType_CostTypeID] 
FOREIGN KEY (CostTypeID) references dbo.[CostType](CostTypeID)