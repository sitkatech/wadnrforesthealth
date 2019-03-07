-- Adding Program Index 235 per Christel's request. Manual entry for now, but may be worth considering building light-weight editor if this becomes a common request and PI isn't exposed through DataMart feed
insert into dbo.ProgramIndex
values
	('235')

insert into dbo.FederalFundCode (FederalFundCodeAbbrev, FederalFundCodeProgram)
values
	('WFCF', null)