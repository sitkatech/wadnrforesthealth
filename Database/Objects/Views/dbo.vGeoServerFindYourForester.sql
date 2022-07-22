if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerFindYourForester'))
	drop view dbo.vGeoServerFindYourForester
go

create view [dbo].vGeoServerFindYourForester
as
select
	fwu.ForesterWorkUnitID as PrimaryKey,
	fwu.ForesterWorkUnitName,
	fwu.ForesterWorkUnitLocation,
	fr.ForesterRoleID,
	fr.ForesterRoleName,
	fr.ForesterRoleDisplayName,
	wup.PersonID,
	p.FirstName,
	p.LastName,
	p.Email,
	p.Phone

from
	dbo.ForesterWorkUnit as fwu
	join dbo.ForesterRole as fr on fwu.ForesterRoleID = fr.ForesterRoleID
	left join dbo.ForesterWorkUnitPerson as wup on wup.ForesterWorkUnitID = fwu.ForesterWorkUnitID
	left join dbo.Person as p on wup.PersonID = p.PersonID
	   	  
GO
