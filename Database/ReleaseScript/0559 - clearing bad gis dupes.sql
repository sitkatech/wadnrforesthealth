

select p2.ProjectID into #projectIDsToDelete from dbo.Project p
join (


select distinct p.ProjectID, p1.ProjectID as ProjectIDSecond from dbo.Project p
join dbo.ProjectProgram pp on p.ProjectID = pp.ProjectID
join dbo.Program pr on pp.ProgramID = pr.ProgramID
join dbo.Organization o on pr.OrganizationID = o.OrganizationID
join dbo.Project p1 on p1.ProjectGisIdentifier = p.ProjectGisIdentifier+' ' 
join dbo.ProjectProgram pp1 on p1.ProjectID = pp1.ProjectID
join dbo.Program pr1 on pr1.ProgramID = pp1.ProgramID
join dbo.Organization o1 on pr1.OrganizationID = o1.OrganizationID
where o1.OrganizationID = o.OrganizationID and p1.ProjectID != p.ProjectID) x on x.ProjectID = p.ProjectID
join dbo.Project p2 on p2.ProjectID = x.ProjectIDSecond
where RIGHT(p2.ProjectGisIdentifier, 1) = ' '



delete from dbo.ProjectOrganization where ProjectID in (select ProjectID from dbo.Project p where p.CreateGisUploadAttemptID = 43)
delete from dbo.ProjectProgram where ProjectID in (select ProjectID from dbo.Project p where p.CreateGisUploadAttemptID = 43)
delete from dbo.Treatment where ProjectID in (select ProjectID from dbo.Project p where p.CreateGisUploadAttemptID = 43)
delete from dbo.ProjectPriorityLandscape where ProjectID in (select ProjectID from dbo.Project p where p.CreateGisUploadAttemptID = 43)
delete from dbo.Project where ProjectID in (select ProjectID from dbo.Project p where p.CreateGisUploadAttemptID = 43)


delete from dbo.ProjectOrganization where ProjectID in (select ProjectID from #projectIDsToDelete)
delete from dbo.ProjectProgram where ProjectID in (select ProjectID from #projectIDsToDelete)
delete from dbo.Treatment where ProjectID in (select ProjectID from #projectIDsToDelete)
delete from dbo.ProjectPriorityLandscape where ProjectID in (select ProjectID from #projectIDsToDelete)
delete from dbo.ProjectRegion where ProjectID in (select ProjectID from #projectIDsToDelete)
delete from dbo.Project where ProjectID in (select ProjectID from #projectIDsToDelete)

ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [CK_Project_ProjectGisIdentifierDoesNotEndWithSpace] CHECK  (RIGHT(ProjectGisIdentifier, 1) != ' ')
GO

ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [CK_Project_ProjectGisIdentifierDoesNotEndWithSpace]
GO

