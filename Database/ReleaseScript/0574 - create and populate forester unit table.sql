




create table dbo.ForesterUnit(
	ForesterUnitID int not null identity(1,1) constraint PK_ForesterUnit_ForesterUnitID primary key,
	ForesterUnitName varchar(100) not null,
	RegionName varchar(100),
	ForesterUnitLocation geometry not null
	
);


insert into dbo.ForesterUnit(ForesterUnitName, RegionName, ForesterUnitLocation)
select
	fu.FP_UNIT_NM as ForesterUnitName,
	fu.REGION_NM as RegionName,
	fu.GEOM
from
	dbo.tmpForesterUnit as fu
