--Bud Westcott's email in the person table apprears to be misspelled, overwritting with the provieded email in the remp forester unit proj table. Used to be bud.westscott@dnr.wa.gov
Update dbo.Person
	set Person.Email = 'bud.westcott@dnr.wa.gov' where FirstName = 'Bud' and LastName = 'Westcott'

-- Delete all service forester records from the forester work unit table
Delete dbo.ForesterWorkUnit where ForesterRoleID = 1


insert into dbo.ForesterWorkUnit(ForesterRoleID, PersonID, ForesterWorkUnitName, RegionName, ForesterWorkUnitLocation)
select 1, Person.PersonID, sfup.Unit_Name, sfup.Unit_Name, sfup.GEOM from dbo.tmp_service_Forester_units_Proje sfup
left join dbo.Person on Person.Email = sfup.Email 

drop table dbo.tmp_service_Forester_units_Proje