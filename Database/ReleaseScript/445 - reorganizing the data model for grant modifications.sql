/*



select * from dbo.[Grant]

select * from dbo.GrantModification

select * from dbo.GrantStatus

select * from dbo.GrantModificationStatus

select * from dbo.GrantAllocation

select * from dbo.[Grant]

*/


INSERT INTO dbo.GrantModification (GrantModificationName, GrantModificationStartDate, GrantModificationEndDate, GrantID, GrantModificationStatusID, GrantModificationAmount)
    SELECT 
            'Initial Grant' as GrantModificationName,
            COALESCE(StartDate, '1753-01-01') as GrantModificationStartDate,
            COALESCE(EndDate, '1753-01-01') as GrantModificationEndDate,
            GrantID as GrantID,
            2 as GrantModificationStatusID,
            AwardedFunds as GrantModificationAmount
    FROM dbo.[Grant]


alter table dbo.GrantAllocation
add GrantModificationID int constraint FK_GrantAllocation_GrantModification_GrantModificationID foreign key references dbo.GrantModification(GrantModificationID);
go

update dbo.GrantAllocation
set 
    dbo.GrantAllocation.GrantModificationID = gm.GrantModificationID
from
    dbo.GrantModification as gm 
    join dbo.GrantAllocation as ga on gm.GrantID = ga.GrantID
where 
    gm.GrantModificationName = 'Initial Grant' 
    and ga.GrantModificationID is null

alter table dbo.GrantAllocation
alter column GrantModificationID int not null;
go

ALTER TABLE dbo.GrantAllocation
DROP CONSTRAINT AK_GrantAllocation_GrantAllocationID_GrantID, FK_GrantAllocation_Grant_GrantID;
go

alter table dbo.GrantAllocation
drop column GrantID;
go


alter table dbo.[Grant]
drop column AwardedFunds


