


delete from dbo.ForesterWorkUnit
where ForesterRoleID = 1 and ForesterWorkUnitName = 'Chelan' and RegionName = 'Northwest Region'




update dbo.Person
set Phone = '(360) 972-4185'
where Email = 'jason.cirksena@dnr.wa.gov'



update dbo.Person
set Phone = '(360) 972-4249'
where Email = 'charles.landsman@dnr.wa.gov'


update dbo.Person
set Phone = '(360) 972-4135'
where Email = 'marc.titus@dnr.wa.gov'

update dbo.Person
set Phone = '(360) 972-4428'
where Email = 'jennifer.coe@dnr.wa.gov'

update dbo.Person
set Phone = '(360) 972-4272'
where Email = 'william.knowlton@dnr.wa.gov'



update dbo.Person
set Phone = '(360) 669-3906'
where Email = 'Tom.chandler@dnr.wa.gov'

update dbo.Person
set Phone = '(360) 306-2251'
where Email = 'Hollis.crapo@dnr.wa.gov'

update dbo.Person
set Phone = '(360) 742-6825'
where Email = 'todd.olson@dnr.wa.gov'

update dbo.Person
set Phone = '(509) 563-957'
where Email = 'Noah.nequette@dnr.wa.gov'

update dbo.Person
set Phone = '(360) 481-9170'
where Email = 'Roslyn.henricks@dnr.wa.gov'

update dbo.Person
set Phone = '(360) 819-7143'
where Email = 'MATTHEW.PROVENCHER@dnr.wa.gov'





insert into dbo.Person(FirstName, LastName, Email, Phone, CreateDate, IsActive, ReceiveSupportEmails)
values ('Kelsey', 'Hagan', 'NE_LOA@dnr.wa.gov', '(509) 684-7474', GETDATE(), 1,	0)


update dbo.Person
set Email = 'bud.westscott@dnr.wa.gov'
where FirstName = 'Bud' and LastName = 'Westcott'


update dbo.Person
set Email = 'ian.mclelland@dnr.wa.gov'
where FirstName = 'Ian' and LastName = 'McLelland'
go




update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person as p where p.Email = 'amanda.moody@dnr.wa.gov')
where ForesterRoleID = 1 and ForesterWorkUnitID in (116, 117, 118, 130)


update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person as p where p.Email = 'andrew.naughton@dnr.wa.gov')
where ForesterRoleID = 1 and ForesterWorkUnitID in (107, 108, 109, 111, 112)


update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person as p where p.Email = 'anne.favolise@dnr.wa.gov')
where ForesterRoleID = 1 and ForesterWorkUnitID = 122

update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person as p where p.Email = 'bud.westscott@dnr.wa.gov')
where ForesterRoleID = 1 and ForesterWorkUnitID = 126


update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person as p where p.Email = 'Gary.Bell@dnr.wa.gov')
where ForesterRoleID = 1 and ForesterWorkUnitID in (103, 131)

update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person as p where p.Email = 'ian.mclelland@dnr.wa.gov')
where ForesterRoleID = 1 and ForesterWorkUnitID = 127

update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person as p where p.Email = 'katie.zander@dnr.wa.gov')
where ForesterRoleID = 1 and ForesterWorkUnitID in (113, 114)

update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person as p where p.Email = 'NE_LOA@dnr.wa.gov')
where ForesterRoleID = 1 and ForesterWorkUnitID = 110

update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person as p where p.Email = 'sharon.frazey@dnr.wa.gov')
where ForesterRoleID = 1 and ForesterWorkUnitID = 121


update dbo.Person
set Email = 'steve.crow@dnr.wa.gov'
where FirstName = 'Steve' and LastName = 'Crow'

insert into dbo.Person(FirstName, LastName, Email, Phone, CreateDate, IsActive, ReceiveSupportEmails)
values ('Bryent', 'Daugherty', 'bryent.daugherty@dnr.wa.gov', '(360-722-9543', GETDATE(), 1,	0)

insert into dbo.Person(FirstName, LastName, Email, Phone, CreateDate, IsActive, ReceiveSupportEmails)
values ('Marc', 'Ratcliff', 'marc.ratcliff@dnr.wa.gov', '(360) 901-2703', GETDATE(), 1,	0)
go


update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person as p where p.Email = 'steve.crow@dnr.wa.gov')
where ForesterRoleID = 3 and ForesterWorkUnitID in (42, 35, 32)

update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person as p where p.Email = 'bryent.daugherty@dnr.wa.gov')
where ForesterRoleID = 3 and ForesterWorkUnitID = 28

update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person as p where p.Email = 'marc.ratcliff@dnr.wa.gov')
where ForesterRoleID = 3 and ForesterWorkUnitID = 24

update dbo.Person set Email = 'daniel.walters@dnr.wa.gov' where FirstName = 'Daniel' and LastName = 'Walters'
update dbo.Person set Email = 'bruce.meyer@dnr.wa.gov' where FirstName = 'Bruce' and LastName = 'Meyer'
update dbo.Person set Email = 'matthew.brady@dnr.wa.gov' where FirstName = 'Matthew' and LastName = 'Brady'
update dbo.Person set Email = 'jennifer.garstang@dnr.wa.gov' where FirstName = 'Jenny' and LastName = 'Garstang'
update dbo.Person set Email = 'darnell.sam@dnr.wa.gov' where FirstName = 'Darnell' and LastName = 'Sam'
update dbo.Person set Email = 'erik.dukes@dnr.wa.gov' where FirstName = 'Erik' and LastName = 'Dukes'
update dbo.Person set Email = 'eric.weinke@dnr.wa.gov' where FirstName = 'Eric' and LastName = 'Weinke'
update dbo.Person set Email = 'christopher.baus@dnr.wa.gov' where FirstName = 'Chris' and LastName = 'Baus'
update dbo.Person set Email = 'brian.wesemann@dnr.wa.gov' where FirstName = 'Brian' and LastName = 'Wesemann'
update dbo.Person set Email = 'kent.stanford@dnr.wa.gov' where FirstName = 'Kent' and LastName = 'Stanford'
update dbo.Person set Email = 'Elliot.mann@dnr.wa.gov' where FirstName = 'Elliot' and LastName = 'Mann'
update dbo.Person set Email = 'dave.dalzotto@dnr.wa.gov' where FirstName = 'Dave' and LastName = 'Dalzotto'
update dbo.Person set Email = 'tom.shedd@dnr.wa.gov' where FirstName = 'Tom' and LastName = 'Shedd'
update dbo.Person set Email = 'martin.mauney@dnr.wa.gov' where FirstName = 'Martin' and LastName = 'Mauney'
update dbo.Person set Email = 'pat.coleman@dnr.wa.gov' where FirstName = 'Pat' and LastName = 'Coleman'
update dbo.Person set Email = 'robert.hinds@dnr.wa.gov' where FirstName = 'Bob' and LastName = 'Hinds'
update dbo.Person set Email = 'jason.sharp@dnr.wa.gov' where FirstName = 'Jason' and LastName = 'Sharp'
update dbo.Person set Email = 'aileen.nichols@dnr.wa.gov' where FirstName = 'Aileen' and LastName = 'Nichols'
update dbo.Person set Email = 'megan.pike@dnr.wa.gov' where FirstName = 'Megan' and LastName = 'Pike'
update dbo.Person set Email = 'eric.dasso@dnr.wa.gov' where FirstName = 'Eric' and LastName = 'Dasso'
update dbo.Person set Email = 'kyle.buckmiller@dnr.wa.gov' where FirstName = 'Kyle' and LastName = 'Buckmiller'
update dbo.Person set Email = 'brooke.acosta@dnr.wa.gov' where FirstName = 'Brooke' and LastName = 'Acosta'
update dbo.Person set Email = 'erica.christie@dnr.wa.gov' where FirstName = 'Erica' and LastName = 'Christie'
update dbo.Person set Email = 'matt.binder@dnr.wa.gov' where FirstName = 'Matt' and LastName = 'Binder'
update dbo.Person set Email = 'jared.coleman@dnr.wa.gov' where FirstName = 'Jared' and LastName = 'Coleman'
update dbo.Person set Email = 'zachary.bastow@dnr.wa.gov' where FirstName = 'Zachary' and LastName = 'Bastow'
update dbo.Person set Email = 'steven.huang@dnr.wa.gov' where FirstName = 'Steven' and LastName = 'Huang'
update dbo.Person set Email = 'dj.greene@dnr.wa.gov' where FirstName = 'Dj' and LastName = 'Greene'
update dbo.Person set Email = 'levi.puksta@dnr.wa.gov' where FirstName = 'Levi' and LastName = 'Puksta'
update dbo.Person set Email = 'jon.byerly@dnr.wa.gov' where FirstName = 'Jon' and LastName = 'Byerly'
update dbo.Person set Email = 'eric.oien@dnr.wa.gov' where FirstName = 'Eric' and LastName = 'Oien'
update dbo.Person set Email = 'lisa.kaino@dnr.wa.gov' where FirstName = 'Lisa' and LastName = 'Kaino'
update dbo.Person set Email = 'geoff.crosby@dnr.wa.gov' where FirstName = 'Geoff' and LastName = 'Crosby'
update dbo.Person set Email = 'tiffany.dhooghe@dnr.wa.gov' where FirstName = 'Tiffany' and LastName = 'Dhooghe'

update dbo.Person set Phone = '(360) 640-1717' where Email = 'Gary.Bell@dnr.wa.gov'
update dbo.Person set Phone = '(360) 640-4387' where Email = 'anne.favolise@dnr.wa.gov'
update dbo.Person set Phone = '(509) 675-1109' where Email = 'Jarett.Cook@dnr.wa.gov'
update dbo.Person set Phone = null where Email = 'mackenna.milosevich@dnr.wa.gov'

