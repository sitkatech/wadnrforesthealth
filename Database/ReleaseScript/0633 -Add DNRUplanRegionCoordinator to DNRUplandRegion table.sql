alter table dbo.DNRUplandRegion
add DNRUplandRegionCoordinator int null constraint FK_DNRUplandRegion_Person_DNRUplandRegionCoordinator_PersonID foreign key (DNRUplandRegionCoordinator) 
references Person(PersonID)

