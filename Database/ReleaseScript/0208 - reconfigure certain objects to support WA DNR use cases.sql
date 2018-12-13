exec sp_rename 'dbo.TenantAttribute.ShowProposalsToThePublic', 'ShowApplicationsToThePublic', 'COLUMN';

-- Planning/Design Date -> Planned Date
Alter Table dbo.Project
Drop Constraint CK_Project_PlanningDesignStartYearLessThanEqualToImplementationYear  -- This constraint will be added back later when ImplementationStartYear becomes Approval/Start Date
exec sp_rename 'dbo.ImportExternalProjectStaging.PlanningDesignStartYear', 'PlannedDate', 'COLUMN';
exec sp_rename 'dbo.Project.PlanningDesignStartYear', 'PlannedDate', 'COLUMN';
exec sp_rename 'dbo.ProjectUpdate.PlanningDesignStartYear', 'PlannedDate', 'COLUMN';

-- PlannedDate int -> datetime (nondestructive)
alter table dbo.project
add PlannedDateTemp datetime null
go

update dbo.Project
set PlannedDateTemp = DATEFROMPARTS(PlannedDate, 1, 1)

alter table dbo.Project
alter column PlannedDate datetime null
go

update dbo.Project
set PlannedDate = PlannedDateTemp

alter table dbo.Project
drop column PlannedDateTemp

alter table dbo.ProjectUpdate
add PlannedDateTemp datetime null
go

update dbo.ProjectUpdate
set PlannedDateTemp = DATEFROMPARTS(PlannedDate, 1, 1)

alter table dbo.ProjectUpdate
alter column PlannedDate datetime null
go

update dbo.ProjectUpdate
set PlannedDate = PlannedDateTemp

alter table dbo.ProjectUpdate
drop column PlannedDateTemp