--Adding Non-Disclosure Agreement and Task Order to AgreementType table
insert into dbo.AgreementType (AgreementTypeAbbrev, AgreementTypeName) values
('Nondisclosure Agreement','NDA'),
('Task Order','TO')