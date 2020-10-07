/*

select 
    * 
from 
    dbo.GrantModification as gm
    join dbo.GrantModificationGrantModificationPurpose as gmp on gm.GrantModificationID = gmp.GrantModificationID
    join dbo.GrantModificationPurpose as p on gmp.GrantModificationPurposeID = p.GrantModificationPurposeID




select * from dbo.GrantModificationGrantModificationPurpose

select * from dbo.GrantModification
where GrantModificationName = 'Initial Award';

-- initial award = 5
select * from dbo.GrantModificationPurpose

*/

ALTER TABLE [dbo].GrantModificationGrantModificationPurpose ADD  CONSTRAINT [AK_GrantModificationGrantModificationPurpose_GrantModificationPurposeID_GrantModificationID] UNIQUE NONCLUSTERED 
(
	GrantModificationPurposeID ASC,
    GrantModificationID ASC
)





insert into dbo.GrantModificationGrantModificationPurpose(GrantModificationID, GrantModificationPurposeID)
select gm.GrantModificationID, 5
from dbo.GrantModification as gm
where gm.GrantModificationName = 'Initial Award'
