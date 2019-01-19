--begin tran

alter table dbo.GrantAllocation add 
	FederalFundCodeID int null
	constraint [FK_GrantAllocation_FederalFundCode_FederalFundCodeID] foreign key([FederalFundCodeID])
	references dbo.FederalFundCode([FederalFundCodeID])

GO

IF OBJECT_ID('tempdb..#tmpGrantFundCode') IS NOT NULL
begin
    DROP TABLE tmpGrantFundCode
end
GO

select ga.GrantAllocationID, 
       tcg.[Federal Fund Code],
	   (select ffc.FederalFundCodeID from dbo.FederalFundCode as ffc where tcg.[Federal Fund Code] = ffc.FederalFundCodeAbbrev) as FederalFundCodeID
into #tmpGrantFundCode
from dbo.GrantAllocation as ga
inner join tmpChildrenGrantsInGrantsTab as tcg on ga.GrantAllocationID = tcg.GrantAllocationID

delete from #tmpGrantFundCode
where FederalFundCodeID is null

update dbo.GrantAllocation
set FederalFundCodeID = tfc.FederalFundCodeID
from #tmpGrantFundCode as tfc
join dbo.GrantAllocation ga on tfc.GrantAllocationID = ga.GrantAllocationID


--rollback tran
