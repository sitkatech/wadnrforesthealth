
alter table dbo.Agreement alter column OrganizationID int null

DROP INDEX [UNIQUE_INDEX_Agreement_AgreementNumber] ON [dbo].[Agreement]
GO

alter table dbo.Agreement add tmpAgreement2ID int null
GO

ALTER TABLE [dbo].[Agreement]  WITH CHECK ADD  CONSTRAINT [FK_Agreement_tmpAgreement2_tmpAgreement2ID] FOREIGN KEY([TmpAgreement2ID])
REFERENCES [dbo].[tmpAgreement2] ([TmpAgreement2ID])
GO

ALTER TABLE [dbo].[Agreement] DROP CONSTRAINT [FK_Agreement_TmpAgreement_TmpAgreementID]
alter table dbo.Agreement drop column TmpAgreementID
GO

INSERT INTO dbo.Agreement
			(AgreementNumber,
			 TmpAgreement2ID,
			 AgreementTitle, 
			 StartDate,
			 EndDate, 
			 AgreementAmount)
SELECT ta2.AgreementNumber,
	   ta2.TmpAgreement2ID,
       ta2.AgreementTitle, 
	   ta2.StartDate,
	   ta2.EndDate, 
	   ta2.AgreementAmount
FROM tmpAgreement2 as ta2


update dbo.Agreement
set AgreementTypeID = agt.AgreementTypeID
from dbo.tmpAgreement2 as ta
left join AgreementType as agt on ta.AgreementType = AgreementTypeAbbrev
where ta.TmpAgreement2ID = dbo.Agreement.TmpAgreement2ID

-- Missing AgreementTypes: AMD & TO
/*
select ta2.AgreementType, aType.AgreementTypeName
from dbo.tmpAgreement2 as ta2
left join dbo.AgreementType as aType on aType.AgreementTypeAbbrev = ta2.AgreementType
where aType.AgreementTypeName is null
*/

-- We've been making mistakes in the past, so we will clean up here forcibly, then get it correct below.
-- This is a testing aid, mostly.
update dbo.Agreement
set OrganizationID = null

--manually adding this organization
insert dbo.Organization (OrganizationName, OrganizationShortName, IsActive, OrganizationUrl, OrganizationTypeID)
values ('C+C', 'C+C', 1, 'https://cplusc.com/', 1065);

--doing it with organization name
update dbo.Agreement
set OrganizationID = org.OrganizationID
from dbo.tmpAgreement2 as ta
left join dbo.Organization as org on ta.Organization = org.OrganizationName
where ta.TmpAgreement2ID = dbo.Agreement.TmpAgreement2ID


--doing it with organization abbreviation
update dbo.Agreement
set OrganizationID = org.OrganizationID
from dbo.tmpAgreement2 as ta
left join dbo.Organization as org on ta.Organization = org.OrganizationShortName
where ta.TmpAgreement2ID = dbo.Agreement.TmpAgreement2ID and org.OrganizationID is not null

-- Missing Organizations. Some of these we will try to manually fix up and match below.
select ta2.Organization, a.* 
from dbo.Agreement as a
inner join dbo.tmpAgreement2 as ta2 on ta2.TmpAgreement2ID = a.tmpAgreement2ID
where a.OrganizationID is null

-- fixing up a crappy name to be better. Because we are right.
update Organization
set OrganizationName = 'Washington State Parks and Recreation Commission'
where OrganizationID = 5793

-- This name fixup we are less confident about, but it definitely needs work of some kind
update Organization
set OrganizationName = 'National Oceanic and Atmospheric Administration - National Weather Service'
where OrganizationID = 5764

update Organization
set OrganizationName = 'Rudeen and Associates, LLC'
where OrganizationID = 5771

update Organization
set OrganizationName = 'Washington Conservation Science Institute, LLC'
where OrganizationID = 5790



-- Manually setting these records
update 
	dbo.Agreement
set 
	OrganizationID = 5793
from 
	dbo.Agreement as a
	inner join dbo.tmpAgreement2 as ta2 on ta2.TmpAgreement2ID = a.tmpAgreement2ID
	where a.OrganizationID is null
	and
	ta2.Organization = 'Washington State Parks & Recreation Commission'


update 
	dbo.Agreement
set 
	OrganizationID = 5764
from 
	dbo.Agreement as a
	inner join dbo.tmpAgreement2 as ta2 on ta2.TmpAgreement2ID = a.tmpAgreement2ID
	where a.OrganizationID is null
	and
	ta2.Organization = 'National Oceanic and Atmospheric Administration'

update 
	dbo.Agreement
set 
	OrganizationID = 5771
from 
	dbo.Agreement as a
	inner join dbo.tmpAgreement2 as ta2 on ta2.TmpAgreement2ID = a.tmpAgreement2ID
	where a.OrganizationID is null
	and
	ta2.Organization = 'Rudeen and Associates, LLC'

update 
	dbo.Agreement
set 
	OrganizationID = 5790
from 
	dbo.Agreement as a
	inner join dbo.tmpAgreement2 as ta2 on ta2.TmpAgreement2ID = a.tmpAgreement2ID
	where a.OrganizationID is null
	and
	ta2.Organization = 'Washington Conservation Science Institute, LLC'




--select ta2.Organization, a.* 
--from dbo.Agreement as a
--inner join dbo.tmpAgreement2 as ta2 on ta2.TmpAgreement2ID = a.tmpAgreement2ID
--where a.OrganizationID is null

--select * from Organization
--where OrganizationName like '%C%'