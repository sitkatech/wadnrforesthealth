-- Add IsProgramManager bit for use in program manager dropdowns, and to separate from security-based roles. We think this is superior to using RoleID, which the MapRoleFromClaims() in AccountController clobbers when a WADNR user logs in through ADFS. 
alter table dbo.Person
add IsProgramManager bit
go

-- Set Program Managers we know about
update dbo.Person
set IsProgramManager = 0
go

update dbo.Person
set IsProgramManager = 1
where LastName in ('Ellerbroek','Lampman','Miketa', 'Tate','Johnston','Hersey','Turley','Kohler','DeBell','Armbruster', 'Boles', 'Chambers')
go

insert into dbo.Person (FirstName, LastName, Email, CreateDate, IsActive, ReceiveSupportEmails, RoleID, IsProgramManager)
VALUES
('Tammi','Ellerbroek',  'tammi.ellerbroek@dnr.wa.gov', GETDATE(), 1, 0, 7, 1),
('Tami','Mikita', 'tamara.mikita@dnr.wa.gov', GETDATE(), 1, 0, 7, 1),
('Darrel','Johnston', 'darrel.johnston@dnr.wa.gov', GETDATE(), 1, 0, 7, 1),
('Glenn','Kohler', 'glenn.kohler@dnr.wa.gov', GETDATE(), 1, 0, 7, 1),
('Jeff','DeBell', 'jeffrey.debell@dnr.wa.gov', GETDATE(), 1, 0, 7, 1),
('Julie','Armbruster', 'julie.armbruster@dnr.wa.gov', GETDATE(), 1, 0, 7, 1)
go

update dbo.Person
set Email = 'chuck.turley@dnr.wa.gov'
where LastName = 'Turley' and FirstName like 'Charles%' 
go

update dbo.Person
set Email = 'chuck.hersey@dnr.wa.gov'
where LastName = 'Hersey' and FirstName = 'Chuck' 
go

update dbo.Person
set Email = 'linden.lampman@dnr.wa.gov'
where LastName = 'Lampman' and FirstName = 'Linden' 
go

/*
	select * from dbo.Person
	where LastName in ('Ellerbroek','Lampman','Miketa', 'Tate','Johnston','Hersey','Turley','Kohler','DeBell','Armbruster', 'Boles', 'Chambers')
	and Email is null
	go
*/
