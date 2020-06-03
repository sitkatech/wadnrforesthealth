
exec sp_rename 'AK_Region_RegionName', 'AK_DNRUplandRegion_DNRUplandRegionName', 'OBJECT'
go

exec sp_rename 'PK_Region_RegionID', 'PK_DNRUplandRegion_DNRUplandRegionID', 'OBJECT'
go

exec sp_rename 'AK_ProjectRegion_ProjectID_RegionID', 'AK_ProjectRegion_ProjectID_DNRUplandRegionID', 'OBJECT'
go

exec sp_rename 'AK_ProjectRegionUpdate_ProjectUpdateBatchID_RegionID', 'AK_ProjectRegionUpdate_ProjectUpdateBatchID_DNRUplandRegionID', 'OBJECT'
go