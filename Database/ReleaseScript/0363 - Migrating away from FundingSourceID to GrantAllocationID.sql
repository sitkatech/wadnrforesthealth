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




--rollback tran

