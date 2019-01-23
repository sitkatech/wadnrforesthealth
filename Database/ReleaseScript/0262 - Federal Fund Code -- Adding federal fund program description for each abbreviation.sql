alter table dbo.FederalFundCode
add FederalFundCodeProgram nvarchar(255) NULL
go

update dbo.FederalFundCode
	set FederalFundCodeProgram =
	case
		when FederalFundCodeAbbrev = 'SPCF' then 'Cooperative Fire'
		--when FederalFundCodeAbbrev = 'SPS2' then 'XXXXXXXX' --Missing from the Federal Fund Code tab in the Grand Master spreadsheet
		--when FederalFundCodeAbbrev = 'SPS5' then 'XXXXXXXX' --Missing from the Federal Fund Code tab in the Grand Master spreadsheet
		when FederalFundCodeAbbrev = 'SPST' then 'Forest Stewardship'
		when FederalFundCodeAbbrev = 'SPUF' then 'Urban and Community Forestry'
		when FederalFundCodeAbbrev = 'WFHF' then 'Wildland Fire Hazardous Fuels/National Fire Plan'
	end
select * from dbo.FederalFundCode