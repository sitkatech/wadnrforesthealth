



insert into dbo.Person(FirstName, LastName, Email, Phone, CreateDate, IsActive, ReceiveSupportEmails)
values ('Jason', 'Cirksena', 'jason.cirksena@dnr.wa.gov', null, GETDATE(), 1,	0)

insert into dbo.Person(FirstName, LastName, Email, Phone, CreateDate, IsActive, ReceiveSupportEmails)
values ('Charlie', 'Landsman', 'charles.landsman@dnr.wa.gov', null, GETDATE(), 1,	0)

insert into dbo.Person(FirstName, LastName, Email, Phone, CreateDate, IsActive, ReceiveSupportEmails)
values ('Marc', 'Titus', 'marc.titus@dnr.wa.gov', null, GETDATE(), 1,	0)

insert into dbo.Person(FirstName, LastName, Email, Phone, CreateDate, IsActive, ReceiveSupportEmails)
values ('Jenny', 'Coe', 'jennifer.coe@dnr.wa.gov', null, GETDATE(), 1,	0)

insert into dbo.Person(FirstName, LastName, Email, Phone, CreateDate, IsActive, ReceiveSupportEmails)
values ('Will', 'Knowlton', 'william.knowlton@dnr.wa.gov', null, GETDATE(), 1,	0)
go

update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person where Email = 'jason.cirksena@dnr.wa.gov')
where ForesterRoleID = 6 and ForesterWorkUnitName in ('Spokane', 'Stevens', 'Pend Oreille', 'Lincoln', 'Whitman')


update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person where Email = 'charles.landsman@dnr.wa.gov')
where ForesterRoleID = 6 and ForesterWorkUnitName in ('Klickitat', 'Benton', 'Franklin', 'Walla Walla', 'Columbia', 'Garfield', 'Asotin')

update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person where Email = 'jake.hardt@dnr.wa.gov')
where ForesterRoleID = 6 and ForesterWorkUnitName in ('Chelan', 'Douglas', 'Kittitas', 'Grant', 'Adams')


update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person where Email = 'marc.titus@dnr.wa.gov')
where ForesterRoleID = 6 and ForesterWorkUnitName in ('Yakima', 'Skamania', 'Lewis Pacific', 'Wahkiakum', 'Cowlitz', 'Clark')



update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person where Email = 'jennifer.coe@dnr.wa.gov')
where ForesterRoleID = 6 and ForesterWorkUnitName in ('Whatcom', 'Skagit', 'Snohomish', 'King', 'Pierce', 'Lewis', 'Thurston', 'Pacific', 'Mason', 'San Juan', 'Island', 'Kitsap')


update dbo.ForesterWorkUnit
set PersonID = (select PersonID from dbo.Person where Email = 'william.knowlton@dnr.wa.gov')
where ForesterRoleID = 6 and ForesterWorkUnitName in ('Okanogan', 'Ferry', 'Grays Harbor', 'Jefferson', 'Clallam')



/*

Jason Cirksena (jason.cirksena@dnr.wa.gov):  Spokane, Stevens, Pend Oreille, Lincoln and Whitman
Charlie Landsman (charles.landsman@dnr.wa.gov):  
Jake Hardt (jake.hardt@dnr.wa.gov):  Chelan, Douglas, Kittitas, Grant and Adams
Marc Titus (marc.titus@dnr.wa.gov):  Yakima, Skamania, Lewis Pacific, Wahkiakum, Cowlitz, Clark
Jenny Coe (jennifer.coe@dnr.wa.gov):  Whatcom, Skagit, Snohomish, King, Pierce, Lewis, Thurston, Pacific, Mason, , San Juan, Island, Kitsap
Will Knowlton (william.knowlton@dnr.wa.gov):  Okanogan, Ferry, Grays Harbor, Jefferson, Clallam




*/
