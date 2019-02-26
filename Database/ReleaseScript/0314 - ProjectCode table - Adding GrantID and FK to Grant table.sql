-- Not needed for now

---- Temp table with connection between Grant and ProjectCode
--DROP TABLE IF EXISTS #GrantToProjectCode

--select *
--into #GrantToProjectCode
--from 
--(
--	select g.GrantID, g.GrantNumber, ga.GrantAllocationID, ga.ProjectName, pc.ProjectCodeID, pc.ProjectCodeAbbrev
--	from dbo.[Grant] g
--	join dbo.GrantAllocation ga on g.GrantID = ga.GrantID
--	join dbo.GrantAllocationProjectCode gapc on ga.GrantAllocationID = gapc.GrantAllocationID
--	join dbo.ProjectCode pc on gapc.ProjectCodeID = pc.ProjectCodeID
--) x

---- Add GrantID column to ProjectCode table 
--alter table dbo.ProjectCode
--add GrantID int null
--go
  
---- Insert GrantID values into ProjectCode table
--update dbo.ProjectCode
--	set ProjectCode.GrantID = #GrantToProjectCode.GrantID
--	from ProjectCode join #GrantToProjectCode on ProjectCode.ProjectCodeID = #GrantToProjectCode.ProjectCodeID
--go

---- Add FK with FK from ProjectCode to Grant table on GrantID
--alter table dbo.ProjectCode
--with check add constraint FK_ProjectCode_Grant_GrantID foreign key(GrantID)
--references dbo.[Grant](GrantID)

