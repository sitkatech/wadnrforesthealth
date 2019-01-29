ALTER TABLE [dbo].[Project] DROP CONSTRAINT [CK_Project_ApprovalStartDateLessThanEqualToCompletionDate]
GO

ALTER TABLE [dbo].[Project] DROP CONSTRAINT [CK_Project_PlannedDateLessThanEqualToImplementationDate]
GO

ALTER TABLE dbo.Project DROP COLUMN ApprovalStartDate
ALTER TABLE dbo.Project ADD ExpirationDate datetime null

ALTER TABLE dbo.ProjectUpdate DROP COLUMN ApprovalStartDate
ALTER TABLE dbo.ProjectUpdate ADD ExpirationDate datetime null

ALTER TABLE dbo.TreatmentActivity ADD TreatmentActivitySlashAcres decimal(18,0) not null

insert into dbo.TreatmentActivityStatus (TreatmentActivityStatusID, TreatmentActivityStatusName, TreatmentActivityStatusDisplayName)
values (3, 'Cancelled','Cancelled')

go

delete from dbo.FocusArea
go

insert dbo.FocusAreaStatus (FocusAreaStatusID, FocusAreaStatusName, FocusAreaStatusDisplayName) 
values 
(1, 'Planned', 'Planned'),
(2, 'In Progress', 'In Progress'),
(3, 'Completed', 'Completed')
go

insert into dbo.FocusArea(FocusAreaName, FocusAreaStatusID, RegionID)
values
('Quartzite - 2018', 2, 7520),
('Hunters - 2018', 2, 7520),
('North Highway 25 - 2018', 2, 7520),
('Antoine - 2018', 2, 7520),
('North Onion - 2018', 2, 7520),
('Tacoma Creek - 2018', 2, 7520),
('Diamond Lake - 2015', 2, 7520),
('Pingston - 2015', 2, 7520),
('Flat-Sheep - 2015', 2, 7520),
('Cayuse - 2015', 2, 7520),
('Gulches - 2014', 2, 7520),
('Winthrop - 2014', 2, 7520),
('Bear Lake - 2014', 2, 7520),
('Four Mound - 2014', 2, 7520),
('Tum Tum - 2014', 2, 7520),
('Chiliwist - 2013', 3, 7520),
('Forker Road - 2013', 3, 7520),
('Hwy 195 - 2013', 3, 7520),
('Upper Kettle - 2016', 2, 7520),
('Clear Lake - 2016', 2, 7520),
('Wauconda - 2016', 2, 7520),
('Barstow - 2016', 2, 7520),
('Horseshoe - 2017', 2, 7520),
('Sawtooth - 2017', 2, 7520),
('Metaline - 2017', 2, 7520),
('Huckleberry - 2017', 2, 7520),
('Sitzmark - 2017', 2, 7520),
('Valleyford - 2017', 2, 7520),
('Deer Creek - 2017', 2, 7520),
('Chiwawa - 2016', 2, 7519),
('Blue Mountain - 2015', 2, 7519),
('Blues-Mill Creek - 2018', 2, 7519),
('Chumstick - 2015', 2, 7519),
('Chumstick - 2018', 2, 7519),
('Douglas - 2017', 2, 7519),
('East Box Canyon - 2016', 2, 7519),
('East Chelan - 2013-005', 3, 7519),
('East Chelan - 2013-706', 3, 7519),
('East Klicikitat - 2017', 2, 7519),
('Hells Canyon - 2018', 2, 7519),
('Klickitat - 2013-005', 3, 7519),
('Klickitat - 2013-706', 3, 7519),
('Klickitat River - 2018', 2, 7519),
('Lake Chelan - 2018', 2, 7519),
('Leavenworth - 2016', 2, 7519),
('North Yakima - 2015', 2, 7519),
('North Yakima - 2016', 2, 7519),
('Upper Kittitas - 2013', 3, 7519),
('Upper Kittitas - 2014', 2, 7519),
('Upper Kittitas - 2016', 2, 7519),
('Upper Kittitas - 2017', 2, 7519),
('Upper Kittitas - 2018', 2, 7519),
('West Blues - 2017', 2, 7519),
('West Klickitat - 2014', 2, 7519),
('West Klickitat - 2016', 2, 7519),
('West Klickitat - 2017', 2, 7519),
('West Klickitat - 2018', 2, 7519)
