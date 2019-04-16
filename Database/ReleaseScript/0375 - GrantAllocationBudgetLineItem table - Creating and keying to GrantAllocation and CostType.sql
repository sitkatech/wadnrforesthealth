-- Move FK off GrantAllocation and onto a new table called GrantAllocationBudgetLineItem
alter table dbo.GrantAllocation
drop constraint FK_GrantAllocation_CostType_CostTypeID
go

alter table dbo.GrantAllocation
drop column CostTypeID
go

create table dbo.GrantAllocationBudgetLineItem
(
	GrantAllocationBudgetLineItemID int Identity(1,1) not null,
	GrantAllocationID int not null,
	CostTypeID int not null,
	GrantAllocationBudgetLineItemAmount money not null,
	GrantAllocationBudgetLineItemNote nvarchar(max),
  constraint PK_GrantAllocationBudgetLineItem_GrantAllocationBudgetLineItemID primary key clustered
  (
	GrantAllocationBudgetLineItemID ASC
  ) on [primary]
)
go

alter table dbo.GrantAllocationBudgetLineItem
  add constraint FK_GrantAllocationBudgetLineItem_GrantAllocation_GrantAllocationID foreign key(GrantAllocationID)
  references dbo.grantAllocation(GrantAllocationID)
go

alter table dbo.GrantAllocationBudgetLineItem
  add constraint FK_GrantAllocationBudgetLineItem_CostType_CostTypeID foreign key(CostTypeID)
  references dbo.CostType(CostTypeID)
go
