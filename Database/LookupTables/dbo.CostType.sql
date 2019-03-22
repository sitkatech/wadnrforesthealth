SET IDENTITY_INSERT dbo.CostType ON;


delete from dbo.CostType

Insert into dbo.CostType (CostTypeID, CostTypeDescription, CostTypeName, IsValidInvoiceLineItemCostType)
values
(1, 'Indirect Costs', 'IndirectCosts', 1),
(2, 'Supplies', 'Supplies', 1),
(3, 'Personnel and Benefits', 'PersonnelAndBenefits', 1),
(4, 'Travel', 'Travel', 1),
(5, 'Contractual', 'Contractual', 1),
(6, 'Agreements', 'Agreements', 0)

SET IDENTITY_INSERT dbo.CostType OFF;
