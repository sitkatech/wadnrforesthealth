if exists (select * from dbo.sysobjects where id = object_id('dbo.vGeoServerFindYourForester'))
	drop view dbo.vGeoServerFindYourForester
go

create view [dbo].vGeoServerFindYourForester
as
select
	ISNULL(fwu.ForesterWorkUnitID, -1) as PrimaryKey,
	fwu.ForesterWorkUnitName,
	COALESCE (fwu.ForesterWorkUnitLocation, '') as ForesterWorkUnitLocation,
	fr.ForesterRoleID,
	fr.ForesterRoleName,
	fr.ForesterRoleDisplayName,
	p.PersonID,
	p.FirstName,
	p.LastName,
	p.Email,
	p.Phone

from
	dbo.ForesterWorkUnit as fwu
	join dbo.ForesterRole as fr on fwu.ForesterRoleID = fr.ForesterRoleID
	left join dbo.Person as p on fwu.PersonID = p.PersonID

GO