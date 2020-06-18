/*
select * from dbo.GrantAllocationBudgetLineItem

select * from dbo.CostType

(1, 'Indirect Costs', 'IndirectCosts', 1),
(2, 'Supplies', 'Supplies', 1),
(3, 'Personnel', 'Personnel', 1),
(4, 'Benefits', 'Benefits', 1),
(5, 'Travel', 'Travel', 1),
(6, 'Contractual', 'Contractual', 1),
(7, 'Agreements', 'Agreements', 0),
(8, 'Equipment', 'Equipment', 1),
(9, 'Other', 'Other', 1)
*/

alter table dbo.CostType
add SortOrder int null
go

update dbo.CostType
set SortOrder = 10 where CostTypeID = 3;

update dbo.CostType
set SortOrder = 20 where CostTypeID = 4;

update dbo.CostType
set SortOrder = 30 where CostTypeID = 5;

update dbo.CostType
set SortOrder = 40 where CostTypeID = 2;

update dbo.CostType
set SortOrder = 50 where CostTypeID = 6;

update dbo.CostType
set SortOrder = 60 where CostTypeID = 1;

update dbo.CostType
set SortOrder = 70 where CostTypeID = 9;

update dbo.CostType
set SortOrder = 80 where CostTypeID = 8;

update dbo.CostType
set SortOrder = 90 where CostTypeID = 7;


alter table dbo.CostType
alter column SortOrder int not null
