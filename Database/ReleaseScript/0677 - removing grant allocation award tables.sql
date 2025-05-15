

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


delete from dbo.FirmaPage where FirmaPageTypeID in (64)

delete from dbo.FieldDefinitionData where FieldDefinitionID in (361, 362, 363, 364, 365, 366, 367, 368, 369, 370, 371, 372, 373, 374, 375, 376, 377, 378, 379, 380, 381, 382, 383, 384, 385, 386, 387, 388, 389, 390, 391, 392, 393, 394, 395, 396, 397, 398, 399, 400, 401, 402, 403, 404, 405, 406, 407, 408, 409, 410, 411, 412, 413, 414, 415, 432, 433, 434, 435, 436, 437, 438, 439, 440, 441, 442, 443, 444, 445, 446, 447, 448, 449, 450, 451, 452, 453, 454, 455, 456, 457, 458, 459, 460, 461, 462, 463, 464)



/*

select * from dbo.GisDefaultMapping where FieldDefinitionID in (361, 362, 363, 364, 365, 366, 367, 368, 369, 370, 371, 372, 373, 374, 375, 376, 377, 378, 379, 380, 381, 382, 383, 384, 385, 386, 387, 388, 389, 390, 391, 392, 393, 394, 395, 396, 397, 398, 399, 400, 401, 402, 403, 404, 405, 406, 407, 408, 409, 410, 411, 412, 413, 414, 415, 432, 433, 434, 435, 436, 437, 438, 439, 440, 441, 442, 443, 444, 445, 446, 447, 448, 449, 450, 451, 452, 453, 454, 455, 456, 457, 458, 459, 460, 461, 462, 463, 464)

*/