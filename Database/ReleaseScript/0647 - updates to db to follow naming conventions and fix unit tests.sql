

exec sp_rename 'GrantAllocationLikelyPerson.GrantAllocationLikelyUserID', 'GrantAllocationLikelyPersonID'


exec sp_rename 'PK_GrantAllocationLikelyUser_GrantAllocationLikelyUserID', 'PK_GrantAllocationLikelyPerson_GrantAllocationLikelyPersonID', 'OBJECT'
exec sp_rename 'PK_GrantAllocationSource_GrantAllocationPriorityID', 'PK_GrantAllocationPriority_GrantAllocationPriorityID', 'OBJECT'