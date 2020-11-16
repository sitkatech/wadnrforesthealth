insert into dbo.FirmaPageType
values (64, 'DNRCostShareTreatments', 'DNR Cost Share Treatments', 1)


go 

insert into dbo.FirmaPage(FirmaPageTypeID, FirmaPageContent)
select 64, 'DNR Cost Share Treatments are Treatments that are associated with a Grant Allocation Award Landowner Cost Share Line Item.'