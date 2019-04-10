--begin tran

-- ProjectFundingSourceExpenditure 
-----------------------------------

select * from ProjectFundingSourceExpenditure

alter table dbo.ProjectFundingSourceExpenditure
add GrantAllocationID int null
GO

update dbo.ProjectFundingSourceExpenditure
set 
	GrantAllocationID = ga.GrantAllocationID
	--GrantID = g.GrantID
from dbo.[grant] as g 
	join dbo.GrantAllocation as ga on g.GrantID = ga.GrantID
	join dbo.FundingSource as fs on g.GrantNumber = fs.FundingSourceName
where dbo.ProjectFundingSourceExpenditure.FundingSourceID = fs.FundingSourceID

select * from dbo.ProjectFundingSourceExpenditure

--select pfse.GrantID, pfse.FundingSourceID, g.GrantNumber, fs.FundingSourceName 
--from  dbo.ProjectFundingSourceExpenditure as pfse
--join dbo.FundingSource as fs on fs.FundingSourceID = pfse.FundingSourceID
--join dbo.GrantAllocation as ga on pfse.GrantAllocationID = ga.GrantAllocationID
--join dbo.[Grant] as g on pfse.GrantID = g.GrantID


ALTER TABLE [dbo].[ProjectFundingSourceExpenditure]  WITH CHECK ADD  CONSTRAINT [FK_ProjectFundingSourceExpenditure_GrantAllocation_GrantAllocationID] FOREIGN KEY(GrantAllocationID)
REFERENCES [dbo].GrantAllocation (GrantAllocationID)
GO

alter table dbo.ProjectFundingSourceExpenditure
alter column GrantAllocationID int not null
GO
alter table dbo.ProjectFundingSourceExpenditure
alter column FundingSourceID int null
GO



-- ProjectFundingSourceRequest 
-----------------------------------

select * from ProjectFundingSourceRequest

alter table dbo.ProjectFundingSourceRequest
add GrantAllocationID int null
GO

--alter table dbo.ProjectFundingSourceRequest
--add GrantID int null
--GO

update dbo.ProjectFundingSourceRequest
set 
	GrantAllocationID = ga.GrantAllocationID
	--GrantID = g.GrantID
from dbo.[grant] as g 
	join dbo.GrantAllocation as ga on g.GrantID = ga.GrantID
	join dbo.FundingSource as fs on g.GrantNumber = fs.FundingSourceName
where dbo.ProjectFundingSourceRequest.FundingSourceID = fs.FundingSourceID

select * from dbo.ProjectFundingSourceRequest

ALTER TABLE [dbo].[ProjectFundingSourceRequest]  WITH CHECK ADD  CONSTRAINT [FK_ProjectFundingSourceRequest_GrantAllocation_GrantAllocationID] FOREIGN KEY(GrantAllocationID)
REFERENCES [dbo].GrantAllocation (GrantAllocationID)
GO

alter table dbo.ProjectFundingSourceRequest
alter column GrantAllocationID int not null
go
alter table dbo.ProjectFundingSourceRequest
alter column FundingSourceID int null
GO


-- ProjectFundingSourceRequestUpdate 
-----------------------------------

select * from ProjectFundingSourceRequestUpdate

alter table dbo.ProjectFundingSourceRequestUpdate
add GrantAllocationID int null
GO

update dbo.ProjectFundingSourceRequestUpdate
set 
	GrantAllocationID = ga.GrantAllocationID
	--GrantID = g.GrantID
from dbo.[grant] as g 
	join dbo.GrantAllocation as ga on g.GrantID = ga.GrantID
	join dbo.FundingSource as fs on g.GrantNumber = fs.FundingSourceName
where dbo.ProjectFundingSourceRequestUpdate.FundingSourceID = fs.FundingSourceID

select * from dbo.ProjectFundingSourceRequestUpdate

ALTER TABLE [dbo].[ProjectFundingSourceRequestUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectFundingSourceRequestUpdate_GrantAllocation_GrantAllocationID] FOREIGN KEY(GrantAllocationID)
REFERENCES [dbo].GrantAllocation (GrantAllocationID)
GO

alter table dbo.ProjectFundingSourceRequestUpdate
alter column GrantAllocationID int not null
GO

alter table dbo.ProjectFundingSourceRequestUpdate
alter column FundingSourceID int null
GO



-- ProjectFundingSourceExpenditureUpdate 
-----------------------------------

select * from ProjectFundingSourceExpenditureUpdate

alter table dbo.ProjectFundingSourceExpenditureUpdate
add GrantAllocationID int null
GO

update dbo.ProjectFundingSourceExpenditureUpdate
set 
	GrantAllocationID = ga.GrantAllocationID
	--GrantID = g.GrantID
from dbo.[grant] as g 
	join dbo.GrantAllocation as ga on g.GrantID = ga.GrantID
	join dbo.FundingSource as fs on g.GrantNumber = fs.FundingSourceName
where dbo.ProjectFundingSourceExpenditureUpdate.FundingSourceID = fs.FundingSourceID

select * from dbo.ProjectFundingSourceExpenditureUpdate

ALTER TABLE [dbo].[ProjectFundingSourceExpenditureUpdate]  WITH CHECK ADD  CONSTRAINT [FK_ProjectFundingSourceExpenditureUpdate_GrantAllocation_GrantAllocationID] FOREIGN KEY(GrantAllocationID)
REFERENCES [dbo].GrantAllocation (GrantAllocationID)
GO

alter table dbo.ProjectFundingSourceExpenditureUpdate
alter column GrantAllocationID int not null
GO

alter table dbo.ProjectFundingSourceExpenditureUpdate
alter column FundingSourceID int null
GO


-- FundingSource 
-----------------------------------

select * from FundingSource

alter table dbo.FundingSource
add GrantAllocationID int null
GO


update dbo.FundingSource
set 
	GrantAllocationID = ga.GrantAllocationID
	--GrantID = g.GrantID
from dbo.[grant] as g 
	join dbo.GrantAllocation as ga on g.GrantID = ga.GrantID
	--join dbo.FundingSource as fs on g.GrantNumber = fs.FundingSourceName
where dbo.FundingSource.FundingSourceName = g.GrantNumber

select * from dbo.FundingSource


-- add tracker to Funding Source


ALTER TABLE [dbo].FundingSource  WITH CHECK ADD  CONSTRAINT [FK_FundingSource_GrantAllocation_GrantAllocationID] FOREIGN KEY(GrantAllocationID)
REFERENCES [dbo].GrantAllocation (GrantAllocationID)
GO



update dbo.FundingSource
set FundingSourceName = 'OBSOLETE_SHOULD_USE_GRANT_ALLOCATION_' + FundingSourceName

select * from dbo.FundingSource


--AK_ProjectFundingSourceExpenditure_ProjectID_FundingSourceID_CalendarYear
ALTER TABLE [dbo].[ProjectFundingSourceExpenditure] 
drop CONSTRAINT [AK_ProjectFundingSourceExpenditure_ProjectID_FundingSourceID_CalendarYear]
GO

/****** Object:  Index [AK_ProjectFundingSourceExpenditure_ProjectID_FundingSourceID_CalendarYear]    Script Date: 4/4/2019 1:21:17 PM ******/
ALTER TABLE [dbo].[ProjectFundingSourceExpenditure] 
ADD  CONSTRAINT [AK_ProjectFundingSourceExpenditure_ProjectID_GrantAllocationID_CalendarYear] UNIQUE NONCLUSTERED 
(
	[ProjectID] ASC,
	GrantAllocationID ASC,
	[CalendarYear] ASC
) ON [PRIMARY]
GO

--AK_ProjectFundingSourceRequest_ProjectID_FundingSourceID
ALTER TABLE [dbo].ProjectFundingSourceRequest 
drop CONSTRAINT [AK_ProjectFundingSourceRequest_ProjectID_FundingSourceID]
GO

ALTER TABLE [dbo].ProjectFundingSourceRequest 
ADD  CONSTRAINT [AK_ProjectFundingSourceRequest_ProjectID_GrantAllocationID] UNIQUE NONCLUSTERED 
(
	[ProjectID] ASC,
	GrantAllocationID ASC
) ON [PRIMARY]
GO



--AK_ProjectFundingSourceRequestUpdate_ProjectID_FundingSourceID
ALTER TABLE [dbo].ProjectFundingSourceRequestUpdate 
drop CONSTRAINT [AK_ProjectFundingSourceRequestUpdate_ProjectUpdateBatchID_FundingSourceID]
GO

ALTER TABLE [dbo].ProjectFundingSourceRequestUpdate 
ADD  CONSTRAINT [AK_ProjectFundingSourceRequestUpdate_ProjectUpdateBatchID_GrantAllocationID] UNIQUE NONCLUSTERED 
(
	ProjectUpdateBatchID ASC,
	GrantAllocationID ASC
) ON [PRIMARY]
GO





-- AK_ProjectFundingSourceExpenditureUpdate_ProjectUpdateBatchID_FundingSourceID_CalendarYear
ALTER TABLE [dbo].ProjectFundingSourceExpenditureUpdate 
drop CONSTRAINT AK_ProjectFundingSourceExpenditureUpdate_ProjectUpdateBatchID_FundingSourceID_CalendarYear
GO

ALTER TABLE [dbo].ProjectFundingSourceExpenditureUpdate 
ADD  CONSTRAINT AK_ProjectFundingSourceExpenditureUpdate_ProjectUpdateBatchID_GrantAllocationID_CalendarYear UNIQUE NONCLUSTERED 
(
	ProjectUpdateBatchID ASC,
	GrantAllocationID ASC,
	CalendarYear
) ON [PRIMARY]
GO


EXEC sp_rename 'dbo.ProjectFundingSourceRequest', 'ProjectGrantAllocationRequest';
EXEC sp_rename 'dbo.ProjectFundingSourceRequestUpdate', 'ProjectGrantAllocationRequestUpdate'; 

EXEC sp_rename 'dbo.ProjectFundingSourceExpenditure', 'ProjectGrantAllocationExpenditure';
EXEC sp_rename 'dbo.ProjectFundingSourceExpenditureUpdate', 'ProjectGrantAllocationExpenditureUpdate'; 

exec sp_rename 'ProjectGrantAllocationExpenditure.ProjectFundingSourceExpenditureID', 'ProjectGrantAllocationExpenditureID'
exec sp_rename 'ProjectGrantAllocationExpenditureUpdate.ProjectFundingSourceExpenditureUpdateID', 'ProjectGrantAllocationExpenditureUpdateID'
exec sp_rename 'ProjectGrantAllocationRequest.ProjectFundingSourceRequestID', 'ProjectGrantAllocationRequestID'
exec sp_rename 'ProjectGrantAllocationRequestUpdate.ProjectFundingSourceRequestUpdateID', 'ProjectGrantAllocationRequestUpdateID'

exec sp_rename 'AK_ProjectFundingSourceExpenditure_ProjectID_GrantAllocationID_CalendarYear', 'AK_ProjectGrantAllocationExpenditure_ProjectID_GrantAllocationID_CalendarYear', 'OBJECT'
exec sp_rename 'PK_ProjectFundingSourceExpenditure_ProjectFundingSourceExpenditureID', 'PK_ProjectGrantAllocationExpenditure_ProjectGrantAllocationExpenditureID', 'OBJECT'

exec sp_rename 'PK_ProjectFundingSourceExpenditureUpdate_ProjectFundingSourceExpenditureUpdateID', 'PK_ProjectGrantAllocationExpenditureUpdate_ProjectGrantAllocationExpenditureUpdateID', 'OBJECT'
exec sp_rename 'AK_ProjectFundingSourceRequest_ProjectID_GrantAllocationID', 'AK_ProjectGrantAllocationRequest_ProjectID_GrantAllocationID', 'OBJECT'
exec sp_rename 'PK_ProjectFundingSourceRequest_ProjectFundingSourceRequestID', 'PK_ProjectGrantAllocationRequest_ProjectGrantAllocationRequestID', 'OBJECT'
exec sp_rename 'PK_ProjectFundingSourceRequestUpdate_ProjectFundingSourceRequestUpdateID', 'PK_ProjectGrantAllocationRequestUpdate_ProjectGrantAllocationRequestUpdateID', 'OBJECT'

exec sp_rename 'FK_ProjectFundingSourceExpenditure_FundingSource_FundingSourceID', 'FK_ProjectGrantAllocationExpenditure_FundingSource_FundingSourceID', 'OBJECT'
exec sp_rename 'FK_ProjectFundingSourceExpenditureUpdate_FundingSource_FundingSourceID', 'FK_ProjectGrantAllocationExpenditureUpdate_FundingSource_FundingSourceID', 'OBJECT'
exec sp_rename 'FK_ProjectFundingSourceRequest_FundingSource_FundingSourceID', 'FK_ProjectGrantAllocationRequest_FundingSource_FundingSourceID', 'OBJECT'
exec sp_rename 'FK_ProjectFundingSourceRequestUpdate_FundingSource_FundingSourceID', 'FK_ProjectGrantAllocationRequestUpdate_FundingSource_FundingSourceID', 'OBJECT'
exec sp_rename 'FK_ProjectFundingSourceExpenditure_GrantAllocation_GrantAllocationID', 'FK_ProjectGrantAllocationExpenditure_GrantAllocation_GrantAllocationID', 'OBJECT'
exec sp_rename 'FK_ProjectFundingSourceExpenditureUpdate_GrantAllocation_GrantAllocationID', 'FK_ProjectGrantAllocationExpenditureUpdate_GrantAllocation_GrantAllocationID', 'OBJECT'
exec sp_rename 'FK_ProjectFundingSourceRequest_GrantAllocation_GrantAllocationID', 'FK_ProjectGrantAllocationRequest_GrantAllocation_GrantAllocationID', 'OBJECT'
exec sp_rename 'FK_ProjectFundingSourceRequestUpdate_GrantAllocation_GrantAllocationID', 'FK_ProjectGrantAllocationRequestUpdate_GrantAllocation_GrantAllocationID', 'OBJECT'
exec sp_rename 'FK_ProjectFundingSourceExpenditure_Project_ProjectID', 'FK_ProjectGrantAllocationExpenditure_Project_ProjectID', 'OBJECT'
exec sp_rename 'FK_ProjectFundingSourceRequest_Project_ProjectID', 'FK_ProjectGrantAllocationRequest_Project_ProjectID', 'OBJECT'

exec sp_rename 'FK_ProjectFundingSourceExpenditureUpdate_ProjectUpdateBatch_ProjectUpdateBatchID', 'FK_ProjectGrantAllocationExpenditureUpdate_ProjectUpdateBatch_ProjectUpdateBatchID', 'OBJECT'
exec sp_rename 'FK_ProjectFundingSourceRequestUpdate_ProjectUpdateBatch_ProjectUpdateBatchID', 'FK_ProjectGrantAllocationRequestUpdate_ProjectUpdateBatch_ProjectUpdateBatchID', 'OBJECT'

-- These were fun
exec sp_rename 'AK_ProjectFundingSourceExpenditureUpdate_ProjectUpdateBatchID_GrantAllocationID_CalendarYear', 'AK_ProjectGrantAllocationExpenditureUpdate_ProjectUpdateBatchID_GrantAllocationID_CalendarYear', 'OBJECT'
exec sp_rename 'AK_ProjectFundingSourceRequestUpdate_ProjectUpdateBatchID_GrantAllocationID', 'AK_ProjectGrantAllocationRequestUpdate_ProjectUpdateBatchID_GrantAllocationID', 'OBJECT'



-- 1 --
alter table dbo.ProjectGrantAllocationExpenditureUpdate
drop constraint FK_ProjectGrantAllocationExpenditureUpdate_FundingSource_FundingSourceID

alter table dbo.ProjectGrantAllocationExpenditureUpdate
drop column FundingSourceID

-- 2 --
alter table dbo.ProjectGrantAllocationRequestUpdate
drop constraint FK_ProjectGrantAllocationRequestUpdate_FundingSource_FundingSourceID

alter table dbo.ProjectGrantAllocationRequestUpdate
drop column FundingSourceID

-- 3-- 
alter table dbo.ProjectGrantAllocationRequest
drop constraint FK_ProjectGrantAllocationRequest_FundingSource_FundingSourceID

alter table dbo.ProjectGrantAllocationRequest
drop column FundingSourceID

-- 4 -- 
alter table dbo.ProjectGrantAllocationExpenditure
drop constraint FK_ProjectGrantAllocationExpenditure_FundingSource_FundingSourceID

alter table dbo.ProjectGrantAllocationExpenditure
drop column FundingSourceID


---------------- At last, we can drop FundingSource

drop table dbo.FundingSource

delete from dbo.FieldDefinitionData
where FieldDefinitionID = (select FieldDefinitionID from dbo.FieldDefinition where FieldDefinitionName = 'FundingSource')

delete from dbo.FieldDefinition
where FieldDefinitionName = 'FundingSource'


---Updating dbo.SupportRequestType
update dbo.SupportRequestType set SupportRequestTypeName = 'NewOrganizationOrGrantAllocation', SupportRequestTypeDisplayName='Need an Organization or Grant Allocation added to the list'
where SupportRequestTypeID = 4

--remove FirmaPageType and FirmaPage for FundingSourceList
delete from dbo.FirmaPage
where FirmaPageTypeID = (select FirmaPageTypeID from dbo.FirmaPageType where FirmaPageTypeName = 'FundingSourcesList')

delete from dbo.FirmaPageType
where FirmaPageTypeName = 'FundingSourcesList'

--rollback tran
