alter table dbo.DNRUplandRegion
add DNRUplandRegionCoordinatorID int null constraint FK_DNRUplandRegion_Person_DNRUplandRegionCoordinatorID_PersonID foreign key (DNRUplandRegionCoordinatorID) 
references dbo.Person(PersonID)

