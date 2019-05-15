SET IDENTITY_INSERT dbo.CostType ON;

--If you are adding a new CostType, make sure you update the following check constraints(if needed):
--CK_InvoiceLineItem_CostTypeValues
delete from dbo.CostType

Insert into dbo.CostType (CostTypeID, CostTypeDisplayName, CostTypeName, IsValidInvoiceLineItemCostType)
values
(1, 'Indirect Costs', 'IndirectCosts', 1),
(2, 'Supplies', 'Supplies', 1),
(3, 'Personnel and Benefits', 'PersonnelAndBenefits', 1),
(4, 'Travel', 'Travel', 1),
(5, 'Contractual', 'Contractual', 1),
(6, 'Agreements', 'Agreements', 0),
(7, 'Equipment', 'Equipment', 1),
(8, 'Other', 'Other', 1)

SET IDENTITY_INSERT dbo.CostType OFF;
