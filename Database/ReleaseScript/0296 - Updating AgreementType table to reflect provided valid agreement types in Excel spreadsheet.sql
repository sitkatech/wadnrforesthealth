-- Remove AgreementType FKs from Agreement that are no longer considered valid
update dbo.Agreement 
set AgreementTypeID = NULL
where AgreementTypeID in (1,7,15) -- 981-HIS, CS-FR, and WRC&DC AgreementTypeAbbrevs that are junk now 
go
	
-- Change AgreementTypeID 4 to 3 (i.e., Cooperative Agreement 14-267 to Cooperative Agreement), as 4 is no longer recognized
update dbo.Agreement 
set AgreementTypeID = 3
where AgreementTypeID = 4
go

-- Delete the now-invalid, unused AgreementTypes
delete from dbo.AgreementType
where AgreementTypeID in (1,4,7,15)
go

-- Clean up existing keys to match Excel spreadsheet provided by WA DNR
update dbo.AgreementType
set AgreementTypeAbbrev = 'AUTH'
where AgreementTypeName = 'Authority'
go

update dbo.AgreementType
set AgreementTypeAbbrev = 'CLA'
where AgreementTypeName = 'Collection Agreement'
go

update dbo.AgreementType
set AgreementTypeName = 'Contract for Service'
where AgreementTypeAbbrev = 'CS'
go

update dbo.AgreementType
set AgreementTypeAbbrev = 'CSA'
where AgreementTypeName = 'Cost-Share Agreement'
go

update dbo.AgreementType
set AgreementTypeName = 'ITPS Work Order', AgreementTypeAbbrev = 'WO'
where AgreementTypeID = 14
go

update dbo.AgreementType
set AgreementTypeName = 'Master Agreement', AgreementTypeAbbrev = 'MA'
where AgreementTypeID = 10
go

update dbo.AgreementType
set AgreementTypeAbbrev = 'PA'
where AgreementTypeName = 'Participating Agreement'
go
