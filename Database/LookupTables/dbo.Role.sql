delete from dbo.Role
go

insert into dbo.Role(RoleID, RoleName, RoleDisplayName, RoleDescription, IsBaseRole) 
values 
(1, 'Admin', 'Administrator', '',1),
(2, 'Normal', 'Normal User', 'Users with this role can propose new EIP projects, update existing EIP projects where their organization is the Lead Implementer, and view almost every page within the EIP Tracker.',1),
(7, 'Unassigned', 'Unassigned', '',1),
(8, 'EsaAdmin', 'ESA Administrator', '',1),
(9, 'ProjectSteward', 'Project Steward', 'Users with this role can approve Project Proposals, create new Projects, and approve Project Updates.',1),
(10, 'ProgramEditor', 'Program Editor', 'Users with this role can edit Projects that are from their Program',0)