exec sp_rename 'dbo.TenantAttribute.ShowProposalsToThePublic', 'ShowApplicationsToThePublic', 'COLUMN';

-- Planning/Design Date -> Planned Date
Alter Table dbo.Project
Drop Constraint CK_Project_PlanningDesignStartYearLessThanEqualToImplementationYear  -- This constraint will be added back later when ImplementationStartYear becomes Approval/Start Date
exec sp_rename 'dbo.ImportExternalProjectStaging.PlanningDesignStartYear', 'PlannedDate', 'COLUMN';
exec sp_rename 'dbo.Project.PlanningDesignStartYear', 'PlannedDate', 'COLUMN';
exec sp_rename 'dbo.ProjectUpdate.PlanningDesignStartYear', 'PlannedDate', 'COLUMN';