alter table dbo.Invoice ADD  PurchaseAuthorityIsLandownerCostShareAgreement bit

go

update dbo.Invoice
set PurchaseAuthorityIsLandownerCostShareAgreement = 1 where InvoiceID = 1

update dbo.Invoice
set PurchaseAuthority = null where InvoiceID = 1

update dbo.Invoice
set PurchaseAuthorityIsLandownerCostShareAgreement = 0 where InvoiceID != 1

go

alter table dbo.Invoice alter column PurchaseAuthorityIsLandownerCostShareAgreement bit not null

