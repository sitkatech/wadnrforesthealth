

EXEC sp_rename 'dbo.Region.RegionID', 'DNRUplandRegionID', 'COLUMN';
EXEC sp_rename 'dbo.Region.RegionAbbrev', 'DNRUplandRegionAbbrev', 'COLUMN';
EXEC sp_rename 'dbo.Region.RegionName', 'DNRUplandRegionName', 'COLUMN';
EXEC sp_rename 'dbo.Region.RegionLocation', 'DNRUplandRegionLocation', 'COLUMN';

EXEC sp_rename 'dbo.Region', 'DNRUplandRegion'; 


EXEC sp_rename 'dbo.GrantAllocation.RegionID', 'DNRUplandRegionID', 'COLUMN';
EXEC sp_rename 'dbo.FK_GrantAllocation_Region_RegionID', 'FK_GrantAllocation_DNRUplandRegion_DNRUplandRegionID';

EXEC sp_rename 'dbo.FocusArea.RegionID', 'DNRUplandRegionID', 'COLUMN';
EXEC sp_rename 'dbo.FK_FocusArea_Region_RegionID', 'FK_FocusArea_DNRUplandRegion_DNRUplandRegionID';

EXEC sp_rename 'dbo.ProjectRegion.RegionID', 'DNRUplandRegionID', 'COLUMN';
EXEC sp_rename 'dbo.FK_ProjectRegion_Region_RegionID', 'FK_ProjectRegion_DNRUplandRegion_DNRUplandRegionID';

EXEC sp_rename 'dbo.ProjectRegionUpdate.RegionID', 'DNRUplandRegionID', 'COLUMN';
EXEC sp_rename 'dbo.FK_ProjectRegionUpdate_Region_RegionID', 'FK_ProjectRegionUpdate_DNRUplandRegion_DNRUplandRegionID';

EXEC sp_rename 'dbo.PersonStewardRegion.RegionID', 'DNRUplandRegionID', 'COLUMN';
EXEC sp_rename 'dbo.FK_PersonStewardRegion_Region_RegionID', 'FK_PersonStewardRegion_DNRUplandRegion_DNRUplandRegionID';

EXEC sp_rename 'dbo.Agreement.RegionID', 'DNRUplandRegionID', 'COLUMN';
EXEC sp_rename 'dbo.FK_Agreement_Region_RegionID', 'FK_Agreement_DNRUplandRegion_DNRUplandRegionID';


