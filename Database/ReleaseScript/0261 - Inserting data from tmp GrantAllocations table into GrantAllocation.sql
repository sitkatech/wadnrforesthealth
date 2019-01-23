--Standardizing grant # convention
update tmpChildrenGrantsInGrantsTab
set [Grant #] = '2016-DG-11062765-710'
where [Grant #] = '16-DG-11062765-710'

update tmpChildrenGrantsInGrantsTab
set [Grant #] = '2016-DG-11062765-717'
where [Grant #] = '16-DG-11062765-717'

update tmpChildrenGrantsInGrantsTab
set [Grant #] = '2016-DG-11062765-718'
where [Grant #] = '16-DG-11062765-718'
GO

---------------------------------------------------------------
-- Adding new child grants to parent Grant table
insert into dbo.[Grant] (GrantNumber, StartDate, EndDate, AwardedFunds, CFDANumber, GrantName, GrantTypeID, GrantStatusID, OrganizationID)
select 
	tcg.[Grant #], 
	tcg.[Start Date],
	tcg.[End Date],
	tcg.[Funds Awarded],
	tcg.[CFDA #],
	tcg.Title,
	1,
	1,
	4708
from dbo.tmpChildrenGrantsInGrantsTab tcg
left join dbo.[Grant] g on tcg.[Grant #] = g.GrantNumber
where g.GrantNumber is null

insert into dbo.GrantAllocation(GrantID, ProjectName, StartDate, EndDate, AllocationAmount, ProgramIndexID)
select 
	gt.[GrantID], --Grant #
	tcg.Title, -- Title
	tcg.[Start Date], --Start Date
	tcg.[End Date], --End Date
	isnull(cast(REPLACE(tcg.[Funds Awarded], '"','') as money),0) as 'Funds Awarded', --Funds Awarded
	(select ProgramIndexID from dbo.ProgramIndex [pi] where [pi].ProgramIndexAbbrev = tcg.[Prog Index]) --Prog Index
from dbo.tmpChildrenGrantsInGrantsTab as tcg
left join dbo.[Grant] gt on tcg.[Grant #] = gt.GrantNumber
where tcg.[Parent Child (SITKA)] = 'PC'
--select * from dbo.[GrantAllocation]
GO

----------------------------------------------------------------------------------------------
-- Adding GrantAllocationID to tmp table to make returning to the well easier.
update tmpChildrenGrantsInGrantsTab
set GrantAllocationID = (
                         select ga.GrantAllocationID from dbo.GrantAllocation as ga 
						 where 
					     ga.ProjectName = Title 
					       and 
						 (select g.GrantID from [Grant] as g where g.GrantNumber = [Grant #]) = GrantID
						   and
					     ga.AllocationAmount =  case when [Funds Awarded] != '' then PARSE([Funds Awarded] AS MONEY) else null end
						)
GO

-------------------------------------------------------------------------------------------------
-- Catch any new federal fund codes that have come into being. Re-build the Federal Fund Code table
alter table dbo.GrantAllocation
NOCHECK CONSTRAINT FK_GrantAllocation_FederalFundCode_FederalFundCodeID; 
delete from dbo.FederalFundCode

DBCC CHECKIDENT ('[FederalFundCode]', RESEED, 0);
insert into dbo.FederalFundCode(FederalFundCodeAbbrev)
	select distinct [Federal Fund Code] from dbo.tmpChildrenGrantsInGrantsTab
	where [Federal Fund Code] <> ''

alter table dbo.GrantAllocation
CHECK CONSTRAINT FK_GrantAllocation_FederalFundCode_FederalFundCodeID
GO

---------------------------------------------------------------------------------------------------
--Adding federal fund code
IF OBJECT_ID('tempdb..#tmpGrantFundCode') IS NOT NULL
begin
    DROP TABLE #tmpGrantFundCode
end
GO

select ga.GrantAllocationID,
       tcg.[Federal Fund Code],
	   (select ffc.FederalFundCodeID from dbo.FederalFundCode as ffc where tcg.[Federal Fund Code] = ffc.FederalFundCodeAbbrev) as FederalFundCodeID
into #tmpGrantFundCode
from dbo.GrantAllocation as ga
inner join tmpChildrenGrantsInGrantsTab as tcg on ga.GrantAllocationID = tcg.GrantAllocationID

--select * from #tmpGrantFundCode

delete from #tmpGrantFundCode
where FederalFundCodeID is null

update dbo.GrantAllocation
set FederalFundCodeID = tfc.FederalFundCodeID
from #tmpGrantFundCode as tfc
join dbo.GrantAllocation ga on tfc.GrantAllocationID = ga.GrantAllocationID
GO

--------------------------------------------------------------------------------------
--Catching any new notes
insert into dbo.GrantAllocationNote(GrantAllocationID,GrantAllocationNoteText, CreatedByPersonID, CreatedDate)

select ga.GrantAllocationID
, tcg.Notes
, 5251 -- Christel
, GETDATE()
 from 
 dbo.[GrantAllocation] ga
 join dbo.tmpChildrenGrantsInGrantsTab tcg on tcg.GrantAllocationID = ga.GrantAllocationID
 where tcg.Notes is not null

