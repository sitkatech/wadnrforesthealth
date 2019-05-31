SET IDENTITY_INSERT dbo.CostType ON;

--If you are adding a new CostType, make sure you update the following check constraints(if needed):
--CK_InvoiceLineItem_CostTypeValues
delete from dbo.CostType

Insert into dbo.CostType (CostTypeID, CostTypeDisplayName, CostTypeName, IsValidInvoiceLineItemCostType)
values
(1, 'Indirect Costs', 'IndirectCosts', 1),
(2, 'Supplies', 'Supplies', 1),
(3, 'Personnel', 'Personnel', 1),
(4, 'Benefits', 'Benefits', 1),
(5, 'Travel', 'Travel', 1),
(6, 'Contractual', 'Contractual', 1),
(7, 'Agreements', 'Agreements', 0),
(8, 'Equipment', 'Equipment', 1),
(9, 'Other', 'Other', 1)


SET IDENTITY_INSERT dbo.CostType OFF;
